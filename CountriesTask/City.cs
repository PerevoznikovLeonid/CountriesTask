namespace CountriesTask;

public record City(
    long Id,
    string Name,
    long Population,
    long CountryId,
    bool IsCapital)
{
    public override string ToString()
    {
        return $"{Name} | {(IsCapital ? "Столица" : "Город")} | Население: {Population:N0} чел.";
    }
}