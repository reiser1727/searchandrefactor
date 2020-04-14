using System;
using System.Collections.Generic;
using System.Text;
using TinyCsvParser.TypeConverter;

namespace Refactoring.FraudDetection
{
    public class LowerStringTypeConverter : ITypeConverter<string>
    {

        public Type TargetType => typeof(string);

        public bool TryConvert(string value, out string result)
        {
            result = string.Empty;
            if(!string.IsNullOrEmpty(value))
            {
                result = value.ToLower();
                return true;
            }
            return false;
        }

    }
}
