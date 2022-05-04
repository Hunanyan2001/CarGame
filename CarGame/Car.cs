using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CarGame
{
    public class Car
    {
        public Point startCarPossition = new Point();
        public int Speed { get; set;}
        public Directions direction { get; set; }

        public Rectangle rectangle { get; set; }
        public Car()
        {
            
        }
        public void MoveUp()
        {
            startCarPossition.Y -= 5;
        }
        public void MoveDown()
        {
            startCarPossition.Y += 1;
        }

        public void MoveLeft()
        {
            startCarPossition.X -= 1;
        }

        public void MoveRight()
        {
            startCarPossition.X += 1;
        }
    }
}
