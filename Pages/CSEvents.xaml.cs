using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Timers;
using System.Windows.Controls;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Samples.Kinect.ControlsBasics.Pages
{
    /// <summary>
    /// Interaction logic for CSEvents.xaml
    /// </summary>
    public partial class CSEvents : UserControl
    {
        private static List<VisibleCSItem> fullCsEventsList = new List<VisibleCSItem>();
        private static List<VisibleCSItem> fullCsNewsList = new List<VisibleCSItem>();
        private static List<VisibleCSItem> fullList = new List<VisibleCSItem>();
        private bool task2;
        private bool task1;
        private Timer waitTimer;
        

        public CSEvents()
        {
            InitializeComponent();
            task1 = false;
            task2 = false;
            GetCSEvents();
            SetWaitTimer();         
        }

        /// <summary>
        /// Call CS website for events in XML format
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
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
        }

        private void SetWaitTimer()
        {
            // Create a timer with a two second interval.
            waitTimer = new Timer(100);
            // Hook up the Elapsed event for the timer. 
            waitTimer.Elapsed += SetCardsEvent;
            waitTimer.AutoReset = true;
            waitTimer.Enabled = true;
        }

        private void SetCardsEvent(object sender, ElapsedEventArgs e)
        {
            if(task1 && task2)
            {
                fullCsEventsList.Sort((x, y) => DateTime.Compare(x.startDate, y.startDate));
                List<VisibleCSItem> comboList = fullCsEventsList.Concat(fullCsNewsList).ToList();
                Dispatcher.Invoke(() =>
                {
                    CSItemsList.ItemsSource = comboList;   
                });
                waitTimer.AutoReset = false;
                waitTimer.Enabled = false;
                waitTimer.Dispose();
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
                }              
            }
            task1 = true;        
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
                }
            }
            task2 = true;
        }
    }
}
