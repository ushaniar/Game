using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Windows.Threading.DispatcherTimer myDispatcherTimer;

        Boolean start = false;

        // Ball
        double X_Movement;
        double Y_Movement;
        double X_Location = 1;
        double Y_Location = 1;

        //Player 
        double Player_Location;

        public MainWindow()
        {
            InitializeComponent();

            // Ball Position at Start
            Canvas.SetTop(Ball, X_Location);
            Canvas.SetLeft(Ball, Y_Location);

            X_Movement = 10;
            Y_Movement = 10;

            myDispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            myDispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            myDispatcherTimer.Tick += render; 
        }

        private void checkWallImpact() 
        {
            if (X_Location >= Level.ActualWidth - Ball.Width || X_Location < 0) 
                X_Movement *= -1;
            else if (Y_Location <= 0)
                Y_Movement *= -1;  
        }

        private bool checkPlayerImpact()
        {
            if (Y_Location >= ( Level.ActualHeight - ( Player.Height + Canvas.GetBottom(Player) ) ) 
                 && ( X_Location <= Player_Location + Player.Width && X_Location >= Player_Location )) 
            {
                return true;
            }
            else return false;
        }

        private void playerReturnBall() 
        {
            double begin = Player_Location;
            double end = Player_Location + Player.Width;
            double middle = (begin + end) / 2;

            double diff = Math.Abs(middle - (X_Location+10));


            double n = (1 - (diff / 90));
            Y_Movement *= -n;
            X_Movement *= -1;
        }

        private void ifFall() 
        {
            if (Y_Location >= Level.ActualHeight) 
            {
                myDispatcherTimer.Tick -= render;
                Console.WriteLine("GAME LOST");

                Canvas.SetTop(Ball, 1);
                Canvas.SetLeft(Ball, 1);

                Console.WriteLine("NEW GAME");
                myDispatcherTimer.Tick += render;
            }
        }

        private void render(Object sender, EventArgs e)
        {
            // Get Ball Location
            X_Location = Canvas.GetLeft(Ball);
            Y_Location = Canvas.GetTop(Ball);

            // Get Player X Location (Y is constant)
            Player_Location = Canvas.GetLeft(Player);

            // Check for impact
            if (checkPlayerImpact()) 
            {
                playerReturnBall(); 
            }
            checkWallImpact();

            Console.WriteLine("Ball Angle: " + Math.Abs(Math.Atan(X_Movement/Y_Movement) * 180 / Math.PI));

            ifFall();
            
            // Set new location
            Canvas.SetTop(Ball, Canvas.GetTop(Ball) + Y_Movement);
            Canvas.SetLeft(Ball, Canvas.GetLeft(Ball) + X_Movement);
        }

        private void MovePlayer(object sender, MouseEventArgs e)
        {
            // Move player -- Mouse at center
            Canvas.SetLeft(Player, e.GetPosition(this).X - Player.Width/2);
        }

        private void StartGame(object sender, MouseButtonEventArgs e)
        {
            // Double-click to start/stop game
            if (start == false) 
            { 
                myDispatcherTimer.Start(); 
                start = true; 
            }
            else if (start == true) 
            { 
                myDispatcherTimer.Stop(); 
                start = false; 
            }
        }
    }
}