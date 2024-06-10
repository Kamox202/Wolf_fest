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
    internal class Wolf : Animal
    {
        private Thread WolfThread;
        private string Name;
        private int victimX, victimY, victim;
        private Rabit Pray;
        private bool locked;
        private List<Rabit> RabitList;
        private List<Sim_Object> Objects;


        public Wolf(string name, Canvas canva, List<Sim_Object> Prays)
        {
            this.Objects = Prays;
            RabitList = Prays.OfType<Rabit>().ToList();
            this.v = 3;
            Name = name;
            initBody(canva);
            body.Fill = Brushes.Black;
            
            

        }
        private void AquirePray(List<Rabit> rabits)
        {

            if (rabits.Count > 0)
            {
                //victim = rabits.FindIndex(r => r.x == rabits.Min((rab => rab.x))-x);

                int sum = (int)Math.Sqrt(Math.Pow(this.x - rabits.First().x, 2) + Math.Pow(this.y - rabits.First().y, 2));
                victimX = rabits.First().x;
                victimY = rabits.First().y;
                foreach (Rabit ra in rabits)
                {
                    int compare = (int)Math.Sqrt(Math.Pow(this.x - ra.x, 2) + Math.Pow(this.y - ra.y, 2));
                    if (compare < sum)
                    {
                        victimX = ra.x;
                        victimY = ra.y;
                        sum = compare;
                        
                    }
                }

                victim = rabits.FindIndex(r => r.x == victimX );
                
                if (victim == -1)
                {
                    locked = false;
                    AquirePray(rabits);
                }


            }

            
        }

        private void moveTo(List<Rabit> rabits)
        {
           
            int dx = victimX - x ;
            int dy = victimY - y ;
            int distance = (int)Math.Sqrt(Math.Pow(dx * dx,2) + Math.Pow(dy * dy,2));

            

            if (dx != 0) dx = dx / Math.Abs(dx); // Normalizacja do -1, 0, 1
            if (dy != 0) dy = dy / Math.Abs(dy); // Normalizacja do -1, 0, 1
            

            x += dx * v;
            y += dy * v;
            

            if (Math.Abs(victimX - x) <= 1 && Math.Abs(victimY - y) <= 1 && rabits.Count > 0)
            {
                eat(rabits);
                locked = false;
            }
        }
        public void hunt()
        {
            while (alive)
            {
                RabitList = Objects.OfType<Rabit>().ToList();
                if (!locked) AquirePray(RabitList);
                moveTo(RabitList);
                Thread.Sleep(100);
            }
        }

        public void eat(List<Rabit> rabits)
        {
            rabits[victim].death();
            
            //rabits.Remove(rabits[victim]);
            locked= false;
            

        }

        private void ThreadAction()
        {
            Debug.WriteLine("Thread started");
            
        }

        public void Start()
        {
            WolfThread = new Thread(new ThreadStart(this.hunt));
            WolfThread.Start();
        }
    }
}
