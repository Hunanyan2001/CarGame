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
using System.Windows.Threading;

namespace CarGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;
        private Point startCarPoss;
        public int hour = 0;
        public int minute = 10;
        public int second = 59;
        public int Score = 0;
        Directions direction;
        Car car = new Car();
        RandomCar rndCar = new RandomCar();
        RandomCar rndCar1 = new RandomCar();
        Point startRndCarPoss;
        List<TrafficLine> lines = new List<TrafficLine>();
        public MainWindow()
        {
            InitializeComponent();
            MyCanvas.Focus();
            //create car
            car.direction = Directions.Top;
            car.Speed = 10;
            car.startCarPossition = new Point(200, 400);
            startCarPoss = car.startCarPossition;
            car.rectangle = CreateCarRectangle();
            //create line
            int linePosY = 0;
            for (int i = 0; i < 5; i++)
            {
                DrawTrafficLine(new Point(MyCanvas.Width / 2, linePosY));
                linePosY += 113;
            }

            // create RandomCar
            rndCar.Speed = 1;
            rndCar.possRandomCar = rndCar.CreateCarPossition();
            rndCar.Rectangle = CreateRandomCarRectangle(startRndCarPoss);
            rndCar.Dirrection = Directions.Buttom;

            //create RandomCar in the Opposite Direction
            rndCar1.Speed = 1;
            rndCar1.possRandomCar = rndCar1.CreateCarPossitionUp();
            rndCar1.Rectangle = CreateRandomCarRectangle(rndCar1.CreateCarPossitionUp());
            rndCar1.Dirrection = Directions.Top;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.0015);
            timer.Tick += timerTick;
            //timer.Start();
        }

        private void DrawTrafficLine(Point point)
        {
            var line = new TrafficLine();
            line.direction = Directions.Buttom;
            line.Speed = 10;
            line.startLinePoss = point;
            line.Rectangle = TrafficLine(line.startLinePoss);
            lines.Add(line);
        }

        private void timerTick(object sender, EventArgs e)
        {
            if (GameOver() == false)
            {
                Score += 1;
                PlayerScore.Content = $"Your Score {Score}";
               Dispatcher.Invoke(() => lb.Content = string.Format("{0}:{1}:{2}", hour.ToString().PadLeft(2, '0'), minute.ToString().PadLeft(2, '0'), second.ToString().PadLeft(2, '0')));
                if (hour == 0 && minute == 0 && second == 0)
                {
                    MessageBox.Show(" You Win next level ");
                    minute = 30;
                    second = 59;
                    rndCar.Speed += 5;
                    rndCar1.Speed += 5;
                }

                else
                {
                    if (second == 0)
                    {
                        second = 60;
                        minute--;
                    }
                    second--;
                    if (minute == -1)
                    {
                        minute = 59;
                        hour--;
                    }
                }
               

                foreach (var line in lines)
                {
                    MyCanvas.Children.Remove(line.Rectangle);
                    if (line.startLinePoss.Y >= MyCanvas.Height - line.Rectangle.Height)
                    {
                        line.startLinePoss.Y = -line.Rectangle.Height;
                    }
                    MyCanvas.Children.Add(line.Rectangle);
                    line.startLinePoss.Y += line.Speed;
                    Canvas.SetLeft(line.Rectangle, line.startLinePoss.X);
                    Canvas.SetTop(line.Rectangle, line.startLinePoss.Y + line.Speed);
                }
                if (rndCar1.possRandomCar.Y < -rndCar1.Rectangle.Height)
                {
                    RandomCar newrndCar1 = new RandomCar();
                    newrndCar1.Speed = rndCar1.Speed;
                    newrndCar1.possRandomCar = newrndCar1.CreateCarPossitionUp();
                    newrndCar1.Rectangle = CreateRandomCarRectangle(rndCar1.possRandomCar);
                    newrndCar1.Dirrection = Directions.Top;
                    rndCar1.possRandomCar = newrndCar1.possRandomCar;
                    //rndCar1.Speed = newrndCar1.Speed;
                }
                rndCar1.MoveUp();
                Canvas.SetLeft(rndCar1.Rectangle, rndCar1.possRandomCar.X);
                Canvas.SetTop(rndCar1.Rectangle, rndCar1.possRandomCar.Y );
                Canvas.SetZIndex(rndCar1.Rectangle, 1);

                if (rndCar.possRandomCar.Y >= MyCanvas.Height)
                {
                    RandomCar newrndCar = new RandomCar();
                    newrndCar.Speed = rndCar.Speed;
                    newrndCar.possRandomCar = newrndCar.CreateCarPossition();
                    newrndCar.Rectangle = CreateRandomCarRectangle(rndCar.possRandomCar);
                    newrndCar.Dirrection = Directions.Buttom;
                    rndCar.possRandomCar = newrndCar.possRandomCar;
                    //rndCar.Speed = newrndCar.Speed;
                }
                rndCar.MoveDown();
                Canvas.SetLeft(rndCar.Rectangle, rndCar.possRandomCar.X);
                Canvas.SetTop(rndCar.Rectangle, rndCar.possRandomCar.Y );
                Canvas.SetZIndex(rndCar.Rectangle, 1);

                if (direction == Directions.Top && car.startCarPossition.Y > 0)
                {
                    car.MoveUp();
                    Canvas.SetLeft(car.rectangle, car.startCarPossition.X);
                    Canvas.SetTop(car.rectangle, car.startCarPossition.Y);

                }
                if (direction == Directions.Buttom && car.startCarPossition.Y <= MyCanvas.Height - car.rectangle.Height)
                {
                    car.MoveDown();
                    Canvas.SetLeft(car.rectangle, car.startCarPossition.X);
                    Canvas.SetTop(car.rectangle, car.startCarPossition.Y);
                }
                if (direction == Directions.Left && car.startCarPossition.X > 0)
                {
                    car.MoveLeft();
                    Canvas.SetLeft(car.rectangle, car.startCarPossition.X);
                    Canvas.SetTop(car.rectangle, car.startCarPossition.Y);
                }
                if (direction == Directions.Right && car.startCarPossition.X < MyCanvas.Width - car.rectangle.Width)
                {
                    car.MoveRight();
                    Canvas.SetLeft(car.rectangle, car.startCarPossition.X);
                    Canvas.SetTop(car.rectangle, car.startCarPossition.Y);
                }

            }
            else
            {
                MessageBox.Show("GameOver");
                this.Close();
            }
        }

        public Rectangle CreateCarRectangle()
        {
            Rectangle rect = new Rectangle();
            rect.Width = 30;
            rect.Height = 50;
            rect.Fill = new SolidColorBrush() { Color = Colors.Black };
            MyCanvas.Children.Add(rect);
            Canvas.SetLeft(rect, startCarPoss.X);
            Canvas.SetTop(rect, startCarPoss.Y);
            Canvas.SetZIndex(rect, 1);
            return rect;
        }

        private void KeyPresend(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                direction = Directions.Top;
            }
            if (e.Key == Key.Down)
            {
                direction = Directions.Buttom;
            }
            if (e.Key == Key.Left)
            {
                direction = Directions.Left;
            }
            if (e.Key == Key.Right)
            {
                direction = Directions.Right;
            }
            if (e.Key == Key.Q)
            {
                PlaySound();
            }
            if (e.Key ==Key.P)
            {
                timer.Stop();
            }
            if (e.Key==Key.Enter)
            {
                timer.Start();
            }
        }

        private Rectangle TrafficLine(Point linePoss)
        {
            Rectangle line = new Rectangle();
            line.Height = 60;
            line.Width = 20;
            line.Fill = new SolidColorBrush() { Color = Colors.White };
            line.StrokeThickness = 1;
            MyCanvas.Children.Add(line);
            Canvas.SetLeft(line, linePoss.X);
            Canvas.SetTop(line, linePoss.Y);
            return line;
        }

        private Rectangle CreateRandomCarRectangle(Point rndPoss)
        {
            Rectangle rndCar = new Rectangle();
            rndCar.Height = 50;
            rndCar.Width = 30;
            rndCar.Fill = RandomBrush();
            rndCar.StrokeThickness = 1;
            MyCanvas.Children.Add(rndCar);
            Canvas.SetLeft(rndCar, rndPoss.X);
            Canvas.SetTop(rndCar, rndPoss.Y);
            return rndCar;
        }

        private Brush RandomBrush()
        {
            Brush[] brushes =
          {
              new SolidColorBrush(){ Color = Colors.Red },
              new SolidColorBrush(){ Color = Colors.Blue},
              new SolidColorBrush(){ Color = Colors.Green},
              new SolidColorBrush(){ Color = Colors.Purple},
              new SolidColorBrush(){ Color = Colors.Brown},
              new SolidColorBrush(){ Color = Colors.DarkBlue},
              new SolidColorBrush(){ Color = Colors.BlueViolet },
            };
            Random color = new Random();
            return brushes[color.Next(0, brushes.Length)];
        }

        private bool GameOver()
        {

            if (car.startCarPossition.X <= 0 || car.startCarPossition.X >= MyCanvas.Width - car.rectangle.Width
                || car.startCarPossition.Y <= 0 || car.startCarPossition.Y >= MyCanvas.Height - car.rectangle.Height)
            {
                return true;
            }
            for (int i = 0; i < car.rectangle.Height; i++)
            {
                for (int j = 0; j < car.rectangle.Width; j++)
                {

                    if (car.startCarPossition.X - j == rndCar.possRandomCar.X
                        && car.startCarPossition.Y-i ==rndCar.possRandomCar.Y)
                    {
                        return true;
                    }
                    if (car.startCarPossition.X+j==rndCar.possRandomCar.X
                        && car.startCarPossition.Y-i==rndCar.possRandomCar.Y)
                    {
                        return true;
                    }
                }
            }
            for (int i = 0; i < car.rectangle.Height; i++)
            {
                for (int j = 0; j < car.rectangle.Width; j++)
                {

                    if (car.startCarPossition.X - j == rndCar1.possRandomCar.X
                        && car.startCarPossition.Y - i == rndCar1.possRandomCar.Y)
                    {
                        return true;
                    }
                    if (car.startCarPossition.X + j == rndCar1.possRandomCar.X
                        && car.startCarPossition.Y - i == rndCar1.possRandomCar.Y
                        )
                    {
                        return true;
                    }
                    if (car.startCarPossition.X - j == rndCar1.possRandomCar.X
                        && car.startCarPossition.Y  == rndCar1.possRandomCar.Y-i)
                    {
                        return true;
                    }
                    if (car.startCarPossition.X + j == rndCar1.possRandomCar.X
                        && car.startCarPossition.Y  == rndCar1.possRandomCar.Y-i
                        )
                    {
                        return true;
                    }
                }
            }
            return false;

        }

        private void PauseClick(object sender, RoutedEventArgs e)
        {
            timer.Stop();
        }

        private void ContinueClick(object sender, RoutedEventArgs e)
        {
            timer.Start();
        }

        private void StartClickGame(object sender, RoutedEventArgs e)
        {
            timer.Start();
            PlaySound();
        }

        public void PlaySound()
        {
            var __mediaPlayer = new MediaPlayer();
            var executionDirectory = Environment.CurrentDirectory;
            var path = System.IO.Path.Combine(executionDirectory, "ASPHALT9LEGENDSOSTСаундтрек5(httpsvkcomdef_touch)_(allmp3.su).mp3");
            __mediaPlayer.Open(new Uri(path));
            __mediaPlayer.Play();
        }
        // reviuer
        //khhbjhbjhbjhbsghvhjbjhhbjsb
    }
}
