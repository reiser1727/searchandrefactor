using System;
using System.Collections.Generic;
using System.Text;
using TinyCsvParser.TypeConverter;

namespace Refactoring.FraudDetection
{
    public class ReplaceTypeConverter : ITypeConverter<string>
    {

        //Dictionary -> Best performace to search indexes and replace
        private Dictionary<string, string> _replace { get; set; }

        public ReplaceTypeConverter(Dictionary<string,string> json)
        {
            _replace = json;
        }

        public Type TargetType => typeof(string);

        public bool TryConvert(string value, out string result)
        {
            if (!string.IsNullOrEmpty(value) && _replace != null)
            {
                result = value.ToLower();
                if (_replace.ContainsKey(result))
                    result = _replace[result];
                return true;
            }
            result = string.Empty;
            return false;
        }

    }
}
