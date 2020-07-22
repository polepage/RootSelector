using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;

namespace RootSelector.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar")]
    public class GameSetupActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_game);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            RegisterButtons();
        }

        private void RegisterButtons()
        {
            var toGame = FindViewById<Button>(Resource.Id.btn_to_results);
            toGame.Click += NavigateToResults;
        }

        private void NavigateToResults(object sender, System.EventArgs e)
        {
            // Check conditions before advancing
            var navigation = new Intent(this, typeof(ResultActivity));
            StartActivity(navigation);
        }
    }
}