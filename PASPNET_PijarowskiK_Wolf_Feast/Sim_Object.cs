using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace PASPNET_PijarowskiK_Wolf_Feast
{
    internal class Sim_Object
    {
        public Ellipse body;
        public Canvas existance;
        public volatile int x, y;
        protected Random random = new Random();


        protected void initBody(Canvas canva)
        {
            body = new Ellipse();
            x = random.Next(0, 800);
            y = random.Next(0, 800);
            body.Width = 10;
            body.Height = 10;

            body.HorizontalAlignment = HorizontalAlignment.Center;
            body.VerticalAlignment = VerticalAlignment.Center;
            Canvas.SetLeft(body, x);
            Canvas.SetTop(body, y);
            canva.Children.Add(body);

            //existance = canva;

        }

        protected void initBody(Canvas canva, int w, int h)
        {
            body = new Ellipse();
            x = random.Next(0, 800);
            y = random.Next(0, 800);
            body.Width = w;
            body.Height = h;

            body.HorizontalAlignment = HorizontalAlignment.Center;
            body.VerticalAlignment = VerticalAlignment.Center;
            Canvas.SetLeft(body, x);
            Canvas.SetTop(body, y);
            canva.Children.Add(body);

            //existance = canva;

        }

        protected int calculate_distance(Sim_Object a)
        {
            return (x - a.x) * (x - a.x) + (y - a.y) * (y - a.y);
        }

        protected int calculate_distance(int X, int Y)
        {
            return (x - X) * (x - X) + (y - Y) * (y - Y);
        }
    }
}
