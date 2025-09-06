using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace JsonSerializer
{
    internal class PrivateMethods
    {
        internal static string GetProperties(object obj)
        {
            if (obj == null) return "";
            Type objType = obj.GetType();

            string json = "{";
            PropertyInfo[] properties = obj.GetType().GetProperties();
            var propertyCount = properties.Length;
            foreach (PropertyInfo property in properties)
            {
                // Check if its a collection
                if (property.PropertyType.GetInterface(typeof(ICollection<>).FullName) != null)
                {
                    if (property.GetValue(obj, null) is IEnumerable elems)
                    {
                        int count = 0;
                        foreach (object val in elems) count++;

                        foreach (var item in elems)
                        {
                            json += $"\"{char.ToLower(property.Name[0]) + property.Name.Substring(1)}\":\"{GetProperties(item)}\"";

                            if (--count > 0)
                                json += ",";
                        }
                    }
                }
                else
                {
                    var propValue = property.GetValue(obj, null);

                    // If it is User Defined Type, Recursively loop again
                    if (property.PropertyType.Assembly == objType.Assembly)
                    {
                        json += $"\"{char.ToLower(property.Name[0]) + property.Name.Substring(1)}\":\"{GetProperties(propValue)}\"";
                    }
                    else
                    {
                        json += $"\"{char.ToLower(property.Name[0]) + property.Name.Substring(1)}\":\"{propValue}\"";
                    }
                }

                if (--propertyCount > 0)
                    json += ",";
            }
            json += "}";
            return json;
        } 
    }
}
