using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PASPNET_PijarowskiK_Wolf_Feast
{
    internal class Nest : Sim_Object
    {
        public int capacity = 5, NoOfRabits, ID, rabitID = 0;
        private Random random = new Random();
        public bool full = false;
        public bool avalible = true;
        private Thread NestThread;
        private List<Sim_Object> Objects;
        private List<Rabit> Rabits;
        private int RabitX, RabitY, RabitDistance;
        private Rabit closestRabit;
        private Canvas canvas;
        private Nest nests;
        private bool active = true;
        




        public Nest(Canvas can = null, int X = 0, int Y = 0, List<Sim_Object> Obj = null, int name = 0) {
            this.ID = name;
            
            initBody(can,X,Y,20,20,2);
            Objects = Obj;
            
        }

        public void UpdateObjects(List<Sim_Object> Obj)
        {
            Objects = Obj;
            Rabits = Objects.OfType<Rabit>().ToList();
            
            Debug.WriteLine("Zaktualizowano");
        }

            public void checkForRabitsInRange(List<Rabit> rabits)
            {
                foreach (Rabit ra in rabits)
                {
                   
                    if (ra.alerted == true && calculate_distance(ra) < 10)
                    {
                    lock (this)
                    {
                        ra.death();
                        
                        ra.alerted = false;
                        Debug.WriteLine("Królik " + ra + " showany");
                        
                    }
                      
                    }
                }
            
            }
           

        
            public void operate()
            {
           
                while (active)
                {


                    checkForRabitsInRange(Rabits);
                    Debug.WriteLine("gniazdo aktywne");
                    Thread.Sleep(100);
                }
            }
            

            public void Start()
            {
                NestThread = new Thread(new ThreadStart(operate));
                NestThread.Start();
            Debug.WriteLine("gniazdo aktywowane"+ WindowActive);
        }

        public void StopNest() { active = false; }
        }
    } 

