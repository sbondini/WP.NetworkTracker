namespace NetworkTracker.WindowsPhone.Data
{
    /// <summary>
    /// Information item used to store parameter title and description
    /// </summary>
    public class NetworkInformationItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkInformationItem"/> class.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="description">The description.</param>
        public NetworkInformationItem(string title, string description)
        {
            Title = title;
            Description = description;
        }

        /// <summary>
        /// Gets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; private set; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; private set; }
    }
}
