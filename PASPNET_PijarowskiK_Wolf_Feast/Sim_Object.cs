using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PASPNET_PijarowskiK_Wolf_Feast
{
    internal class Sim_Object
    {
        public Ellipse body;
        public bool hasBody = false;
        public Canvas existance;
        public volatile int x, y, color, width, height;
        protected bool WindowActive = true;
        protected Random random = new Random();
        //public MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;


        protected void initBody( int X = 300, int Y = 400, int w=10, int h=10, int color = 0)
        {
            
            x = X;
            y = Y;
            width = w;
            height = h;
            this.color = color;

            
            

            

        }

       public void Stop(){ WindowActive = false; }

        protected int calculate_distance(Sim_Object a)
        {
            return (int)Math.Sqrt((this.x - a.x) * (this.x - a.x) + (this.y - a.y) * (this.y - a.y));
           
        }

        protected int calculate_distance(int X, int Y)
        {
            return (int)Math.Sqrt((x - X) * (x - X) + (y - Y) * (y - Y));
        }
    }
}
