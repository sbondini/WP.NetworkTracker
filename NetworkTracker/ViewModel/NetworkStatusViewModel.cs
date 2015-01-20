using System.Diagnostics;
using System.Net.NetworkInformation;
using Windows.ApplicationModel.Background;
using Windows.Networking.Connectivity;
using NetworkTracker.WindowsPhone.Facades;

namespace NetworkTracker.WindowsPhone.ViewModel
{
    /// <summary>
    /// This class contains properties that the network status hub section can bind to.
    /// </summary>
    public class NetworkStatusViewModel : BaseViewModel
    {
        private readonly NetworkBackgroundTaskFacade _backgroundTaskFacade;
        private string _networkStatus;
        private string _ssid;
        private bool _isRegisteredInBackground;

        /// <summary>
        /// Gets or sets the network status string.
        /// </summary>
        /// <value>
        /// The network status string.
        /// </value>
        public string NetworkStatusString
        {
            get { return _networkStatus; }
            set { SetProperty(() => NetworkStatusString, ref _networkStatus, value); }
        }

        /// <summary>
        /// Gets or sets the ssid.
        /// </summary>
        /// <value>
        /// The ssid.
        /// </value>
        public string Ssid
        {
            get { return _ssid; }
            set { SetProperty(() => Ssid, ref _ssid, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether application registered background tasks.
        /// </summary>
        /// <value>
        /// <c>true</c> if this application registered background tasks; otherwise, <c>false</c>.
        /// </value>
        public bool IsRegisteredInBackground
        {
            get { return _isRegisteredInBackground; }
            set
            {
                SetProperty(() => IsRegisteredInBackground, ref _isRegisteredInBackground, value);
                onBackgroundRegistrationStatusChanged(value);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkStatusViewModel"/> class.
        /// </summary>
        /// <param name="backgroundTaskFacade">The background task facade.</param>
        public NetworkStatusViewModel(NetworkBackgroundTaskFacade backgroundTaskFacade)
        {
            _backgroundTaskFacade = backgroundTaskFacade;
            NetworkInformation.NetworkStatusChanged += onNetworkStatusChanged;
            updatePageInformation();
            checkIfBackgroundTaskRegistered(backgroundTaskFacade);
        }

        private void onNetworkStatusChanged(object sender)
        {
            updatePageInformation();
        }

        private void updatePageInformation()
        {
            NetworkStatusString = NetworkInterface.GetIsNetworkAvailable() ? "Connected" : "Not Connected";
            updateSsid();
        }

        private void updateSsid()
        {
            var profile = NetworkInformation.GetInternetConnectionProfile();
            Ssid = profile != null && profile.WlanConnectionProfileDetails != null
                ? profile.WlanConnectionProfileDetails.GetConnectedSsid()
                : "Unknown";
        }

        private void checkIfBackgroundTaskRegistered(NetworkBackgroundTaskFacade backgroundTaskFacade)
        {
            BackgroundTaskRegistration taskRegistration;
            _isRegisteredInBackground = backgroundTaskFacade.IsTaskRegistered(out taskRegistration);
        }

        private async void onBackgroundRegistrationStatusChanged(bool isRegisteredInBackground)
        {
            if (isRegisteredInBackground)
            {
                var result = await _backgroundTaskFacade.RegisterBackgroundTask().ConfigureAwait(false);
                Debug.WriteLine("Background Task Registration: {0}", result == null ? "failed" : "succeeded");
            }
            else
            {
                _backgroundTaskFacade.UnregisterBackgroundTask();
            }
        }
    }
}
