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
        private List<Sim_Object> Objects, ObjectsCopy;
        protected Random random = new Random();
        public Barrier wolfBarrier;
        MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
        private List<RespawnT> respawnThreads = new List<RespawnT>();


        public Wolf(string name, Canvas canva, List<Sim_Object> Prays)
        {
            this.Objects = Prays;
            RabitList = Prays.OfType<Rabit>().ToList();
            this.v = 5;
            Name = name;
            initBody(canva,400,400);
            
            wolfBarrier = new Barrier(3, RespawnRabits);
            

        }

        private void RespawnRabits(Barrier barrier)
        {
            for (int i = 0; i < wolfBarrier.ParticipantCount * 2; i++)
            { 
            mainWindow.spawn_rab(random.Next(200, 600), random.Next(200, 600));
            }

        }
        private void AquirePray(List<Rabit> rabits)
        {

            if (rabits.Count > 0)
            {
                //victim = rabits.FindIndex(r => r.x == rabits.Min((rab => rab.x))-x);

                int sum = (int)Math.Sqrt(Math.Pow(this.x - rabits.First().x, 2) + Math.Pow(this.y - rabits.First().y, 2));
                
                if (rabits.First().hiden == false)
                {
                    victimX = rabits.First().x;
                    victimY = rabits.First().y;
                }
                else
                {
                    victimX = this.x;
                    victimY = this.y;
                    sum = 10000;
                }
                
                foreach (Rabit ra in rabits)
                {
                    int compare = (int)Math.Sqrt(Math.Pow(this.x - ra.x, 2) + Math.Pow(this.y - ra.y, 2));
                    if (compare < sum && ra.hiden == false)
                    {
                        victimX = ra.x;
                        victimY = ra.y;
                        sum = compare;
                        Pray = ra;
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
            

            if (Math.Abs(victimX - x) <= 2 && Math.Abs(victimY - y) <= 2 )
            {
                eat(rabits);
                locked = false;
            }
        }

        public void Return()
        {
            int dx = 450 - x;
            int dy = 450 - y;
            int distance = (int)Math.Sqrt(Math.Pow(dx * dx, 2) + Math.Pow(dy * dy, 2));

            if (dx != 0) dx = dx / Math.Abs(dx); // Normalizacja do -1, 0, 1
            if (dy != 0) dy = dy / Math.Abs(dy); // Normalizacja do -1, 0, 1


            x += dx * v;
            y += dy * v;
            if(Math.Abs(victimX - x) <= 2 && Math.Abs(victimY - y) <= 2 )
            { Thread.Sleep(100); }

        }

        public void hunt()
        {
         while (WindowActive)  
            {while (alive)
                {
                    ObjectsCopy = new List<Sim_Object>(Objects);
                    RabitList = new List<Rabit>(ObjectsCopy.OfType<Rabit>().ToList());
                    if (RabitList.Count == 0) { Return(); }
                    else
                    {
                        if (!locked) AquirePray(RabitList);
                        moveTo(RabitList);
                    }
                   // Debug.WriteLine("Willk: " + this.Name + " Aktywny");
                    Thread.Sleep(100);
                }
            }
         foreach (RespawnT RT in respawnThreads)
            {
                RT.Stop();
            }
        }

        public void eat(List<Rabit> rabits)
        {
            rabits[victim].death();
            
            //rabits.Remove(rabits[victim]);
            locked= false;
            lock (this)
            {
                RespawnT respawn = new RespawnT(wolfBarrier);
                respawnThreads.Add(respawn);
                respawn.Start();

            }
            Thread.Sleep(500);
            

        }

        public void Start()
        {
            WolfThread = new Thread(new ThreadStart(this.hunt));
            WolfThread.Start();
        }
    }
}
