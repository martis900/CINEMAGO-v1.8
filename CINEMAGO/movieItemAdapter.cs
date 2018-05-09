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
    class movieAdapter : BaseAdapter

    {
        private readonly Context context;
        Typeface font;
        List<MovieDetails> moviedetails = new List<MovieDetails>();

        public movieAdapter(Context c, List<MovieDetails> details, Typeface font)
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
            View view = null;

            LayoutInflater inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);
            if (convertView == null)
            {
                view = new View(context);
                view = inflater.Inflate(Resource.Layout.MovieTVItem, null);
                TextView txtView = view.FindViewById<TextView>(Resource.Id.txtItem);
                ImageView imgView = view.FindViewById<ImageView>(Resource.Id.imgItem);

                txtView.SetTypeface(font, TypefaceStyle.Normal);
                txtView.Text = moviedetails[position].title;
                txtView.Gravity = GravityFlags.Center;
                try
                {
                    imgView.SetImageBitmap(moviedetails[position].poster);
                }
                catch
                {
                    imgView.SetImageResource(Resource.Drawable.Icon);
                }


            }

            return view;
        }

}
}