using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using RestSharp;
using System.Threading.Tasks;
using Android.Graphics;
using System.Net;
using Newtonsoft.Json.Linq;

namespace CINEMAGO
{
    class getDataFromApi
	{
		public static async Task<MovieDetails> GetBasicInfo(string movieID, string type)
		{
			string key = "253ceb1e198e26b094348524da5bc8ab";

			string query = string.Format("https://api.themoviedb.org/3/{0}/{1}?api_key={2}", type, movieID, key);

			var result = downloadjson.getJson(query);

			MovieDetails current = null;

			if (type == "movie")
			{

				//create new object
				current = new MovieDetails();
				try { current.title = result["original_title"].ToString(); } catch (Exception ex) { Console.Write(ex.ToString()); };
                try { current.ID = (int)result["id"]; } catch (Exception ex) { Console.Write(ex.ToString()); }
				try
				{
					Java.Net.URL link2 = new Java.Net.URL("http://image.tmdb.org/t/p/w185/" + result["poster_path"].ToString());
					await Task.Run(() => { current.poster = BitmapFactory.DecodeStream(link2.OpenStream()); });
				}
                catch(Exception ex){Console.Write(ex.ToString()); }

			}
			else if (type == "tv")
			{
				//create new object
				current = new MovieDetails();
				try { current.title = result["name"].ToString(); } catch (Exception ex) { Console.Write(ex.ToString()); };
                try { current.ID = (int)result["id"]; } catch (Exception ex) { Console.Write(ex.ToString()); }
				try
				{
					Java.Net.URL link2 = new Java.Net.URL("http://image.tmdb.org/t/p/w185/" + result["poster_path"].ToString());
					await Task.Run(() => { current.poster = BitmapFactory.DecodeStream(link2.OpenStream()); });
				}
				catch (Exception ex) { Console.Write(ex.ToString()); }
			}


			return current;


		}

		public static string[] Lolas ( JObject result, string ob,string name)
		{
			JArray items = (JArray)result[ob];
			int length = items.Count;
			string[] ar = new string[length];
			for (int i = 0; i < length; i++)
			{
				ar[i] = result[ob][i][name].ToString();
			}
			return ar;
		}

        public static async Task<MovieDetails> GetMovieDetails(string movieID,string type)
        {
            string key = "253ceb1e198e26b094348524da5bc8ab";

            string query = string.Format("https://api.themoviedb.org/3/{0}/{1}?api_key={2}", type,movieID, key);

            var result = downloadjson.getJson(query);

			MovieDetails current = null;

            if (type == "movie")
            {
               
                //create new object
                current = new MovieDetails();
                try { current.title = result["original_title"].ToString(); } catch (Exception ex) { Console.Write(ex.ToString()); };
                try { current.ID = (int)result["id"]; } catch (Exception ex) { Console.Write(ex.ToString()); }
                try { current.imdbID = result["imdb_id"].ToString(); } catch (Exception ex) { Console.Write(ex.ToString()); }
                try { current.budget = (string)result["budget"]; } catch (Exception ex) { Console.Write(ex.ToString()); }
                try { current.overview = result["overview"].ToString(); } catch (Exception ex) { Console.Write(ex.ToString()); }
                try { current.release_date = result["release_date"].ToString(); } catch (Exception ex) { Console.Write(ex.ToString()); }
                try { current.duration = (int)result["runtime"]; } catch (Exception ex) { Console.Write(ex.ToString()); }
                try { current.revenue = (int)result["revenue"]; } catch (Exception ex) { Console.Write(ex.ToString()); }

                try
                {
                    Java.Net.URL link2 = new Java.Net.URL("http://image.tmdb.org/t/p/w185/" + result["poster_path"].ToString());
                    await Task.Run(() => { current.poster = BitmapFactory.DecodeStream(link2.OpenStream()); });
                }
                catch (Exception ex) { Console.Write(ex.ToString()); }
                //**************** SKATIOMA ZANRUS ***************************

                JArray items = (JArray)result["genres"];
                int length = items.Count;
                string[] ar = new string[length];
                for (int i = 0; i < length; i++)
                {
                    ar[i] = result["genres"][i]["name"].ToString();
                }
                current.genres = ar;

                try
                {
                    query = string.Format("http://api.themoviedb.org/3/movie/{0}/videos?api_key={1}", movieID, key);
                    var objectas = downloadjson.getJson(query);
                    current.trailer = objectas["results"][0]["key"].ToString();
                }
                catch (Exception ex) { Console.Write(ex.ToString()); }

            }
            else if (type == "tv")
            {
				current = new MovieDetails();
				try { current.title = result["name"].ToString(); } catch (Exception ex) { Console.Write(ex.ToString()); };
				try { current.ID = (int)result["id"]; } catch (Exception ex) { Console.Write(ex.ToString()); }
				try { current.imdbID = result["imdb_id"].ToString(); } catch (Exception ex) { Console.Write(ex.ToString()); }
				try { current.overview = result["overview"].ToString(); } catch (Exception ex) { Console.Write(ex.ToString()); }
				try { current.first_air_date_date = result["first_air_date"].ToString(); } catch (Exception ex) { Console.Write(ex.ToString()); }
                current.duration = (int)result["episode_run_time"][0];
				try
				{
					Java.Net.URL link2 = new Java.Net.URL("http://image.tmdb.org/t/p/w185/" + result["poster_path"].ToString());
					await Task.Run(() => { current.poster = BitmapFactory.DecodeStream(link2.OpenStream()); });
				}
				catch (Exception ex) { Console.Write(ex.ToString()); }
				//**************** SKATIOMA ZANRUS ***************************

				JArray items = (JArray)result["genres"];
				int length = items.Count;
				string[] ar = new string[length];
				for (int i = 0; i < length; i++)
				{
					ar[i] = result["genres"][i]["name"].ToString();
				}
				current.genres = ar;

				try
				{
					query = string.Format("http://api.themoviedb.org/3/tv/{0}/videos?api_key={1}", movieID, key);
					var objectas = downloadjson.getJson(query);
					current.trailer = objectas["results"][0]["key"].ToString();
				}
				catch (Exception ex) { Console.Write(ex.ToString()); }

                current.created_by_name = Lolas(result, "created_by", "name");
                current.seasons = Lolas(result, "seasons", "id");
                current.networks_id = Lolas(result, "networks", "name");
                current.status = result["status"].ToString();
                current.number_of_seasons = result["number_of_seasons"].ToString();
                current.number_of_episodes = result["number_of_episodes"].ToString();
                current.release_date = result["first_air_date"].ToString();

            }
            

            return current;
            
        }



