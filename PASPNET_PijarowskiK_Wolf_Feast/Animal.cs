using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;

namespace PASPNET_PijarowskiK_Wolf_Feast
{
    internal class Animal
    {
        protected Random random = new Random();
        public volatile int x, y, v;
        public Ellipse body;
        public Canvas existance;
        public bool alive = true;


        protected void initBody(Canvas canva)
            { 
            body = new Ellipse();
            x = random.Next(0, 800);
            y = random.Next(0, 780);
            body.Width = 10;
            body.Height = 10;

            body.HorizontalAlignment = HorizontalAlignment.Center;
            body.VerticalAlignment = VerticalAlignment.Center;
            Canvas.SetLeft(body, x);
            Canvas.SetTop(body, y);
            canva.Children.Add(body);
            
            //existance = canva;

        }

        public void death()
        {
            alive = false;
            //existance.Children.Remove(body);
           
        }
        
    }

}
