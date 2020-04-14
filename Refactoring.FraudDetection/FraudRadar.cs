// <copyright file="FraudRadar.cs" company="Payvision">
// Copyright (c) Payvision. All rights reserved.
// </copyright>

namespace Refactoring.FraudDetection
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class FraudRadar
    {

        public IEnumerable<FraudResult> Check(IEnumerable<Order> orders)
        {
            var fraudResults = new List<FraudResult>();
            if (orders != null)
            {
                // CHECK FRAUD
                for (int i = 0; i < orders.Count(); i++)
                {
                    var current = orders.ElementAt(i);
                    if (current != null && fraudResults.SingleOrDefault(x => x.OrderId == current.OrderId) == null)
                    {
                        var filter = orders.Where(x => x.DealId == current.DealId && x.CreditCard != current.CreditCard);
                        if (filter.Count() > 0)
                        {
                            var fraudulents = filter.Where(x => (x.Email == current.Email) ||
                            (x.State == current.State && x.ZipCode == current.ZipCode && x.Street == current.Street
                            && x.City == current.City));
                            if (fraudulents.Count() > 0)
                                fraudResults.AddRange(fraudulents.Select(p => new FraudResult { OrderId = p.OrderId, IsFraudulent = true }));
                        }
                        fraudResults.Add(new FraudResult { OrderId = current.OrderId, IsFraudulent = false });
                    }
                }
            }
            return fraudResults.OrderBy(x => x.OrderId);
        }

    }
}