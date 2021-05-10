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
            foreach(var token in body.Children())
            {
                if (token.Type == Newtonsoft.Json.Linq.JTokenType.Object)
                {
                    var obj = (Newtonsoft.Json.Linq.JObject)token;
                    var prop = obj.Property("@odata.type");
                    if(null != prop)
                    {
                        var type_name = prop.Value.ToObject<string>();
                        if (!type_name.StartsWith("#"))
                        {
                            throw new ArgumentException($"Invalid tag @odata.type type name {type_name}");
                        }
                        obj.AddFirst(new Newtonsoft.Json.Linq.JProperty("$type", type_name.Substring(1) + "," + type_name.Substring(1, type_name.LastIndexOf('.')-1)));
                        obj.Remove(prop.Name);
                    }
                }
                Prepare(token);
            }
        }
        public static T Deserialize<T>(Newtonsoft.Json.Linq.JToken body)
        {
            //var type = body["@odata.type"];
            //if(null != type)
            //{
            //    if (type.Type != Newtonsoft.Json.Linq.JTokenType.String)
            //    {
            //        throw new ArgumentException($"Invalid tag @odata.type type {body["@odata.type"].Type}");
            //    }
            //    var type_name = (string)body["@odata.type"].ToObject<string>();
            //    if (!type_name.StartsWith("#"))
            //    {
            //        throw new ArgumentException($"Invalid tag @odata.type type name {type_name}");
            //    }
            //    return (T)body.ToObject(typeof(T).Assembly.GetType(type_name.Substring(1)));
            //}
            Prepare(body);
            return body.ToObject<T>(new JsonSerializer { TypeNameHandling = TypeNameHandling.Auto });
        }
        public static T Deserialize<T>(string body)
        {
            return Deserialize<T>(Newtonsoft.Json.Linq.JObject.Parse(body));
        }
    }
}
