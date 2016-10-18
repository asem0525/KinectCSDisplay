
namespace Microsoft.Samples.Kinect.ControlsBasics.DataModel
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using Microsoft.Samples.Kinect.ControlsBasics.Common;
    using System.Globalization;
    using Pages;


    // The data model defined by this file serves as a representative example of a strongly-typed
    // model that supports notification when members are added, removed, or modified.  The property
    // names chosen coincide with data bindings in the standard item templates.
    // Applications may use this model as a starting point and build on it, or discard it entirely and
    // replace it with something appropriate to their needs.

    /// <summary>
    /// Creates a collection of groups and items with hard-coded content.
    /// SampleDataSource initializes with placeholder data rather than live production
    /// data so that sample data is provided at both design-time and run-time.
    /// </summary>
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "File is from Windows Store template")]
    public sealed class DataSource
    {
        private static DataSource sampleDataSource = new DataSource();

        private ObservableCollection<DataCollection> allGroups = new ObservableCollection<DataCollection>();

        private static Uri darkGrayImage = new Uri("Assets/DarkGray.png", UriKind.Relative);
        private static Uri mediumGrayImage = new Uri("assets/mediumGray.png", UriKind.Relative);
        private static Uri lightGrayImage = new Uri("assets/lightGray.png", UriKind.Relative);

        public DataSource()
        {
            string itemContent = string.Format(
                                    CultureInfo.CurrentCulture,
                                    "Item Content: {0}\n\n{0}\n\n{0}\n\n{0}\n\n{0}\n\n{0}\n\n{0}",
                                    "Curabitur class aliquam vestibulum nam curae maecenas sed integer cras phasellus suspendisse quisque donec dis praesent accumsan bibendum pellentesque condimentum adipiscing etiam consequat vivamus dictumst aliquam duis convallis scelerisque est parturient ullamcorper aliquet fusce suspendisse nunc hac eleifend amet blandit facilisi condimentum commodo scelerisque faucibus aenean ullamcorper ante mauris dignissim consectetuer nullam lorem vestibulum habitant conubia elementum pellentesque morbi facilisis arcu sollicitudin diam cubilia aptent vestibulum auctor eget dapibus pellentesque inceptos leo egestas interdum nulla consectetuer suspendisse adipiscing pellentesque proin lobortis sollicitudin augue elit mus congue fermentum parturient fringilla euismod feugiat");

            var mainWindowPages = new DataCollection(
                    "Group-1",
                    "Group Title: MainPage Items",
                    "Group Subtitle: ",
                    DataSource.mediumGrayImage,
                    "Series of applets that users can interact with");
            mainWindowPages.Items.Add(
                    new DataItem(
                        "Group-1-Item-Weather",
                        "Weather",
                        "More Details",
                        new Uri("../Images/WeatherIcons/mostlycloudy.png", UriKind.Relative),
                        "Expanded weather information",
                        itemContent,
                        mainWindowPages,
                        typeof(WeatherPage)));
            mainWindowPages.Items.Add(
                    new DataItem(
                        "Group-1-Bongo-Info",
                        "Bus Schedule",
                        "Complete Schedule",
                        new Uri("../Assets/BusLogoFront.png", UriKind.Relative),
                        "Expanded Bongo data",
                        itemContent,
                        mainWindowPages,
                        typeof(BongoPage)));
            mainWindowPages.Items.Add(
                    new DataItem(
                        "Group-1-CS-Events",
                        "News & Events",
                        string.Empty,
                        new Uri("../Assets/newsIcon.png", UriKind.Relative),
                        "CS Events, CS News, and Today's Headlines",
                        itemContent,
                        mainWindowPages,
                        typeof(CSEvents)));
            mainWindowPages.Items.Add(
                   new DataItem(
                       "Group-1-People",
                       "People",
                       "List of Faculty",
                       new Uri("../Assets/people Icon.png", UriKind.Relative),
                       "List of faculty members",
                       itemContent,
                       mainWindowPages,
                       typeof(PeoplePage)));
            mainWindowPages.Items.Add(
                   new DataItem(
                       "Group-1-Maps",
                       "Building Map",
                       string.Empty,
                       new Uri("../Assets/mapIcon.png", UriKind.Relative),
                       "Map of building",
                       itemContent,
                       mainWindowPages,
                       typeof(MapsPage)));

            this.AllGroups.Add(mainWindowPages);
        }

        public ObservableCollection<DataCollection> AllGroups
        {
            get { return this.allGroups; }
        }

        public static DataCollection GetGroup(string uniqueId)
        {
            // Simple linear search is acceptable for small data sets
            var matches = sampleDataSource.AllGroups.Where((group) => group.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1)
            {
                return matches.First();
            }

            return null;
        }

        public static DataItem GetItem(string uniqueId)
        {
            // Simple linear search is acceptable for small data sets
            var matches = sampleDataSource.AllGroups.SelectMany(group => group.Items).Where((item) => item.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1)
            {
                return matches.First();
            }

            return null;
        }
    }

    /// <summary>
    /// Base class for <see cref="DataItem"/> and <see cref="DataCollection"/> that
    /// defines properties common to both.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1649:FileHeaderFileNameDocumentationMustMatchTypeName", Justification = "Reviewed.")]
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "File is from Windows Store template")]
    public abstract class SampleDataCommon : BindableBase
    {
        /// <summary>
        /// baseUri for image loading purposes
        /// </summary>
        private static Uri baseUri = new Uri("pack://application:,,,/");

        /// <summary>
        /// Field to store uniqueId
        /// </summary>
        private string uniqueId = string.Empty;

        /// <summary>
        /// Field to store title
        /// </summary>
        private string title = string.Empty;

        /// <summary>
        /// Field to store subtitle
        /// </summary>
        private string subtitle = string.Empty;

        /// <summary>
        /// Field to store description
        /// </summary>
        private string description = string.Empty;

        /// <summary>
        /// Field to store image
        /// </summary>
        private ImageSource image = null;

        /// <summary>
        /// Field to store image path
        /// </summary>
        private Uri imagePath = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="SampleDataCommon" /> class.
        /// </summary>
        /// <param name="uniqueId">The unique id of this item.</param>
        /// <param name="title">The title of this item.</param>
        /// <param name="subtitle">The subtitle of this item.</param>
        /// <param name="imagePath">A relative path of the image for this item.</param>
        /// <param name="description">A description of this item.</param>
        protected SampleDataCommon(string uniqueId, string title, string subtitle, Uri imagePath, string description)
        {
            this.uniqueId = uniqueId;
            this.title = title;
            this.subtitle = subtitle;
            this.description = description;
            this.imagePath = imagePath;
        }

        /// <summary>
        /// Gets or sets UniqueId.
        /// </summary>
        public string UniqueId
        {
            get { return this.uniqueId; }
            set { this.SetProperty(ref this.uniqueId, value); }
        }

        public string Title
        {
            get { return this.title; }
            set { this.SetProperty(ref this.title, value); }
        }

        public string Subtitle
        {
            get { return this.subtitle; }
            set { this.SetProperty(ref this.subtitle, value); }
        }

        public string Description
        {
            get { return this.description; }
            set { this.SetProperty(ref this.description, value); }
        }

        public ImageSource Image
        {
            get
            {
                if (this.image == null && this.imagePath != null)
                {
                    this.image = new BitmapImage(new Uri(SampleDataCommon.baseUri, this.imagePath));
                }

                return this.image;
            }

            set
            {
                this.imagePath = null;
                this.SetProperty(ref this.image, value);
            }
        }

        public void SetImage(Uri path)
        {
            this.image = null;
            this.imagePath = path;
            this.OnPropertyChanged("Image");
        }

        public override string ToString()
        {
            return this.Title;
        }
    }

    /// <summary>
    /// Generic item data model.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "File is from Windows Store template")]
    public class DataItem : SampleDataCommon
    {
        private string content = string.Empty;
        private DataCollection group;
        private Type navigationPage;

        public DataItem(string uniqueId, string title, string subtitle, Uri imagePath, string description, string content, DataCollection group)
            : base(uniqueId, title, subtitle, imagePath, description)
        {
            this.content = content;
            this.group = group;
            this.navigationPage = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataItem" /> class.
        /// </summary>
        /// <param name="uniqueId">The unique id of this item.</param>
        /// <param name="title">The title of this item.</param>
        /// <param name="subtitle">The subtitle of this item.</param>
        /// <param name="imagePath">A relative path of the image for this item.</param>
        /// <param name="description">A description of this item.</param>
        /// <param name="content">The content of this item.</param>
        /// <param name="group">The group of this item.</param>
        /// <param name="navigationPage">What page should launch when clicking this item.</param>
        public DataItem(string uniqueId, string title, string subtitle, Uri imagePath, string description, string content, DataCollection group, Type navigationPage)
            : base(uniqueId, title, subtitle, imagePath, description)
        {
            this.content = content;
            this.group = group;
            this.navigationPage = navigationPage;
        }

        public string Content
        {
            get { return this.content; }
            set { this.SetProperty(ref this.content, value); }
        }

        public DataCollection Group
        {
            get { return this.group; }
            set { this.SetProperty(ref this.group, value); }
        }

        public Type NavigationPage
        {
            get { return this.navigationPage; }
            set { this.SetProperty(ref this.navigationPage, value); }
        }
    }

    /// <summary>
    /// Generic group data model.
    /// </summary>
    public class DataCollection : SampleDataCommon, IEnumerable
    {
        private ObservableCollection<DataItem> items = new ObservableCollection<DataItem>();
        private ObservableCollection<DataItem> topItem = new ObservableCollection<DataItem>();

        public DataCollection(string uniqueId, string title, string subtitle, Uri imagePath, string description)
            : base(uniqueId, title, subtitle, imagePath, description)
        {
            this.Items.CollectionChanged += this.ItemsCollectionChanged;
        }

        public ObservableCollection<DataItem> Items
        {
            get { return this.items; }
        }

        public ObservableCollection<DataItem> TopItems
        {
            get { return this.topItem; }
        }

        public IEnumerator GetEnumerator()
        {
            return this.Items.GetEnumerator();
        }

        private void ItemsCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            // Provides a subset of the full items collection to bind to from a GroupedItemsPage
            // for two reasons: GridView will not virtualize large items collections, and it
            // improves the user experience when browsing through groups with large numbers of
            // items.
            // A maximum of 12 items are displayed because it results in filled grid columns
            // whether there are 1, 2, 3, 4, or 6 rows displayed
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (e.NewStartingIndex < 12)
                    {
                        this.TopItems.Insert(e.NewStartingIndex, this.Items[e.NewStartingIndex]);
                        if (this.TopItems.Count > 12)
                        {
                            this.TopItems.RemoveAt(12);
                        }
                    }

                    break;
                case NotifyCollectionChangedAction.Move:
                    if (e.OldStartingIndex < 12 && e.NewStartingIndex < 12)
                    {
                        this.TopItems.Move(e.OldStartingIndex, e.NewStartingIndex);
                    }
                    else if (e.OldStartingIndex < 12)
                    {
                        this.TopItems.RemoveAt(e.OldStartingIndex);
                        this.TopItems.Add(Items[11]);
                    }
                    else if (e.NewStartingIndex < 12)
                    {
                        this.TopItems.Insert(e.NewStartingIndex, this.Items[e.NewStartingIndex]);
                        this.TopItems.RemoveAt(12);
                    }

                    break;
                case NotifyCollectionChangedAction.Remove:
                    if (e.OldStartingIndex < 12)
                    {
                        this.TopItems.RemoveAt(e.OldStartingIndex);
                        if (this.Items.Count >= 12)
                        {
                            this.TopItems.Add(this.Items[11]);
                        }
                    }

                    break;
                case NotifyCollectionChangedAction.Replace:
                    if (e.OldStartingIndex < 12)
                    {
                        this.TopItems[e.OldStartingIndex] = this.Items[e.OldStartingIndex];
                    }

                    break;
                case NotifyCollectionChangedAction.Reset:
                    this.TopItems.Clear();
                    while (this.TopItems.Count < this.Items.Count && this.TopItems.Count < 12)
                    {
                        this.TopItems.Add(this.Items[this.TopItems.Count]);
                    }

                    break;
            }
        }
    }
}
