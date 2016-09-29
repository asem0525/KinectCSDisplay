﻿
namespace Microsoft.Samples.Kinect.ControlsBasics
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using Microsoft.Kinect;
    using Microsoft.Kinect.Wpf.Controls;
    using DataModel;
    using System.Timers;

    using System.Diagnostics;

    using System.Windows.Media.Animation;

    using System.Net;
    using System.IO;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using System.Windows.Media.Imaging;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Windows.Automation.Peers;
    using System.Linq;
    using System.Text;
    using System.Windows.Navigation;


    /// <summary>
    /// Interaction logic for MainWindow
    /// </summary>
    public partial class MainWindow
    {
        
        private static Timer clockTimer;
        private static Timer bongoSwapTimer;
        private static Timer bongoGetTimer;
        private static Timer weatherTimer;
        private BongoData bongoData;
        private int bongoIndex = 0;
        private static Simpleforecast weatherData;
        private static List<VisibleBongoData> fullBongoData = new List<VisibleBongoData>();
        private static Timer csEventsTimer;
        public static Rootobject fullWeatherData;
        private static List<VisibleCSItem> fullCsEventsList = new List<VisibleCSItem>();
        
        private static List<VisibleCSItem> fullCsNewsList = new List<VisibleCSItem>();
        private bool task1;
        private bool task2;
        private Timer waitTimer;
        private NavigationService navService;

        internal static List<VisibleBongoData> FullBongoData
        {
            get
            {
                return fullBongoData;
            }

            set
            {
                fullBongoData = value;
            }
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class. 
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();

            navService = NavigationService.GetNavigationService(this);

            KinectRegion.SetKinectRegion(this, kinectRegion);

            App app = ((App)Application.Current);
            app.KinectRegion = kinectRegion;

            // Use the default sensor
            this.kinectRegion.KinectSensor = KinectSensor.GetDefault();

            //// Add in display content
            var sampleDataSource = DataSource.GetGroup("Group-1");
            itemsControl.ItemsSource = sampleDataSource;

            //Starts timer for clock
            SetClockTimer();

            //Set bongo timers
            SetBongoGetTimer();
            

            SetSwapTimer();

            //Sets the scrolling bottom bar text event
            StartBottomBar();

            //Sets timers and weather data            
            SetWeatherTimer();
            SetCSEventsTimer();

            GetInitalApiData();
        }

        /// <summary>
        /// Timer used to call an API update for Weather Data
        /// </summary>
        private void SetWeatherTimer()
        {
            // Create a timer with a 10 min interval.
            weatherTimer = new Timer(600000);
            // Hook up the Elapsed event for the timer. 
            weatherTimer.Elapsed += GetWeatherData;
            weatherTimer.AutoReset = true;
            weatherTimer.Enabled = true;
        }

        private void SetWaitTimer()
        {
            task1 = false;
            task2 = false;
            // Create a timer with a two second interval.
            waitTimer = new Timer(100);
            // Hook up the Elapsed event for the timer. 
            waitTimer.Elapsed += SetCSCards;
            waitTimer.AutoReset = true;
            waitTimer.Enabled = true;
        }

        /// <summary>
        /// Timer used to call an API update for CS Events
        /// </summary>
        private void SetCSEventsTimer()
        {
            // Create a timer with a 13 min interval.
            csEventsTimer = new Timer(800000);
            // Hook up the Elapsed event for the timer. 
            csEventsTimer.Elapsed += GetCSEvents;
            csEventsTimer.AutoReset = true;
            csEventsTimer.Enabled = true;
        }

        /// <summary>
        /// Call CS website for events in XML format
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void GetCSEvents(object obj, ElapsedEventArgs e)
        {
            GetCSEvents();
        }

        private void GetCSEvents()
        {
            //CS Events found on Iowa CS Website
            Uri feedUri = new Uri("https://www.cs.uiowa.edu/events/rss.xml");
            using (WebClient downloader = new WebClient())
            {
                downloader.DownloadStringCompleted += new DownloadStringCompletedEventHandler(downloader_DownloadStringCompletedCSEvents);
                downloader.DownloadStringAsync(feedUri);
            }

            Uri newsURI = new Uri("https://www.cs.uiowa.edu/news/rss.xml");
            using (WebClient downloader = new WebClient())
            {
                downloader.DownloadStringCompleted += new DownloadStringCompletedEventHandler(downloader_DownloadStringCompletedCSNews);
                downloader.DownloadStringAsync(newsURI);
            }

            SetWaitTimer();
        }

        /// <summary>
        /// Event on a timer for getting the bus data for stop 00001, outside of MacLean Hall.
        /// This method reads in JSON data using async methods.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void GetBusData(object source, ElapsedEventArgs e)
        {
            GetBusData();
        }

        private void GetBusData()
        {
            //Prediction URI from Bongo API for stop 0001 Downtown Interchange
            Uri feedUri = new Uri("http://api.ebongo.org/prediction?format=json&stopid=0001&api_key=XXXX");
            using (WebClient downloader = new WebClient())
            {
                downloader.DownloadStringCompleted += new DownloadStringCompletedEventHandler(downloader_DownloadStringCompletedBongo);
                downloader.DownloadStringAsync(feedUri);
            }
        }


        /// <summary>
        /// Gets weather in JSON form from WeatherUnderground
        /// Calls convsersion to WeatherData objects.
        /// Only used on timer call
        /// </summary>
        private void GetWeatherData(object source, ElapsedEventArgs e)
        {
            GetWeatherData();
        }

        private void GetWeatherData()
        {
            //Weather Forcast from Weather Underground.com
            Uri feedUri = new Uri("http://api.wunderground.com/api/75c131024c99cf58/forecast10day/q/IA/Iowa_City.json");
            using (WebClient downloader = new WebClient())
            {
                downloader.DownloadStringCompleted += new DownloadStringCompletedEventHandler(downloader_DownloadStringCompletedWeather);
                downloader.DownloadStringAsync(feedUri);
            }
        }

        /// <summary>
        /// Gets weather in JSON form from WeatherUnderground
        /// Calls convsersion to WeatherData objects.
        /// Only used on initalize
        /// </summary>
        private void GetInitalApiData()
        {
            task1 = false;
            task2 = false;
            SetWaitTimer();
            GetCSEvents();
            GetBusData();
            GetWeatherData();           
        }
     
        /// <summary>
        /// Gets the Weather forcast data (Json form) and creates WeatherData objects
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void downloader_DownloadStringCompletedWeather(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                string responseStream = e.Result;
                try
                {
                    Rootobject root = JsonConvert.DeserializeObject<Rootobject>(responseStream);
                    fullWeatherData = root;
                    weatherData = root.forecast.simpleforecast;
                    SetWeatherData();
                }
                catch { }                
            }
        }

        
        private void downloader_DownloadStringCompletedCSEvents(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                string responseStream = e.Result;
                
                XmlSerializer serializer = new XmlSerializer(typeof(nodes));
                fullCsEventsList.Clear();
                using (TextReader reader = new StringReader(e.Result))
                {
                    nodes result = (nodes)serializer.Deserialize(reader);

                    foreach (var nd in result.node)
                    {
                        try
                        {
                            string date = nd.startdate;
                            Regex reg = new Regex("\\(All\\sday\\)");
                            if (reg.IsMatch(nd.startdate))
                            {
                                date = nd.startdate.Substring(0, reg.Match(nd.startdate).Index);
                            }
                            DateTime startDate = DateTime.Parse(date);

                            fullCsEventsList.Add(new VisibleCSItem()
                            {
                                csEventLocation = nd.location == null ? "" : Encoding.UTF8.GetString(Encoding.Default.GetBytes(nd.location)),
                                csEventTime = startDate.Date.ToString("MMMM d, yyyy"),
                                csEventTitle = nd.title == null ? "" : Encoding.UTF8.GetString(Encoding.Default.GetBytes(nd.title)),
                                startDate = startDate,
                                isEvent = true
                            });
                        }
                        catch { }
                    }
                    task1 = true;
                }
            }
        }

        private void downloader_DownloadStringCompletedCSNews(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                string responseStream = e.Result;
                XmlSerializer serializer = new XmlSerializer(typeof(nodes));
                
                using (TextReader reader = new StringReader(e.Result))
                {
                    nodes result = (nodes)serializer.Deserialize(reader);

                    foreach (var nd in result.node)
                    {
                        try
                        {
                            string date = nd.startdate;
                            Regex reg = new Regex("\\(All\\sday\\)");
                            if (reg.IsMatch(nd.startdate))
                            {
                                date = nd.startdate.Substring(0, reg.Match(nd.startdate).Index);
                            }
                            DateTime startDate = DateTime.Parse(date);

                            fullCsNewsList.Add(new VisibleCSItem()
                            {
                                csEventLocation = nd.location == null ? "" : Encoding.UTF8.GetString(Encoding.Default.GetBytes(nd.location)),
                                csEventTime = startDate.Date.ToString("MMMM d, yyyy"),
                                csEventTitle = nd.title == null ? "" : Encoding.UTF8.GetString(Encoding.Default.GetBytes(nd.title)),
                                startDate = startDate,
                                isEvent = false
                            });
                        }
                        catch { }
                    }
                    task2 = true;
                }
            }
        }
        //TODO set page timer
        //TODO set Loading bars
        //TODO make kinect viewing camera better
        //TODO Make icons
        //TODO add comments
        //TODO Make people page async
        //TODO South Side stop and north side stop
        //TODO Remove button on CLAS Logo
        //TODO smoothen animations
        //TODO switch news error
        

        /// <summary>
        /// Sets the weather data with the icons on the mainwindow
        /// </summary>
        private void SetWeatherData()
        {
            if (weatherData != null)
            {
                int i = 0;
                string hostIconURL = "Images/WeatherIcons/";
                if(DateTime.Now.Hour >= 18 || DateTime.Now.Hour <= 4)
                {
                    hostIconURL = hostIconURL + "nt_";
                }
                while (i <= 3 && i < weatherData.forecastday.Length)
                {
                    if (i == 0)
                    {
                        Temp1.Text = weatherData.forecastday[0].high.fahrenheit + "°/" + weatherData.forecastday[0].low.fahrenheit + "°";
                        weatherIcon1.Source = new BitmapImage(new Uri(hostIconURL + weatherData.forecastday[0].icon + ".png", UriKind.Relative));
                    }
                    else if(i == 1)
                    {
                        day2.Text = weatherData.forecastday[1].date.weekday;
                        Temp2.Text = weatherData.forecastday[1].high.fahrenheit + "°/" + weatherData.forecastday[1].low.fahrenheit + "°";
                        weatherIcon2.Source = new BitmapImage(new Uri(hostIconURL + weatherData.forecastday[1].icon + ".png", UriKind.Relative));
                    }
                    else if (i == 2)
                    {
                        day3.Text = weatherData.forecastday[2].date.weekday;
                        Temp3.Text = weatherData.forecastday[2].high.fahrenheit + "°/" + weatherData.forecastday[2].low.fahrenheit + "°";
                        weatherIcon3.Source = new BitmapImage(new Uri(hostIconURL + weatherData.forecastday[2].icon + ".png", UriKind.Relative));
                    }
                    else if (i == 3)
                    {
                        day4.Text = weatherData.forecastday[3].date.weekday;
                        Temp4.Text = weatherData.forecastday[3].high.fahrenheit + "°/" + weatherData.forecastday[3].low.fahrenheit + "°";
                        weatherIcon4.Source = new BitmapImage(new Uri(hostIconURL + weatherData.forecastday[3].icon + ".png", UriKind.Relative));
                    }
                    i++;
                }
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
        /// Creates a time that runs the clock and date for the UI.
        /// </summary>
        private void SetClockTimer()
        {
            // Create a timer with a two second interval.
            clockTimer = new Timer(1000);
            // Hook up the Elapsed event for the timer. 
            clockTimer.Elapsed += SetClockTimeEvent;
            clockTimer.AutoReset = true;
            clockTimer.Enabled = true;
        }

        /// <summary>
        /// Sets the digital clock and date.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void SetClockTimeEvent(Object source, ElapsedEventArgs e)
        {
            try
            {
                Dispatcher.Invoke(() =>
                {
                    dateText.Text = DateTime.Now.Date.ToString("ddd, MMM dd");
                    clockText.Text = DateTime.Now.ToString("h:mm:ss tt");
                });
            }
            catch { }
        }

        /// <summary>
        /// Timer for swaping out the Bongo info cards.
        /// </summary>
        private void SetSwapTimer()
        {
            // Create a timer with a two second interval.
            bongoSwapTimer = new Timer(5000);
            // Hook up the Elapsed event for the timer. 
            bongoSwapTimer.Elapsed += SetBongoSwapEvent;
            bongoSwapTimer.AutoReset = true;
            bongoSwapTimer.Enabled = true;
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
        /// Sets the full list of Bongo data for ListView Data Binding
        /// </summary>
        private void SetBongoData()
        {
            if (bongoData != null)
            {
                FullBongoData.Clear();           
                foreach (var bd in bongoData.predictions)
                {
                    string minString = bd.minutes.ToString() + "min.";

                    if(bd.minutes == 0)
                    {
                        minString = "Arriving";
                    }
                    
                    string colorString = "#FFFFFF";
                    if (bd.agency.Equals("cambus"))
                    {
                        colorString = "#FFEB3B";
                    }
                    else if(bd.agency.Equals("iowa-city"))
                    {
                        colorString = "indianred";
                    }
                    else if (bd.agency.Equals("coralville"))
                    {
                        colorString = "royalblue";
                    }

                    if(bd.minutes <= 15 || bongoData.predictions.Count <= 8)
                    {
                        FullBongoData.Add(new VisibleBongoData() { stopname = bd.stopname, minutes = minString, routename = bd.title, color = colorString });
                    }
                }
            }
            else
            {
                FullBongoData.Add(new VisibleBongoData() { stopname = "No buses running", color="#FFFFFF"});
            }
        }

        /// <summary>
        /// Swaps out cards with cs event data on MainWindow
        /// </summary>
        private void SetCSCards(object o, ElapsedEventArgs e)
        {
            if(task1 && task2)
            {
                fullCsEventsList.Sort((x, y) => DateTime.Compare(x.startDate, y.startDate));
                List<VisibleCSItem> comboList = fullCsEventsList.Concat(fullCsNewsList).ToList();
                List<VisibleCSItem> groupedCSEventData = new List<VisibleCSItem>();

                for (int i = 0; i < 4 && i < comboList.Count; i++)
                {
                    groupedCSEventData.Add(comboList[i]);
                }

                try
                {
                    //Udpdate UI thread with new event group of 4
                    Dispatcher.Invoke(() =>
                    {
                        if (groupedCSEventData.Count >= 1)
                        {
                            csEventsList.ItemsSource = groupedCSEventData;
                        }
                    });
                }
                catch { }
                waitTimer.AutoReset = false;
                waitTimer.Enabled = false;
                waitTimer.Dispose();
            } 
            
        }



        /// <summary>
        /// Swaps out cards with Bongo data on MainWindow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetBongoSwapEvent(object sender, ElapsedEventArgs e)
        {
            List<VisibleBongoData> groupedBongoData = new List<VisibleBongoData>();
            if(bongoIndex < FullBongoData.Count)
            {
                int i = 0;
                while (i + bongoIndex < FullBongoData.Count && i < 4)
                {
                    groupedBongoData.Add(FullBongoData[i + bongoIndex]);
                    i++;
                }
                bongoIndex += 3;
            }
            else
            {
                bongoIndex = 0;
            }
            try
            {
                //Udpdate UI thread with new bus group of 4
                Dispatcher.Invoke(() =>
                {
                    if (groupedBongoData.Count >= 1)
                    {
                        BusGrid.DataContext = groupedBongoData;
                    }
                    //else if(groupedBongoData.Count == 0)
                    //{
                    //    List<VisibleBongoData> l = new List<VisibleBongoData>();
                    //    l.Add(new VisibleBongoData() { stopname = "No buses running at this time" });
                    //    BusGrid.DataContext = l;
                    //}
                });
            }
            catch { };
        }


        /// <summary>
        /// Handle a button click from the wrap panel.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
        private void OpenPage(object sender, RoutedEventArgs e)
        {            
            var button = (Button)e.OriginalSource;
            DataItem sampleDataItem = button.DataContext as DataItem;

            if (sampleDataItem != null && sampleDataItem.NavigationPage != null)
            {
                backButton.Visibility = Visibility.Visible;
                navigationRegion.Content = Activator.CreateInstance(sampleDataItem.NavigationPage);
            }
            else
            {
                var selectionDisplay = new SelectionDisplay(button.Content as string);
                kinectRegionGrid.Children.Add(selectionDisplay);

                // Selection dialog covers the entire interact-able area, so the current press interaction
                // should be completed. Otherwise hover capture will allow the button to be clicked again within
                // the same interaction (even whilst no longer visible).
                selectionDisplay.Focus();

                // Since the selection dialog covers the entire interact-able area, we should also complete
                // the current interaction of all other pointers.  This prevents other users interacting with elements
                // that are no longer visible.
                this.kinectRegion.InputPointerManager.CompleteGestures();

                e.Handled = true;
            }
        }

        /// <summary>
        /// Handle the back button click.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
        private void GoBack(object sender, RoutedEventArgs e)
        {
            backButton.Visibility = Visibility.Hidden;
            navigationRegion.Content = this.kinectRegionGrid;
        }

        /// <summary>
        /// Starts infinite scrolling bottom bar that contains quick help instructions.
        /// </summary>
        private void StartBottomBar()
        {
            Storyboard s = (Storyboard) bottomBar.TryFindResource("sb");
            s.Begin();	// Start animation
        }

        private void DateTimeClock_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            backButton.Visibility = Visibility.Visible;
            navigationRegion.Content = Activator.CreateInstance(typeof(KinectPointerPointSample));
        }
    }
}
