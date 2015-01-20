using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace NetworkTracker.BackgroundCore
{
    /// <summary>
    /// Class responsible for showing toast
    /// </summary>
    public sealed class ToastSender
    {
        /// <summary>
        /// Shows toas with the specified text.
        /// </summary>
        /// <param name="toastText">The toast text.</param>
        public void Send(string toastText)
        {
            var toastXml = createToastXml(toastText);

            var toast = new ToastNotification(toastXml);
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }

        private XmlDocument createToastXml(string toastText)
        {
            var toastTemplate = ToastTemplateType.ToastText01;
            var toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);

            var toastTextElements = toastXml.GetElementsByTagName("text");
            toastTextElements[0].AppendChild(toastXml.CreateTextNode(toastText));

            var toastNode = toastXml.SelectSingleNode("/toast");
            ((XmlElement)toastNode).SetAttribute("launch", "{\"type\":\"toast\"}");

            return toastXml;
        }
    }
}
