using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Mappings
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new GeographyEntities();
            foreach (var continent in context.Continents)
            {
                Console.WriteLine(continent.ContinentName);
            }
            Console.ReadLine();
        }
    }
}
