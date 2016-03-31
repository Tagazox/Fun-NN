using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
namespace Test
{
    class food
    {
        public RectangleShape thefood { get;internal set; }

        public food()
        {
            thefood = new RectangleShape(new SFML.System.Vector2f(5, 5));
            thefood.FillColor = Color.Red;

            Repop();
        }

        public void Repop()
        {


                thefood.Position = new SFML.System.Vector2f((float)Program.rand.NextDouble() * (IHM.size.X - 64) + 64, (float)Program.rand.NextDouble() * (IHM.size.Y - 64) + 64);

            
            
        }
    }
}
