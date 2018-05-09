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
using Newtonsoft.Json;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace CINEMAGO
{
    class DataContainer
    {
        public static async Task<JContainer> GetDataFromService(string link)
        {
            //connect to remote server
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(link);
            JContainer data = null;  
            if (response != null)
	        {
                data = JsonConvert.DeserializeObject<JContainer>(response.Content.ReadAsStringAsync().Result);     
            }
            return data;
        }
    }
}