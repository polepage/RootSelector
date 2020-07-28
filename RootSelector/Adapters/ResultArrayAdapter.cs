using Android.Content;
using Android.Views;
using Android.Widget;
using Root;

namespace RootSelector.Adapters
{
    class ResultArrayAdapter: ArrayAdapter<Faction>
    {
        public ResultArrayAdapter(Context context)
            : this(context, -1)
        {
        }

        public ResultArrayAdapter(Context context, int startPosition)
            : base(context, -1)
        {
            StartPosition = startPosition;
        }

        public int StartPosition { get; set; }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var inflater = (LayoutInflater)Context.GetSystemService(Context.LayoutInflaterService);
            View rowView = inflater.Inflate(Resource.Layout.listview_results, parent, false);

            if (position == StartPosition)
            {
                rowView.SetBackgroundColor(new Android.Graphics.Color(Context.GetColor(Resource.Color.colorAccent)));
            }

            Faction faction = GetItem(position);

            var pos = rowView.FindViewById<TextView>(Resource.Id.results_position);
            pos.Text = (position + 1).ToString() + ".";

            var id = rowView.FindViewById<TextView>(Resource.Id.results_id);
            id.Text = faction.Id.ToString();

            var name = rowView.FindViewById<TextView>(Resource.Id.results_name);
            name.Text = faction.Name;

            return rowView;
        }
    }
}