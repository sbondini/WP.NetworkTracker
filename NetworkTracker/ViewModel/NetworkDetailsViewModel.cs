using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.Networking.Connectivity;
using NetworkTracker.WindowsPhone.Data;

namespace NetworkTracker.WindowsPhone.ViewModel
{
    /// <summary>
    /// This class contains properties that the network details hub section can bind to.
    /// </summary>
    public class NetworkDetailsViewModel : BaseViewModel
    {
        /// <summary>
        /// Gets or sets the network information items.
        /// </summary>
        /// <value>
        /// The network information items.
        /// </value>
        public ObservableCollection<NetworkInformationItem> NetworkInformationItems { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkDetailsViewModel"/> class.
        /// </summary>
        public NetworkDetailsViewModel()
        {
            NetworkInformationItems = new ObservableCollection<NetworkInformationItem>();
            NetworkInformation.NetworkStatusChanged += onNetworkStatusChanged;
            updateNetworkDetails();
        }

        private void onNetworkStatusChanged(object sender)
        {
            updateNetworkDetails();
        }

        private void updateNetworkDetails()
        {
            var profile = NetworkInformation.GetInternetConnectionProfile();

            var informationItems = new List<NetworkInformationItem>();
            if (profile != null)
            {
                informationItems.Add(new NetworkInformationItem("Profile Name", profile.ProfileName));
                informationItems.Add(new NetworkInformationItem("Connectivity Level", profile.GetNetworkConnectivityLevel().ToString()));
                informationItems.Add(new NetworkInformationItem("Domain Connectivity Level", profile.GetDomainConnectivityLevel().ToString()));

                var connectionCost = profile.GetConnectionCost();
                if (connectionCost != null)
                {
                    informationItems.Add(new NetworkInformationItem("Cost", connectionCost.NetworkCostType.ToString()));
                    informationItems.Add(new NetworkInformationItem("Roaming", connectionCost.Roaming.ToString()));
                    informationItems.Add(new NetworkInformationItem("Over Data Limit", connectionCost.OverDataLimit.ToString()));
                    informationItems.Add(new NetworkInformationItem("Approaching Data Limit", connectionCost.ApproachingDataLimit.ToString()));
                }
            }

            updateNetworkInformationItems(informationItems);
        }



        private void updateNetworkInformationItems(IEnumerable<NetworkInformationItem> items)
        {
            InvokeOnUI(() =>
            {
                NetworkInformationItems.Clear();
                foreach (var item in items)
                {
                    NetworkInformationItems.Add(item);
                }
            });
        }
    }
}
