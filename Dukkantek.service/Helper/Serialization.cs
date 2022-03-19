using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Dukkantek.Service.Helpers
{
    public class Serialization
    {
        /// <summary>
        /// Serialize object to json
        /// </summary>
        public static string SerializeToJson(object value)
        {
            string serializeData = JsonSerializer.Serialize(value);
            return serializeData;
        }
    }
}
