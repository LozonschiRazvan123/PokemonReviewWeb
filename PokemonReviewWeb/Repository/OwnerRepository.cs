using PokemonReviewWeb.Data;
using PokemonReviewWeb.Interfaces;
using PokemonReviewWeb.Models;

namespace PokemonReviewWeb.Repository
{
    public class OwnerRepository: IOwnerRepository
    { 
        private readonly DataContext _context;
        public OwnerRepository(DataContext context)
        {
            _context = context;
        }

        public Owner GetOwner(Guid ownerId)
        {
            return _context.Owners.Find(ownerId);
        }

        public ICollection<Owner> GetOwnerOfPokemon(Guid ownerId)
        {
           return _context.PokemonsOwners.Where(p => p.OwnerId == ownerId).Select(o => o.Owner).ToList();
        }

        public ICollection<Owner> GetOwners()
        {
            return _context.Owners.ToList();
        }

        public ICollection<Pokemon> GetPokemonofOwner(Guid ownerId)
        {
            return _context.PokemonsOwners.Where(p => p.OwnerId == ownerId).Select(p => p.Pokemon).ToList();
        }


        public bool OwnerExist(Guid ownerId)
        {
            return _context.Owners.Any(s => s.Id == ownerId);
        }
    }
}
