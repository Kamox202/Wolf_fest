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
    internal class Rabit : Animal
    {
        private Thread RabitThread;
        private Random random = new Random();
        private bool dead = true;
        private int targetX, targetY;
        private string Name;
        
        
        public Rabit(string name = null, Canvas can = null)
        {
            
            this.Name = name;
            this.v = 1;
            dead = false;
            initBody(can);
            body.Fill = Brushes.White;
            set_wonder_destination();




            RabitThread = new Thread(wander);
            
        }
        private void set_wonder_destination()
        {
            targetX = random.Next(0, 1000);
            targetY = random.Next(0, 1000);
            if (targetX % 2 == 0) { targetX = 1; }else { targetX = -1; }
            if (targetY % 2 == 0) { targetY = 1; }else { targetY = -1; }

            if (x <= 0) { targetX = 1; }
            if (x >= 800) { targetX = -1; }
            if (y <= 0) { targetY = 1; }
            if (y >= 800) { targetY = -1; }
        }
        public void wander()
        {
            set_wonder_destination();
            x += targetX; Canvas.SetLeft(body, x);
            y += targetY; Canvas.SetTop(body, y);
        }

        public void death()
        { 
            
            existance.Children.Remove(body);
            
        }

        ~Rabit() { }
    }
}
