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

namespace CINEMAGO
{
    [Activity(Label = "SearchActivity")]
    public class SearchActivity : Activity
    {
        TextView txt;
        ImageView img;
        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.SearchLayout);

            txt = FindViewById(Resource.Id.seartchtxt) as TextView;

            img = FindViewById(Resource.Id.searchphoto) as ImageView;

            string searchresult = Intent.GetStringExtra("searchResult");

            MovieDetails current = await getDataFromApi.GetSearchResult(searchresult);
            txt.Text = current.title;
            img.SetImageBitmap(current.poster); 
        }
    }
}