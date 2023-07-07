using PokemonReviewWeb.Models;

namespace PokemonReviewWeb.Interfaces
{
    public interface IOwnerRepository
    {
        ICollection<Owner> GetOwners();
        Owner GetOwner(Guid ownerId);
        ICollection<Owner> GetOwnerOfPokemon(Guid pokemonId);
        ICollection<Pokemon> GetPokemonofOwner(Guid ownerId);
        bool OwnerExist(Guid ownerId);
        bool CreateOwner(Owner owner);
        bool UpdateOwner(Owner owner);
        bool DeleteOwner(Owner owner);
        bool Save();
    }
}
