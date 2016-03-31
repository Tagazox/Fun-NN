using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
namespace Test
{
    class Entiter
    {
        public CircleShape theEntiter { get;  set; }
        public Capteur LeCapteur { get;internal set; }
        public NN.ReseauDeNeurones hisBrain { get;  set; }
        float acceleration = 0;
        float rotateacceleration = 0;
        food[] theFood;
        List<Entiter> Friend;
        public enum Move { Haut, Bas, Droite, Gauche,Rien };
       public bool isAlive { get;  set; }
        int nombreDemouvement = 0;
        public bool HaveEat { get; set; }
        
        public Entiter(food[] tf,List<Entiter> fr, NN.ReseauDeNeurones b)
        {
            isAlive = true;
            theFood = tf;
            hisBrain = b;
            Friend = fr;
            theEntiter = new CircleShape(3);
            LeCapteur = new Capteur();
            theEntiter.Origin = new Vector2f(theEntiter.Radius, theEntiter.Radius);
           theEntiter.Position=new Vector2f((float)Program.rand.NextDouble()* IHM.size.X, (float)Program.rand.NextDouble() * IHM.size.Y);
        }
        public Entiter(food[] tf, List<Entiter> fr,Vector2f pos, NN.ReseauDeNeurones b)
        {
            theFood = tf;
            hisBrain = b;
            Friend = fr;
            isAlive = true;
            theEntiter = new CircleShape(3);
            LeCapteur = new Capteur();
            theEntiter.Origin = new Vector2f(theEntiter.Radius, theEntiter.Radius);
            theEntiter.Position = pos;
        }
        public  void mouvement(Move s)
       {
           switch (s)
           {
               case Move.Haut:
                   acceleration+=.125f;
                   break;
               case Move.Bas:
                   acceleration -= .125f;
                   break;
               case Move.Droite:
                    rotateacceleration += 1;
                   break;
               case Move.Gauche:
                    rotateacceleration -= 1;
                   break;
               default:
                   break;
           }
           acceleration *= .99f;
            rotateacceleration *= .85f;
            theEntiter.Rotation += rotateacceleration;
            theEntiter.Position += new Vector2f((float)Math.Cos(theEntiter.Rotation * Program.angletorad), (float)Math.Sin(theEntiter.Rotation * Program.angletorad))*acceleration;
            nombreDemouvement++;

            
        }
        public void ActionDelentiter()
        {
            Move themove = new Move();
            gestionDesEntree();
            float resultatDuCerveau1=(float) hisBrain.getNeuronesSortieNumeroI(0).sortie *3;
            float resultatDuCerveau2 = (float)hisBrain.getNeuronesSortieNumeroI(1).sortie *3;
            if (resultatDuCerveau1 < -1)
                themove = Move.Bas;
            else if (resultatDuCerveau1 > 1)
                themove = Move.Haut;
            else if (resultatDuCerveau1 > -1 && resultatDuCerveau1 < 1)
                themove = Move.Rien;

            mouvement(themove);
            if (resultatDuCerveau2 <-1 )
                themove = Move.Droite;
            else if(resultatDuCerveau2>1)
                themove = Move.Gauche;
            else if (resultatDuCerveau2 > -1 && resultatDuCerveau2 < 1)
                themove = Move.Rien;
            mouvement(themove);
            testDevitaliter();
        }
        void gestionDesEntree()
        {
            List<double> entree = new List<double>();
            entree.AddRange(LeCapteur.getArrayValueForThisFloatrectWithThisDist(new FloatRect(0, 0, IHM.size.X, IHM.size.Y), theEntiter.Position, theEntiter.Rotation, 45));
            int plusprocheAmi= Quelestlamileplusproche();
            entree.AddRange(LeCapteur.getArrayValueForThisFloatrectWithThisDist(Friend[plusprocheAmi].theEntiter.GetGlobalBounds(), theEntiter.Position, theEntiter.Rotation, 250));
            int plusprochefood = Quelestlfoodleplusproche();
            entree.AddRange(LeCapteur.getArrayValueForThisFloatrectWithThisDist(theFood[plusprochefood].thefood.GetGlobalBounds(), theEntiter.Position, theEntiter.Rotation, 450));
            

           hisBrain.CalculateCouches(entree);
        }

        private int Quelestlfoodleplusproche()
        {
            float distmin = IHM.size.X * 6;
            int numEntiterRetour = 0;
            for (int i = 0; i < theFood.Length; i++)
            {
                float dist = (float)Math.Sqrt(Math.Pow(theEntiter.Position.X - theFood[i].thefood.Position.X, 2) + Math.Pow(theEntiter.Position.Y - theFood[i].thefood.Position.Y, 2));
                if (dist < distmin)
                {
                    numEntiterRetour = i;
                    distmin = dist;
                }
            }
            return numEntiterRetour;
        }

        private int Quelestlamileplusproche()
        {
            float distmin = IHM.size.X * 6;
            int numEntiterRetour = 0;
            for (int i = 0; i < Friend.Count; i++)
            {
                float dist=(float)Math.Sqrt(Math.Pow(theEntiter.Position.X - Friend[i].theEntiter.Position.X, 2) + Math.Pow(theEntiter.Position.Y - Friend[i].theEntiter.Position.Y, 2));
                if (dist < distmin)
                {
                    numEntiterRetour = i;
                    distmin = dist;
                }
            }
            return numEntiterRetour;
        }

        private void testDevitaliter()
        {
            if(nombreDemouvement>1000)
            {
                isAlive = false;
            }
            for (int i = 0; i < theFood.Length; i++)
                if (theEntiter.GetGlobalBounds().Intersects(theFood[i].thefood.GetGlobalBounds()))
                {
                    theFood[i].Repop();
                    hisBrain.score += 10;
                    nombreDemouvement = 0;
                    HaveEat = true;
                }
            if (!theEntiter.GetGlobalBounds().Intersects(new FloatRect(0, 0, IHM.size.X, IHM.size.Y)))
            {
                isAlive = false;
            }
        }
        
    }
}
