using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace CarGame
{
    public class RandomCar
    {
        public Directions Dirrection { get; set; }
        public int Speed { get; set; }

        public Point possRandomCar = new Point();

        public Rectangle Rectangle { get; set; }
        public Point CreateCarPossition()
        {
            int index = 0;
            Random rnd = new Random();
            Point[] possRandomCar =
            {
                new Point(100,0),
                new Point(120,0),
                new Point(150,0),
                new Point(50,0),
                new Point(30,0),
                new Point(70,0),
                new Point(50,0),
                new Point(110,0)
            };
            index = rnd.Next(possRandomCar.Length);
            return possRandomCar[index];
        }

        public Point CreateCarPossitionUp()
        {
            int index = 0;
            Random rnd = new Random();
            Point[] possRandomCar =
            {
                new Point(230,500),
                new Point(300,500),
                new Point(240,500),
                new Point(300,500),
                new Point(350,500),
                new Point(390,500),
                new Point(270,500),
                new Point(400,500)
            };
            index = rnd.Next(possRandomCar.Length);
            return possRandomCar[index];
        }

        public void MoveUp()
        {
            possRandomCar.Y -= Speed;
        }
        public void MoveDown()
        {
            possRandomCar.Y += Speed;
        }

        public void MoveLeft(Point poss)
        {
            poss.X -= 1;
        }

        public void MoveRight(Point poss)
        {
            poss.X += 1;
        }

    }
}
