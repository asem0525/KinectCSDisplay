using System;
using System.Collections.Generic;
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
    /// Interaction logic for MapsPage.xaml
    /// </summary>
    public partial class MapsPage : UserControl
    {
        private Dictionary<string, string> MapSourceDict;

        public MapsPage()
        {
            InitializeComponent();

            MapSourceDict = new Dictionary<string, string>()
            {
                { "Floor 1", "../Images/Maps/floor 1.jpg" },
                { "Floor 2", "../Images/Maps/floor 2.jpg" },
                { "Floor 3", "../Images/Maps/floor 3.jpg" },
                { "Ground Floor", "../Images/Maps/groundFloor.jpg" },
                { "Basement", "../Images/Maps/basement.jpg" },
            };

            MapImage.Source = new BitmapImage(new Uri(MapSourceDict["Floor 1"], UriKind.Relative));
            MapTitle.Text = "Map: Floor 1";
        }



        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            MapImage.Source = new BitmapImage(new Uri(MapSourceDict[(string)rb.Content], UriKind.Relative));
            MapTitle.Text = "Map: " + (string)rb.Content;
        }

        private void centerImageView_ManipulationStarting(object sender, System.Windows.Input.ManipulationStartingEventArgs e)
        {
            e.ManipulationContainer = this;
            e.Handled = true;
        }

        private void centerImageView_ManipulationDelta(object sender, System.Windows.Input.ManipulationDeltaEventArgs e)
        {
        // get current matrix of the element.
         Matrix borderMatrix = ((System.Windows.Media.MatrixTransform)MapImage.RenderTransform).Matrix;
            //determine if action is zoom or pinch
            var maxScale = Math.Max(e.DeltaManipulation.Scale.X, e.DeltaManipulation.Scale.Y);
            //check if not crossing minimum and maximum zoom limit
            if ((maxScale < 1 && borderMatrix.M11 * maxScale > 0) ||
            (maxScale > 1 && borderMatrix.M11 * maxScale < 100))
            {
                //scale to most recent change (delta) in X & Y 
                borderMatrix.ScaleAt(e.DeltaManipulation.Scale.X,
                        e.DeltaManipulation.Scale.Y,
                        MapImage.ActualWidth / 2,
                        MapImage.ActualHeight / 2);
                //render new matrix
                MapImage.RenderTransform = new MatrixTransform(borderMatrix);
            }

        }

        private void MapImage_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {

        }
    }
}
