using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewWeb.Controllers;
using PokemonReviewWeb.DTO;
using PokemonReviewWeb.Interfaces;
using PokemonReviewWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PokemonReviewApp.Tests.Controller
{
    public class PokemonControllerTests
    {
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IReviewerRepository _reviewerRepository;
        private readonly IMapper _mapper;
        public PokemonControllerTests()
        {
            _pokemonRepository = A.Fake<IPokemonRepository>();
            _reviewerRepository = A.Fake<IReviewerRepository>();
            _mapper = A.Fake<IMapper>();
        }

        [Fact]
        public void PokemonController_GetPokemons_ReturnOK()
        {
            //Arrange
            var pokemons = A.Fake<ICollection<PokemonDTO>>();
            var pokemonList = A.Fake<List<PokemonDTO>>();
            A.CallTo(() => _mapper.Map<List<PokemonDTO>>(pokemons)).Returns(pokemonList);
            var controller = new PokemonController(_pokemonRepository, _mapper, _reviewerRepository);

           //Act
           var result = controller.GetPokemons();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public void PokemonController_CreatePokemon_ReturnsOK()
        {
            //Arrange
            Guid ownerId = new Guid("9245fe4a-d402-451c-b9ed-9c1a04247482");
            int catId = 2;
            var pokemonMap = A.Fake<Pokemon>();
            var pokemon = A.Fake<Pokemon>();
            var pokemonCreated = A.Fake<PokemonDTO>();
            var pokemons = A.Fake<ICollection<PokemonDTO>>();
            var pokemonList = A.Fake<IList<PokemonDTO>>();
            A.CallTo(() => _pokemonRepository.GetPokemonTrimToUpper(pokemonCreated)).Returns(pokemon);
            A.CallTo(() => _mapper.Map<Pokemon>(pokemonCreated)).Returns(pokemon);
            A.CallTo(() => _pokemonRepository.CreatePokemon(ownerId,catId,pokemonMap)).Returns(true);
            var controller = new PokemonController(_pokemonRepository,_mapper, _reviewerRepository);

            //Act
            var result = controller.CreatePokemon(ownerId, catId, pokemonCreated);

            //Assert
            result.Should().NotBeNull();
        }
    }
}
