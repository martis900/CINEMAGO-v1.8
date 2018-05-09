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
    class MovieDetails
    {
        [JsonProperty]
        public string title { get; set; }
        public int ID { get; set; }
        public string imdbID { get; set; }
        public string budget { get; set; }
        public string[] genres  { get; set; }
        public string overview { get; set; }
        public string release_date { get; set; }
        public int duration { get; set; }
        public int revenue { get; set; }
        public Bitmap backdrop { get; set; }
        public Bitmap poster { get; set; }
        public string trailer { get; set; }
        public string IMDBrating { get; set; }



        //TVSHOWS +++++++++++++++++++++++++++++++++
		public string[] created_by_name { get; set; }
		public string[] seasons { get; set; }
		public string[] networks_id { get; set; }
		public string number_of_episodes { get; set; }
		public string number_of_seasons { get; set; }
		public string first_air_date_date { get; set; }
		public string status { get; set; }
    }
}