using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Framework.Utilities.Reflection
{
    public static class ClassExplorer
    {
        public static bool HasProperty<T>(this T thisClass, string propertyName)
            => thisClass.GetType().GetProperties().Any(ep => ep.Name.Equals(propertyName));

        public static TReturn GetValueOfProperty<T, TReturn>(this T thisClass, string propertyName)
            => (TReturn)thisClass.GetType().GetProperty(propertyName).GetValue(thisClass, null);

        public static List<string> GetListProperties<T>(this T thisClass)
        {
            var properties = thisClass.GetType().GetProperties()
                .Where(p => typeof(IList).IsAssignableFrom(p.PropertyType.GetTypeInfo()));
            return properties.Select(ep => ep.Name).ToList();
        }
    }
}
