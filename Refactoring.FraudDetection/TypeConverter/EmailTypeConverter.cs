using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using TinyCsvParser.TypeConverter;

namespace Refactoring.FraudDetection
{
    public class EmailTypeConverter : ITypeConverter<string>
    {

        public Type TargetType => typeof(string);

        public bool TryConvert(string value, out string result)
        {
            try
            {
                //Check Mail Format
                var m = new MailAddress(value);
                result = m.Address;
                return true;
            }
            catch (FormatException)
            {
                //TODO: Log
                result = string.Empty;
                return false;
            }
        }

    }
}
