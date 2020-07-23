using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Root;
using System.Linq;

namespace RootSelector.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class SetupActivity : AppCompatActivity
    {
        private Spinner _playerCount;

        private ArrayAdapter<int> _playerCountAdapter;

        private int PlayerCount => _playerCountAdapter.GetItem(_playerCount.SelectedItemPosition);

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_setup);

            RegisterPlayerCount();
            RegisterProcess();
        }

        // Player Count
        private void RegisterPlayerCount()
        {
            _playerCount = FindViewById<Spinner>(Resource.Id.spinner_playercount);
            _playerCount.ItemSelected += PlayerCountChanged;

            _playerCountAdapter = new ArrayAdapter<int>(
                this,
                Resource.Layout.spinner_base,
                Enumerable.Range(Rules.PlayerCount.Min, Rules.PlayerCount.Max - Rules.PlayerCount.Min + 1).ToArray());
            _playerCount.Adapter = _playerCountAdapter;

            _playerCount.SetSelection(_playerCountAdapter.GetPosition(Rules.PlayerCount.Default));
        }

        private void PlayerCountChanged(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            // Update others
        }

        // Process
        private void RegisterProcess()
        {
            var toGame = FindViewById<Button>(Resource.Id.btn_process);
            toGame.Click += CreateGame;
        }

        private void CreateGame(object sender, System.EventArgs e)
        {
            // Check conditions before advancing
            var navigation = new Intent(this, typeof(ResultsActivity));
            StartActivity(navigation);
        }
    }
}