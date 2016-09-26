using HtmlAgilityPack;
using Microsoft.Samples.Kinect.ControlsBasics.Helper_Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
        }

        private void GetPeopleList()
        {
            HtmlDocument doc = new HtmlWeb().Load("https://www.cs.uiowa.edu/people");
            List<CSPeople> people = new List<CSPeople>();
            var rows = doc.DocumentNode.SelectNodes("//table[contains(@class,'views-table')]//tr");
            
            foreach(var tr in rows)
            {
                
                //Debug.WriteLine(tr.OuterHtml);
                CSPeople person = new CSPeople();
                foreach (var td in tr.ChildNodes)
                {
                    //Debug.WriteLine(td.InnerText);


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
                    else if (td.GetAttributeValue("class", "Not found").Equals("views-field views-field-field-telephone"))
                    {
                            person.Phone = td.InnerText;                        
                    }
                    else if (td.GetAttributeValue("class", "Not found").Equals("views-field views-field-field-email"))
                    {
                            person.Email = td.InnerText;
                    }
                }
                
                people.Add(person);
                
            }
           for(int i = 0; i < people.Count; i++)
           {
                if (people[i].Name.Equals("\n            Name          "))
                {
                    people.Remove(people[i]);
                }
           }
            PeoplGrid.DataContext = people;
        }

        
    }
}
