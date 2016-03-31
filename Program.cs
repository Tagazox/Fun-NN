using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static public Random rand;
        static public double angletorad = (Math.PI / 180);
        static void Main(string[] args)
        {
            rand = new Random();
            Terrain theTerrain=new Terrain();
            IHM ihm = new IHM(theTerrain);
        }
    }
}
