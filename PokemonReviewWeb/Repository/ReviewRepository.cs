using PokemonReviewWeb.Data;
using PokemonReviewWeb.Interfaces;
using PokemonReviewWeb.Models;

namespace PokemonReviewWeb.Repository
{

    public class ReviewRepository:IReviewRepository
    {
        private readonly DataContext _context;
        public ReviewRepository(DataContext context)
        {
            _context = context;
        }

        public Review GetReview(int id)
        {
            return _context.Reviews.Where(r => r.Id == id).FirstOrDefault();
        }

        public ICollection<Review> GetReviewOfPokemon(int pokemonId)
        {
            return _context.Reviews.Where(r => r.Pokemon.Id==pokemonId).ToList();
        }

        public ICollection<Review> GetReviews()
        {
            return _context.Reviews.ToList();
        }

        public bool ReviewExist(int id)
        {
            return _context.Reviews.Any(r => r.Id==id);
        }
    }
}
