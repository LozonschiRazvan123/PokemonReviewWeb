using AutoMapper;
using PokemonReviewWeb.DTO;
using PokemonReviewWeb.Models;

namespace PokemonReviewWeb.Helper
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Pokemon, PokemonDTO>();
            CreateMap<Category, CategoryDTO>();
            CreateMap<Country, CountryDTO>();
            CreateMap<Owner, OwnerDTO>();
            CreateMap<Review, ReviewDTO>();
            CreateMap<Reviewer, ReviewerDTO>();
        }
        
    }
}
