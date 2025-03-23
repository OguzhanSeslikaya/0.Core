using System.Globalization;

namespace _0.Core.Helpers
{
    public static class ConversationHelper
    {
        public static T ToObject<T>(IDictionary<string, string> data) where T : class, new()
        {
            var obj = new T();
            var objType = obj.GetType();

            var numericTypes = new List<Enum>()
            {
                TypeCode.Byte,
                TypeCode.SByte,
                TypeCode.UInt16,
                TypeCode.UInt32,
                TypeCode.UInt64,
                TypeCode.Int16,
                TypeCode.Int32,
                TypeCode.Int64,
                TypeCode.Decimal,
                TypeCode.Double,
                TypeCode.Single
            };

            foreach (var item in data)
            {
                var prop = objType.GetProperty(item.Key);
                if (prop != null)
                {
                    object? value;

                    Type propType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;

                    try
                    {
                        if (propType.IsEnum)
                        {
                            value = Enum.Parse(propType, item.Value);
                        }
                        else
                        {
                            if (numericTypes.Contains(Type.GetTypeCode(propType)))
                            {
                                value = Convert.ChangeType(item.Value.Replace(",", "."), propType, CultureInfo.GetCultureInfo("en"));
                            }
                            else
                            {
                                value = Convert.ChangeType(item.Value, propType);
                            }
                        }
                    }
                    catch
                    {
                        value = null;
                    }

                    prop.SetValue(obj, value, null);
                }
            }
            return obj;
        }
    }
}
