
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace CINEMAGO
{
    class CastAdapter : BaseAdapter
    {
		private readonly Context context;
		List<CastDetails> castdetails = new List<CastDetails>();
		Typeface font;

		public CastAdapter(Context c, List<CastDetails> details, Typeface font)
		{
			context = c;
			castdetails = details;
			this.font = font;
		}

		public override int Count
		{
			get
			{
				return castdetails.Count;
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

				ImageView pic = view.FindViewById(Resource.Id.imgItem) as ImageView;
				TextView name = view.FindViewById(Resource.Id.txtItem) as TextView;

				name.SetTypeface(font, TypefaceStyle.Normal);

				pic.SetImageBitmap(castdetails[position].profile_path);
                name.Text = castdetails[position].Name;
			}

			return view;
		}
	}
}
