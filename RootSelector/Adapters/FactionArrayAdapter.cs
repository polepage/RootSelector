using Android.Content;
using Android.Views;
using Android.Widget;
using Root;
using System;

namespace RootSelector.Adapters
{
    class FactionArrayAdapter : ArrayAdapter<AvailableFaction>
    {
        public event Action UpdatedAvailability;

        public FactionArrayAdapter(Context context)
            : base(context, -1)
        {
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var inflater = (LayoutInflater)Context.GetSystemService(Context.LayoutInflaterService);
            View rowView = inflater.Inflate(Resource.Layout.listview_factions, parent, false);

            AvailableFaction faction = GetItem(position);

            var id = rowView.FindViewById<TextView>(Resource.Id.factions_id);
            id.Text = faction.Faction.Id.ToString();

            var name = rowView.FindViewById<TextView>(Resource.Id.factions_name);
            name.Text = faction.Faction.Name;

            var reach = rowView.FindViewById<TextView>(Resource.Id.factions_reach);
            reach.Text = "(" + faction.Faction.Reach + ")";

            var toggle = rowView.FindViewById<Switch>(Resource.Id.factions_switch);
            toggle.Checked = faction.Available;
            toggle.CheckedChange += (sender, e) => ToggleChanged(faction, e);

            return rowView;
        }

        private void ToggleChanged(AvailableFaction faction, CompoundButton.CheckedChangeEventArgs e)
        {
            faction.Available = e.IsChecked;
            UpdatedAvailability?.Invoke();
        }
    }
}