using PokemonReviewWeb.Models;

namespace PokemonReviewWeb.Interfaces
{
    public interface IReviewerRepository
    {
        ICollection<Reviewer> GetReviewers();
        Reviewer GetReviewer(int id);
        ICollection<Review> GetReviewsByReviewer(int id);
        bool ExistReviewer(int id);
    }
}
