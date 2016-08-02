using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace BusinessLogic.Converter
{
    //[ValueConversion(typeof(double), typeof(double))]
    public class RealToWorldCoordinates : DependencyObject, IValueConverter
    {
        private double sceneWidth;
        private double sceneHeight;

        //public double SceneWidth
        //{
        //    get
        //    {
        //        return this.sceneWidth;
        //    }
        //    set
        //    {
        //        this.sceneWidth = value;
        //    }
        //}
        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("MouseCoord", typeof(double),
                typeof(RealToWorldCoordinates));//, new UIPropertyMetadata(0.0));
        public double MouseCoord
        {
            get { return (double)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //double input = (double)parameter;
            //return input + 1000;
            return 10;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return 0;
        }
    }
}
