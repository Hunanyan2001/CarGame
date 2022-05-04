using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace CarGame
{
    public class TrafficLine
    {
        public Point startLinePoss;
        public Directions direction { get; set; }
        public Rectangle Rectangle { get; set; } = new();

        public int Speed { get; set; }

        //public void TrafficCarMove()
        //{
        //    for (int i = 0; i < startLinePoss.Length; i++)
        //    {
        //        startLinePoss[i].Y += 1;
        //    }
        //}
    }
}
