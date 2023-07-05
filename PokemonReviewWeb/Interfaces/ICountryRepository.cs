using PokemonReviewWeb.Models;

namespace PokemonReviewWeb.Interfaces
{
    public interface ICountryRepository
    {
        ICollection<Country> GetCountries();
        Country GetCountry(int id);
        Country GetCountryByOwner(Guid ownerId);
        ICollection<Owner> GetOwnersByACountry(int countryId);
        bool CountryExist(int id);
    }
}
