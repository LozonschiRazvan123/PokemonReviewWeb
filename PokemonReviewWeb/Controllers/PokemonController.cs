using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewWeb.Data;
using PokemonReviewWeb.DTO;
using PokemonReviewWeb.Interfaces;
using PokemonReviewWeb.Models;

namespace PokemonReviewWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : Controller
    {
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IMapper _mapper;

        public PokemonController(IPokemonRepository pokemonRepository, IMapper mapper)
        {
            _pokemonRepository = pokemonRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        public IActionResult GetPokemons()
        {
            var pokemons = _mapper.Map<List<PokemonDTO>>(_pokemonRepository.GetPokemons());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(pokemons);

        }

        [HttpGet("{PokemonId}")]
        public IActionResult GetPokemon(int PokemonId)
        {
            if(!_pokemonRepository.PokemonExists(PokemonId))
                return NotFound();
            var pokemon = _mapper.Map<PokemonDTO>(_pokemonRepository.GetPokemon(PokemonId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(pokemon);
        }

        [HttpGet("{PokemonId}/rating")]
        public IActionResult GetPokemonRating(int PokemonId)
        {
            if (!_pokemonRepository.PokemonExists(PokemonId))
                return NotFound();
            var pokemonRating = _pokemonRepository.GetPokemonRatings(PokemonId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(pokemonRating);
        }
    }
}
