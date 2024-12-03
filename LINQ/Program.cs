using System.Diagnostics.Metrics;
using System.Net.WebSockets;

namespace LINQ
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var countries = new List<Country>
            {
                new Country { CountryName = "Україна", CapitalName = "Київ", Population = 41000000, Area = 603628, Continent = "Європа" },
                new Country { CountryName = "Китай", CapitalName = "Пекін", Population = 1444216107, Area = 9596961, Continent = "Азія" },
                new Country { CountryName = "Єгипет", CapitalName = "Каїр", Population = 104124440, Area = 1002450, Continent = "Африка" },
                new Country { CountryName = "Німеччина", CapitalName = "Берлін", Population = 83783942, Area = 357022, Continent = "Європа" },
                new Country { CountryName = "Японія", CapitalName = "Токіо", Population = 125960000, Area = 377975, Continent = "Азія" }
            };


            var allCountries = from country in countries
                               select new
                               {
                                   country.CountryName,
                                   country.CapitalName,
                                   country.Population,
                                   country.Area,
                                   country.Continent
                               };

            Console.WriteLine("Відобразити всю інформацію про країни");
            foreach(var country in allCountries)
            {
                Console.WriteLine("Назва"+country.CountryName);
                Console.WriteLine("Столиця" + country.CapitalName);
                Console.WriteLine("Популяция" + country.Population);
                Console.WriteLine("Площа"+"" + country.Area);
                Console.WriteLine("Континент" + country.Continent);
            }

            Console.WriteLine();
            Console.WriteLine();

            var countryNames = from country in countries
                               select country.CountryName;


            Console.WriteLine("Відобразити назви країн");
            foreach (var country in countryNames)
            {
                Console.WriteLine(country);
            }

            Console.WriteLine();
            Console.WriteLine();
            var capitalNames = from country in countries
                               select country.CapitalName;

            Console.WriteLine("Відобразити назви столиць");
            foreach (var country in capitalNames)
            {
                Console.WriteLine(country);
            }
            Console.WriteLine();
            Console.WriteLine();
            var europeanCountries = from country in countries
                                    where country.Continent == "Europe"
                                    select country.CountryName;

            Console.WriteLine("Відобразити назву усіх європейських країн");
            foreach (var country in europeanCountries)
            {
                Console.WriteLine(country);
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Відобразити назви країн з площею, більшою від певного числа");
            Console.WriteLine("Площа:");
            double minArea = double.Parse(Console.ReadLine());

            var largeAreaCountries = from country in countries
                                     where country.Area > minArea
                                     select country.CountryName;

            foreach (var country in largeAreaCountries)
            {
                Console.WriteLine(country);
            }

            Console.WriteLine();
            Console.WriteLine();
            var countriesWithAU = from country in countries
                                  where country.CountryName.Contains("а") && country.CountryName.Contains("у")
                                  select country.CountryName;

            Console.WriteLine("Відобразити усі країни, в назвах яких є літери ‘a’ та ‘у’");
            foreach (var country in countriesWithAU)
            {
                Console.WriteLine(country);
            }
            Console.WriteLine();
            Console.WriteLine();
            var countriesWithA = from country in countries
                                 where country.CountryName.Contains("а")
                                 select country.CountryName;

            Console.WriteLine("Відобразити усі країни, в назвах яких є літера ‘a’");
            foreach (var country in countriesWithA)
            {
                Console.WriteLine(country);
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Відобразити назву країн, площа яких зазначена у вказаному діапазоні");
            Console.WriteLine("Мінімальна площа:");
            double minRange = double.Parse(Console.ReadLine());
            Console.WriteLine("Макс площа:");
            double maxRange = double.Parse(Console.ReadLine());

            var countriesInAreaRange = from country in countries
                                       where country.Area >= minRange && country.Area <= maxRange
                                       select country.CountryName;


            foreach (var country in countriesInAreaRange)
            {
                Console.WriteLine(country);
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Відобразити назву країн, в яких кількість жителів більша за вказану кількість");
            Console.WriteLine("Кількість:");
            long minPopulation = long.Parse(Console.ReadLine());

            var populatedCountries = from country in countries
                                    where country.Population > minPopulation
                                    select country.CountryName;


            foreach (var country in populatedCountries)
            {
                Console.WriteLine(country);
            }
            Console.WriteLine();
            Console.WriteLine();
            var top5ByArea = (from country in countries
                              orderby country.Area descending
                              select country).Take(5);

            Console.WriteLine("Показати Топ-5 країн за площею");
            foreach (var country in top5ByArea)
            {
                Console.WriteLine(country.CountryName);
            }
            Console.WriteLine();
            Console.WriteLine();
            var top5ByPopulation = (from country in countries
                                    orderby country.Population descending
                                    select country).Take(5);

            Console.WriteLine("Показати Топ-5 країн за популярністю");
            foreach (var country in top5ByPopulation)
            {
                Console.WriteLine(country.CountryName);
            }
            Console.WriteLine();
            Console.WriteLine();
            var largestCountryByArea = (from country in countries
                                        orderby country.Area descending
                                        select country).FirstOrDefault();

            Console.WriteLine("Показати країну з найбільшою площею");
            Console.WriteLine(largestCountryByArea.CountryName);
            Console.WriteLine();
            Console.WriteLine();
            var largestCountryByPopulation = (from country in countries
                                              orderby country.Population descending
                                              select country).FirstOrDefault();

            Console.WriteLine("Показати країну з найбільшою кількістю жителів");
            Console.WriteLine(largestCountryByPopulation.CountryName);
            Console.WriteLine();
            Console.WriteLine();
            var smallestInAfrica = (from country in countries
                                    where country.Continent == "Африка"
                                    orderby country.Area ascending
                                    select country).FirstOrDefault();

            Console.WriteLine("Показати країну з найменшою площею в Африці");
            Console.WriteLine(smallestInAfrica.CountryName);
            Console.WriteLine();
            Console.WriteLine();
            var averageAreaInAsia = (from country in countries
                                     where country.Continent == "Азія"
                                     select country.Area).Average();

            Console.WriteLine("Показати середню площу країн в Азії");
            Console.WriteLine(averageAreaInAsia);

        }
    }

    public class Country
    {
        public string CountryName { get; set; }
        public string CapitalName { get; set; }
        public long Population { get; set; }
        public double Area { get; set; }
        public string Continent { get; set; }
    }
}
