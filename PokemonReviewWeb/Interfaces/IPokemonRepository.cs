using PokemonReviewWeb.DTO;
using PokemonReviewWeb.Models;

namespace PokemonReviewWeb.Interfaces
{
    public interface IPokemonRepository
    {
        ICollection<Pokemon> GetPokemons();
        Pokemon GetPokemon(int id);
        Pokemon GetPokemon(string name);
        Pokemon GetPokemonTrimToUpper(PokemonDTO pokemonCreate);
        decimal GetPokemonRatings(int PokemonId);
        bool PokemonExists(int PokemonId);
        bool CreatePokemon(Guid ownerId, int categoryId, Pokemon pokemon);
        bool UpdatePokemon(Pokemon pokemon);
        bool DeletePokemon(Pokemon pokemon);
        bool Save();

    }
}
