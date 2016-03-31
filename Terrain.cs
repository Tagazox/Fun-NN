using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Threading;

namespace Test
{
    class Terrain
    {
        List<Entiter> someEntiter;
        food[] SomeFood;
        Thread[] thread ;
        NN.LiaisonNN_Jeux Agent;
        int nbGeneration=0;
        static Mutex mtx=new Mutex();
        private void action()
        {
            
           // Thread.CurrentThread.Priority = ThreadPriority.Highest;
            while (true)
            {
               /* try
                {*/
                    mtx.WaitOne(10000, false);
                    for (int i = 0; i < someEntiter.Count; i++)
                        if (someEntiter[i].HaveEat)
                        {
                          /*  for (int j = 0; j < 4; j++)
                                someEntiter.Add(new Entiter(SomeFood, someEntiter, someEntiter[i].theEntiter.Position, Agent.Creatachildren(someEntiter[i].hisBrain)));
                            someEntiter[i].HaveEat = false;*/
                            someEntiter[i].theEntiter.FillColor = new Color(0xFF00FF);
                        }
                    lock (someEntiter)
                    {
                        foreach (Entiter entiter in someEntiter.ToArray())
                            if (entiter.isAlive)
                            {
                                entiter.ActionDelentiter();
                            }
                            else
                            {
                                someEntiter.Remove(entiter);
                                //Agent.theIa.Remove(entiter.hisBrain);
                            }
                    }

                    if (someEntiter.Count == 0)
                    {
                        Agent.iterate();
                        for (int i = 0; i < Agent.theIa.Count; i++)
                        {
                            someEntiter.Add(new Entiter(SomeFood, someEntiter, Agent.theIa[i]));
                        }
                        IHM.generation++;
                    }
                    mtx.ReleaseMutex();
                    Thread.Sleep(IHM.sleep);
                //}
                //catch (Exception e) { Console.WriteLine(e.Message); }
            }
        }

        public Terrain()
        {
            someEntiter = new List<Entiter>();
            Agent = new NN.LiaisonNN_Jeux();
            SomeFood = new food[20];
            for (int i = 0; i < SomeFood.Length; i++)
                SomeFood[i] = new food();
            for (int i = 0; i < Agent.theIa.Count; i++)
            {
                someEntiter.Add(new Entiter(SomeFood, someEntiter,Agent.theIa[i]));
            }
            thread = new Thread[10];
                for (int i = 0; i < thread.Length; i++){
                   thread[i]=new Thread(action);
                  thread[i].Start();
                }
        }
        public void draw(RenderWindow win)
        {


            
             for (int i = 0; i < SomeFood.Length; i++)
                 win.Draw(SomeFood[i].thefood);
            for (int i = 0; i < someEntiter.Count; i++)
                if (someEntiter[i].isAlive)
                {
                    win.Draw(someEntiter[i].theEntiter);
                   
                        if (IHM.DebugDraw)
                            for (int j = 0; j < someEntiter[i].LeCapteur.LeCapteur.Length; j++)
                            {
                                if (i < someEntiter.Count)
                                {

                                    RectangleShape shape = someEntiter[i].LeCapteur.LeCapteur[j];
                                    if (shape != null)
                                    {
                                        win.Draw(shape);
                                    }
                                }
                            }
                            
                    }
                }
               
        }
    }

