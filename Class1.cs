using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApplication1 {

    public class LevelOne : Canvas {
        public SpaceShip myPlayer;
        public Ball myBall;

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

        public LevelOne() {   
            this.Width = 400;
            this.Height = 300;

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
            // addVitamins();             
        }
    }
}
