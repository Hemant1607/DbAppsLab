using EF_Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Export_Monasteries_as_XML
{
    class ExportMonasteriesAsXml
    {
        static void Main(string[] args)
        {
            var context = new GeographyEntities();
            var countriesQuery = context.Countries.Where(c=>c.Monasteries.Any()).OrderBy(c => c.CountryName).Select(c => new
            {
                c.CountryName,
                Monastries = c.Monasteries.OrderBy(m => m.Name).Select(m => m.Name)
            });
            foreach (var country in countriesQuery)
            {
                Console.WriteLine(country.CountryName+" "+string.Join(",",country.Monastries));
            }

            var xmlMonastries = new XElement("monastries");

            foreach (var country in countriesQuery)
            {
                var xmlCountry = new XElement("country");
                xmlCountry.Add(new XAttribute("name", country.CountryName));
                xmlMonastries.Add(xmlCountry);
                foreach (var monastery in country.Monastries)
                {
                    xmlCountry.Add(new XElement("monastery", monastery));
                }
            }
            Console.WriteLine(xmlMonastries);
            var xmlDoc = new XDocument(xmlMonastries);
            xmlDoc.Save("monasteries.xml");

            Console.ReadLine();
        }
    }
}