		public static async Task<CastDetails> GetCast(string movieID, int num, string type)
		{
			string key = "253ceb1e198e26b094348524da5bc8ab";

            string query = string.Format("https://api.themoviedb.org/3/{0}/{1}/credits?api_key={2}", type, movieID, key);

			var result = downloadjson.getJson(query);

			CastDetails currentPerson = null;


			//create new object
			currentPerson = new CastDetails();
            try
            {
				currentPerson.Name = result["cast"][num]["name"].ToString();
				currentPerson.ID = result["cast"][num]["id"].ToString();
            }
            catch (Exception ex){}


            try
            {
				Java.Net.URL link2 = new Java.Net.URL("http://image.tmdb.org/t/p/w185/" + result["cast"][num]["profile_path"].ToString());
				await Task.Run(() => { currentPerson.profile_path = BitmapFactory.DecodeStream(link2.OpenStream()); });
            }
            catch (Exception ex){}

			return currentPerson;
		}

		public static async Task<List<Bitmap>> GetBackdrop(string movieID,string type)
		{
			string key = "253ceb1e198e26b094348524da5bc8ab";

            string query = string.Format("https://api.themoviedb.org/3/{0}/{1}?api_key={2}",type, movieID, key);

			var result = downloadjson.getJson(query);

            List<Bitmap> photob = new List<Bitmap>();
            Bitmap ph = null;
            try
            {
				Java.Net.URL link1 = new Java.Net.URL("http://image.tmdb.org/t/p/w780/" + result["backdrop_path"].ToString());
				await Task.Run(() => { ph = BitmapFactory.DecodeStream(link1.OpenStream()); });
				photob.Add(ph);
            }
            catch (Exception ex){

            }


            try
            {
				Java.Net.URL link2 = new Java.Net.URL("http://image.tmdb.org/t/p/w185/" + result["poster_path"].ToString());
				await Task.Run(() => { ph = BitmapFactory.DecodeStream(link2.OpenStream()); });
				photob.Add(ph);
				return photob;
            }
            catch (Exception ex){}
            return photob;

		}
        public static async Task<string[]> GetPopular(string type, string action, int maxresult)
        {
            int MaxResults = maxresult;
            string[] id = new string[MaxResults];
            string key = "253ceb1e198e26b094348524da5bc8ab";

            string query = string.Format("https://api.themoviedb.org/3/discover/{0}?api_key={1}&language=en-US&sort_by={2}&include_adult=false&include_video=false&page=1", type, key, action);

            var result = downloadjson.getJson(query);

            if ((int)result["total_results"] < MaxResults)
            {
                MaxResults = (int)result["total_results"];
            }
            for (int i = 0; i < MaxResults; i++)
            {
                id[i] = (string)result["results"][i]["id"];
            }

            return id;
        }

		public static async Task<CastDetails> GetSeasons(string movieID, string season)
		{
			string key = "253ceb1e198e26b094348524da5bc8ab";

            string query = string.Format("https://api.themoviedb.org/3/tv/{0}/season/{1}?api_key={2}&language=en-US", movieID,season, key);

			var result = downloadjson.getJson(query);

			CastDetails currentPerson = null;


			//create new object
			currentPerson = new CastDetails();
			try
			{
				currentPerson.Name = result["name"].ToString();
				currentPerson.ID = result["id"].ToString();
			}
			catch (Exception ex) { }


			try
			{
				Java.Net.URL link2 = new Java.Net.URL("http://image.tmdb.org/t/p/w185/" + result["poster_path"].ToString());
				await Task.Run(() => { currentPerson.profile_path = BitmapFactory.DecodeStream(link2.OpenStream()); });
			}
			catch (Exception ex) { }

			return currentPerson;
		}


	}
}