using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Text;
using Windows.ApplicationModel.Background;
using Windows.Networking.Connectivity;

namespace NetworkTracker.BackgroundCore
{
    /// <summary>
    /// Background task, executed every time network state changes
    /// </summary>
    public sealed class NetworkBackgroundTask : IBackgroundTask
    {
        private readonly NotificationSender _notificationSender = new NotificationSender();
        private readonly ToastSender _toastSender = new ToastSender();

        /// <summary>
        /// Runs the specified background task instance.
        /// </summary>
        /// <param name="taskInstance">The task instance.</param>
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            Debug.WriteLine("NetworkBackgroundTask is running...");
            
            _notificationSender.Send("Network State", getNetworkStatusMessage());
            _toastSender.Send(getNetworkStatusMessage());

            Debug.WriteLine("NetworkBackgroundTask is finished");
        }

        private string getNetworkStatusMessage()
        {
            var stringBuilder = new StringBuilder("Network ");
            
            var networkProfile = NetworkInformation.GetInternetConnectionProfile();
            if (networkProfile != null && networkProfile.WlanConnectionProfileDetails != null)
            {
                stringBuilder.Append(networkProfile.WlanConnectionProfileDetails.GetConnectedSsid() + " ");
            }

            var connectionStatus = NetworkInterface.GetIsNetworkAvailable() ? "is connected" : "is not connected";
            stringBuilder.Append(connectionStatus);

            return stringBuilder.ToString();
        }
    }
}
