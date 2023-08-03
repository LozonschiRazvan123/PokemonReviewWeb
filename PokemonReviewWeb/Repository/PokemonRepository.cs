using PokemonReviewWeb.Data;
using PokemonReviewWeb.DTO;
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

        public bool CreatePokemon(Guid ownerId, int categoryId, Pokemon pokemon)
        {
            var pokemonOwnerEntity = _context.Owners.Where(a => a.Id == ownerId).FirstOrDefault();
            var category = _context.Categories.Where(a => a.Id == categoryId).FirstOrDefault();

            var pokemonOwner = new PokemonOwner()
            {
                Owner = pokemonOwnerEntity,
                Pokemon = pokemon,
            };

            _context.Add(pokemonOwner);

            var pokemonCategory = new PokemonCategory()
            {
                Category = category,
                Pokemon = pokemon,
            };

            _context.Add(pokemonCategory);

            _context.Add(pokemon);

            return Save();
        }

        public bool DeletePokemon(Pokemon pokemon)
        {
            _context.Remove(pokemon);
            return Save();
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

        public Pokemon GetPokemonTrimToUpper(PokemonDTO pokemonCreate)
        {
            return GetPokemons().Where(c => c.Name.Trim().ToUpper() == pokemonCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();
        }

        public bool PokemonExists(int PokemonId)
        {
            return _context.Pokemons.Any(p=> p.Id == PokemonId);
        }

        public bool Save()
        {
            //throw new NotImplementedException();
            var pokemon = _context.SaveChanges();
            return pokemon > 0 ? true : false;
        }

        public bool UpdatePokemon(Pokemon pokemon)
        {
            _context.Update(pokemon);
            return Save();
        }
    }
}
