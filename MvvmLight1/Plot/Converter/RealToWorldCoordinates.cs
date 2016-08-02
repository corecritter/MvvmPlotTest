using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Plot.Converter
{
    public class RealToWorldCoordinates : DependencyObject, IMultiValueConverter
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
        //public static readonly DependencyProperty DataProperty =
        //    DependencyProperty.Register("MouseCoord", typeof(double),
        //        typeof(RealToWorldCoordinates));//, new UIPropertyMetadata(0.0));
        //public static readonly DependencyProperty SceneWidthProperty =
        //    DependencyProperty.Register("SceneWidth", typeof(double),
        //        typeof(RealToWorldCoordinates));

        //public double MouseCoord
        //{
        //    get { return (double)GetValue(DataProperty); }
        //    set { SetValue(DataProperty, value); }
        //}

        //public double SceneWidth
        //{
        //    set
        //    {
        //        this.sceneWidth = value;
        //    }
        //}
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //double input = (double)parameter;
            //return input + 1000;
            return (double)value + sceneWidth;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return 0;
        }

        object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            /*
             * values[0] = MouseCoordinate (pixels)
             * values[1] = SceneWidth (pixels)
             * values[2] = SceneHeight (pixels)
             * values[3] = CameraWidth (world width)
             */

            //Convert to [-1, 1] range
            double worldCoord = (2 * (double)values[0]) / (double)values[1] - 1;
            //Convert To World Coordinates
            worldCoord = (worldCoord * (double)values[3]) / 2.0;
            return worldCoord.ToString();
        }

        object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
            //return 0;
        }
    }
}
