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
    internal class Animal : Sim_Object
    {
        protected Random random = new Random();
        public volatile int v;
        public Ellipse body;
        
        public bool alive = true;


        //protected override void initBody(Canvas canva, int X = 300, int Y = 400)
        //    { 
        //    this.body = new Ellipse();
        //    this.x = X;
        //    this.y = Y;
        //    this.body.Width = 10;
        //    this.body.Height = 10;

        //    this.body.HorizontalAlignment = HorizontalAlignment.Center;
        //    this.body.VerticalAlignment = VerticalAlignment.Center;
        //    Canvas.SetLeft(body, x);
        //    Canvas.SetTop(body, y);
        //    canva.Children.Add(body);
            
         

        //}


        public void death()
        {
            alive = false;     
        }
        
    }

}
