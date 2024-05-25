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
        
        
        public Wolf(string name = null, Canvas canva = null)
        {
            v = 1;
            WolfThread = new Thread(ThreadAction);
            WolfThread.Start();
            Name = name;

            initBody(canva);
            body.Fill = Brushes.Black;
           
        }
        private void AquirePray(List<Rabit> rabits)
        {

            //if (rabits.Count > 0)
            //{
            //    Pray = rabits.First();
            //    victimX = Pray.x; victimY = Pray.y;
            //    int sum = victimX - x + victimY - y;
            //    foreach (Rabit ra in rabits)
            //    {
            //        int compare = ra.x - x + ra.y - y;
            //        if (compare < sum)
            //        {
            //            victimX = ra.x;
            //            victimY = ra.y;
            //            sum = compare;
            //        }
            //    }


            //}

            if (rabits.Count > 0)
            {
                
                
                victimX = rabits[rabits.Count - 1].x;
                victimY = rabits[rabits.Count - 1].y;
            }
        }
        public void hunt(List<Rabit> rabits)
        {
            AquirePray(rabits);
            if (rabits.Count == 0) { victimX = x; victimY = y; }
            if (victimX < x) { x -= v; Canvas.SetLeft(body, x); }
            if (victimX > x) { x += v; Canvas.SetLeft(body, x); }
            if (victimY < y) { y -= v; Canvas.SetTop(body, y); }
            if (victimY > y) { y += v; Canvas.SetTop(body, y); }
            if (victimX - x == 0 && victimY - y == 0 && rabits.Count > 0)
            {
                eat(rabits);
            }
        }

        public void eat(List<Rabit> rabits)
        {
            rabits[^1].death();
            
            rabits.Remove(rabits[^1]);
            

        }

        private void ThreadAction()
        {
            Console.WriteLine("Thread started");
        }
    }
}
