using PokemonReviewWeb.Data;
using PokemonReviewWeb.Interfaces;
using PokemonReviewWeb.Models;

namespace PokemonReviewWeb.Repository
{
    public class ReviewerRepository : IReviewerRepository
    {
        private readonly DataContext _context;
        public ReviewerRepository(DataContext context) 
        { 
            _context = context;

        }
        public bool ExistReviewer(int id)
        {
            return _context.Reviewers.Any(r=>r.Id == id);
        }

        public Reviewer GetReviewer(int id)
        {
            return _context.Reviewers.Where(r => r.Id == id).FirstOrDefault();
        }

        public ICollection<Reviewer> GetReviewers()
        {
            return _context.Reviewers.ToList();
        }

        public ICollection<Review> GetReviewsByReviewer(int id)
        {
            return _context.Reviews.Where(r => r.Reviewer.Id == id).ToList();
        }
    }
}
