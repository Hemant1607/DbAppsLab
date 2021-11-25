using EF_Mappings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Export_Rivers_as_JSON
{
    class ExportRiversAsJson
    {
        private static object riversQuery;

        static void Main(string[] args)
        {
            var context = new GeographyEntities();
            var rivers = context.Rivers.OrderByDescending(r=>r.Length).Select(r => new
            {
                r.RiverName,
                r.Length,
                Countries = r.Countries.OrderBy(c=>c.CountryName).Select(c => c.CountryName)
            });
            foreach (var river in rivers)
            {
                Console.WriteLine(river.RiverName);
            }

            var jsSerializer = new JavaScriptSerializer();
            var riversJson = jsSerializer.Serialize(rivers.ToList());
            Console.WriteLine(riversJson);

            File.WriteAllText("rivers.json", riversJson);

            Console.ReadLine();
        }
    }
}
