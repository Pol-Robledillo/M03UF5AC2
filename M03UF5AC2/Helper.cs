using CsvHelper;
using System.Globalization;
using System.Xml.Linq;

namespace M03UF5AC2
{
    public static class Helper
    {
        public static void CsvToXml(string csvPath, string xmlPath)
        {
            const string FileAlreadyExistsMsg = "El fitxer ja existeix i conté informació";
            if (File.Exists(xmlPath))
            {
                XDocument doc = XDocument.Load(xmlPath);
                if (doc.Root == null)
                {
                    SaveDataToXml(GetDataFromCsv(csvPath), xmlPath);
                } else
                {
                    Console.WriteLine(FileAlreadyExistsMsg);
                }
            }
            else
            {
                SaveDataToXml(GetDataFromCsv(csvPath), xmlPath);
            }
        }
        public static List<InfoComarca> GetDataFromCsv(string csvPath)
        {
            using var reader = new StreamReader(csvPath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            return csv.GetRecords<InfoComarca>().ToList();
        }
        public static void SaveDataToXml(List<InfoComarca> infoComarcas, string xmlPath)
        {
            const string SuccessMsg = "Dades emmagatzemades correctament";
            const string ErrorMsg = "Error: {0}";
            try
            {
                XDocument doc;

                if (File.Exists(xmlPath))
                {
                    doc = XDocument.Load(xmlPath);

                    if (doc.Root == null)
                    {
                        doc.Add(new XElement("Comarcas"));
                    }
                }
                else
                {
                    doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), new XElement("Comarcas"));
                }
                foreach (InfoComarca infoComarca in infoComarcas)
                {
                    XElement comarca = 
                        new XElement("Comarca",
                        new XElement("Any", infoComarca.Any),
                        new XElement("CodiComarca", infoComarca.CodiComarca),
                        new XElement("NomComarca", infoComarca.NomComarca),
                        new XElement("Poblacio", infoComarca.Poblacio),
                        new XElement("DomesticXarxa", infoComarca.DomesticXarxa),
                        new XElement("ActivitatsEconomiques", infoComarca.ActivitatsEconomiques),
                        new XElement("Total", infoComarca.Total),
                        new XElement("ConsumDomesticPerCapita", infoComarca.ConsumDomesticPerCapita));
                    doc.Root.Add(comarca);
                }
                doc.Save(xmlPath);
                Console.WriteLine();
                Console.WriteLine(SuccessMsg);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ErrorMsg, ex.Message);
            }
        }
        public static bool CheckRange(int option, int min, int max)
        {
            if (option < min || option > max)
            {
                Console.WriteLine("Opció no vàlida");
                return true;
            } else
            {
                return false;
            }
        }
        public static List<InfoComarca> GetComarquesWithPopulationOver200000(string xmlPath)
        {
            XDocument doc = XDocument.Load(xmlPath);
            var comarcas = from comarca in doc.Descendants("Comarca")
                           where (int)comarca.Element("Poblacio") > 200000
                           select new InfoComarca 
                           { 
                               Any = (int)comarca.Element("Any"),
                               CodiComarca = (int)comarca.Element("CodiComarca"),
                               NomComarca = (string)comarca.Element("NomComarca"),
                               Poblacio = (int)comarca.Element("Poblacio"),
                               DomesticXarxa = (double)comarca.Element("DomesticXarxa"),
                               ActivitatsEconomiques = (double)comarca.Element("ActivitatsEconomiques"),
                               Total = (double)comarca.Element("Total"),
                               ConsumDomesticPerCapita = (double)comarca.Element("ConsumDomesticPerCapita")
                           };
            return comarcas.ToList();
        }
        public static void GetAverageConsumDomesticPerCapitaForEachComarca(string xmlPath)
        {
            XDocument doc = XDocument.Load(xmlPath);

            var comarcas = from comarca in doc.Descendants("Comarca")
                           group comarca by (string)comarca.Element("NomComarca") into g
                           select new
                           {
                               NomComarca = g.Key,
                               ConsumDomesticPerCapita = g.Average(c => (double)c.Element("ConsumDomesticPerCapita"))
                           };
            foreach (var comarca in comarcas)
            {
                Console.WriteLine($"Comarca: {comarca.NomComarca}, Consum domèstic per càpita: {comarca.ConsumDomesticPerCapita}");
                Console.WriteLine();
            }
        }
        public static List<InfoComarca> GetHighestConsumDomesticPerCapitaForEachYear(string xmlPath)
        {
            XDocument doc = XDocument.Load(xmlPath);
            List<InfoComarca> comarcas = new List<InfoComarca>();
            for (int i = 2022; i > 2011; i--)
            {
                var comarcaYear = from comarca in doc.Descendants("Comarca")
                               where (int)comarca.Element("Any") == i && (double)comarca.Element("ConsumDomesticPerCapita") == doc.Descendants("Comarca").Where(c => (int)c.Element("Any") == i).Max(c => (double)c.Element("ConsumDomesticPerCapita"))
                               select new InfoComarca
                               {
                                   Any = (int)comarca.Element("Any"),
                                   CodiComarca = (int)comarca.Element("CodiComarca"),
                                   NomComarca = (string)comarca.Element("NomComarca"),
                                   Poblacio = (int)comarca.Element("Poblacio"),
                                   DomesticXarxa = (double)comarca.Element("DomesticXarxa"),
                                   ActivitatsEconomiques = (double)comarca.Element("ActivitatsEconomiques"),
                                   Total = (double)comarca.Element("Total"),
                                   ConsumDomesticPerCapita = (double)comarca.Element("ConsumDomesticPerCapita")
                               };
                comarcas.Add(comarcaYear.ToList()[0]);
            }
            return comarcas;
        }
        public static List<InfoComarca> GetLowestConsumDomesticPerCapitaForEachYear(string xmlPath)
        {
            XDocument doc = XDocument.Load(xmlPath);
            List<InfoComarca> comarcas = new List<InfoComarca>();
            for (int i = 2022; i > 2011; i--)
            {
                var comarcaYear = from comarca in doc.Descendants("Comarca")
                                  where (int)comarca.Element("Any") == i && (double)comarca.Element("ConsumDomesticPerCapita") == doc.Descendants("Comarca").Where(c => (int)c.Element("Any") == i).Min(c => (double)c.Element("ConsumDomesticPerCapita"))
                                  select new InfoComarca
                                  {
                                      Any = (int)comarca.Element("Any"),
                                      CodiComarca = (int)comarca.Element("CodiComarca"),
                                      NomComarca = (string)comarca.Element("NomComarca"),
                                      Poblacio = (int)comarca.Element("Poblacio"),
                                      DomesticXarxa = (double)comarca.Element("DomesticXarxa"),
                                      ActivitatsEconomiques = (double)comarca.Element("ActivitatsEconomiques"),
                                      Total = (double)comarca.Element("Total"),
                                      ConsumDomesticPerCapita = (double)comarca.Element("ConsumDomesticPerCapita")
                                  };
                comarcas.Add(comarcaYear.ToList()[0]);
            }
            return comarcas;
        }
        public static List<InfoComarca> FilterComarcasByName(string xmlPath, string name)
        {
            XDocument doc = XDocument.Load(xmlPath);
            var comarcas = from comarca in doc.Descendants("Comarca")
                           where (string)comarca.Element("NomComarca") == name
                           select new InfoComarca
                           {
                               Any = (int)comarca.Element("Any"),
                               CodiComarca = (int)comarca.Element("CodiComarca"),
                               NomComarca = (string)comarca.Element("NomComarca"),
                               Poblacio = (int)comarca.Element("Poblacio"),
                               DomesticXarxa = (double)comarca.Element("DomesticXarxa"),
                               ActivitatsEconomiques = (double)comarca.Element("ActivitatsEconomiques"),
                               Total = (double)comarca.Element("Total"),
                               ConsumDomesticPerCapita = (double)comarca.Element("ConsumDomesticPerCapita")
                           };
            return comarcas.ToList();
        }
        public static List<InfoComarca> FilterComarcasByCode(string xmlPath, int code)
        {
            XDocument doc = XDocument.Load(xmlPath);
            var comarcas = from comarca in doc.Descendants("Comarca")
                           where (int)comarca.Element("CodiComarca") == code
                           select new InfoComarca
                           {
                               Any = (int)comarca.Element("Any"),
                               CodiComarca = (int)comarca.Element("CodiComarca"),
                               NomComarca = (string)comarca.Element("NomComarca"),
                               Poblacio = (int)comarca.Element("Poblacio"),
                               DomesticXarxa = (double)comarca.Element("DomesticXarxa"),
                               ActivitatsEconomiques = (double)comarca.Element("ActivitatsEconomiques"),
                               Total = (double)comarca.Element("Total"),
                               ConsumDomesticPerCapita = (double)comarca.Element("ConsumDomesticPerCapita")
                           };
            return comarcas.ToList();
        }
    }
}
