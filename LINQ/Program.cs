using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Diagnostics;
using System.Linq;

namespace LINQ
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string connectionString =
               @"Data Source=LINK\SQLEXPRESS;Initial Catalog=Krainy;Integrated Security=True;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;";

            using (var connection = new SqlConnection(connectionString))
            {
                List<Country> countries = null;
                try
                {
                    Console.WriteLine("Підключення до бази даних успішне.");
                    ConnectToDatabase(connection, connectionString);

                    string query = "SELECT * FROM Countries";
                    using (DbCommand command = connection.CreateCommand())
                    {
                        command.CommandText = query;
                        command.CommandType = CommandType.Text;

                        using (DbDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                countries.Add(
                                   new Country()
                                   {
                                       CountryName = reader.GetString(1) ?? default,
                                       CapitalName = reader.GetString(2) ?? default,
                                       Population = reader.GetInt64(3),
                                       Area = reader.GetDouble(4),
                                       Continent = reader.GetString(5) ?? default,

                                   });
                            }
                        }
                    }


                    while (true)
                    {
                        while (true)
                        {
                            Console.Clear();
                            Console.WriteLine("x - Підключитися до бази даних");
                            Console.WriteLine("c - Відключитися від бази даних");
                            Console.WriteLine("1. Відобразити всю інформацію про країни");
                            Console.WriteLine("2. Відобразити назви країн");
                            Console.WriteLine("3. Відобразити назви столиць");
                            Console.WriteLine("4. Відобразити європейські країни");
                            Console.WriteLine("5. Відобразити країни з площею більше заданого числа");
                            Console.WriteLine("6. Відобразити країни з літерами 'а' та 'у'");
                            Console.WriteLine("7. Відобразити країни в діапазоні площі");
                            Console.WriteLine("8. Топ-5 країн за площею");
                            Console.WriteLine("9. Країна з найбільшою площею");
                            Console.WriteLine("10. Відобразити повну інформацію про країни");
                            Console.WriteLine("11. Відобразити часткову інформацію про країни");
                            Console.WriteLine("12. Відобразити інформацію про конкретну країну");
                            Console.WriteLine("13. Відобразити міста конкретної країни");
                            Console.WriteLine("14. Відобразити країни за літерою");
                            Console.WriteLine("15. Відобразити столиці за літерою");
                            Console.WriteLine("16. Топ-3 столиці з найменшим населенням");
                            Console.WriteLine("17. Топ-3 країни з найменшим населенням");
                            Console.WriteLine("18. Середнє населення столиць за континентами");
                            Console.WriteLine("19. Топ-3 країни по континентах з найменшим населенням");
                            Console.WriteLine("20. Топ-3 країни по континентах з найбільшим населенням");
                            Console.WriteLine("21. Середнє населення у країні");
                            Console.WriteLine("22. Місто з найменшим населенням у країні");
                            Console.WriteLine("0. Вийти");
                            Console.WriteLine("Виберіть опцію:");

                            string choice = Console.ReadLine();

                            switch (choice)
                            {
                                case "x":
                                    ConnectToDatabase(connection, connectionString);
                                    break;
                                case "c":
                                    DisconnectFromDatabase(connection);
                                    break;
                                case "1":
                                    ShowCountryNames(countries);
                                    break;
                                case "2":
                                    ShowCountryNames(countries);
                                    break;
                                case "3":
                                    ShowCapitalNames(countries);
                                    break;
                                case "4":
                                    ShowEuropeanCountries(countries);
                                    break;
                                case "5":
                                    ShowLargeAreaCountries(countries);
                                    break;
                                case "6":
                                    ShowCountriesWithAU(countries);
                                    break;
                                case "7":
                                    ShowCountriesInAreaRange(countries);
                                    break;
                                case "8":
                                    ShowTop5ByArea(countries);
                                    break;
                                case "9":
                                    ShowLargestCountryByArea(countries);
                                    break;
                                case "10":
                                    DisplayFullInfo(countries);
                                    break;
                                case "11":
                                    DisplayPartialInfo(countries);
                                    break;
                                case "12":
                                    DisplayCountryInfo(countries);
                                    break;
                                case "13":
                                    DisplayCitiesInCountry(countries);
                                    break;
                                case "14":
                                    DisplayCountriesByLetter(countries);
                                    break;
                                case "15":
                                    DisplayCapitalsByLetter(countries);
                                    break;
                                case "16":
                                    DisplayTop3SmallestCapitals(countries);
                                    break;
                                case "17":
                                    DisplayTop3SmallestCountries(countries);
                                    break;
                                case "18":
                                    DisplayAveragePopulationByContinent(countries);
                                    break;
                                case "19":
                                    DisplayTop3SmallestByContinent(countries);
                                    break;
                                case "20":
                                    DisplayTop3LargestByContinent(countries);
                                    break;
                                case "21":
                                    DisplayAveragePopulationInCountry(countries);
                                    break;
                                case "22":
                                    DisplaySmallestCityInCountry(countries);
                                    break;
                                case "0":
                                    return;
                                default:
                                    Console.WriteLine("Неправильний вибір, спробуйте ще раз.");
                                    break;
                            }

                            Console.WriteLine("\nНатисніть будь-яку клавішу для продовження...");
                            Console.ReadKey();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Помилка підключення: {ex.Message}");
                }
            }

            static void ConnectToDatabase(SqlConnection connection, string connectionString)
            {
                if (connection == null)
                {
                    connection = new SqlConnection(connectionString);
                }

                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                    Console.WriteLine("Успішно підключено до бази даних!");
                }
                else
                {
                    Console.WriteLine("Ви вже підключені до бази даних.");
                }
            }

            static void DisconnectFromDatabase(SqlConnection connection)
            {
                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                    Console.WriteLine("Підключення до бази даних закрито.");
                }
                else
                {
                    Console.WriteLine("Підключення до бази даних не активне.");
                }
            }

            static void ShowCountryNames(List<Country> countries)
            {
                var countryNames = from country in countries
                                   select country.CountryName;

                foreach (var country in countryNames)
                {
                    Console.WriteLine(country);
                }
            }

            static void ShowCapitalNames(List<Country> countries)
            {
                var capitalNames = from country in countries
                                   select country.CapitalName;

                foreach (var capital in capitalNames)
                {
                    Console.WriteLine(capital);
                }
            }

            static void ShowEuropeanCountries(List<Country> countries)
            {
                var europeanCountries = from country in countries
                                        where country.Continent == "Європа"
                                        select country.CountryName;

                foreach (var country in europeanCountries)
                {
                    Console.WriteLine(country);
                }
            }

            static void ShowLargeAreaCountries(List<Country> countries)
            {
                Console.WriteLine("Введіть мінімальну площу:");
                double minArea = double.Parse(Console.ReadLine());

                var largeAreaCountries = from country in countries
                                         where country.Area > minArea
                                         select country.CountryName;

                foreach (var country in largeAreaCountries)
                {
                    Console.WriteLine(country);
                }
            }

            static void ShowCountriesWithAU(List<Country> countries)
            {
                var countriesWithAU = from country in countries
                                      where country.CountryName.Contains("а") && country.CountryName.Contains("у")
                                      select country.CountryName;

                foreach (var country in countriesWithAU)
                {
                    Console.WriteLine(country);
                }
            }

            static void ShowCountriesInAreaRange(List<Country> countries)
            {
                Console.WriteLine("Введіть мінімальну площу:");
                double minRange = double.Parse(Console.ReadLine());
                Console.WriteLine("Введіть максимальну площу:");
                double maxRange = double.Parse(Console.ReadLine());

                var countriesInAreaRange = from country in countries
                                           where country.Area >= minRange && country.Area <= maxRange
                                           select country.CountryName;

                foreach (var country in countriesInAreaRange)
                {
                    Console.WriteLine(country);
                }
            }

            static void ShowTop5ByArea(List<Country> countries)
            {
                var top5Countries = (from country in countries
                                     orderby country.Area descending
                                     select country).Take(5);

                foreach (var country in top5Countries)
                {
                    Console.WriteLine(country.CountryName);
                }
            }

            static void ShowLargestCountryByArea(List<Country> countries)
            {
                var largestCountry = (from country in countries
                                      orderby country.Area descending
                                      select country).FirstOrDefault();

                if (largestCountry != null)
                    Console.WriteLine($"Найбільша країна: {largestCountry.CountryName}");
            }

            static void DisplayFullInfo(List<Country> countries)
            {
                countries.ForEach(country =>
                {
                    Console.WriteLine($"Країна: {country.CountryName}, Столиця: {country.CapitalName}, " +
                                      $"Населення: {country.Population}, Площа: {country.Area}, " +
                                      $"Континент: {country.Continent}");
                });
            }


            static void DisplayPartialInfo(List<Country> countries)
            {
                Console.WriteLine("Введіть кількість полів для відображення (1-5):");
                int fields = int.Parse(Console.ReadLine());

                countries.ForEach(country =>
                {
                    Console.WriteLine($"{(fields >= 1 ? $"Країна: {country.CountryName}" : "")} " +
                                      $"{(fields >= 2 ? $", Столиця: {country.CapitalName}" : "")} " +
                                      $"{(fields >= 3 ? $", Населення: {country.Population}" : "")} " +
                                      $"{(fields >= 4 ? $", Площа: {country.Area}" : "")} " +
                                      $"{(fields >= 5 ? $", Континент: {country.Continent}" : "")}");
                });
            }


            static void DisplayCountryInfo(List<Country> countries)
            {
                Console.WriteLine("Введіть назву країни:");
                string name = Console.ReadLine();

                var country = countries.FirstOrDefault(c => c.CountryName.Equals(name, StringComparison.OrdinalIgnoreCase));
                if (country != null)
                {
                    Console.WriteLine($"Країна: {country.CountryName}, Столиця: {country.CapitalName}, " +
                                      $"Населення: {country.Population}, Площа: {country.Area}, " +
                                      $"Континент: {country.Continent}");
                }
                else
                {
                    Console.WriteLine("Країна не знайдена.");
                }
            }


            static void DisplayCitiesInCountry(List<Country> countries)
            {
                Console.WriteLine("Введите название страны:");
                string countryName = Console.ReadLine();

                var country = countries.FirstOrDefault(c => c.CountryName.Equals(countryName, StringComparison.OrdinalIgnoreCase));

                if (country != null)
                {
                    Console.WriteLine($"Города страны {country.CountryName}: {country.CapitalName} (столица)");
                }
                else
                {
                    Console.WriteLine("Страна не найдена.");
                }
            }

            static void DisplayCountriesByLetter(List<Country> countries)
            {
                Console.WriteLine("Введіть літеру:");
                char letter = Console.ReadLine()[0];

                countries.Where(c => c.CountryName.StartsWith(letter.ToString(), StringComparison.OrdinalIgnoreCase))
                         .ToList()
                         .ForEach(c => Console.WriteLine(c.CountryName));
            }
            static void DisplayCapitalsByLetter(List<Country> countries)
            {
                Console.WriteLine("Введіть літеру:");
                char letter = Console.ReadLine()[0];

                countries.Where(c => c.CapitalName.StartsWith(letter.ToString(), StringComparison.OrdinalIgnoreCase))
                         .ToList()
                         .ForEach(c => Console.WriteLine(c.CapitalName));
            }
            static void DisplayTop3SmallestCapitals(List<Country> countries)
            {
                var smallestCapitals = countries
                    .OrderBy(c => c.Population)
                    .Take(3)
                    .Select(c => new { c.CapitalName, c.Population });

                Console.WriteLine("Топ-3 столиці с найменшим населением:");
                foreach (var capital in smallestCapitals)
                {
                    Console.WriteLine($"{capital.CapitalName}: {capital.Population} чел.");
                }
            }

            static void DisplayTop3SmallestCountries(List<Country> countries)
            {
                countries.OrderBy(c => c.Population)
                         .Take(3)
                         .ToList()
                         .ForEach(c => Console.WriteLine($"Країна: {c.CountryName}, Населення: {c.Population}"));
            }
            static void DisplayAveragePopulationByContinent(List<Country> countries)
            {
                var averages = countries.GroupBy(c => c.Continent)
                                        .Select(g => new
                                        {
                                            Continent = g.Key,
                                            AveragePopulation = g.Average(c => c.Population)
                                        });

                foreach (var avg in averages)
                {
                    Console.WriteLine($"Континент: {avg.Continent}, Середнє населення: {avg.AveragePopulation}");
                }
            }
            static void DisplayTop3SmallestByContinent(List<Country> countries)
            {
                var result = countries.GroupBy(c => c.Continent)
                                      .Select(g => new
                                      {
                                          Continent = g.Key,
                                          Countries = g.OrderBy(c => c.Population).Take(3)
                                      });

                foreach (var group in result)
                {
                    Console.WriteLine($"Континент: {group.Continent}");
                    group.Countries.ToList().ForEach(c => Console.WriteLine($"  {c.CountryName}, Населення: {c.Population}"));
                }
            }
            static void DisplayTop3LargestByContinent(List<Country> countries)
            {
                var result = countries.GroupBy(c => c.Continent)
                                      .Select(g => new
                                      {
                                          Continent = g.Key,
                                          Countries = g.OrderByDescending(c => c.Population).Take(3)
                                      });

                foreach (var group in result)
                {
                    Console.WriteLine($"Континент: {group.Continent}");
                    group.Countries.ToList().ForEach(c => Console.WriteLine($"  {c.CountryName}, Населення: {c.Population}"));
                }
            }
            static void DisplayAveragePopulationInCountry(List<Country> countries)
            {
                Console.WriteLine("Введіть назву країни:");
                string name = Console.ReadLine();

                var country = countries.FirstOrDefault(c => c.CountryName.Equals(name, StringComparison.OrdinalIgnoreCase));
                if (country != null)
                {
                    Console.WriteLine($"Середня кількість мешканців у {country.CountryName}: {country.Population / 2}");
                }
            }
            static void DisplaySmallestCityInCountry(List<Country> countries)
            {
                Console.WriteLine("Введіть назву країни:");
                string countryName = Console.ReadLine();

                var country = countries.FirstOrDefault(c => c.CountryName.Equals(countryName, StringComparison.OrdinalIgnoreCase));

                if (country != null)
                {
                    Console.WriteLine($"Город з найменшим населенням країни {country.CountryName}: {country.CapitalName} ({country.Population} чел.)");
                }
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
}
