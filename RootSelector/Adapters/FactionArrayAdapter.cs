using Android.Content;
using Android.Views;
using Android.Widget;
using Root;

namespace RootSelector.Adapters
{
    class FactionArrayAdapter : ArrayAdapter<Faction>
    {
        public FactionArrayAdapter(Context context)
            : base(context, -1)
        {
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var inflater = (LayoutInflater)Context.GetSystemService(Context.LayoutInflaterService);
            View rowView = inflater.Inflate(Resource.Layout.listview_factions, parent, false);

            Faction faction = GetItem(position);

            var id = rowView.FindViewById<TextView>(Resource.Id.factions_id);
            id.Text = faction.Id.ToString();

            var name = rowView.FindViewById<TextView>(Resource.Id.factions_name);
            name.Text = faction.Name;

            var reach = rowView.FindViewById<TextView>(Resource.Id.factions_reach);
            reach.Text = "(" + faction.Reach + ")";

            return rowView;
        }
    }
}