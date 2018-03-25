using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KP.CsvLib
{
    internal static class TypeHelper
    {
        public static bool IsNullable(this Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }
    }

    public class CsvSerializer
    {
        private const char SEPARATOR = ';';

        private readonly Encoding _encoding;
        private readonly CultureInfo _cultureInfo;
        
        public CsvSerializer(Encoding encoding, CultureInfo cultureInfo)
        {
            _encoding = encoding;
            _cultureInfo = cultureInfo;
        }

        private IEnumerable<Tuple<PropertyInfo, CsvFieldAttribute>> GetProps(Type type)
        {
            return from prop in type.GetProperties()
                   let attr = prop.GetCustomAttribute<CsvFieldAttribute>()
                   where attr != null
                   orderby attr.Index
                   select Tuple.Create(prop, attr);
        }

        private IEnumerable<Tuple<Type, CsvTypeAttribute>> GetDerived<T>()
        {
            var t = typeof (T);
            return from type in Assembly.GetAssembly(t).GetTypes()
                   where t.IsAssignableFrom(type)
                   where !type.IsAbstract
                   let attr = type.GetCustomAttribute<CsvTypeAttribute>()
                   where attr != null
                   select Tuple.Create(type, attr);
        }

        private Type GetTypeToCreate<T>(string[] cells, IEnumerable<Tuple<Type, CsvTypeAttribute>> derived)
        {
            var type = typeof (T);

            var derivedList = derived as IList<Tuple<Type, CsvTypeAttribute>> ?? derived.ToList();

            if (type.IsAbstract && derivedList.Any())
            {
                var t = derivedList.FirstOrDefault(x => cells[x.Item2.Index] == x.Item2.Value);
                if (t != null)
                {
                    return t.Item1;
                }

                throw new ArgumentException("", nameof(derived));
            }

            if (!type.IsAbstract)
            {
                return type;
            }

            throw new ArgumentException();
        }

        private object HandleEnumSerialization(object propertyValue, Type propertyType)
        {
            if (propertyType.IsEnum)
            {
                var targetType = Enum.GetUnderlyingType(propertyType);
                var converted = Convert.ChangeType(propertyValue, targetType);
                return converted;
            }
            else
            {
                return propertyValue;
            }
        }

        public void Serialize<T>(string filepath, IEnumerable<T> items)
        {
            var lines = from item in items
                        let props = GetProps(item.GetType())
                        let cells = from prop in props
                                    let value = prop.Item1.GetValue(item)
                                    select string.Format(
                                        _cultureInfo,
                                        $"{{0:{prop.Item2.Format}}}",
                                        HandleEnumSerialization(value, prop.Item1.PropertyType))
                        select string.Join(SEPARATOR.ToString(), cells);

            File.WriteAllLines(filepath, lines, _encoding);
        }

        private IEnumerable<T> DeserializeInternal<T>(string filepath, Encoding encoding, CultureInfo cultureInfo)
        {
            var types = GetDerived<T>().ToList();

            var lines = File.ReadLines(filepath, encoding);

            foreach (var line in lines)
            {
                var cells = line.Split(SEPARATOR);
                var type = GetTypeToCreate<T>(cells, types);
                var item = (T)Activator.CreateInstance(type);

                var props = GetProps(type).ToList();

                foreach (var prop in props)
                {
                    var value = cells[prop.Item2.Index].Trim();
                    var isNullable = prop.Item1.PropertyType.IsNullable();

                    if (isNullable && string.IsNullOrWhiteSpace(value))
                        continue;

                    var targetType = isNullable
                        ? Nullable.GetUnderlyingType(prop.Item1.PropertyType)
                        : prop.Item1.PropertyType;

                    var converted = Convert.ChangeType(value, targetType, cultureInfo);
                    prop.Item1.SetValue(item, converted);
                }

                yield return item;
            }
        }

        public IEnumerable<T> Deserialize<T>(string filepath)
        {
            return DeserializeInternal<T>(filepath, _encoding, _cultureInfo);
        }

        public IEnumerable<T> Deserialize<T>(string filepath, CultureInfo cultureInfo)
        {
            return DeserializeInternal<T>(filepath, _encoding, cultureInfo);
        }

        public IEnumerable<T> Deserialize<T>(string filepath, Encoding encoding)
        {
            return DeserializeInternal<T>(filepath, encoding, _cultureInfo);
        }

        public IEnumerable<T> Deserialize<T>(string filepath, Encoding encoding, CultureInfo cultureInfo)
        {
            return DeserializeInternal<T>(filepath, encoding, cultureInfo);
        }
    }
}
