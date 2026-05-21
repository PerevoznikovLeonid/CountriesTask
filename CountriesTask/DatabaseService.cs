using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Microsoft.Data.Sqlite;

namespace CountriesTask;

public class DatabaseService(string dbPath)
{
    private readonly string _connectionString = $"Data Source={dbPath};";
    private List<City> _cities = [];
    private List<Country> _countries = [];
    public bool IsConnected { get; private set; }

    public string? Connect()
    {
        if (IsConnected) return null;
        try
        {
            SqlMapper.AddTypeHandler(new IntToBoolHandler());
            using (var connection = new SqliteConnection(_connectionString))
            {
                _countries = connection
                    .Query<Country>("""
                                    SELECT
                                        id AS Id,
                                        name AS Name,
                                        continent AS Continent,
                                        area AS Area,
                                        population AS Population
                                    FROM table_countries
                                    """).AsList();
                _cities = connection
                    .Query<City>("""
                                 SELECT 
                                     id AS Id,
                                     name AS Name,
                                     population AS Population,
                                     country_id AS CountryId,
                                     is_capital AS IsCapital 
                                 FROM table_cities
                                 """)
                    .AsList();
            }

            IsConnected = true;
            return null;
        }
        catch (Exception ex)
        {
            IsConnected = false;
            return ex.Message;
        }
    }

    public void Disconnect()
    {
        _countries.Clear();
        _cities.Clear();
        IsConnected = false;
    }

    public IEnumerable<Country> GetAllCountries()
    {
        return _countries;
    }

    public IEnumerable<Country> GetPartialCountries(int countryAmount)
    {
        return _countries.Take(countryAmount);
    }

    public Country? GetCountryByName(string countryName)
    {
        return _countries.FirstOrDefault(c =>
            c.Name.Equals(countryName, StringComparison.OrdinalIgnoreCase));
    }

    public IEnumerable<City>? GetCountryCities(string countryName)
    {
        var country = GetCountryByName(countryName);
        return country == null
            ? null
            : _cities.Where(c => c.CountryId == country.Id);
    }

    public IEnumerable<Country> GetCountriesStartingWithLetter(char letter)
    {
        return _countries.Where(c => c.Name.StartsWith(letter.ToString(), StringComparison.OrdinalIgnoreCase));
    }

    public IEnumerable<City> GetCapitalsStartingWithLetter(string letter)
    {
        return _cities.Where(c => c.IsCapital && c.Name.StartsWith(letter, StringComparison.OrdinalIgnoreCase));
    }

    public IEnumerable<City> GetTop3CapitalsLowestPopulation()
    {
        return _cities.Where(c => c.IsCapital).OrderBy(c => c.Population).Take(3);
    }

    public IEnumerable<Country> GetTop3CountriesLowestPopulation()
    {
        return _countries.OrderBy(c => c.Population).Take(3);
    }

    public IEnumerable<(string Continent, double AvgPopulation)> GetAvgCapitalPopulationByContinent()
    {
        var query = from country in _countries
            join capital in _cities on country.Id equals capital.CountryId
            where capital.IsCapital
            group capital by country.Continent
            into g
            select (Continent: g.Key, AvgPopulation: g.Average(c => c.Population));

        return query.ToList();
    }

    public IEnumerable<(string Continent, IEnumerable<Country> Top3Min)> GetTop3CountriesMinPopulationPerContinent()
    {
        return _countries.GroupBy(c => c.Continent)
            .Select(g => (Continent: g.Key,
                Top3Min: g.OrderBy(c => c.Population).Take(3)));
    }

    public IEnumerable<(string Continent, IEnumerable<Country> Top3Max)> GetTop3CountriesMaxPopulationPerContinent()
    {
        return _countries.GroupBy(c => c.Continent)
            .Select(g => (Continent: g.Key,
                Top3Max: g.OrderByDescending(c => c.Population).Take(3)));
    }

    public double? GetAverageCityPopulationInCountry(string countryName)
    {
        var countryCities = GetCountryCities(countryName);
        return countryCities?.Average(c => c.Population);
    }

    public City? GetCityWithMinPopulationInCountry(string countryName)
    {
        var countryCities = GetCountryCities(countryName);
        return countryCities?.OrderBy(c => c.Population).FirstOrDefault();
    }
}