using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Input;

namespace WpfApplication1 {

    public class SpaceShip : Shape {
        RectangleGeometry mySpaceShipGeometry;

        protected override Geometry DefiningGeometry {
            get { return mySpaceShipGeometry; }
        }

        public void move(object sender, MouseEventArgs e) {
            mySpaceShipGeometry.Rect = new Rect(e.GetPosition(this).X - (25), 300, 50, 25);
        }

        public SpaceShip() {
            mySpaceShipGeometry = new RectangleGeometry();
            mySpaceShipGeometry.Rect = new Rect(100, 300, 50, 25);
            this.Fill = Brushes.Aqua;
        }  
    }

    public class Ball : Shape {
        EllipseGeometry myBall;
        Point myPoint;

        protected override Geometry DefiningGeometry {
            get { return myBall; }
        }

        public void render(Object sender, EventArgs w) {
            // Moves the ball
            myBall.Center = new Point(myPoint.X++, myPoint.Y++);
        }

        public Ball() {
            myBall = new EllipseGeometry(myPoint, 20, 20);
            this.Fill = Brushes.SkyBlue;
        }
    }

    public class Vitamin : Shape {
        RectangleGeometry myVitamin;
        Random r;

        protected override Geometry DefiningGeometry {
            get { return myVitamin; }
        }

        private void setRandomColor() {
            r = new Random();
            switch (r.Next(1, 3)) {
                case 1:
                    this.Fill = Brushes.Blue;
                    break;
                case 2:
                    this.Fill = Brushes.Green;
                    break;
                case 3:
                    this.Fill = Brushes.Red;
                    break;
                default:
                    this.Fill = Brushes.Black;
                    break;
            }
        }

        public Vitamin(Rect myRect) {
            myVitamin = new RectangleGeometry();
            myVitamin.Rect = myRect;
            setRandomColor();
        }
    }
}
