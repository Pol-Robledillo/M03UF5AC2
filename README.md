# M03UF5AC2
## Fuentes
- ChatGPT. (s. f.-b). [ChatGPT)](https://chat.openai.com/)
- Raquel Alamán Navas. (2024, 7 abril). M03 UF5 Teoría. UF5. POO. Classes fonamentals i 
persistència en BD. M03.UF5. [Teoria](https://docs.google.com/document/d/1JJhB0bqxqns69wKZyF-s8eacxkzv55gO-ez6QvXEvhw/edit#heading=h.4heg8liyhxx)
- Pol Robledillo, Tomas Vicente, Miroslav Magerov. (2024, 7 abril). Easter Egg. Easter Egg. (https://github.com/Pol-Robledillo/EasterEgg)
## IAs
Prompt: No puedo hacer que los nombres de los atributos y de la cabecera del csv sean iguales.
Respuesta(Chat GPT): Si los nombres de los atributos en tu clase InfoComarca y los encabezados en el CSV no pueden ser iguales, necesitarás proporcionar una forma de mapear los nombres de las columnas del CSV a los nombres de las propiedades de tu clase. Puedes hacer esto utilizando los atributos Name proporcionados por la biblioteca CsvHelper. Aquí te muestro cómo hacerlo:

Primero, asegúrate de agregar el espacio de nombres necesario:
~~~
using CsvHelper.Configuration.Attributes;
~~~
Luego, modifica tu clase InfoComarca de la siguiente manera:
~~~
public class InfoComarca
{
    public int Any { get; set; }

    [Name("Codi comarca")]
    public int CodiComarca { get; set; }

    [Name("Comarca")]
    public string? NomComarca { get; set; }

    public int Poblacio { get; set; }

    [Name("Domèstic xarxa")]
    public double DomesticXarxa { get; set; }

    [Name("Activitats econòmiques i fonts pròpies")]
    public double ActivitatsEconomiques { get; set; }

    public double Total { get; set; }

    [Name("Consum domèstic per càpita")]
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
}
~~~
Con esto, los atributos Name se utilizan para indicar a CsvHelper cómo mapear las columnas del CSV a las propiedades de tu clase, incluso si los nombres no coinciden exactamente. Ahora, el código debería funcionar correctamente incluso con nombres de columnas diferentes en el CSV.
(En la practica he puesto los indices en lugar de los nombres de los campos)
