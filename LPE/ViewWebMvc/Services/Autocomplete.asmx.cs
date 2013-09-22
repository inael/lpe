using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;

namespace FacebookLikeAutocompleteDemo.Services
{
    /// <summary>
    /// Summary description for Autocomplete
    /// </summary>
    [WebService(Namespace = "futuriti")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Autocomplete : System.Web.Services.WebService
    {

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string GetCountries(string tag)
        {
            Dictionary<string, string> countries = new Dictionary<string, string>();

            countries.Add("Macedonia", "1");
            countries.Add("India", "2");
            countries.Add("France", "3");
            countries.Add("Italy", "4");
            countries.Add("Spain", "5");
            countries.Add("Germany", "6");

            var items =
               from c in countries
               select new
               {
                   key = c.Key,
                   value = c.Value
               };

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var output = serializer.Serialize(items);

            return output;
        }  
    }
}
