using HtmlAgilityPack;
using Microsoft.Samples.Kinect.ControlsBasics.Helper_Classes;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Controls;
using System;
using System.Windows.Threading;
using System.Text.RegularExpressions;
using System.Windows.Media.Imaging;
using WpfAnimatedGif;
using System.Windows;

namespace Microsoft.Samples.Kinect.ControlsBasics.Pages
{
    /// <summary>
    /// Interaction logic for PeoplePage.xaml
    /// </summary>
    public partial class PeoplePage : UserControl
    {
         
        public PeoplePage()
        {
            InitializeComponent();
            GetPeopleList();

            //BitmapImage bitmap = new BitmapImage();
            //bitmap.BeginInit();
            //bitmap.UriSource = new Uri("../Assets/spin.gif", UriKind.Relative);
            //bitmap.EndInit();
            //ImageBehavior.SetAnimatedSource(LoadingImage, bitmap);
        }
      

        /// <summary>
        /// Async method that waits for API call and then updates UI
        /// </summary>
        private async void GetPeopleList()
        {
            List<CSPeople> p = await ParseHttpPeopleAsync();
            
            Dispatcher.Invoke(DispatcherPriority.DataBind, new Action(delegate { PeopleGrid.DataContext = p; }));

            Loading.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Async method that returns Parsed list of CSPeople
        /// </summary>
        /// <returns></returns>
        async Task<List<CSPeople>> ParseHttpPeopleAsync()
        {
            HttpClient client = new HttpClient();
            var doc = new HtmlDocument();
            var html = await client.GetStringAsync("https://www.cs.uiowa.edu/people");
            doc.LoadHtml(html);

            List<CSPeople> p = await Task.Run(() => { 
                List<CSPeople> people = new List<CSPeople>();
                var rows = doc.DocumentNode.SelectNodes("//table[contains(@class,'views-table')]//tr");

                foreach (var tr in rows)
                {

                
                    CSPeople person = new CSPeople();
                    foreach (var td in tr.ChildNodes)
                    {
                        if (td.GetAttributeValue("class", "Not found").Equals("views-field views-field-title"))
                        {
                            person.Name = td.InnerText;
                        }
                        else if (td.GetAttributeValue("class", "Not found").Equals("views-field views-field-field-office"))
                        {
                            person.Office = td.InnerText;
                        }
                        else if (td.GetAttributeValue("class", "Not found").Equals("views-field views-field-field-hours"))
                        {
                            person.Hours = td.InnerText;
                        }                    
                        else if (td.GetAttributeValue("class", "Not found").Equals("views-field views-field-field-email"))
                        {
                            person.Email = td.InnerText;
                        }
                    }
                    people.Add(person);
                }
                Regex r = new Regex(@"\W*Name\W*");
                Regex blanksReg = new Regex(@"^\W*$");
                
                people.RemoveAll(person => r.IsMatch(person.Name));
                people.RemoveAll(person => blanksReg.IsMatch(person.Office));
                
                return people;
            });
            
            return p;   
        }
    }
}
