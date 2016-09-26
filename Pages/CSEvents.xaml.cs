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
        private static List<VisibleNewsItem> fullNewsList = new List<VisibleNewsItem>();
        private Timer swapTimer;
        private int swapIndex;
        private enum newsSource
        {
            gn,
            bbc,
            clas,
            cnet
        }
        private newsSource source;

        public CSEvents()
        {
            InitializeComponent();
            GetCSEvents();
            SetSwapTimer();
            swapIndex = 0;
            source = newsSource.gn;            
        }

        private void GetNews()
        {
            switch (source)
            {
                case newsSource.gn:
                    //News found on Google News
                    Uri feedUri = new Uri("https://news.google.com/news?output=rss&num=10");
                    using (WebClient downloader = new WebClient())
                    {
                        downloader.DownloadStringCompleted += new DownloadStringCompletedEventHandler(downloader_DownloadStringCompletedGN);
                        downloader.DownloadStringAsync(feedUri);
                    }
                    break;
                case newsSource.bbc:
                    //News found on BBC
                    Uri feedUri2 = new Uri("http://feeds.bbci.co.uk/news/world/rss.xml#");
                    using (WebClient downloader = new WebClient())
                    {
                        downloader.DownloadStringCompleted += new DownloadStringCompletedEventHandler(downloader_DownloadStringCompletedBBC);
                        downloader.DownloadStringAsync(feedUri2);
                    }
                    break;
                case newsSource.cnet:
                    //News found on CNET
                    Uri feedUri3 = new Uri("http://www.cnet.com/rss/news/");
                    using (WebClient downloader = new WebClient())
                    {
                        downloader.DownloadStringCompleted += new DownloadStringCompletedEventHandler(downloader_DownloadStringCompletedCNET);
                        downloader.DownloadStringAsync(feedUri3);
                    }
                    break;
                case newsSource.clas:
                    //News found on CLAS
                    Uri feedUri4 = new Uri("https://clas.uiowa.edu/news/rss.xml");
                    using (WebClient downloader = new WebClient())
                    {
                        downloader.DownloadStringCompleted += new DownloadStringCompletedEventHandler(downloader_DownloadStringCompletedCLAS);
                        downloader.DownloadStringAsync(feedUri4);
                    }
                    break;
            }
        }

        private void downloader_DownloadStringCompletedCLAS(object sender, DownloadStringCompletedEventArgs e)
        {
            //if (e.Error == null)
            //{
            //    string responseStream = e.Result;

            //    XmlSerializer serializer = new XmlSerializer(typeof(nodes));

            //    fullCsEventsList.Clear();
            //    using (TextReader reader = new StringReader(e.Result))
            //    {
            //        nodes result = (nodes)serializer.Deserialize(reader);
            //        foreach (var nd in result.node)
            //        {
            //            try
            //            {
            //                string date = nd.startdate;
            //                Regex reg = new Regex("\\(All\\sday\\)");
            //                if (reg.IsMatch(nd.startdate))
            //                {
            //                    date = nd.startdate.Substring(0, reg.Match(nd.startdate).Index);
            //                }
            //                DateTime startDate = DateTime.Parse(date);

            //                fullCsEventsList.Add(new VisibleCSItem()
            //                {
            //                    csEventLocation = nd.location == null ? "" : Encoding.UTF8.GetString(Encoding.Default.GetBytes(nd.location)),
            //                    csEventTime = startDate.Date.ToString("MMMM d, yyyy"),
            //                    csEventTitle = nd.title == null ? "" : Encoding.UTF8.GetString(Encoding.Default.GetBytes(nd.title)),
            //                    startDate = startDate,
            //                    isEvent = true
            //                });
            //            }
            //            catch { }
            //        }
            //    }
            //}
            //fullCsEventsList.Sort((x, y) => DateTime.Compare(x.startDate, y.startDate));

            ////Sets the data grid
            //Dispatcher.Invoke(() =>
            //{
            //    EventsDataGrid.DataContext = fullCsEventsList;
            //});
        }

        private void downloader_DownloadStringCompletedCNET(object sender, DownloadStringCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void downloader_DownloadStringCompletedBBC(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                string responseStream = e.Result;

                XmlSerializer serializer = new XmlSerializer(typeof(BBCNews.rss));

                fullNewsList.Clear();
                using (TextReader reader = new StringReader(e.Result))
                {
                    //reader.Namespaces = false;
                    BBCNews.rss result = (BBCNews.rss)serializer.Deserialize(reader);
                    foreach (var nd in result.channel.item)
                    {
                        try
                        {
                            fullNewsList.Add(new VisibleNewsItem()
                            {
                                NewsTitle = nd.title,
                                NewsDescription = nd.description,
                                NewsPublicationDate = nd.pubDate
                            });
                        }
                        catch { }
                    }
                }
            }

            //Sets the data grid
            Dispatcher.Invoke(() =>
            {
                genNewsTitle.Text = fullNewsList[0].NewsTitle;
                genNewsDate.Text = fullNewsList[0].NewsPublicationDate;
                genNewsDescription.Text = fullNewsList[0].NewsDescription;
            });
        }

        private void downloader_DownloadStringCompletedGN(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                string responseStream = e.Result;

                XmlSerializer serializer = new XmlSerializer(typeof(nodes));

                fullNewsList.Clear();
                using (TextReader reader = new StringReader(e.Result))
                {
                    rssChannel result = (rssChannel)serializer.Deserialize(reader);
                    foreach (var nd in result.item)
                    {
                        try
                        {
                            fullNewsList.Add(new VisibleNewsItem()
                            {
                                NewsTitle = nd.title,
                                NewsDescription = nd.description,
                                NewsPublicationDate = nd.pubDate
                            });
                        }
                        catch { }
                    }
                }
            }
            
            //Sets the data grid
            Dispatcher.Invoke(() =>
            {
                genNewsTitle.Text = fullNewsList[0].NewsTitle;
                genNewsDate.Text = fullNewsList[0].NewsPublicationDate;
                genNewsDescription.Text = fullNewsList[0].NewsDescription;
            });
        }

        private void SetSwapTimer()
        {
            // Create a timer with a two second interval.
            swapTimer = new Timer(8000);
            // Hook up the Elapsed event for the timer. 
            swapTimer.Elapsed += SetCSCards;
            swapTimer.AutoReset = true;
            swapTimer.Enabled = true;
    }

        private void SetCSCards(object sender, ElapsedEventArgs e)
        {
            //Swap and Fade Animation Code
            Dispatcher.Invoke(() =>
            {
                MaterialDesignThemes.Wpf.Card csCard = (MaterialDesignThemes.Wpf.Card)FindName("csCard");
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
                csCard.Resources.Add("unique_id", sb);
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
                
                csEventTitle.Text = fullCsNewsList[swapIndex].csEventTitle;
                csEventLocation.Text = fullCsNewsList[swapIndex].csEventLocation;
                csEventTime.Text = fullCsNewsList[swapIndex].csEventTime;

                csCard.Resources.Remove("unique_id");               
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

                    //Set the inital card value
                    Dispatcher.Invoke(() =>
                    {
                        csEventTitle.Text = fullCsNewsList[0].csEventTitle;
                        csEventLocation.Text = fullCsNewsList[0].csEventLocation;
                        csEventTime.Text = fullCsNewsList[0].csEventTime;
                    });
                }
            }
         
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            if (((string)rb.Content).Equals("Google News"))
            {
                source = newsSource.gn;
            } else if(((string)rb.Content).Equals("BBC World News"))
            {
                source = newsSource.bbc;
            } else if(((string)rb.Content).Equals("CNET Tech News"))
            {
                source = newsSource.cnet;
            } else if(((string)rb.Content).Equals("CLAS News"))
            {
                source = newsSource.clas;
            }
            GetNews();
        }
    }
}
