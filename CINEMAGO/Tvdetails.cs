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
using Android.Graphics;
using Newtonsoft.Json;

namespace CINEMAGO
{
    class Tvdetails
    {
        [JsonProperty]
        public string title { get; set; }
        public int ID { get; set; }
        public string imdbID { get; set; }
        public string created_by_name { get; set; }
        public string[] genres  { get; set; }
        public string[] seasons { get; set; }
        public string[] networks { get; set; }
        public string[] networks_pic_path { get; set; }
        public int number_of_episodes { get; set; }
        public int number_of_seasons { get; set; }
        public string overview { get; set; }
        public string releasefirst_air_date_date { get; set; }
        public int duration { get; set; }
        public int revenue { get; set; }
        public Bitmap backdrop { get; set; }
        public Bitmap poster { get; set; }
        public string trailer { get; set; }
        public string IMDBrating { get; set; }
        public string status { get; set; }


    }
}