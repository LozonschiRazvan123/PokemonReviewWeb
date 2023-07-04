using PokemonReviewWeb.Data;
using PokemonReviewWeb.Interfaces;
using PokemonReviewWeb.Models;

namespace PokemonReviewWeb.Repository
{
    public class PokemonRepository: IPokemonRepository
    {
        private readonly DataContext _context;
        public PokemonRepository(DataContext context)
        {
            _context=context;

        }

        public Pokemon GetPokemon(int id)
        {
            return _context.Pokemons.Where(p=> p.Id == id).FirstOrDefault();
        }

        public Pokemon GetPokemon(string name)
        {
            return _context.Pokemons.Where(p=> p.Name == name).FirstOrDefault();
        }

        public decimal GetPokemonRatings(int PokemonId)
        {
            var review = _context.Reviews.Where(p=>p.Pokemon.Id==PokemonId);
            if(review.Count() <= 0)
                return 0;
            return ((decimal)review.Sum(r => r.Rating)/review.Count());
        }

        public ICollection<Pokemon> GetPokemons()
        {
            return _context.Pokemons.OrderBy(p => p.Id).ToList();
        }

        public bool PokemonExists(int PokemonId)
        {
            return _context.Pokemons.Any(p=> p.Id == PokemonId);
        }
    }
}
