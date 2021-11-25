using EF_Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Import_Rivers_From_XML
{
    class ImportRiversFromXml
    {
        static void Main(string[] args)
        {
            Console.WriteLine(new GeographyEntities().Rivers.Count());

            var xmlDoc = XDocument.Load(@"..\..\rivers.xml");
            var riverNodes = xmlDoc.XPathSelectElements("/rivers/river");
            foreach (var riverNode in riverNodes)
            {
                string riverName = riverNode.Element("name").Value;
                int riverLength = int.Parse(riverNode.Element("length").Value);
                string riverOutflow = riverNode.Element("outflow").Value;

                int? drainageArea = null;
                if (riverNode.Element("drainage-area") != null)
                {
                    drainageArea = int.Parse(riverNode.Element("drainage-area").Value);
                }

                int? averageDischarge = null;
                if (riverNode.Element("average-discharge") != null)
                {
                    averageDischarge = int.Parse(riverNode.Element("average-discharge").Value);
                }

                Console.WriteLine("{0} {1} {2} {3} {4}", riverName, riverLength, riverOutflow, drainageArea, averageDischarge);

                var countryNodes = riverNode.XPathSelectElements("countries/country");
                var countries = countryNodes.Select(c => c.Value);

                Console.WriteLine("{0}->{1}", riverName, string.Join(",", countries));

                var context = new GeographyEntities();
                var river = new River()
                {
                    RiverName = riverName,
                    Length = riverLength,
                    Outflow = riverOutflow,
                    DrainageArea = drainageArea,
                    AverageDischarge = averageDischarge
                };
                
                

                foreach (var countryName in countries)
                {
                    var country = context.Countries.FirstOrDefault(c => c.CountryName == countryName);
                    river.Countries.Add(country);
                }
                context.Rivers.Add(river);
                context.SaveChanges();
            }

           

            Console.ReadLine();
        }
    }
}
