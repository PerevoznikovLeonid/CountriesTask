using System;
using System.Collections.Generic;
using System.Linq;
using CountriesTask;

const string dbPath = @"C:\Users\User\RiderProjects\CountriesTask\CountriesTask\countries.db";
var databaseService = new DatabaseService(dbPath);
Main(databaseService);
return;

void Main(DatabaseService dbService)
{
    while (true)
    {
        Console.Clear();
        Console.WriteLine("Система работы с БД \"Страны\"");
        Console.WriteLine($"Состояние подключения: {(dbService.IsConnected ? "ПОДКЛЮЧЕНО" : "ОТКЛЮЧЕНО")}");
        Console.WriteLine("1. Подключиться к БД");
        Console.WriteLine("2. Отключиться от БД");
        Console.WriteLine("3. Вывести отчёты");
        Console.WriteLine("0. Выход");
        Console.Write("Выберите действие: ");
        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                if (dbService.IsConnected)
                {
                    Console.WriteLine("Уже подключено.");
                    break;
                }

                var errorMessage = dbService.Connect();
                if (!dbService.IsConnected)
                {
                    Console.WriteLine($"Ошибка подключения: {errorMessage}");
                }
                
                Console.WriteLine("Подключение успешно.");
                break;
            case "2":
                dbService.Disconnect();
                Console.WriteLine("Отключено.");
                break;
            case "3":
                if (dbService.IsConnected) ShowReports();
                else Console.WriteLine("Ошибка: сначала подключитесь к базе данных.");
                break;
            case "0":
                Console.Clear();
                return;
            default:
                Console.WriteLine("Неверный ввод.");
                break;
        }
        Console.WriteLine("Нажмите любую клавишу...");
        Console.ReadKey();
    }
}

