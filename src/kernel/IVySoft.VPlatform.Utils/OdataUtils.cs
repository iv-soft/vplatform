using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace IVySoft.VPlatform.Utils
{
    public static class OdataUtils
    {
        private static void Prepare(Newtonsoft.Json.Linq.JToken body)
        {
            if (body.Type == Newtonsoft.Json.Linq.JTokenType.Object)
            {
                var obj = (Newtonsoft.Json.Linq.JObject)body;
                var prop = obj.Property("@odata.type");
                if (null != prop)
                {
                    var type_name = prop.Value.ToObject<string>();
                    if (!type_name.StartsWith("#"))
                    {
                        throw new ArgumentException($"Invalid tag @odata.type type name {type_name}");
                    }
                    obj.AddFirst(new Newtonsoft.Json.Linq.JProperty("$type", type_name.Substring(1) + "," + type_name.Substring(1, type_name.LastIndexOf('.') - 1)));
                    obj.Remove(prop.Name);
                }
            }

            foreach (var token in body.Children())
            {
                Prepare(token);
            }
        }
        public static T Deserialize<T>(Newtonsoft.Json.Linq.JToken body)
        {
            Prepare(body);
            return body.ToObject<T>(new JsonSerializer { TypeNameHandling = TypeNameHandling.Auto });
        }
        public static T Deserialize<T>(string body)
        {
            return Deserialize<T>(Newtonsoft.Json.Linq.JObject.Parse(body));
        }

        public static string Serialize<T>(T value)
        {
            var options = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            options.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());

            var token = Newtonsoft.Json.Linq.JToken.Parse(JsonConvert.SerializeObject(value, options));
            FixToken(typeof(T), token, value);

            return token.ToString();
        }

        private static void FixToken(Type targetType, Newtonsoft.Json.Linq.JToken token, object value)
        {
            if (token.Type == Newtonsoft.Json.Linq.JTokenType.Object)
            {
                var obj = (Newtonsoft.Json.Linq.JObject)token;
                if (targetType != value.GetType())
                {
                    obj.AddFirst(new Newtonsoft.Json.Linq.JProperty("@odata.type", value.GetType().FullName));
                }
                foreach (var property in value.GetType().GetProperties())
                {
                    var propToken = obj[property.Name];
                    if (null != propToken)
                    {
                        FixToken(property.PropertyType, propToken, property.GetValue(value));
                    }
                }
            }
            else if(token.Type == Newtonsoft.Json.Linq.JTokenType.Array)
            {
                var target = ((Newtonsoft.Json.Linq.JArray)token).Children().GetEnumerator();
                var source = ((System.Collections.IEnumerable)value).GetEnumerator();
                while (source.MoveNext())
                {
                    if (!target.MoveNext())
                    {
                        throw new InvalidOperationException();
                    }

                    FixToken(value.GetType().GetGenericArguments()[0], target.Current, source.Current);
                }
            }
        }
    }
}
