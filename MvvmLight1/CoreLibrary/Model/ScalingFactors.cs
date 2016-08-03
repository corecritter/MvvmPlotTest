using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLibrary.Model
{
    public class ScalingFactors
    {
        public double SceneAspectRatio { get; set; } //(Actual Width/ Actual Height)
        public double SceneScalingRatio { get; set; }
        public double SegmentScale { get; set; }
        public double PointScale { get; set; }
        public double SceneWidth { get; set; }
        public double SceneHeight { get; set; }
        public double WorldWidth { get; set; }
        public double WorldHeight { get; set; }
    }
}
