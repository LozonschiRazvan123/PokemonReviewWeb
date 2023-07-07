using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewWeb.DTO;
using PokemonReviewWeb.Interfaces;
using PokemonReviewWeb.Models;
using PokemonReviewWeb.Repository;

namespace PokemonReviewWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : Controller
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;
        public CountryController(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Country>))]
        public IActionResult GetCountries()
        {
            var country = _mapper.Map<List<CountryDTO>>(_countryRepository.GetCountries());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(country);
        }

        [HttpGet("{countryId}")]
        public IActionResult GetCountry(int countryId)
        {
            if (!_countryRepository.CountryExist(countryId))
                return NotFound();

            var country = _mapper.Map<CountryDTO>(_countryRepository.GetCountry(countryId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(country);
        }

        [HttpGet("/owners/{ownerId}")]
        public IActionResult GetCountryOfAnOwner(Guid ownerId)
        {
            var country = _mapper.Map<CountryDTO>(_countryRepository.GetCountryByOwner(ownerId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(country);
        }

        [HttpPost]
        public IActionResult CreateCountry(CountryDTO countryCreate)
        {
            if (countryCreate == null)
                return NotFound();

            var country = _countryRepository.GetCountries()
                .Where(c => c.Name.Trim().ToLower() == countryCreate.Name.Trim().ToLower())
                .FirstOrDefault();

            if (country != null)
            {
                ModelState.AddModelError("", "Country already exist");
                return StatusCode(422, ModelState);
            }

            var countryMap = _mapper.Map<Country>(countryCreate);
            if (!_countryRepository.CreateCountry(countryMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");
        }

        [HttpPut("{countryId}")]
        public IActionResult countryUpdate(int countryId, [FromBody] CountryDTO updateCountry)
        {
            if (updateCountry == null || updateCountry.Id!=countryId)
                return BadRequest(ModelState);

            if(!ModelState.IsValid) 
                return BadRequest();

            var countryUpdateMap = _mapper.Map<Country>(updateCountry);

            if(!_countryRepository.UpdateCountry(countryUpdateMap))
            {
                ModelState.AddModelError("", "Something went wrong updating country");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{countryId}")]
        public IActionResult deleteCountry(int countryId)
        {
            if(!_countryRepository.CountryExist(countryId))
                return NotFound();

            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var countryDelete = _countryRepository.GetCountry(countryId);
            if (!_countryRepository.DeleteCountry(countryDelete))
                ModelState.AddModelError("", "Something went wrong deleting category");

            return NoContent();
        }
    }
}
