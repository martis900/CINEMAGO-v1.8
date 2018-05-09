using Android.App;
using Android.Widget;
using Android.OS;
using Android.Graphics;
using System;
using System.Collections.Generic;
using Android.Content;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Android.Views;
using Android.Views.Animations;

namespace CINEMAGO
{
    [Activity(Label = "CINEMAGO", MainLauncher = false, Icon = "@drawable/CINEMAGO_ICON_2")]
    public class MainActivity : Activity
    {
        TextView appname;
        ImageView searchimg;
        TextView movieslbl;
        TextView tvlbl;
        GridView placeformovies;
        GridView placefortv;
        MovieDetails current;
        EditText searchmovie;
        Typeface font;
        RelativeLayout moviebar;
        RelativeLayout searchbar;

        List<MovieDetails> moviedetails = new List<MovieDetails>();
        // To animate view slide out from right to left

        protected override async void OnCreate(Bundle bundle)
        {
            font = Typeface.CreateFromAsset(Assets, "Womby.ttf");

            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            ActionBar.Hide();
            string[] movies = await getDataFromApi.GetPopular("movie", "popularity.desc", 20);
            appname = FindViewById(Resource.Id.appname) as TextView;
            tvlbl = FindViewById(Resource.Id.tv) as TextView;
            movieslbl = FindViewById(Resource.Id.movies) as TextView;
            searchimg = FindViewById(Resource.Id.search) as ImageView;
            searchmovie = FindViewById(Resource.Id.searchbaredit) as EditText;
            placeformovies = FindViewById(Resource.Id.placeformovies) as GridView;
            moviebar = FindViewById(Resource.Id.moviebar) as RelativeLayout;
            searchbar = FindViewById(Resource.Id.searchbar) as RelativeLayout;
            placefortv = FindViewById(Resource.Id.placefortv) as GridView;
            placefortv.ItemClick += Placefortv_ItemClick1;
            placeformovies.ItemClick += GridView_ItemClick;

            placefortv.Visibility = ViewStates.Gone;
            searchbar.Visibility = Android.Views.ViewStates.Gone;

            searchmovie.EditorAction += Searchmovie_EditorAction;

            searchimg.Click += delegate
            {
                searchbar.Visibility = Android.Views.ViewStates.Visible;
                moviebar.Visibility = Android.Views.ViewStates.Gone;
            };

            tvlbl.Click += async delegate
            {
                placeformovies.Visibility = ViewStates.Gone;

                placefortv.Visibility = ViewStates.Visible;

                moviedetails.Clear();
                movies = await getDataFromApi.GetPopular("tv", "popularity.desc", 10);
                for (int i = 0; i < movies.Length; i++)
                {
                    current = await getDataFromApi.GetBasicInfo(movies[i],"tv");
                    moviedetails.Add(current);
                }
                placefortv.Adapter = new movieAdapter(this, moviedetails, font);
            };

            movieslbl.Click += async delegate
            {
                placeformovies.Visibility = ViewStates.Visible;
                placefortv.Visibility = ViewStates.Gone;
                moviedetails.Clear();
                movies = await getDataFromApi.GetPopular("movie", "popularity.desc", 10);
                for (int i = 0; i < movies.Length; i++)
                {
                    current = await getDataFromApi.GetBasicInfo(movies[i], "movie");
                    moviedetails.Add(current);
                }
                placeformovies.Adapter = new movieAdapter(this, moviedetails, font);       
            };

            

            //Nuskaitomi visi filmai ir paduodami i gridVIEW
            for (int i = 0; i < movies.Length; i++)
            {
                current = await getDataFromApi.GetBasicInfo(movies[i],"movie");
                moviedetails.Add(current);
            }
            placeformovies.Adapter = new movieAdapter(this, moviedetails, font);
           // placeformovies.ItemClick += GridView_ItemClick;
            //*******





            appname.SetTypeface(font, TypefaceStyle.Normal);
            movieslbl.SetTypeface(font, TypefaceStyle.Normal);
            tvlbl.SetTypeface(font, TypefaceStyle.Normal);
            searchmovie.SetTypeface(font, TypefaceStyle.Normal);


        }

        private async void Placefortv_ItemClick1(object sender, AdapterView.ItemClickEventArgs e)
        {
            MovieDetails current1 = await getDataFromApi.GetMovieDetails(moviedetails[e.Position].ID.ToString(), "tv");

            var activity2 = new Intent(this, typeof(FullMovieActivity));
            activity2.PutExtra("movieID", moviedetails[e.Position].ID.ToString());
            activity2.PutExtra("type", "tv");
            StartActivity(activity2);
        }

        private async void GridView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            MovieDetails current1 = await getDataFromApi.GetMovieDetails(moviedetails[e.Position].ID.ToString(),"movie");

            var activity2 = new Intent(this, typeof(FullMovieActivity));
            activity2.PutExtra("movieID", moviedetails[e.Position].ID.ToString());
            activity2.PutExtra("type","movie");
            StartActivity(activity2);
        }

        private void Searchmovie_EditorAction(object sender, TextView.EditorActionEventArgs e)
        {
            string search = searchmovie.Text;
            searchmovie.Text = null;
            if (e.ActionId == Android.Views.InputMethods.ImeAction.Search)
            {
				var activity3 = new Intent(this, typeof(SearchActivity2));
				activity3.PutExtra("searchResult", search);
				searchbar.Visibility = Android.Views.ViewStates.Gone;
				moviebar.Visibility = Android.Views.ViewStates.Visible;
				StartActivity(activity3);
            }
        }

        public override void OnBackPressed()
        {
            searchbar.Visibility = ViewStates.Gone;
            moviebar.Visibility = ViewStates.Visible;
            searchmovie.Text = null;
        }


	}
}

