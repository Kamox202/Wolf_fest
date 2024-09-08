using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using System.Diagnostics;


namespace PASPNET_PijarowskiK_Wolf_Feast
{
    internal class Rabit : Animal
    {
        private Thread RabitThread;
        private int targetX, targetY, distanceToClosestWolf = 10000, distanceToClosestNest = 10000;
        public int ID;
        private bool destination;
        public bool alerted;
        private List<Sim_Object> Objects;
        private List<Nest> Nests;
        private List<Wolf> Wolfs;
        private int SafeNestX, SafeNestY;
        
        

        public Rabit(int name = 0, List<Sim_Object> Obj = null, int X = 10, int Y = 0)
        {
            
            this.ID = name;
            this.v = 1;
            
            initBody(X, Y,10,10,1);
            
            set_wonder_destination();
            this.Objects = Obj;
            Wolfs = Objects.OfType<Wolf>().ToList();
            Nests = Objects.OfType<Nest>().ToList();
        }
        private void set_wonder_destination()
        {
            targetX = random.Next(0, 800);
            targetY = random.Next(0, 800);
            destination = true;
        }
        public void wander()
        {
            while (WindowActive)
            {
                while (alive)
                {
                    
                    foreach (Wolf wolf in Wolfs)
                    {
                        if(calculate_distance(wolf) < distanceToClosestWolf) distanceToClosestWolf = calculate_distance(wolf);
                    }
                    if (distanceToClosestWolf < 200) { alerted = true; }

                    if (alerted)
                    {

                        v = 2;

                        foreach (Nest nest in Nests)
                        {
                            if (calculate_distance(nest) < distanceToClosestNest) {
                                distanceToClosestNest = calculate_distance(nest);
                                targetX = nest.x;
                                targetY = nest.y;
                            }
                        }
                        
                    }
                    if (!destination && !alerted) set_wonder_destination();
                    move();
                  //  Debug.WriteLine("Królik: " + this.ID + " aktywny");
                    Thread.Sleep(100);
                }
            }
        }

        public void updateObjects(List<Sim_Object> objs)
        { 
        Objects = objs;
        Wolfs = objs.OfType<Wolf>().ToList();
        Nests = objs.OfType<Nest>().ToList();
        }
      

        private void move()
        {

            int dx = targetX - x;
            int dy = targetY - y;
            int distance = (int)Math.Sqrt(Math.Pow(dx * dx, 2) + Math.Pow(dy * dy, 2));



            if (dx != 0) dx = dx / Math.Abs(dx); 
            if (dy != 0) dy = dy / Math.Abs(dy); 


            x += dx * v;
            y += dy * v;


            if (x == targetX && y == targetY) destination = false;
        }

        public void Start()
        {
            RabitThread = new Thread(new ThreadStart(this.wander));

            RabitThread.Start();
        }

        


    }
}
