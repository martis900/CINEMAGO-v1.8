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
using Java.Lang;

namespace CINEMAGO
{
    class SearchlistviewAdapter : BaseAdapter
    {
		private readonly Context context;
		List<MovieDetails> moviedetails = new List<MovieDetails>();
        Typeface font;

		public SearchlistviewAdapter(Context c, List<MovieDetails> details, Typeface font)
		{
			context = c;
			moviedetails = details;
            this.font = font;
		}

		public override int Count
		{
			get
			{
				return moviedetails.Count;
			}
		}

		public override Java.Lang.Object GetItem(int position)
		{
			return null;
		}

		public override long GetItemId(int position)
		{
			return 0;
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			View view=null;

			LayoutInflater inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);

            if (convertView == null)
			{
				view = new View(context);

				view = inflater.Inflate(Resource.Layout.SearchItemLayout, null);

				ImageView imgg = view.FindViewById(Resource.Id.searchitemphoto) as ImageView;
			    TextView title = view.FindViewById(Resource.Id.searchitemtitle) as TextView;
				TextView date = view.FindViewById(Resource.Id.searchitemdate) as TextView;
				TextView duration = view.FindViewById(Resource.Id.searchitemduration) as TextView;

                


                title.SetTypeface(font, TypefaceStyle.Normal);
                date.SetTypeface(font, TypefaceStyle.Normal);
                duration.SetTypeface(font, TypefaceStyle.Normal);

                imgg.SetImageBitmap(moviedetails[position].poster);
                title.Text = moviedetails[position].title;
                date.Text = moviedetails[position].release_date;
                duration.Text = moviedetails[position].duration.ToString();


			}

			return view;
		}
    }
}
