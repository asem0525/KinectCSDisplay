using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Timers;
using System.Windows;
using System.Windows.Controls;



namespace Microsoft.Samples.Kinect.ControlsBasics.Pages
{
    /// <summary>
    /// Interaction logic for BongoPage.xaml
    /// </summary>
    public partial class BongoPage : UserControl
    {
           private Dictionary<string, string> bongoStops;
        private Timer bongoGetTimer;
        private static BongoData bongoData;
        private Dictionary<string, string> busStopNames;

        public string stopCode { get; private set; }
        public string stopName { get; private set; }

        public BongoPage()
        {
            InitializeComponent();
            
            InitializeBongoHash();
            stopName = "Downtown Interchange";
            stopCode = "0001";

            GetBusData();
            SetBongoGetTimer();
        }

        /// <summary>
        /// Timer for downloading Bongo Data
        /// </summary>
        private void SetBongoGetTimer()
        {
            // Create a timer with a 1 min interval.
            bongoGetTimer = new Timer(60000);
            // Hook up the Elapsed event for the timer. 
            bongoGetTimer.Elapsed += GetBusData;
            bongoGetTimer.AutoReset = true;
            bongoGetTimer.Enabled = true;
        }

        /// <summary>
        /// Event on a timer for getting the bus data for the given stop, outside of MacLean Hall.
        /// This method reads in JSON data using async methods.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void GetBusData(object source, ElapsedEventArgs e)
        {
            
            //Prediction URI from Bongo API for stop id
            Uri feedUri = new Uri(@"http://api.ebongo.org/prediction?format=json&stopid=" + stopCode + "&api_key=XXXX");
            using (WebClient downloader = new WebClient())
            {
                downloader.DownloadStringCompleted += new DownloadStringCompletedEventHandler(downloader_DownloadStringCompletedBongo);
                downloader.DownloadStringAsync(feedUri);
            }
        }

        /// <summary>
        /// Gets the bus data from the Bongo API passing in the stop code
        /// </summary>
        private void GetBusData()
        {

            //Prediction URI from Bongo API for stop id
            Uri feedUri = new Uri(@"http://api.ebongo.org/prediction?format=json&stopid=" + stopCode + "&api_key=XXXX");
            using (WebClient downloader = new WebClient())
            {
                downloader.DownloadStringCompleted += new DownloadStringCompletedEventHandler(downloader_DownloadStringCompletedBongo);
                downloader.DownloadStringAsync(feedUri);
            }
        }

        /// <summary>
        /// Gets the Bongo data (Json form) and creates BongoData objects
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void downloader_DownloadStringCompletedBongo(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                string responseStream = e.Result;
               
                bongoData = JsonConvert.DeserializeObject<BongoData>(responseStream);
            }
            SetBongoData();
        }

        /// <summary>
        /// Initializes Dictionary for Stop to code conversion.
        /// </summary>
        private void InitializeBongoHash()
        {
            bongoStops = new Dictionary<string, string>()
            {
                { "Dwtn. Interchange (Schaeffer Hall)", "0001" },
                { "IC Dwtn. Interchange (Mall Side)", "0002" },
                { "MacBride Hall", "0120" },
            };
            busStopNames = new Dictionary<string, string>()
            {
                { "Dwtn. Interchange (Schaeffer Hall)", "Downtown Interchange" },
                { "IC Dwtn. Interchange (Mall Side)", "Iowa City Downtown Interchange" },
                { "MacBride Hall" , "MacBride Hall"}
            };
            
        }

        /// <summary>
        /// Parses and sets the datagrid for Bongo Data
        /// </summary>
        private void SetBongoData()
        {
            List<VisibleBongoData> currentBongoData = new List<VisibleBongoData>();
            
                currentBongoData.Clear();
                if (bongoData != null)
                {
                    foreach (var bd in bongoData.predictions)
                    {
                        string minString = bd.minutes.ToString() + "min.";

                        if (bd.minutes == 0)
                        {
                            minString = "Arriving";
                        }

                        string colorString = "#FFFFFF";
                        if (bd.agency.Equals("cambus"))
                        {
                            colorString = "#FFEB3B";
                        }
                        else if (bd.agency.Equals("iowa-city"))
                        {
                            colorString = "indianred";
                        }
                        else if (bd.agency.Equals("coralville"))
                        {
                            colorString = "royalblue";
                        }
                        currentBongoData.Add(new VisibleBongoData() { stopname = bd.stopname, minutes = minString, routename = bd.title, color = colorString });
                    }         
            }
            else
            {
                currentBongoData.Add(new VisibleBongoData() { stopname = "No busses running at this time" });
            }
            
            try
            {
                Dispatcher.Invoke(() =>
                {
                    BusTimesText.Text = "Bus Times: " + stopName;
                    BusGrid.DataContext = currentBongoData;
                });
            }
            catch { };
        }

        /// <summary>
        /// Radio button event handlers that sets stopName and stopCode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            stopName = busStopNames[(string)rb.Content];
            stopCode = bongoStops[(string)rb.Content];
            GetBusData();
        }
    }

   
}
