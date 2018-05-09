
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
using Newtonsoft.Json.Linq;

namespace CINEMAGO
{
    [Activity(Label = "SearchActivity2")]
    public class SearchActivity2 : Activity
    {
        Typeface font;

        ListView lv;

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.SearchLayout);
            List<MovieDetails> moviedetails = new List<MovieDetails>();
            font = Typeface.CreateFromAsset(Assets, "Womby.ttf");


            lv = FindViewById(Resource.Id.lv) as ListView;

            string searchresults = Intent.GetStringExtra("searchResult");

            ActionBar.Title = "Search result for: " + searchresults;


            Dictionary<int, string> currentID = GetSearchResultID(searchresults);



            foreach (var item in currentID)
            {
                if (item.Value == "tv")
                {
                    MovieDetails current1 = await getDataFromApi.GetMovieDetails(item.Key.ToString(), "tv");
                    moviedetails.Add(current1);
                }
                else if (item.Value == "movie")
                {
                    MovieDetails current2 = await getDataFromApi.GetMovieDetails(item.Key.ToString(), "movie");
                    moviedetails.Add(current2);
                }
            }
                   
            
                   
            
            
               lv.Adapter = new SearchlistviewAdapter(this, moviedetails, font);
            
               lv.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) =>
               {
                   var activity2 = new Intent(this, typeof(FullMovieActivity));
                    activity2.PutExtra("movieID", moviedetails[e.Position].ID.ToString());
                   foreach (var item in currentID)
                   {
                       if (item.Key == moviedetails[e.Position].ID)
                       {
                        activity2.PutExtra("type", item.Value.ToString());
                           break;
                       }
                   }
                   StartActivity(activity2);
               };


        }

        public static Dictionary<int, string> GetSearchResultID(string query)
		{
            int MaxResult = 10;
            Dictionary<int, string> dict = new Dictionary<int, string>();

			string key = "253ceb1e198e26b094348524da5bc8ab";
            string query1 = string.Format("https://api.themoviedb.org/3/search/multi?api_key={0}&query={1}&page=1&include_adult=false", key, query);
			
			JObject objectas = downloadjson.getJson(query1);
			if ((int)objectas["total_results"] <= MaxResult)
			{
				MaxResult = (int)objectas["total_results"];
			}
			for (int i = 0; i < MaxResult; i++)
			{
                if (objectas["results"][i]["media_type"].ToString() == "tv")
				{
                    dict.Add((int)objectas["results"][i]["id"],"tv");
				}
				else if (objectas["results"][i]["media_type"].ToString() == "movie")
				{
					dict.Add((int)objectas["results"][i]["id"],"movie");
				}
			
			}

			return dict;
        }
    }
}
