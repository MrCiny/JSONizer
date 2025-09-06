namespace JsonSerializer
{
    public class JSONizer
    {
        public static string Serialize(object obj)
        {
            if (obj == null) return "{}";
            string json = "{";
            json += PrivateMethods.GetProperties(obj);
            
            json += "}";
            return json;
        }
    }
}
