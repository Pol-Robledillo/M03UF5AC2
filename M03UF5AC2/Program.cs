using System.Collections;

namespace M03UF5AC2
{
    class Program
    {
        static void Main()
        {
            const string CsvPath = @"..\..\..\files\Consum_d_aigua_a_Catalunya_per_comarques_20240402.csv";
            const string XmlPath = @"..\..\..\files\Consum_d_aigua_a_Catalunya_per_comarques_20240402.xml";
            const string SelectOptionMsg = "Selecciona una opción: \n" +
                                           "1. Identificar les comarques amb una població superior a 200000 \n" +
                                           "2. Calcular el consum domèstic mitjà per comarca \n" +
                                           "3. Mostrar les comarques amb el consum domèstic per càpita més alt \n" +
                                           "4. Mostrar les comarques amb el consum domèstic per càpita més baix \n" +
                                           "5. Filtrar les comarques per nom o codi \n" +
                                           "6. Sortir";
            const string FilterByMsg = "Selecciona una opción: \n" +
                                       "1. Filtrar per nom \n" +
                                       "2. Filtrar per codi";
            const string InputNameMsg = "Introdueix el nom de la comarca: ";
            const string InputCodeMsg = "Introdueix el codi de la comarca: ";
            const int MinOption = 1, MaxOption = 6, MinOptionFour = 1, MaxOptionFour = 2, Exit = 6;
            
            int option;

            Helper.CsvToXml(CsvPath, XmlPath);
            do
            {
                do
                {
                    Console.WriteLine(SelectOptionMsg);
                    option = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine();
                } while (Helper.CheckRange(option, MinOption, MaxOption));
                Console.WriteLine();
                switch (option)
                {
                    case 1:
                        foreach (InfoComarca comarca in Helper.GetComarquesWithPopulationOver200000(XmlPath))
                        {
                            Console.WriteLine(comarca.ToString());
                            Console.WriteLine();
                        }
                        break;
                    case 2:
                        Helper.GetAverageConsumDomesticPerCapitaForEachComarca(XmlPath);
                        break;
                    case 3:
                        foreach (InfoComarca comarca in Helper.GetHighestConsumDomesticPerCapitaForEachYear(XmlPath))
                        {
                            Console.WriteLine(comarca.ToString());
                            Console.WriteLine();
                        }
                        break;
                    case 4:
                        foreach (InfoComarca comarca in Helper.GetLowestConsumDomesticPerCapitaForEachYear(XmlPath))
                        {
                            Console.WriteLine(comarca.ToString());
                            Console.WriteLine();
                        }
                        break;
                    case 5:
                        do
                        {
                            Console.WriteLine(FilterByMsg);
                            option = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine();
                        } while (Helper.CheckRange(option, MinOptionFour, MaxOptionFour));
                        switch (option)
                        {
                            case 1:
                                Console.WriteLine(InputNameMsg);
                                string nom = Console.ReadLine();
                                Console.WriteLine();
                                foreach (InfoComarca comarca in Helper.FilterComarcasByName(XmlPath, nom))
                                {
                                    Console.WriteLine(comarca.ToString());
                                    Console.WriteLine();
                                }
                                break;
                            case 2:
                                Console.WriteLine(InputCodeMsg);
                                int codi = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine();
                                foreach (InfoComarca comarca in Helper.FilterComarcasByCode(XmlPath, codi))
                                {
                                    Console.WriteLine(comarca.ToString());
                                    Console.WriteLine();
                                }
                                break;
                        }
                        break;
                }
            } while (option != Exit);
        }
    }
}