namespace CountriesTask;

public record Country(
    long Id,
    string Name,
    string Continent,
    double Area,
    long Population)
{
    public override string ToString()
    {
        return $"{Name} | {Continent} | Площадь: {Area:N0} км^2 | Население: {Population:N0} чел.";
    }
}