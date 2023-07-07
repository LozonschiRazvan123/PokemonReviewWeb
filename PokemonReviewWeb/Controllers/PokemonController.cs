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

        [HttpPost]
        public IActionResult CreatePokemon([FromBody] PokemonDTO pokemonCreated)
        {
            if (pokemonCreated == null)
                return NotFound();

            var pokemon = _pokemonRepository.GetPokemons()
                .Where(p => p.Name.Trim().ToLower() == pokemonCreated.Name.Trim().ToLower())
                .FirstOrDefault();

            if(pokemon != null)
            {
                ModelState.AddModelError("", "Pokemon already exists");
                return StatusCode(422, ModelState);
            }

            var pokemonMap = _mapper.Map<Pokemon>(pokemonCreated);
            if(!_pokemonRepository.CreatePokemon(pokemonMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{pokemonId}")]
        public IActionResult pokemonUpdate(int pokemonId,[FromBody]PokemonDTO updatePokemon)
        {
            if(updatePokemon == null || updatePokemon.Id!=pokemonId)
                return BadRequest(ModelState);

            if(!ModelState.IsValid)
                return BadRequest();

            var updatePokemonMap = _mapper.Map<Pokemon>(updatePokemon);
            if(!_pokemonRepository.UpdatePokemon(updatePokemonMap))
            {
                ModelState.AddModelError("", "Something went wrong updating pokemon");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        [HttpDelete("{pokemonId}")]
        public IActionResult deletePokemon(int pokemonId)
        {
            if(!_pokemonRepository.PokemonExists(pokemonId))
                return NotFound();
            if(!ModelState.IsValid) 
                return BadRequest();
            
            var pokemonDelete = _pokemonRepository.GetPokemon(pokemonId);
            if(!_pokemonRepository.DeletePokemon(pokemonDelete))
                ModelState.AddModelError("", "Something went wrong deleting pokemon");

            return NoContent();
        }
    }
}
