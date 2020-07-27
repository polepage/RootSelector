using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Root;
using RootSelector.Adapters;
using System.Linq;

namespace RootSelector.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class SetupActivity : AppCompatActivity
    {
        private Spinner _playerCount;
        private Spinner _targetReach;
        private ListView _factions;

        private ArrayAdapter<int> _playerCountAdapter;
        private DefaultValueArrayAdapter<int> _targetReachAdapter;
        private ArrayAdapter<Faction> _factionsAdapter;

        private int PlayerCount
        {
            get => _playerCountAdapter.GetItem(_playerCount.SelectedItemPosition);
            set => _playerCount.SetSelection(_playerCountAdapter.GetPosition(value));
        }

        private int TargetReach
        {
            get => _targetReachAdapter.GetItem(_targetReach.SelectedItemPosition);
            set => _targetReach.SetSelection(_targetReachAdapter.GetPosition(value));
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_setup);

            RegisterPlayerCount();
            RegisterReach();
            RegisterFactions();
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

            PlayerCount = Rules.PlayerCount.Default;
        }

        private void PlayerCountChanged(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            UpdateReach();
        }

        // Target Reach
        private void RegisterReach()
        {
            _targetReach = FindViewById<Spinner>(Resource.Id.spinner_reach);
            _targetReach.ItemSelected += TargetReachChanged;
            _targetReachAdapter = new DefaultValueArrayAdapter<int>(this, Resource.Layout.spinner_base);
            _targetReach.Adapter = _targetReachAdapter;
            UpdateReach();
        }

        private void TargetReachChanged(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            UpdateFactions();
        }

        private void UpdateReach()
        {
            var reachRange = Rules.GetTargetReach(PlayerCount);
            _targetReachAdapter.Clear();
            _targetReachAdapter.DefaultValue = reachRange.Default;
            _targetReachAdapter.AddAll(Enumerable.Range(reachRange.Min, reachRange.Max - reachRange.Min + 1).ToList());

            TargetReach = reachRange.Default;
        }

        // Factions
        private void RegisterFactions()
        {
            _factions = FindViewById<ListView>(Resource.Id.listview_factions);
            _factionsAdapter = new FactionArrayAdapter(this);
            _factions.Adapter = _factionsAdapter;
            UpdateFactions();
        }

        private void UpdateFactions()
        {
            _factionsAdapter.Clear();
            _factionsAdapter.AddAll(Rules.GetAvailableFactions(PlayerCount, TargetReach));
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