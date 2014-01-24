namespace OAuth2.Common
{
    using System.Collections.Generic;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public static class StringExtensions
    {
        public static dynamic ParseQueryString(this string queryString)
        {
            dynamic parsedQueryString = new System.Dynamic.ExpandoObject();

            var nameV = new Dictionary<string, string>();
            if (string.IsNullOrEmpty(queryString)) return parsedQueryString;


            var pairs = queryString.Split('&');
            foreach (var pair in pairs)
            {
                var parts = pair.Split(new []{'='}, 2);

                var name = System.Uri.UnescapeDataString(parts[0]);
                var value = parts.Length == 1 ? string.Empty : System.Uri.UnescapeDataString(parts[1]);

                nameV.Add(name, value);
            }
            //DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Dictionary<int, List<int>>));

            //using (MemoryStream ms = new MemoryStream())
            //{
            //    serializer.WriteObject(ms, nameV);
            //}
            var temp = JsonConvert.SerializeObject(nameV, new KeyValuePairConverter());

            return temp;
        }
    }
}
