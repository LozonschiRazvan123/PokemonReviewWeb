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

        public bool CreateOwner(Owner owner)
        {
            //throw new NotImplementedException();
            _context.Add(owner);
            return Save();
        }

        public bool DeleteOwner(Owner owner)
        {
            _context.Remove(owner);
            return Save();
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

        public bool Save()
        {
            var owner = _context.SaveChanges();
            return owner > 0 ? true : false;
        }

        public bool UpdateOwner(Owner owner)
        {
            //throw new NotImplementedException();
            _context.Update(owner);
            return Save();
        }
    }
}
