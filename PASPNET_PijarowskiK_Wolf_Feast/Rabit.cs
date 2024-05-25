using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PASPNET_PijarowskiK_Wolf_Feast
{
    internal class Rabit : Animal
    {
        private Thread RabitThread;
        public Rabit()
        {
            v = 10;
            RabitThread = new Thread(ThreadAction);
            RabitThread.Start();
        }



        private void ThreadAction()
        {
            Console.WriteLine("Thread started");
        }
    }
}
