namespace NetworkTracker.WindowsPhone.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can bind to.
    /// </summary>
    public class MainViewModel : BaseViewModel
    {
        private readonly NetworkDetailsViewModel _networkDetailsViewModel;
        private readonly NetworkStatusViewModel _networkStatusViewModel;

        /// <summary>
        /// Gets the network details view model.
        /// </summary>
        /// <value>
        /// The network details view model.
        /// </value>
        public NetworkDetailsViewModel NetworkDetailsViewModel
        {
            get { return _networkDetailsViewModel; }
        }

        /// <summary>
        /// Gets the network status view model.
        /// </summary>
        /// <value>
        /// The network status view model.
        /// </value>
        public NetworkStatusViewModel NetworkStatusViewModel
        {
            get { return _networkStatusViewModel; }
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(NetworkDetailsViewModel detailsViewModel, NetworkStatusViewModel statusViewModel)
        {
            _networkDetailsViewModel = detailsViewModel;
            _networkStatusViewModel = statusViewModel;
        }
    }
}