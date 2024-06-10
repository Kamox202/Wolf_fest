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
        private int targetX, targetY;
        public int ID;
        private bool destination;
        
        public Rabit(int name = 0, Canvas can = null)
        {
            
            this.ID = name;
            this.v = 1;
            
            initBody(can);
            body.Fill = Brushes.White;
            set_wonder_destination();
        }
        private void set_wonder_destination()
        {
            targetX = random.Next(0, 800);
            targetY = random.Next(0, 800);
            destination = true;
        }
        public void wander()
        {
            while (alive)
            {
               if(!destination) set_wonder_destination();
                move();
                Thread.Sleep(100);
            }
        }

        private void move()
        {

            int dx = targetX - x;
            int dy = targetY - y;
            int distance = (int)Math.Sqrt(Math.Pow(dx * dx, 2) + Math.Pow(dy * dy, 2));



            if (dx != 0) dx = dx / Math.Abs(dx); // Normalizacja do -1, 0, 1
            if (dy != 0) dy = dy / Math.Abs(dy); // Normalizacja do -1, 0, 1


            x += dx * v;
            y += dy * v;


            if (x == targetX && y == targetY) destination = false;
        }

        ~Rabit() { }


        public void Decompose(List<Rabit> rabits)
        {
            rabits.Remove(this);
            
        }
        public void Start()
        {
            RabitThread = new Thread(new ThreadStart(this.wander));
            RabitThread.Start();
        }
    }
}
