using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Samples.Kinect.ControlsBasics
{
    class BongoData
    {
        public StopInfo stopinfo { get; set; }
        public List<PredictionData> predictions { get; set; }
    }

    class PredictionData
    {
        public string title { get; set; }
        public string tag { get; set; }
        public int minutes { get; set; }
        public string agency { get; set; }
        public string agencyName { get; set; }
        public string direction { get; set; }
        public string stopname { get; set; }

    }

    class StopInfo
    {
        public string stopid { get; set; }
    }

    class VisibleBongoData
    {
        public string stopname { get; set; }
        public string routename { get; set; }
        public string minutes { get; set; }
        public string color { get; set; }
    }
}
