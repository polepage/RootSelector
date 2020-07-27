using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Droid.Utils;
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

        private TextView _warning;
        private TextView _error;

        private Button _confirm;

        private ArrayAdapter<int> _playerCountAdapter;
        private DefaultValueArrayAdapter<int> _targetReachAdapter;
        private FactionArrayAdapter _factionsAdapter;

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
            RegisterError();
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
            _targetReachAdapter = new DefaultValueArrayAdapter<int>(this, Resource.Layout.spinner_base, "Recommended");
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
            _factionsAdapter.UpdatedAvailability += FactionUpdated;
            _factions.Adapter = _factionsAdapter;
            UpdateFactions();
        }

        private void UpdateFactions()
        {
            _factionsAdapter.Clear();
            _factionsAdapter.AddAll(Rules.GetAvailableFactions(PlayerCount, TargetReach));
        }

        private void FactionUpdated()
        {
            var availableFactions = _factionsAdapter
                .EnumerateAdapter()
                .Where(af => af.Available)
                .Select(af => af.Faction);

            if (Rules.GetNormalizedFactionCount(availableFactions) < PlayerCount)
            {
                ShowError(GetString(Resource.String.error_playercount));
                DisableConfirm();
                return;
            }

            int maxReach = Rules.GetMaxReach(availableFactions, PlayerCount);
            if (maxReach < Rules.MinimumReach)
            {
                ShowError(string.Format(GetString(Resource.String.error_minreach), maxReach, Rules.MinimumReach));
                DisableConfirm();
                return;
            }

            if (maxReach < TargetReach)
            {
                ShowWarning(string.Format(GetString(Resource.String.warning_targetreach), maxReach, TargetReach));
                EnableConfirm();
                return;
            }

            HideErrors();
            EnableConfirm();
        }

        // Error
        private void RegisterError()
        {
            _warning = FindViewById<TextView>(Resource.Id.text_warning);
            _error = FindViewById<TextView>(Resource.Id.text_error);
        }

        private void ShowWarning(string text)
        {
            _warning.Text = text;
            _error.Visibility = Android.Views.ViewStates.Gone;
            _warning.Visibility = Android.Views.ViewStates.Visible;
        }

        private void ShowError(string text)
        {
            _error.Text = text;
            _warning.Visibility = Android.Views.ViewStates.Gone;
            _error.Visibility = Android.Views.ViewStates.Visible;
        }

        private void HideErrors()
        {
            _warning.Visibility = Android.Views.ViewStates.Gone;
            _error.Visibility = Android.Views.ViewStates.Gone;
        }

        // Process
        private void RegisterProcess()
        {
            _confirm = FindViewById<Button>(Resource.Id.btn_process);
            _confirm.Click += CreateGame;
        }

        private void EnableConfirm()
        {
            _confirm.Enabled = true;
        }

        private void DisableConfirm()
        {
            _confirm.Enabled = false;
        }

        private void CreateGame(object sender, System.EventArgs e)
        {
            // Check conditions before advancing
            var navigation = new Intent(this, typeof(ResultsActivity));
            StartActivity(navigation);
        }
    }
}