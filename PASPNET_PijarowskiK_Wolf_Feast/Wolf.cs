using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASPNET_PijarowskiK_Wolf_Feast
{
    internal class Wolf : Animal
    {
        private Thread WolfThread;
        public Wolf() {
            v = 10;
            WolfThread = new Thread(ThreadAction);
            WolfThread.Start();
        }



        private void ThreadAction()
        {
            Console.WriteLine("Thread started");
        }
    }
}
