using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLibrary.Model
{
    public interface ILine : IShape
    {
        //For Y=Mx+B
        double slope { get; set; }
        double yIntercept { get; set; }
    }
}
