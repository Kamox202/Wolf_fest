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
        private Random random = new Random();
        private int targetX, targetY;
        public int ID;
        private bool destination;
        public bool hiden, alerted;
        private List<Sim_Object> Objects;
        private List<Nest> Nests;
        private List<Wolf> Wolfs;
        private int predatorX, predatorY, SafeNestX, SafeNestY, predatorD;
        protected Barrier barrier;
        

        public Rabit(int name = 0, Canvas can = null,  List<Sim_Object> Obj = null, int X = 10, int Y = 0)
        {
            
            this.ID = name;
            this.v = 1;
            
            initBody(can, X, Y,10,10,1);
            
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
                    calculateDistanceToClosestWolf(Wolfs);
                    if (alerted)
                    {

                        v = 2;
                        calculateDistanceToClosestNest(Nests);
                        targetX = SafeNestX;
                        targetY = SafeNestY;
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
        private void calculateDistanceToClosestWolf(List<Wolf> wolfs)
        {
            int sum = (int)Math.Sqrt(Math.Pow(this.x - wolfs.First().x, 2) + Math.Pow(this.y - wolfs.First().y, 2));
            predatorX = wolfs.First().x;
            predatorY = wolfs.First().y;
            foreach (Wolf wolf in wolfs)
            {
                int compare = (int)Math.Sqrt(Math.Pow(this.x - wolf.x, 2) + Math.Pow(this.y - wolf.y, 2));
                if (compare < 200 || sum < 200)
                {
                    alerted = true;

                }
            }
           
        }

        public void hide()
        {
            hiden = true;
            alerted = false;
        }
        private void calculateDistanceToClosestNest(List<Nest> nests)
        {
            int buforDistance = 10000;
            foreach (Nest nest in nests)
            {
                if (nest.avalible && calculate_distance(nest) < buforDistance)
                {
                    buforDistance = calculate_distance(nest);
                    SafeNestX = nest.x;
                    SafeNestY = nest.y;
                }
            }




        
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
           // Debug.WriteLine("aktywowano Królika: " + barrier);
        }

        


    }
}
