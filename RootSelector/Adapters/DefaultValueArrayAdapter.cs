using Android.Content;
using Android.Views;
using Android.Widget;

namespace RootSelector.Adapters
{
    class DefaultValueArrayAdapter<T> : ArrayAdapter<T>
    {
        public DefaultValueArrayAdapter(Context context, int textViewResourceId, T defaultValue)
            : base(context, textViewResourceId)
        {
            DefaultValue = defaultValue;
        }

        public DefaultValueArrayAdapter(Context context, int textViewResourceId)
            : this(context, textViewResourceId, default)
        {
        }

        public T DefaultValue { get; set; }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = (TextView)base.GetView(position, convertView, parent);
            UpdateDefault(view, position);
            return view;
        }

        public override View GetDropDownView(int position, View convertView, ViewGroup parent)
        {
            var view = (TextView)base.GetDropDownView(position, convertView, parent);
            UpdateDefault(view, position);
            return view;
        }

        private void UpdateDefault(TextView view, int position)
        {
            T item = GetItem(position);
            if (DefaultValue != null && DefaultValue.Equals(item))
            {
                view.Text += "  (Default)";
            }
        }
    }
}