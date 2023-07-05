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
    }
}
