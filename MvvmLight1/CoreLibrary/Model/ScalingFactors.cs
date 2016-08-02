using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLibrary.Model
{
    public class ScalingFactors
    {
        public double sceneAspectRatio { get; set; } //(Actual Width/ Actual Height)
        public double sceneScalingRatio { get; set; }
        public double segmentScale { get; set; }
        public double pointScale { get; set; }
    }
}
