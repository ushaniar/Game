using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApplication1 {

    public class LevelOne : Canvas {
        public SpaceShip myPlayer;
        public Ball myBall;

        // Better to be collection ?
        private Vitamin[,] vArray = new Vitamin[5, 10];

        // Add Vitamins to Level
        private void addVitamins() {
            double vWidth = this.Width / vArray.GetLength(0);
            double vHeight = this.Height / vArray.GetLength(1);

            for (int i = 0; i < vArray.GetLength(0); i++) {
                for (int k = 0; k < vArray.GetLength(1); k++) {
                    // Needs fixing
                    vArray[i, k] = new Vitamin(new Rect(i * vWidth, k * vHeight * .4 + 50, vWidth - 1, 100 / vHeight));
                    this.Children.Add(vArray[i, k]);
                }
            }
        }

        public void render(Object sender, EventArgs w) {
                // if ball hits bottom
            if (myBall.ballLocation.Y >= this.Height) {
                myBall.ballAngle = 360 - myBall.ballAngle;
            }
                // if ball hits top
            if (myBall.ballLocation.Y <= 0) {
                myBall.ballAngle = 360 - myBall.ballAngle;
            }
                // if ball hits left
            if (myBall.ballLocation.X < 0) {
                myBall.ballAngle = 180 - myBall.ballAngle;
            }
                // if ball hits right
            if (myBall.ballLocation.X >= this.Width) {
                myBall.ballAngle = 180 - myBall.ballAngle;
            }

            if ((myPlayer.mySpaceShipGeometry.Rect.Location.X < myBall.ballLocation.X && myPlayer.mySpaceShipGeometry.Rect.Location.X + 60 >= myBall.ballLocation.X)
                && myBall.ballLocation.Y >= myPlayer.mySpaceShipGeometry.Rect.Location.Y && myBall.ballLocation.Y <= myPlayer.mySpaceShipGeometry.Rect.Location.Y + 10) {

                double divLength = myPlayer.Width / myPlayer.div;
                
                for (int i = 0; i < myPlayer.div; i++) {
                    if (myBall.ballLocation.X >= i * divLength + myPlayer.mySpaceShipGeometry.Rect.Location.X && myBall.ballLocation.X <= (i + 1) * divLength + myPlayer.mySpaceShipGeometry.Rect.Location.X) {
                        myBall.ballAngle = 180 / (myPlayer.div+1) * (i+1) + 180;
                    }
                }
            }

            // Change ball's location depending on angle
            myBall.myBall.Center = new Point(myBall.ballLocation.X += myBall.ballSpeed * Math.Cos(myBall.ballAngle / (180 / Math.PI)), myBall.ballLocation.Y += myBall.ballSpeed * Math.Sin(myBall.ballAngle / (180 / Math.PI)));

        }

        public LevelOne() {

            this.Background = Brushes.Beige;

            // Create objects
            myPlayer = new SpaceShip();
            myBall = new Ball();

            // Add objects to level
            this.Children.Add(myBall);
            this.Children.Add(myPlayer);

            // Add event to move player 
            this.MouseMove += myPlayer.move;

            // Add vitamins
            //addVitamins();
        }
    }
}
