using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
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
using WpfAnimatedGif;

namespace Microsoft.Samples.Kinect.ControlsBasics
{
    /// <summary>
    /// Interaction logic for WeatherPage.xaml
    /// </summary>
    public partial class WeatherPage : UserControl
    {
        private Rootobject fullWeatherData;
       
        public WeatherPage()
        {
            InitializeComponent();
            fullWeatherData = MainWindow.fullWeatherData;
            PopulateForecast();
            GetRadar();
        }

        private void GetRadar()
        {

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(@"http://api.wunderground.com/api/75c131024c99cf58/animatedradar/q/IA/Iowa_City.gif?newmaps=1&timelabel=1&timelabel.y=10&num=5&delay=50&width=550&height=550");
            bitmap.EndInit();

            ImageBehavior.SetAnimatedSource(RadarImage, bitmap);
        }

      
        private void PopulateForecast()
        {
            Simpleforecast weatherData = fullWeatherData.forecast.simpleforecast;
            string hostIconURL = "http://icons.wxug.com/i/c/i/";
            if (DateTime.Now.Hour >= 18)
            {
                hostIconURL = hostIconURL + "nt_";
            }

            try
            {            
                for (int i = 0; i < 10 && i < weatherData.forecastday.Length; i++)
                {
                    if (i == 0)
                    {
                        day1.Text = weatherData.forecastday[i].date.weekday;
                        day1Num.Text = weatherData.forecastday[i].date.month + "/" + weatherData.forecastday[i].date.day;
                        Temp1.Text = weatherData.forecastday[i].high.fahrenheit + "°/" + weatherData.forecastday[i].low.fahrenheit + "°";
                        weatherIcon1.Source = new BitmapImage(new Uri(hostIconURL + weatherData.forecastday[0].icon + ".gif"));
                    }
                    else if (i == 1)
                    {
                        day2.Text = weatherData.forecastday[i].date.weekday;
                        day2Num.Text = weatherData.forecastday[i].date.month + "/" + weatherData.forecastday[i].date.day;
                        Temp2.Text = weatherData.forecastday[1].high.fahrenheit + "°/" + weatherData.forecastday[1].low.fahrenheit + "°";
                        weatherIcon2.Source = new BitmapImage(new Uri(hostIconURL + weatherData.forecastday[1].icon + ".gif"));
                    }
                    else if (i == 2)
                    {
                        day3.Text = weatherData.forecastday[i].date.weekday;
                        day3Num.Text = weatherData.forecastday[i].date.month + "/" + weatherData.forecastday[i].date.day;
                        Temp3.Text = weatherData.forecastday[2].high.fahrenheit + "°/" + weatherData.forecastday[2].low.fahrenheit + "°";
                        weatherIcon3.Source = new BitmapImage(new Uri(hostIconURL + weatherData.forecastday[2].icon + ".gif"));
                    }
                    else if (i == 3)
                    {
                        day4.Text = weatherData.forecastday[i].date.weekday;
                        day4Num.Text = weatherData.forecastday[i].date.month + "/" + weatherData.forecastday[i].date.day;
                        Temp4.Text = weatherData.forecastday[3].high.fahrenheit + "°/" + weatherData.forecastday[3].low.fahrenheit + "°";
                        weatherIcon4.Source = new BitmapImage(new Uri(hostIconURL + weatherData.forecastday[3].icon + ".gif"));
                    }
                    else if (i == 4)
                    {
                        day5.Text = weatherData.forecastday[i].date.weekday;
                        day5Num.Text = weatherData.forecastday[i].date.month + "/" + weatherData.forecastday[i].date.day;
                        Temp5.Text = weatherData.forecastday[4].high.fahrenheit + "°/" + weatherData.forecastday[4].low.fahrenheit + "°";
                        weatherIcon5.Source = new BitmapImage(new Uri(hostIconURL + weatherData.forecastday[4].icon + ".gif"));
                    }
                    else if (i == 5)
                    {
                        day6.Text = weatherData.forecastday[i].date.weekday;
                        day6Num.Text = weatherData.forecastday[i].date.month + "/" + weatherData.forecastday[i].date.day;
                        Temp6.Text = weatherData.forecastday[5].high.fahrenheit + "°/" + weatherData.forecastday[5].low.fahrenheit + "°";
                        weatherIcon6.Source = new BitmapImage(new Uri(hostIconURL + weatherData.forecastday[5].icon + ".gif"));
                    }
                    else if (i == 6)
                    {
                        day7.Text = weatherData.forecastday[i].date.weekday;
                        day7Num.Text = weatherData.forecastday[i].date.month + "/" + weatherData.forecastday[i].date.day;
                        Temp7.Text = weatherData.forecastday[6].high.fahrenheit + "°/" + weatherData.forecastday[6].low.fahrenheit + "°";
                        weatherIcon7.Source = new BitmapImage(new Uri(hostIconURL + weatherData.forecastday[6].icon + ".gif"));
                    }
                    else if (i == 7)
                    {
                        day8.Text = weatherData.forecastday[i].date.weekday;
                        day8Num.Text = weatherData.forecastday[i].date.month + "/" + weatherData.forecastday[i].date.day;
                        Temp8.Text = weatherData.forecastday[7].high.fahrenheit + "°/" + weatherData.forecastday[7].low.fahrenheit + "°";
                        weatherIcon8.Source = new BitmapImage(new Uri(hostIconURL + weatherData.forecastday[7].icon + ".gif"));
                    }
                    else if (i == 8)
                    {
                        day9.Text = weatherData.forecastday[i].date.weekday;
                        day9Num.Text = weatherData.forecastday[i].date.month + "/" + weatherData.forecastday[i].date.day;
                        Temp9.Text = weatherData.forecastday[8].high.fahrenheit + "°/" + weatherData.forecastday[8].low.fahrenheit + "°";
                        weatherIcon9.Source = new BitmapImage(new Uri(hostIconURL + weatherData.forecastday[8].icon + ".gif"));
                    }
                    else if (i == 9)
                    {
                        day10.Text = weatherData.forecastday[i].date.weekday;
                        day10Num.Text = weatherData.forecastday[i].date.month + "/" + weatherData.forecastday[i].date.day;
                        Temp10.Text = weatherData.forecastday[9].high.fahrenheit + "°/" + weatherData.forecastday[9].low.fahrenheit + "°";
                        weatherIcon10.Source = new BitmapImage(new Uri(hostIconURL + weatherData.forecastday[9].icon + ".gif"));
                    }
                }
                moreDetailDayTitle.Text = fullWeatherData.forecast.txt_forecast.forecastday[0].title;
                moreDetailDayText.Text = fullWeatherData.forecast.txt_forecast.forecastday[0].fcttext;
                moreDetailNightTitle.Text = fullWeatherData.forecast.txt_forecast.forecastday[1].title;
                moreDetailNightText.Text = fullWeatherData.forecast.txt_forecast.forecastday[1].fcttext;
            }
            catch { }
        }

        private void MoreDetailsClick(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;            
            int day = int.Parse(button.Name.Substring(2)) * 2;
            try
            {
                moreDetailDayTitle.Text = fullWeatherData.forecast.txt_forecast.forecastday[day].title;
                moreDetailDayText.Text = fullWeatherData.forecast.txt_forecast.forecastday[day].fcttext;
                moreDetailNightTitle.Text = fullWeatherData.forecast.txt_forecast.forecastday[day + 1].title;
                moreDetailNightText.Text = fullWeatherData.forecast.txt_forecast.forecastday[day + 1].fcttext;
            } catch{ }
            
        }
    }
}
