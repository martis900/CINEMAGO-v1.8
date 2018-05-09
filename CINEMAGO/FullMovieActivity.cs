
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
using Android.Webkit;
using Android.Widget;
using Newtonsoft.Json;

namespace CINEMAGO
{
    [Activity(Label = "FullMovieActivity")]
    public class FullMovieActivity : Activity
    {
        TextView title;
        ImageView poster;
        Button playtrailer;
        ImageView backdrop;
        TextView overview;
        TextView rating;
        List<CastDetails> CastList = new List<CastDetails>();
        List<CastDetails> SeasonsList = new List<CastDetails>();
        GridView CastView;
        GridView SeasonView;
        Typeface font;
        TextView duration;
        TextView redate;
        TextView genres;
        TextView budget;
        FrameLayout seasons;
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            ActionBar.Hide();
            base.OnCreate(savedInstanceState);
            font = Typeface.CreateFromAsset(Assets, "Womby.ttf");
            SetContentView(Resource.Layout.FullMovieLayout);

            title = FindViewById(Resource.Id.title) as TextView;
            overview = FindViewById(Resource.Id.overview) as TextView;
            rating = FindViewById(Resource.Id.IMDBrating) as TextView;
            genres = FindViewById(Resource.Id.genres) as TextView;
            redate = FindViewById(Resource.Id.Movieredate) as TextView;
            poster = FindViewById(Resource.Id.poster) as ImageView;
            backdrop = FindViewById(Resource.Id.backdrop) as ImageView;
            playtrailer = FindViewById(Resource.Id.playtrailer) as Button;
			CastView = FindViewById(Resource.Id.lv2) as GridView;
            SeasonView = FindViewById(Resource.Id.lv3) as GridView;
            duration = FindViewById(Resource.Id.Movieduration) as TextView;
            budget = FindViewById(Resource.Id.Moviebudget) as TextView;
            rating.SetTypeface(font, TypefaceStyle.Normal);
            seasons = FindViewById(Resource.Id.seasons) as FrameLayout;

            string movieID = Intent.GetStringExtra("movieID");
            string type = Intent.GetStringExtra("type");

            MovieDetails current = await getDataFromApi.GetMovieDetails(movieID,type);

            List<Bitmap> img = await getDataFromApi.GetBackdrop(current.ID.ToString(),type);
            if(type == "movie")
            {
                seasons.Visibility = ViewStates.Gone;
				try
				{
					title.Text = current.title;
					duration.Text = current.duration.ToString() + " min.";
					redate.Text = current.release_date;
					budget.Text = "$ " + current.budget;
					poster.SetImageBitmap(img[1]);
					if (current.genres != null)
					{
						for (int i = 0; i < current.genres.Count(); i++)
						{
							if (i == current.genres.Count() - 1)
							{
								genres.Text += current.genres[i];
								break;
							}
							genres.Text += current.genres[i] + " | ";
						}
					}
					backdrop.SetImageBitmap(img[0]);
					overview.Text = current.overview;

					IMDb imdb = new IMDb(current.title, true);
					rating.Text = imdb.Rating;

					for (int i = 0; i < 10; i++)
					{
						try
						{
							CastDetails currentPerson = await getDataFromApi.GetCast(current.ID.ToString(), i, type);
							CastList.Add(currentPerson);
						}
						catch (Exception ex) { }

					}



					CastView.Adapter = new CastAdapter(this, CastList, font);
					CastView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) =>
					{
						Toast.MakeText(this, CastList[e.Position].Name, ToastLength.Short).Show();
					};

				}
				catch (Exception ex) { Console.Write(ex.ToString()); }
            }

            else if(type == "tv")
            {
                seasons.Visibility = ViewStates.Visible;

				title.Text = current.title;

                //Setup directors
                budget.Text = "Directed by: ";
                if (current.created_by_name != null)
                {
                    for (int i = 0; i < current.created_by_name.Length; i++)
                    {
                        if (i == current.created_by_name.Length - 1)
                        {
                            budget.Text += current.created_by_name[i];
    
                            break;
                        }
                        budget.Text += current.created_by_name[i] + " and ";
                    }
                }
                //*******************
                duration.Text = current.status;
                redate.Text = current.first_air_date_date;

				poster.SetImageBitmap(img[1]);

				if (current.genres != null)
				{
					for (int i = 0; i < current.genres.Count(); i++)
					{
						if (i == current.genres.Count() - 1)
						{
							genres.Text += current.genres[i];
							break;
						}
						genres.Text += current.genres[i] + " | ";
					}
				}
				backdrop.SetImageBitmap(img[0]);
				overview.Text = current.overview;

				IMDb imdb = new IMDb(current.title, true);
				rating.Text = imdb.Rating;
				//SETTING UP SEASONS****************************************************
				for (int i = 0; i < current.seasons.Length; i++)
				{
					try
					{
						CastDetails currentPerson = await getDataFromApi.GetSeasons(current.ID.ToString(), i.ToString());
						SeasonsList.Add(currentPerson);
					}
					catch (Exception ex) { Console.WriteLine(ex.ToString());}

				}



				SeasonView.Adapter = new CastAdapter(this, SeasonsList, font);
				SeasonView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) =>
				{
					Toast.MakeText(this, SeasonsList[e.Position].Name, ToastLength.Short).Show();
				};


				//*********************************************************************
				//**SETING UP CAST*****************************************************
				for (int i = 0; i < 10; i++)
				{
					try
					{
						CastDetails currentPerson = await getDataFromApi.GetCast(current.ID.ToString(), i, type);
						CastList.Add(currentPerson);
					}
					catch (Exception ex) {Console.WriteLine(ex.ToString()); }

				}

				CastView.Adapter = new CastAdapter(this, CastList, font);
				CastView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) =>
				{
					Toast.MakeText(this, CastList[e.Position].Name, ToastLength.Short).Show();
				};
			}

			playtrailer.Click += delegate {
				var webView = FindViewById(Resource.Id.videoView) as WebView;
                webView.Visibility = ViewStates.Gone;
				WebSettings settings = webView.Settings;
				settings.JavaScriptEnabled = true;
				webView.SetWebChromeClient(new WebChromeClient());
				webView.LoadUrl("http://www.youtube.com/embed/" + current.trailer);
            };

        }


    }
}
