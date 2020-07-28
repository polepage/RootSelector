using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Droid.Utils;
using Root;
using RootSelector.Adapters;
using RootSelector.Intents;
using System.Linq;

namespace RootSelector.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class ResultsActivity : AppCompatActivity
    {
        private ResultArrayAdapter _resultsAdapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_results);

            RegisterResults();
        }

        protected override void OnStart()
        {
            base.OnStart();
            UpdateResults(Intent.GetTypedExtra<GameSetupParcelable>(GameSetupParcelable.IntentId).GameSetup);
        }

        private void RegisterResults()
        {
            var results = FindViewById<ListView>(Resource.Id.listview_results);
            _resultsAdapter = new ResultArrayAdapter(this);
            results.Adapter = _resultsAdapter;
        }

        private void UpdateResults(GameSetup gameSetup)
        {
            _resultsAdapter.Clear();
            _resultsAdapter.StartPosition = gameSetup.FirstPlayer;
            _resultsAdapter.AddAll(gameSetup.Seats.ToArray());
        }
    }
}