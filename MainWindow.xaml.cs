using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Kinect;
using Microsoft.Kinect.Wpf.Controls;

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
using System.Windows.Media;
using System.ComponentModel;
using System.Windows.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using HtmlAgilityPack;
using Microsoft.Samples.Kinect.ControlsBasics.DataModel;

namespace Microsoft.Samples.Kinect.ControlsBasics
{
    


    /// <summary>
    /// Interaction logic for MainWindow
    /// </summary>
    public partial class MainWindow
    {
        
        private static Timer clockTimer;
        private static Timer bongoSwapTimer;
        private static Timer bongoGetTimer;
        private static Timer weatherTimer;
        private static Timer slideSwapTimer;
        private BongoData bongoData;
        private int bongoIndex = 0;
        private static Simpleforecast weatherData;
        private static List<VisibleBongoData> fullBongoData = new List<VisibleBongoData>();
        private static Timer csEventsTimer;
        public static Rootobject fullWeatherData;
        private static List<VisibleCSItem> fullCsEventsList = new List<VisibleCSItem>();
        
        private static List<VisibleCSItem> fullCsNewsList = new List<VisibleCSItem>();
        private static List<BitmapImage> slideImages = new List<BitmapImage>();

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

        private KinectSensor kinectSensor = null;
      
        /// <summary>
        /// Reader for color frames
        /// </summary>
        private ColorFrameReader colorFrameReader = null;

        /// <summary>
        /// Bitmap to display
        /// </summary>
        private WriteableBitmap colorBitmap = null;
        private int swapIndex;

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

            this.kinectSensor = KinectSensor.GetDefault();

            //// open the reader for the color frames
            this.colorFrameReader = this.kinectSensor.ColorFrameSource.OpenReader();

            // wire handler for frame arrival
            this.colorFrameReader.FrameArrived += this.Reader_ColorFrameArrived;

            // create the colorFrameDescription from the ColorFrameSource using Bgra format
            FrameDescription colorFrameDescription = this.kinectSensor.ColorFrameSource.CreateFrameDescription(ColorImageFormat.Bgra);

            // create the bitmap to display
            this.colorBitmap = new WriteableBitmap(colorFrameDescription.Width, colorFrameDescription.Height, 96.0, 96.0, PixelFormats.Bgr32, null);
            
            // open the sensor
            this.kinectSensor.Open();

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

            GetSlides();

            SetSlideSwapTimer();
        }



        ///// <summary>
        ///// Execute shutdown tasks
        ///// </summary>
        ///// <param name="sender">object sending the event</param>
        ///// <param name="e">event arguments</param>
        //private void MainWindow_Closing(object sender, CancelEventArgs e)
        //{
        //    if (this.colorFrameReader != null)
        //    {
        //        // ColorFrameReder is IDisposable
        //        this.colorFrameReader.Dispose();
        //        this.colorFrameReader = null;
        //    }

        //    if (this.kinectSensor != null)
        //    {
        //        this.kinectSensor.Close();
        //        this.kinectSensor = null;
        //    }
        //}

