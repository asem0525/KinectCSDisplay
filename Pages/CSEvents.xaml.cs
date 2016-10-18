using Microsoft.Samples.Kinect.ControlsBasics.Helper_Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Timers;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
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
        private static List<VisibleNewsItem> apNewsList = new List<VisibleNewsItem>();
        private static List<VisibleNewsItem> cnetNewsList = new List<VisibleNewsItem>();
        private static List<VisibleNewsItem> bbcNewsList = new List<VisibleNewsItem>();
        private static List<VisibleNewsItem> clasNewsList = new List<VisibleNewsItem>();
        private Timer swapTimer;
        
        private int swapIndex;
        private enum NewsSource
        {
            ap,
            bbc,
            clas,
            cnet
        }
        
        public CSEvents()
        {
            InitializeComponent();
            GetCSEvents();
            SetSwapTimer();
            swapIndex = 0;
            
            GetNews();
        }

        private void GetNews()
        {
            //News found on AP
            Uri feedUri = new Uri(@"http://hosted.ap.org/lineups/TOPHEADS.rss?SITE=AP&SECTION=HOME");
            using (WebClient downloader = new WebClient())
            {
                downloader.DownloadStringCompleted += new DownloadStringCompletedEventHandler(downloader_DownloadStringCompletedAP);
                downloader.DownloadStringAsync(feedUri);
            }
                
            //News found on BBC
            Uri feedUri2 = new Uri("http://feeds.bbci.co.uk/news/world/rss.xml#");
            using (WebClient downloader = new WebClient())
            {
                downloader.DownloadStringCompleted += new DownloadStringCompletedEventHandler(downloader_DownloadStringCompletedBBC);
                downloader.DownloadStringAsync(feedUri2);
            }

            //News found on CNET
            Uri feedUri3 = new Uri("http://www.cnet.com/rss/news/");
            using (WebClient downloader = new WebClient())
            {
                downloader.DownloadStringCompleted += new DownloadStringCompletedEventHandler(downloader_DownloadStringCompletedCNET);
                downloader.DownloadStringAsync(feedUri3);
            }

            //News found on CLAS
            Uri feedUri4 = new Uri("https://clas.uiowa.edu/news/rss.xml");
            using (WebClient downloader = new WebClient())
            {
                downloader.DownloadStringCompleted += new DownloadStringCompletedEventHandler(downloader_DownloadStringCompletedCLAS);
                downloader.DownloadStringAsync(feedUri4);
            }          
        }

        private void downloader_DownloadStringCompletedCLAS(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                string responseStream = e.Result;
                    clasNewsList.Clear();
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(nodes));

                    using (TextReader reader = new StringReader(e.Result))
                    {
                        //reader.Namespaces = false;
                        nodes result = (nodes)serializer.Deserialize(reader);
                        foreach (var nd in result.node)
                        {
                            clasNewsList.Add(new VisibleNewsItem()
                            {
                                NewsTitle = nd.title == null ? "" : Encoding.UTF8.GetString(Encoding.Default.GetBytes(nd.title)),
                            });
                        }
                    }
                }
                catch { }
                finally
                {
                    if (clasNewsList.Count < 1)
                    {
                        clasNewsList.Add(new VisibleNewsItem() { NewsTitle = "No CLAS news at this time" });
                    }
                }      
            }
        }

        private void downloader_DownloadStringCompletedCNET(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                string responseStream = e.Result;
                cnetNewsList.Clear();
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(CNETNews.rss));

                    using (TextReader reader = new StringReader(e.Result))
                    {
                        //reader.Namespaces = false;
                        CNETNews.rss result = (CNETNews.rss)serializer.Deserialize(reader);
                        foreach (var nd in result.channel.item)
                        {
                            
                            cnetNewsList.Add(new VisibleNewsItem()
                            {
                                NewsTitle = nd.title == null ? "" : Encoding.UTF8.GetString(Encoding.Default.GetBytes(nd.title)),
                                NewsDescription = nd.description == null ? "" : Encoding.UTF8.GetString(Encoding.Default.GetBytes(nd.description)),
                                NewsPublicationDate = nd.pubDate
                            });                            
                        }
                    }
                }
                catch { }
                finally
                {
                    if (cnetNewsList.Count < 1)
                    {
                        cnetNewsList.Add(new VisibleNewsItem() { NewsTitle = "No CNET news at this time" });
                    }
                }

            }
        }

        private void downloader_DownloadStringCompletedBBC(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                string responseStream = e.Result;
                bbcNewsList.Clear();
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(BBCNews.rss));

                    using (TextReader reader = new StringReader(e.Result))
                    {
                        //reader.Namespaces = false;
                        BBCNews.rss result = (BBCNews.rss)serializer.Deserialize(reader);
                        foreach (var nd in result.channel.item)
                        {
                            try
                            {
                                bbcNewsList.Add(new VisibleNewsItem()
                                {
                                    NewsTitle = nd.title == null ? "" : Encoding.UTF8.GetString(Encoding.Default.GetBytes(nd.title)),
                                    NewsDescription = nd.description == null ? "" : Encoding.UTF8.GetString(Encoding.Default.GetBytes(nd.description)),
                                    NewsPublicationDate = nd.pubDate
                                });
                            }
                            catch { }
                            finally
                            {
                                if (bbcNewsList.Count < 1)
                                {
                                    bbcNewsList.Add(new VisibleNewsItem() { NewsTitle = "No BBC news at this time" });
                                }
                            }
                        }
                    }
                }
                catch { }
            }
        }

        private void downloader_DownloadStringCompletedAP(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                string responseStream = e.Result;
                apNewsList.Clear();
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(APNews.rss));
                    using (TextReader reader = new StringReader(e.Result))
                    {
                        APNews.rss result = (APNews.rss)serializer.Deserialize(reader);
                        foreach (var nd in result.channel.item)
                        {
                            apNewsList.Add(new VisibleNewsItem()
                            {
                                NewsTitle = nd.title == null ? "" : Encoding.UTF8.GetString(Encoding.Default.GetBytes(nd.title)),
                                NewsDescription = nd.description == null ? "" : Encoding.UTF8.GetString(Encoding.Default.GetBytes(nd.description)).Replace("&amp;", "&").Replace("&lt;", "<").Replace("&gt;", ">").Replace("&quot;", "\"").Replace("&apos;", "'"),
                            });
                        }
                    }
                }
                catch { }
                finally
                {
                    if (apNewsList.Count < 1)
                    {
                        apNewsList.Add(new VisibleNewsItem() { NewsTitle = "No CLAS news at this time" });
                    }
                    SetGenNewsListView(NewsSource.ap, "AP Top News");
                }

            }
        }


        private void SetGenNewsListView(NewsSource news, string sourceTitle)
        {
            switch (news)
            {
                case NewsSource.ap:
                    genNewsList.ItemsSource = apNewsList;
                    break;
                case NewsSource.bbc:
                    genNewsList.ItemsSource = bbcNewsList;
                    break;

                case NewsSource.clas:
                    genNewsList.ItemsSource = clasNewsList;
                    break;

                case NewsSource.cnet:
                    genNewsList.ItemsSource = cnetNewsList;
                    break;
            }
            generalNewsTitle.Text = "Today's Headlines: " + sourceTitle;
                        
        }
        
        private void SetSwapTimer()
        {
            // Create a timer with a two second interval.
            swapTimer = new Timer(24000);
            // Hook up the Elapsed event for the timer. 
            swapTimer.Elapsed += SetCSCards;
            
            swapTimer.AutoReset = true;
            swapTimer.Enabled = true;
        }

        private void SetCSCards(object sender, ElapsedEventArgs e)
        {
            SetCSNewsCard();
        }

        private void SetCSNewsCard()
        {
            //Swap and Fade Animation Code
            Dispatcher.Invoke(() =>
            {
                StackPanel csCard = (StackPanel)FindName("csCard");
                // Create a duration of 2 seconds.
                Duration duration = new Duration(TimeSpan.FromSeconds(4));
                Duration duration2 = new Duration(TimeSpan.FromSeconds(4));

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

                Storyboard.SetTargetName(myDoubleAnimation1, "csCard");
                Storyboard.SetTargetName(myDoubleAnimation2, "csCard");

                // Set the attached properties of Opacity
                // to be the target properties of the two respective DoubleAnimations.
                Storyboard.SetTargetProperty(myDoubleAnimation1, new PropertyPath("Opacity"));
                Storyboard.SetTargetProperty(myDoubleAnimation2, new PropertyPath("Opacity"));

                myDoubleAnimation1.To = 0;
                myDoubleAnimation2.From = 0;
                myDoubleAnimation2.To = 1;

                // Make the Storyboard a resource.
                csCard.Resources.Add("unique_id_news_page", sb);
                // Begin the animation.
                sb.Begin();

                if (swapIndex < fullCsNewsList.Count - 1)
                {
                    swapIndex++;
                }
                else
                {
                    swapIndex = 0;
                }
                if(fullCsNewsList.Count > 1)
                {
                    csNewsTitle.Text = fullCsNewsList[swapIndex].csEventTitle;
                    csNewsLocation.Text = fullCsNewsList[swapIndex].csEventLocation;
                    csNewsTime.Text = fullCsNewsList[swapIndex].csEventTime;
                }
                csCard.Resources.Remove("unique_id_news_page");
            });
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

        /// <summary>
        /// Reads CS events async and then sets the datagrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    finally
                    {
                        if (fullCsEventsList.Count < 1)
                        {
                            fullCsEventsList.Add(new VisibleCSItem() { csEventTitle = "No CS Events at this time" });
                        }
                    }
                }              
            }
            fullCsEventsList.Sort((x, y) => DateTime.Compare(x.startDate, y.startDate));

            //Sets the data grid
            Dispatcher.Invoke(() =>
            {
                EventsDataGrid.DataContext = fullCsEventsList;
            });

        }

        /// <summary>
        /// Reads the CS News, sets the card, and fades the animations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void downloader_DownloadStringCompletedCSNews(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                string responseStream = e.Result;
                XmlSerializer serializer = new XmlSerializer(typeof(nodes));

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

                        fullCsNewsList.Add(new VisibleCSItem()
                        {
                            csEventLocation = nd.location == null ? "" : Encoding.UTF8.GetString(Encoding.Default.GetBytes(nd.location)),
                            csEventTime = startDate.Date.ToString("MMMM d, yyyy"),
                            csEventTitle = nd.title == null ? "" : Encoding.UTF8.GetString(Encoding.Default.GetBytes(nd.title)),
                            startDate = startDate,
                            isEvent = false
                        });
                    }
                    }
                    catch { }
                    SetInitalCSCards();
                }
            }
        }

        private void SetInitalCSCards()
        {
            //Set the inital card value
            Dispatcher.Invoke(() =>
            {
                if (fullCsNewsList.Count > 0)
                {
                    csNewsTitle.Text = fullCsNewsList[0].csEventTitle;
                    csNewsLocation.Text = fullCsNewsList[0].csEventLocation;
                    csNewsTime.Text = fullCsNewsList[0].csEventTime;
                }
            });
        }
        
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            NewsSource source = new NewsSource();
            RadioButton rb = (RadioButton)sender;
            if (((string)rb.Content).Equals("AP Top News"))
            {
                source = NewsSource.ap;
            } else if(((string)rb.Content).Equals("BBC World News"))
            {
                source = NewsSource.bbc;
            } else if(((string)rb.Content).Equals("CNET Tech News"))
            {
                source = NewsSource.cnet;
            } else if(((string)rb.Content).Equals("CLAS News"))
            {
                source = NewsSource.clas;
            }
            SetGenNewsListView(source, (string)rb.Content);           
        }
    }
}
