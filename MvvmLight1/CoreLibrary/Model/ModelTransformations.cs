using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace CoreLibrary.Model
{
    public class ModelTransformations
    {
        private Transform3DGroup transformGroup3D;          //holds all models
        private TranslateTransform3D translateTransform3D;  //translation object
        private RotateTransform3D rotateTransform3D;        //rotation object
        private AxisAngleRotation3D axisAngleRotation3D;    //rotation attributes
        private ScaleTransform3D scaleTransform3D;
        public ModelTransformations()
        {
            transformGroup3D = new Transform3DGroup();
            translateTransform3D = new TranslateTransform3D();
            rotateTransform3D = new RotateTransform3D();
            axisAngleRotation3D = new AxisAngleRotation3D();
            scaleTransform3D = new ScaleTransform3D();

            axisAngleRotation3D.Axis = new Vector3D(0, 0, 1); //Rotate around z-axis
            rotateTransform3D.CenterX = 0;                    //Rotate around (0,0,0)
            rotateTransform3D.CenterY = 0;
            rotateTransform3D.CenterZ = 0;
        }
        public void rotate(double angle)
        {
            axisAngleRotation3D.Angle = angle;
            rotateTransform3D.Rotation = axisAngleRotation3D;
        }
        public void scale(double scaleX, double scaleY)
        {
            scaleTransform3D.ScaleX = scaleX;
            scaleTransform3D.ScaleY = scaleY;
            scaleTransform3D.ScaleZ = 0;

        }
        public void scale(double scaleFactor)
        {
            scaleTransform3D.ScaleY = scaleFactor;
        }
        public void scaleX(double scaleFactor)
        {
            scaleTransform3D.ScaleX = scaleFactor;
        }
        public void scaleCenter(double x, double y)
        {
            scaleTransform3D.CenterX = x;
            scaleTransform3D.CenterY = y;
        }
        public void translate(double translateX, double translateY)
        {
            translateTransform3D.OffsetX = translateX;
            translateTransform3D.OffsetY = translateY;
            translateTransform3D.OffsetZ = 0;
        }
        public void translateX(double translateX)
        {
            translateTransform3D.OffsetX = translateX;
        }
        public void translateY(double translateY)
        {
            translateTransform3D.OffsetY = translateY;
        }
        public Transform3DGroup getTransformations()
        {
            transformGroup3D.Children.Clear();
            transformGroup3D.Children.Add(scaleTransform3D);
            transformGroup3D.Children.Add(rotateTransform3D);
            transformGroup3D.Children.Add(translateTransform3D);
            return transformGroup3D;
        }
        public TranslateTransform3D getTranslation()
        {
            return translateTransform3D;
        }
        public ScaleTransform3D getScale()
        {
            return scaleTransform3D;
        }
        public RotateTransform3D getRotation()
        {
            return rotateTransform3D;
        }
        public double getRoationAngle()
        {
            return axisAngleRotation3D.Angle;
        }
    }
}
