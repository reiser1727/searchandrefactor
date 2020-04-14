using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TinyCsvParser.Mapping;

namespace Refactoring.FraudDetection
{

    //TinyCSV is much faster and user far less memory than CSVHelper
    //https://dotnetcoretutorials.com/2018/08/04/csv-parsing-in-net-core/
    public class OrderMapping : CsvMapping<Order>
    {

        //Extract from JSON File, API or BBDD -> Extensible
        private string _streetMap = @"{""st."": ""street"", ""rd."": ""road""}";

        //Extract from JSON File, API or BBDD -> Extensible
        private string _stateMap = @"{""il"": ""illinois"", ""ca"": ""california"", ""ny"": ""new york""}";

        public OrderMapping() : base()
        {
            MapProperty(0, x => x.OrderId);
            MapProperty(1, x => x.DealId);
            MapProperty(2, x => x.Email, new EmailTypeConverter());
            //Reusable Dictionary if needed
            MapProperty(3, x => x.Street, new ReplaceTypeConverter(JsonConvert.DeserializeObject<Dictionary<string,string>>(_streetMap)));
            MapProperty(4, x => x.City, new LowerStringTypeConverter());
            //Reusable Dictionary if needed
            MapProperty(5, x => x.State, new ReplaceTypeConverter(JsonConvert.DeserializeObject<Dictionary<string, string>>(_stateMap)));
            MapProperty(6, x => x.ZipCode);
            MapProperty(7, x => x.CreditCard);
        }

    }
}
