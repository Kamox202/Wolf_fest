using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
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
        List<Sim_Object> objects = new List<Sim_Object>();
        List<Sim_Object> DeadRabits = new List<Sim_Object>();
        List<Nest> nests = new List<Nest>();
        List<Wolf> wolfs = new List<Wolf>();
        public Random random = new Random();
        Thread t;
        




       private bool started = false, running = false, threadstarted = false;

        public MainWindow()
        {
            InitializeComponent();
            spawn_wol(null, null);
            spawn_nest(700, 100);
            spawn_nest(100, 700);
            spawn_nest(100, 100);
            spawn_nest(700, 700);
            for (int i = 0; i < 10; i++)
            {
                spawn_rab(random.Next(200, 600), random.Next(200, 600));
                
            }
        }

        int rabitID = 0;
        int nestID = 0;
        int WolfID = 1;
        

        private void spawn_rabit_from_button(object sender, RoutedEventArgs e)
        {
            spawn_rab(random.Next(200, 600), random.Next(200, 600));
        }
        public void spawn_rab(int X = 0, int Y = 0)
        {  
            Rabit rabit = new Rabit(rabitID, objects, X, Y);
            rabitID++;
            lock (this)
            {
                objects.Add(rabit);
                if (started) rabit.Start();
            }

            List<Nest> nests = new List<Nest>(objects.OfType<Nest>());
            foreach (Nest nest in nests)
            {
                nest.UpdateObjects(objects);
            }
            Paint();
        }

        private void spawn_nest(int X, int Y)
        {
            
            Nest nest = new Nest( X, Y, objects ,nestID);
            nestID++;        
            lock (this)
            {
                objects.Add(nest);
                nests.Add(nest);
                
            }
            if (started) { nest.Start(); }
            Paint();
        }
        private void spawn_wol(object sender, RoutedEventArgs e)
        {
            Wolf wolf = new Wolf("Wolf " + WolfID, objects);
            foreach(Rabit rabit in objects.OfType<Rabit>())
            {
                rabit.updateObjects(objects);
            }
            WolfID++;
            objects.Add(wolf);
            if (started) wolf.Start();
            Paint();
        }

        private void start_sim(object sender, RoutedEventArgs e)
        {
            threadstarted = true;
            running = true;
            if (!started) {
                
                t = new Thread(new ThreadStart(animate));
                
            t.Start();
        }
            started = true;

        }
        private void Paint()
        {
            Dispatcher.Invoke(() =>
            {

                List<Sim_Object> active_objects = new List<Sim_Object>(objects);
                lock (this)
                {
                    DeadRabits.Clear();
                }

                foreach (Sim_Object ob in active_objects)
                {
                    if (ob.hasBody == false)
                    {
                        ob.body = new Ellipse();
                        switch (ob.color)
                        {
                            case 0: ob.body.Fill = Brushes.Black; break;
                            case 1: ob.body.Fill = Brushes.White; break;
                            case 2: ob.body.Fill = Brushes.Brown; break;
                            default: break;

                        }
                        ob.body.Width = ob.width;
                        ob.body.Height = ob.height;
                        ob.body.HorizontalAlignment = HorizontalAlignment.Center;
                        ob.body.VerticalAlignment = VerticalAlignment.Center;
                        canvas.Children.Add(ob.body);
                        Canvas.SetLeft(ob.body, ob.x);
                        Canvas.SetTop(ob.body, ob.y);
                        ob.hasBody = true;
                    }
                    Canvas.SetLeft(ob.body, ob.x);
                    Canvas.SetTop(ob.body, ob.y);

                    
                }

                foreach (Wolf wolf in objects.OfType<Wolf>())
                {

                    
                    if (wolf.alive == false)
                    {
                        wolf.Stop();
                        DeadRabits.Add(wolf);
                    }
                }

                foreach (Rabit rabit in objects.OfType<Rabit>())
                {

                   
                    if (rabit.alive == false)
                    {
                        rabit.Stop();
                        DeadRabits.Add(rabit);

                    }
                }

                foreach (Sim_Object corpse in DeadRabits)
                {
                    lock (corpse)
                    {
                        corpse.Stop();
                        canvas.Children.Remove(corpse.body);
                        objects.Remove(corpse);
                        
                    }
                }
                if (threadstarted == true)
                {
                    foreach (Sim_Object obj in active_objects)
                    {
                        obj.Stop();
                    }
                    
                }
            });

            

        }
        private void animate()
        {
            foreach (Nest n in nests) { n.Start(); }
            foreach (Rabit rabit in objects.OfType<Rabit>())
            {
                rabit.Start();
            }
            foreach (Wolf f in objects.OfType<Wolf>()) { f.Start(); }
            Dispatcher.Invoke(() =>
            {
                
            });
                while (running)
            {
                Paint();
                List<Sim_Object> corpses = new List<Sim_Object>(DeadRabits);
             
                Thread.Sleep(100);
            }
        }
     
       

        protected override void OnClosed(EventArgs e)
        {
            running = false;
            threadstarted = false;
            started = false;
            running = false;
            foreach (Wolf wolf in objects.OfType<Wolf>())
            {
                wolf.death();

            }
            foreach (Rabit rabit in objects.OfType<Rabit>())
            { rabit.death(); }
            objects.Clear();


            foreach (Sim_Object obj in objects)
            {
                obj.Stop();
            }

            foreach (Nest obj in nests)
            {
                obj.StopNest();
            }

            Paint();

           
        }

    }
}