using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VueWebApplication.Helpers
{
    public static class JsonHelper
    {
        public static string SerializeObject(object _object)
        {
            JsonSerializerSettings jsSettings = new JsonSerializerSettings();
            jsSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return JsonConvert.SerializeObject(_object, Formatting.None, jsSettings);
        }
    }
}