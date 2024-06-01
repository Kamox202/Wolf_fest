using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace PASPNET_PijarowskiK_Wolf_Feast
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Rabit> rabits = new List<Rabit>();
        List<Rabit> DeadRabits = new List<Rabit>();

        List<Wolf> wolfs = new List<Wolf>();

        

        bool started = false;

        public MainWindow()
        {
            InitializeComponent();

            for (int i = 0; i < 10; i++)
            {
               // spawn_rab(null, null);
            }
        }

       
        

        int rabitID = 0;
        int WolfID = 1;
        private void spawn_rab(object sender, RoutedEventArgs e)
        {
            
            Rabit rabit = new Rabit(rabitID, can);
            rabitID++;
            rabits.Add(rabit);


            Debug.WriteLine("spawn rabit button clicked");
        }
        private void spawn_wol(object sender, RoutedEventArgs e)
        {
            Wolf wolf = new Wolf("Wolf " + WolfID, can, rabits);
            WolfID++;
            wolfs.Add(wolf);

            
        }
        private void start_sim(object sender, RoutedEventArgs e)
        {
            Thread t = new Thread(new ThreadStart(animate));
            t.Start();


        }

        private void animate()
        {
            if(started) {return;}

            started = true;
            Debug.WriteLine("Kliknięto klawisz th.ID:{0}",
                           Thread.CurrentThread.ManagedThreadId);


            while (started)
            {
                Dispatcher.Invoke((Action)(() =>
                {
                    foreach (Wolf wolf in wolfs)
                    {
                        wolf.Start();
                        Canvas.SetLeft(wolf.body, wolf.x);
                        Canvas.SetTop(wolf.body, wolf.y);
                    }

                    foreach (Rabit rabit in rabits)
                    {
                        rabit.Start();
                        Canvas.SetLeft(rabit.body, rabit.x);
                        Canvas.SetTop(rabit.body, rabit.y);
                        if (rabit.alive == false)
                        {
                            DeadRabits.Add(rabit);
                        }
                    }
                    foreach (Rabit rabit in DeadRabits)
                    {
                        lock (rabit)
                        {
                            can.Children.Remove(rabit.body);
                            rabits.Remove(rabit);
                        }
                    }
                    lock (this)
                    {
                        DeadRabits.Clear();
                    }
                }));


                Thread.Sleep(1);
            }
        }

        private void stop(object sender, RoutedEventArgs e)
        {

            close(null, null);
        }
        private void close(object sender, System.ComponentModel.CancelEventArgs e)
        {
            started = false;
            Thread.Sleep(500);
        }

        }
}