        /// <summary>
        /// Handles the color frame data arriving from the sensor
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void Reader_ColorFrameArrived(object sender, ColorFrameArrivedEventArgs e)
        {
            // ColorFrame is IDisposable
            using (ColorFrame colorFrame = e.FrameReference.AcquireFrame())
            {
                if (colorFrame != null)
                {
                    FrameDescription colorFrameDescription = colorFrame.FrameDescription;

                    using (KinectBuffer colorBuffer = colorFrame.LockRawImageBuffer())
                    {
                        this.colorBitmap.Lock();

                        // verify data and write the new color frame data to the display bitmap
                        if ((colorFrameDescription.Width == this.colorBitmap.PixelWidth) && (colorFrameDescription.Height == this.colorBitmap.PixelHeight))
                        {
                            colorFrame.CopyConvertedFrameDataToIntPtr(
                                this.colorBitmap.BackBuffer,
                                (uint)(colorFrameDescription.Width * colorFrameDescription.Height * 4),
                                ColorImageFormat.Bgra);

                            this.colorBitmap.AddDirtyRect(new Int32Rect(0, 0, this.colorBitmap.PixelWidth, this.colorBitmap.PixelHeight));
                            KinectStreamView.Source = this.colorBitmap;
                        }

                        this.colorBitmap.Unlock();
                    }
                }
            }
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

        /// <summary>
        /// Timer used to call an API update for CS Events
        /// </summary>
        private void SetCSEventsTimer()
        {
            // Create a timer with a 13 min interval.
            csEventsTimer = new Timer(800000);
            // Hook up the Elapsed event for the timer. 
            csEventsTimer.Elapsed += GetCSEvents;
            csEventsTimer.Elapsed += GetSlideURLs;
            csEventsTimer.AutoReset = true;
            csEventsTimer.Enabled = true;
        }

      

        private void SetSlideSwapTimer()
        {
            slideSwapTimer = new Timer(10000);
            slideSwapTimer.Elapsed += RotateSlides;
            slideSwapTimer.AutoReset = true;
            slideSwapTimer.Enabled = true;
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

            //Uri newsURI = new Uri("https://www.cs.uiowa.edu/news/rss.xml");
            //using (WebClient downloader = new WebClient())
            //{
            //    downloader.DownloadStringCompleted += new DownloadStringCompletedEventHandler(downloader_DownloadStringCompletedCSNews);
            //    downloader.DownloadStringAsync(newsURI);
            //}

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
            GetCSEvents();
            GetBusData();
            GetWeatherData();
            GetSlides();         
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
                    try
                    {
                        nodes result = (nodes)serializer.Deserialize(reader);
                    
                    foreach (var nd in result.node)
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
                    }
                    catch { }

                }
                SetCSCards();
            }
        }

        //TODO set page timer
        //TODO set Loading bars
        //TODO Make icons
        //TODO add comments
        //TODO South Side stop and north side stop
        //TODO Remove button on CLAS Logo
        //TODO switch news error
        //TODO fix clas news error
        //TODO bus page scroll
        //TODO update drupal slides feed on timer
        //Scheduler http://www.howtogeek.com/123393/how-to-automatically-run-programs-and-set-reminders-with-the-windows-task-scheduler/



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
            // Create a timer with a one second interval.
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
            // Create a timer with a 7 second interval.
            bongoSwapTimer = new Timer(7000);
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
        private void SetCSCards()
        {
            
                fullCsEventsList.Sort((x, y) => DateTime.Compare(x.startDate, y.startDate));
               
                List<VisibleCSItem> groupedCSEventData = new List<VisibleCSItem>();

                for (int i = 0; i < 4 && i < fullCsEventsList.Count; i++)
                {
                    groupedCSEventData.Add(fullCsEventsList[i]);
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
                if(groupedBongoData.Count >= 1)
                {
                    Dispatcher.Invoke(DispatcherPriority.DataBind, new Action(delegate { BusGrid.DataContext = groupedBongoData; }));
                }
                
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


        private async void GetSlides()
        {
            List<BitmapImage> imageList = await GetSlidesAsync();
            //Dispatcher.Invoke(() => {
                slideImages = imageList;
            //});
        }

        private void GetSlideURLs(object sender, ElapsedEventArgs e)
        {
            GetSlides();
        }
        async Task<List<BitmapImage>> GetSlidesAsync()
        {
            
            HttpClient client = new HttpClient();
            var doc = new HtmlDocument();
            var html = await client.GetStringAsync("https://clas.uiowa.edu/signage/computer-science");
            doc.LoadHtml(html);
            List<string> imageLinks = await Task.Run(() =>
            {
                List<string> links = new List<string>();
                try
                {
                    var rows = doc.DocumentNode.SelectNodes("//*[@id='flexslider-1']/ul/li/img");
                    foreach (var row in rows)
                    {
                        links.Add(row.Attributes["src"].Value);
                    }
                    return links;
                }
                catch { return links; }                
            });

            List<BitmapImage> images = await Task.Run(() =>
            {
                List<BitmapImage> imgs = new List<BitmapImage>();
                foreach (var link in imageLinks)
                {
                    var webClient = new WebClient();
                    try
                    {
                        var buffer = webClient.DownloadData(link);


                        var bitmapImage = new BitmapImage();

                        using (var stream = new MemoryStream(buffer))
                        {
                            bitmapImage.BeginInit();
                            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                            bitmapImage.StreamSource = stream;
                            bitmapImage.EndInit();
                            bitmapImage.Freeze();
                            imgs.Add(bitmapImage);
                        }
                    }
                    catch { }
                    
                }
                return imgs;
            });
            return images;
        }

        private void RotateSlides(object obj, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                //Swap and Fade Animation Code
                ScrollViewer slideBox = (ScrollViewer)FindName("Slides");
                // Create a duration of 2 seconds.
                Duration duration = new Duration(TimeSpan.FromSeconds(2));
                Duration duration2 = new Duration(TimeSpan.FromSeconds(2));

                // Create two DoubleAnimations and set their properties.
                DoubleAnimation myDoubleAnimation1 = new DoubleAnimation();
                DoubleAnimation myDoubleAnimation2 = new DoubleAnimation();

                myDoubleAnimation1.Duration = duration;
                myDoubleAnimation2.Duration = duration2;
                myDoubleAnimation2.BeginTime = TimeSpan.FromSeconds(0);

                Storyboard sb = new Storyboard();
                sb.Duration = duration;

                sb.Children.Add(myDoubleAnimation1);
                sb.Children.Add(myDoubleAnimation2);

                Storyboard.SetTargetName(myDoubleAnimation1, "Slides");
                Storyboard.SetTargetName(myDoubleAnimation2, "Slides");

                // Set the attached properties of Opacity
                // to be the target properties of the two respective DoubleAnimations.
                Storyboard.SetTargetProperty(myDoubleAnimation1, new PropertyPath("Opacity"));
                Storyboard.SetTargetProperty(myDoubleAnimation2, new PropertyPath("Opacity"));

                myDoubleAnimation1.To = 0;
                myDoubleAnimation2.From = 0;
                myDoubleAnimation2.To = 1;

                // Make the Storyboard a resource.
                slideBox.Resources.Add("unique_id", sb);
                // Begin the animation.
                try
                {
                    sb.Begin();
                }
                catch { };

                if (swapIndex < slideImages.Count - 1)
                {
                    Slides.Visibility = Visibility.Visible;
                    EventsSlide.Visibility = Visibility.Collapsed;
                    if (slideImages.Count > 1)
                    {
                       
                        
                        SlideImage.Source = slideImages[swapIndex]; 
                    }
                    swapIndex++;
                }
                else
                {
                    Slides.Visibility = Visibility.Collapsed;
                    EventsSlide.Visibility = Visibility.Visible;
                    swapIndex = 0;
                }

                slideBox.Resources.Remove("unique_id");
            });


        }

    }
}
