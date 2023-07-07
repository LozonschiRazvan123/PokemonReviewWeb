using AutoMapper;
using PokemonReviewWeb.Data;
using PokemonReviewWeb.Interfaces;
using PokemonReviewWeb.Models;

namespace PokemonReviewWeb.Repository
{
    public class CountryRepository : ICountryRepository
    {

        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public CountryRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public bool CountryExist(int id)
        {
            return _context.Countries.Any(c=>c.Id==id);
        }

        public bool CreateCountry(Country country)
        {
            //throw new NotImplementedException();
            _context.Add(country);
            return Save();
        }

        public bool DeleteCountry(Country country)
        {
            _context.Remove(country);
            return Save();
        }

        public ICollection<Country> GetCountries()
        {
            return _context.Countries.OrderBy(c=>c.Id).ToList();
        }

        public Country GetCountry(int id)
        {
            return _context.Countries.Where(c=>c.Id==id).FirstOrDefault();
        }

        public Country GetCountryByOwner(Guid ownerId)
        {
           return _context.Owners.Where(o => o.Id == ownerId).Select(c => c.Country).FirstOrDefault();
        }

        public ICollection<Owner> GetOwnersByACountry(int countryId)
        {
            return _context.Owners.Where(c=>c.Country.Id==countryId).ToList();
        }

        public bool Save()
        {
            var country = _context.SaveChanges();
            return country > 0 ? true : false;
        }

        public bool UpdateCountry(Country country)
        {
            _context.Update(country);
            return Save();
        }
    }
}
