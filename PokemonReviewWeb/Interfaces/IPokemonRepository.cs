using PokemonReviewWeb.Models;

namespace PokemonReviewWeb.Interfaces
{
    public interface IPokemonRepository
    {
        ICollection<Pokemon> GetPokemons();
        Pokemon GetPokemon(int id);
        Pokemon GetPokemon(string name);
        decimal GetPokemonRatings(int PokemonId);
        bool PokemonExists(int PokemonId);

    }
}
