using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.Linq;
using Xamarin.Essentials;

namespace connectivity
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class ConnectivityActivity : AppCompatActivity
    {
        private TextView _resultTextView;
        private TextView _connectionProfileTextView;
        private Button _checkConnectivityButton;
        private Button _checkconnectionProfileButton;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            UIReferences();
            UIClickEvents();
            CheckConnectivityChanged();
        }

        private void UIReferences()
        {
            _resultTextView = FindViewById<TextView>(Resource.Id.resultTextView);
            _connectionProfileTextView = FindViewById<TextView>(Resource.Id.connectionProfileTextView);
            _checkConnectivityButton = FindViewById<Button>(Resource.Id.checkConnectivityButton);
            _checkconnectionProfileButton = FindViewById<Button>(Resource.Id.checkconnectionProfileButton);
        }

        private void UIClickEvents()
        {
            _checkConnectivityButton.Click += CheckConnectivityButton_Click;
            _checkconnectionProfileButton.Click += CheckConnectivityProfileButton_Click;
        }

        private void CheckConnectivityProfileButton_Click(object sender, EventArgs e)
        {
            var profiles = Connectivity.ConnectionProfiles;
            if (profiles.Contains(ConnectionProfile.WiFi))
            {
                _connectionProfileTextView.Text = "Connected to WiFi";
            }
            else if (profiles.Contains(ConnectionProfile.Cellular))
            {
                _connectionProfileTextView.Text = "Connected to Cellular";
            }
            else if (profiles.Contains(ConnectionProfile.Ethernet))
            {
                _connectionProfileTextView.Text = "Connected to Ethernet";
            }
            else
            {
                _connectionProfileTextView.Text = "Unknown";
            }
        }

        private void CheckConnectivityButton_Click(object sender, EventArgs e)
        {
            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
            {
                _resultTextView.Text = "Connected to Internet";
            }
            else
            {
                _resultTextView.Text = "No Internet";
            }
        }

        private void CheckConnectivityChanged()
        {
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        private void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            var access = e.NetworkAccess;
            var profiles = e.ConnectionProfiles;
            Toast.MakeText(this, text: $"Network Access: {access} \nConnection Profiles: {profiles}", ToastLength.Long).Show();
        }
    }
}