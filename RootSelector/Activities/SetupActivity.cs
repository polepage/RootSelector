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
        private Spinner _targetReach;

        private ArrayAdapter<int> _playerCountAdapter;
        private ArrayAdapter<int> _targetReachAdapter;

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
            RegisterReset();
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

        private void RegisterReach()
        {
            _targetReach = FindViewById<Spinner>(Resource.Id.spinner_reach);
            _targetReach.ItemSelected += TargetReachChanged;
            UpdateReach();
        }

        private void TargetReachChanged(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            // Do something?
        }

        private void UpdateReach()
        {
            var reachRange = Rules.TargetReach(PlayerCount);
            _targetReachAdapter = new ArrayAdapter<int>(
                this,
                Resource.Layout.spinner_base,
                Enumerable.Range(reachRange.Min, reachRange.Max - reachRange.Min + 1).ToArray());
            _targetReach.Adapter = _targetReachAdapter;

            TargetReach = reachRange.Default;
        }

        // Reset
        private void RegisterReset()
        {
            var reset = FindViewById<Button>(Resource.Id.btn_reset);
            reset.Click += ResetForm;
        }

        private void ResetForm(object sender, System.EventArgs e)
        {
            UpdateReach();
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