using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;

namespace Test
{
    class Capteur
    {
        public RectangleShape[] LeCapteur { get; internal set; }
        public Capteur()
        {
            LeCapteur = new RectangleShape[5];
        }
        public List<double> getArrayValueForThisFloatrectWithThisDist(FloatRect col, Vector2f pos, float rotate, float dist)
        {
            List<double> LesValeurDucapteur=new List<double>();
            for (int i = 0; i < LeCapteur.Length; i++)
            {
                LeCapteur[i] = new RectangleShape(new Vector2f(dist, 1));
                LeCapteur[i].Position = pos;
                LeCapteur[i].Rotation = rotate+i*(100/4)-70;
                if (LeCapteur[i].GetGlobalBounds().Intersects(col))
                {
                    //  System.Console.WriteLine("true"+DateTime.Now.ToString());
                    LesValeurDucapteur.Add(1);
                }
                else
                    LesValeurDucapteur.Add(0); 

            }
            return LesValeurDucapteur;
        }
      /*  public double getArrayValueForThisFloatrectWithThisDist(FloatRect col, Vector2f pos, float rotate, float dist)
        {
            LeCapteur = new RectangleShape(new Vector2f(dist, 1));
            LeCapteur.Position = pos;
            LeCapteur.Rotation = rotate;
           // distance=col.Top()
            if (LeCapteur.GetGlobalBounds().Intersects(col))
            {
                //  System.Console.WriteLine("true"+DateTime.Now.ToString());
                return 1;
            }
            else
                return 0;
        }*/
    }
}
