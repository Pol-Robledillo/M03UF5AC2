using CsvHelper.Configuration.Attributes;

namespace M03UF5AC2
{
    public class InfoComarca
    {
        [Index(0)]
        public int Any { get; set; }
        [Index(1)]
        public int CodiComarca { get; set; }
        [Index(2)]
        public string? NomComarca { get; set; }
        [Index(3)]
        public int Poblacio { get; set; }
        [Index(4)]
        public double DomesticXarxa { get; set; }
        [Index(5)]
        public double ActivitatsEconomiques { get; set; }
        [Index(6)]
        public double Total { get; set; }
        [Index(7)]
        public double ConsumDomesticPerCapita { get; set; }
        public InfoComarca(int any, int codiComarca, string nomComarca, int poblacio, double domesticXarxa, double activitatsEconomiques, double total, double consumDomesticPerCapita)
        {
            Any = any;
            CodiComarca = codiComarca;
            NomComarca = nomComarca;
            Poblacio = poblacio;
            DomesticXarxa = domesticXarxa;
            ActivitatsEconomiques = activitatsEconomiques;
            Total = total;
            ConsumDomesticPerCapita = consumDomesticPerCapita;
        }
        public InfoComarca()
        {
        }
        public override string ToString()
        {
            return $"Any: {Any}, CodiComarca: {CodiComarca}, NomComarca: {NomComarca}, Poblacio: {Poblacio}, DomesticXarxa: {DomesticXarxa}, ActivitatsEconomiques: {ActivitatsEconomiques}, Total: {Total}, ConsumDomesticPerCapita: {ConsumDomesticPerCapita}";
        }
    }
}
