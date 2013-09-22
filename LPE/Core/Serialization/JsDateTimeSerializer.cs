using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Web.Script.Serialization;
using Modelo;

namespace Core.Serialization
{
    public class JsDateTimeSerializer : JavaScriptConverter
    {
        private static string _dateFormat = "yyyy-MM-dd HH:mm:ss";

        public override IEnumerable<Type> SupportedTypes
        {
            get
            {
                return new[] { typeof(Entity) };
            }
        }

        public static void AlterDateTimePattern(String pattern)
        {
            _dateFormat = pattern;
        }

        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            throw new Exception("Only serialization!");
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            Entity p = (Entity)obj;
            IDictionary<string, object> serialized = new Dictionary<string, object>();

            if (obj != null)
                foreach (PropertyInfo pi in obj.GetType().GetProperties())
                {
                    if (pi.PropertyType == typeof(DateTime))
                    {
                        serialized[pi.Name] = ((DateTime)pi.GetValue(p, null)).ToString(_dateFormat);
                    }
                    else if (pi.PropertyType.IsSubclassOf(typeof(Entity)))
                    {
                        serialized[pi.Name] = Serialize(pi.GetValue(p, null), serializer);
                    }
                    else
                    {
                        serialized[pi.Name] = pi.GetValue(p, null);
                    }

                }

            return serialized;
        }

        public static JavaScriptSerializer GetSerializer()
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = Int32.MaxValue;
            serializer.RegisterConverters(new[] { new JsDateTimeSerializer() });
            return serializer;
        }
    }

}