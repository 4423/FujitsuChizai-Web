using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;

namespace FujitsuChizai.Models
{
    public static class JsonExtension
    {
        public static string ToJson<T>(this T obj) where T : class
        {
            var serializer = new DataContractJsonSerializer(typeof(T));

            using (var stream = new MemoryStream())
            {
                serializer.WriteObject(stream, obj);
                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }
    }
}