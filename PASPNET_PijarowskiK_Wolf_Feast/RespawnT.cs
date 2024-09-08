using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PASPNET_PijarowskiK_Wolf_Feast
{
    internal class RespawnT
    {
        private Thread RespawnThread;
        public Barrier Barrier;
        private bool active = true;
        private CancellationTokenSource cts = new CancellationTokenSource();

        public RespawnT(Barrier b)
        {
            Barrier = b;
        }

        public void Run(CancellationToken cts) {

            try
            {
                Barrier.SignalAndWait(cts);
            }
            catch {
                Debug.WriteLine("Program zakończony, Instrukcja SignalAndWait anulowana w celu zakończenia pracy wątków");
            }
            
        }

        public void Start()
        {
            RespawnThread = new Thread(() => Run(cts.Token));
            RespawnThread.Start();

        }

        public void Stop()
        {
            cts.Cancel();
        }
    }
}
