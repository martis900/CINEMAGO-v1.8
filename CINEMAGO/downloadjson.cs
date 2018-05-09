using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json.Linq;
using System.Net;

namespace CINEMAGO
{
    class downloadjson
    {

        public static JObject getJson(string link)
        {
            string json2;

            using (WebClient webClient = new System.Net.WebClient())
            {
                WebClient n = new WebClient();
                var json = n.DownloadString(link);
                json2 = Convert.ToString(json);
            }

            JObject objectas = JObject.Parse(json2);


            return objectas;
        }
            
    }
}