void ShowReports()
{
    while (true)
    {
        Console.Clear();
        Console.WriteLine("=== ОТЧЁТЫ ===");
        Console.WriteLine("1. Полная инфо о странах");
        Console.WriteLine("2. Частичная инфо о странах");
        Console.WriteLine("3. Инфо о конкретной стране по названию");
        Console.WriteLine("4. Инфо о городах конкретной страны по названию");
        Console.WriteLine("5. Страны на букву");
        Console.WriteLine("6. Столицы на букву");
        Console.WriteLine("7. Топ-3 столиц по населению (мин)");
        Console.WriteLine("8. Топ-3 стран по населению (мин)");
        Console.WriteLine("9. Среднее население столиц по частям света");
        Console.WriteLine("10. Топ-3 стран (мин) по частям света");
        Console.WriteLine("11. Топ-3 стран (макс) по частям света");
        Console.WriteLine("12. Среднее население городов в стране по названию");
        Console.WriteLine("13. Город с мин населением в стране по названию");
        Console.WriteLine("0. Назад");
        Console.Write("Выбор: ");
        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1": PrintCountries(databaseService.GetAllCountries()); break;
            case "2":
                Console.WriteLine("Введите кол-во стран для получения: ");
                if (int.TryParse(Console.ReadLine(), out var countryAmount))
                {
                    PrintCountries(databaseService.GetPartialCountries(countryAmount));
                    break;
                }

                Console.WriteLine("Неверный ввод.");
                break;
            case "3":
                Console.WriteLine("Введите название страны: ");
                var concreteCountryName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(concreteCountryName))
                {
                    var countryByName = databaseService.GetCountryByName(concreteCountryName);
                    if (countryByName != null)
                    {
                        Console.WriteLine(countryByName);
                        break;
                    }

                    Console.WriteLine("Страна не найдена.");
                    break;
                }

                Console.WriteLine("Неверный ввод.");
                break;
            case "4":
                Console.WriteLine("Введите название страны: ");
                var citiesCountryName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(citiesCountryName))
                {
                    var countryCities = databaseService.GetCountryCities(citiesCountryName);
                    if (countryCities != null)
                    {
                        PrintCities(countryCities);
                        break;
                    }

                    Console.WriteLine("Страна не найдена.");
                    break;
                }

                Console.WriteLine("Неверный ввод.");
                break;
            case "5":
                Console.Write("Введите букву: ");
                if (char.TryParse(Console.ReadLine(), out var countryLetter))
                {
                    PrintCountries(databaseService.GetCountriesStartingWithLetter(countryLetter));
                    break;
                }

                Console.WriteLine("Неверный ввод.");
                break;
            case "6":
                Console.Write("Введите букву: ");
                var capitalLetter = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(capitalLetter))
                {
                    var caps = databaseService.GetCapitalsStartingWithLetter(capitalLetter);
                    PrintCities(caps);
                    break;
                }

                Console.WriteLine("Неверный ввод. Буква не может быть пустой.");
                break;
            case "7":
                var top3Capitals = databaseService.GetTop3CapitalsLowestPopulation();
                PrintCities(top3Capitals);
                break;
            case "8":
                var top3CountriesLowest = databaseService.GetTop3CountriesLowestPopulation();
                PrintCountries(top3CountriesLowest);
                break;
            case "9":
                var avgCapitalPopByContinent = databaseService.GetAvgCapitalPopulationByContinent();
                Console.WriteLine("Среднее население столиц по частям света:");
                foreach (var item in avgCapitalPopByContinent)
                    Console.WriteLine($"  {item.Continent}: {item.AvgPopulation:N0} чел.");
                break;

            case "10":
                var minPerContinent = databaseService.GetTop3CountriesMinPopulationPerContinent();
                foreach (var group in minPerContinent)
                {
                    Console.WriteLine($"\n{group.Continent}:");
                    foreach (var country in group.Top3Min)
                        Console.WriteLine($"  {country}");
                }

                break;

            case "11":
                var maxPerContinent = databaseService.GetTop3CountriesMaxPopulationPerContinent();
                foreach (var group in maxPerContinent)
                {
                    Console.WriteLine($"\n{group.Continent}:");
                    foreach (var country in group.Top3Max)
                        Console.WriteLine($"  {country}");
                }

                break;

            case "12":
                Console.Write("Введите название страны: ");
                var countryForAvg = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(countryForAvg))
                {
                    var avgCityPop = databaseService.GetAverageCityPopulationInCountry(countryForAvg);
                    Console.WriteLine(avgCityPop.HasValue
                        ? $"Среднее население городов в стране '{countryForAvg}': {avgCityPop.Value:N0} чел."
                        : $"Страна '{countryForAvg}' не найдена или в ней нет городов.");
                }
                else
                {
                    Console.WriteLine("Неверный ввод.");
                }

                break;

            case "13":
                Console.Write("Введите название страны: ");
                var countryForMinCity = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(countryForMinCity))
                {
                    var minCity = databaseService.GetCityWithMinPopulationInCountry(countryForMinCity);
                    if (minCity != null)
                        Console.WriteLine(minCity);
                    else
                        Console.WriteLine($"Страна '{countryForMinCity}' не найдена или в ней нет городов.");
                }
                else
                {
                    Console.WriteLine("Неверный ввод.");
                }

                break;
            case "0":
                return;
            default:
                Console.WriteLine("Неверный ввод.");
                break;
        }

        Console.WriteLine("Нажмите любую клавишу...");
        Console.ReadKey();
    }
}

void PrintCountries(IEnumerable<Country> countries)
{
    var enumerable = countries.ToArray();
    if (enumerable.Length == 0)
        Console.WriteLine("Нет данных.");
    foreach (var c in enumerable)
        Console.WriteLine($"{c.Name} | {c.Continent} | Площадь: {c.Area:N0} | Население: {c.Population:N0}");
}

void PrintCities(IEnumerable<City> cities)
{
    var enumerable = cities.ToArray();
    if (enumerable.Length == 0)
        Console.WriteLine("Нет данных.");
    foreach (var c in enumerable)
        Console.WriteLine($" {c.Name} | Население: {c.Population:N0} чел. | {(c.IsCapital ? "Столица" : "Город")}");
}