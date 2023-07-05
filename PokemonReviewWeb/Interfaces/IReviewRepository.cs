using PokemonReviewWeb.Models;

namespace PokemonReviewWeb.Interfaces
{
    public interface IReviewRepository
    {
        ICollection<Review> GetReviews();
        Review GetReview(int id);
        ICollection<Review> GetReviewOfPokemon(int pokemonId);
        bool ReviewExist(int id);
    }
}
