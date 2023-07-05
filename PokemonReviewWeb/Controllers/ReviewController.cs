using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewWeb.DTO;
using PokemonReviewWeb.Interfaces;
using PokemonReviewWeb.Repository;

namespace PokemonReviewWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController: Controller
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;
        public ReviewController(IReviewRepository reviewRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetReviews()
        {
            var review = _mapper.Map<List<ReviewDTO>>(_reviewRepository.GetReviews());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(review);
        }

        [HttpGet("{id}")]
        public IActionResult GetOfReview(int id)
        {
            if(!_reviewRepository.ReviewExist(id))
                return NotFound();
            var review = _mapper.Map<ReviewDTO>(_reviewRepository.GetReview(id));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(review);
        }

        [HttpGet("pokemon/{PokemonId}")]
        public IActionResult GetReviewOfPokemon(int PokemonId)
        {
            if (!_reviewRepository.ReviewExist(PokemonId))
                return NotFound();
            var review = _mapper.Map<List<ReviewDTO>>(_reviewRepository.GetReviewOfPokemon(PokemonId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(review);
        }


    }
}
