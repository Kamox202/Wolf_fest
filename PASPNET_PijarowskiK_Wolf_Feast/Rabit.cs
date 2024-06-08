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
            while (alive)
            {
                set_wonder_destination();
                x += targetX;
                y += targetY;
            }
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
