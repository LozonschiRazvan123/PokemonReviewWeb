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
        bool CreatePokemon(Pokemon pokemon);
        bool UpdatePokemon(Pokemon pokemon);
        bool DeletePokemon(Pokemon pokemon);
        bool Save();

    }
}
