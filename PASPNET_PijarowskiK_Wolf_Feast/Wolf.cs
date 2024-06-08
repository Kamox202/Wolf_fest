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
        
        
        public Wolf(string name = null, Canvas canva = null, List<Rabit> Prays = null)
        {
            RabitList = Prays;
            v = 3;
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
                //var minElement = rabits
                //.Select((value, index) => new { Value = value, Index = index })
                //.OrderBy(r => r.Value.x)
                //.ThenBy(r => r.Value.y)
                //.FirstOrDefault();
                //victim = 0;
                //victimX = rabits[victim].x; victimY = rabits[victim].y;



                //foreach (Rabit ra in rabits)
                //{
                //    int compare = ra.x - x + ra.y - y;
                //    if (compare < sum)
                //    {
                //        victimX = ra.x;
                //        victimY = ra.y;
                //        sum = compare;
                //        victim = ra.ID;
                //    }
                //    i++;
                //}


            }

            
        }

        private void moveTo(List<Rabit> rabits)
        {
            if (rabits.Count == 0) { victimX = x; victimY = y; }
            //if (victimX < x) { x -= v; Canvas.SetLeft(body, x); }
            //if (victimX > x) { x += v; Canvas.SetLeft(body, x); }
            //if (victimY < y) { y -= v; Canvas.SetTop(body, y); }
            //if (victimY > y) { y += v; Canvas.SetTop(body, y); }
            int dx = victimX - x ;
            int dy = victimY - y ;
            int distance = (int)Math.Sqrt(Math.Pow(dx * dx,2) + Math.Pow(dy * dy,2));

            Debug.WriteLine(dx + " " + dy + " " + distance);

            if (dx != 0) dx = dx / Math.Abs(dx); // Normalizacja do -1, 0, 1
            if (dy != 0) dy = dy / Math.Abs(dy); // Normalizacja do -1, 0, 1
            Debug.WriteLine(dx + " " + dy + " " + distance);

            x += dx * v;
            y += dy * v;
            

            if (Math.Abs(victimX - x) <= 1 && Math.Abs(victimY - y) <= 1 && rabits.Count > 0)
            {
                eat(rabits);
            }
        }
        public void hunt()
        {
            while (RabitList.Count > 0)
            {
                if (!locked) AquirePray(RabitList);
                moveTo(RabitList);
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
