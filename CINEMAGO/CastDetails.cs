
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace CINEMAGO
{
    class CastDetails
    {
		[JsonProperty]
		public string Name { get; set; }
		public string ID { get; set; }
        public Bitmap profile_path { get; set; }

    }
}
