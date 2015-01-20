using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace NetworkTracker.BackgroundCore
{
    /// <summary>
    /// Class responsible for sending local notifications
    /// </summary>
    public sealed class NotificationSender
    {
        /// <summary>
        /// Sends the notification configured with specified title and text.
        /// </summary>
        /// <param name="title">The notification title.</param>
        /// <param name="text">The notification text.</param>
        public void Send(string title, string text)
        {
            var tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150Text09);
            var tileTexts = tileXml.GetElementsByTagName("text");
            ((XmlElement)tileTexts[0]).InnerText = title;
            ((XmlElement)tileTexts[1]).InnerText = text;

            var squareTileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare150x150Text02);
            var squareTileTexts = squareTileXml.GetElementsByTagName("text");
            ((XmlElement)squareTileTexts[0]).InnerText = title;
            ((XmlElement)squareTileTexts[1]).InnerText = text;

            var node = tileXml.ImportNode(squareTileXml.GetElementsByTagName("binding").Item(0), true);
            tileXml.GetElementsByTagName("visual").Item(0).AppendChild(node);
            
            var scheduledTileNotification = new TileNotification(tileXml);

            TileUpdateManager.CreateTileUpdaterForApplication().Update(scheduledTileNotification);
        }
    }
}
