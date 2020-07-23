using Android.App;
using Android.OS;
using Android.Support.V7.App;

namespace RootSelector.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class ResultsActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_results);
        }
    }
}