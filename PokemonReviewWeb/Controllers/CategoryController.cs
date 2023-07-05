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
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        public IActionResult GetCategories() 
        {
            var categories = _mapper.Map<List<CategoryDTO>>(_categoryRepository.GetCategories());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(categories);
        }

        [HttpGet("{CategoryId}")]
        public IActionResult GetCategory(int categoryId)
        {
            if (!_categoryRepository.CategoryExist(categoryId))
                return NotFound();

            var category = _mapper.Map<CategoryDTO>(_categoryRepository.GetCategory(categoryId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(category);
        }

        [HttpGet("Pokemon/{PokemonId}")]
        public IActionResult GetPokemonByCategory(int PokemonId)
        {
            var pokemonCategory = _mapper.Map<PokemonDTO>(_categoryRepository.GetPokemonByCategory(PokemonId).FirstOrDefault());
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(pokemonCategory);
        }
    }
}
