using RestaurantAPI.API.Data;
using RestaurantAPI.API.Models.Domain;
using RestaurantAPI.API.Models.DTO;
using RestaurantAPI.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace RestaurantAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private readonly IRestaurantRepository restaurantRepository;

        public RestaurantsController(IRestaurantRepository restaurantRepository)
        {
            this.restaurantRepository = restaurantRepository;
        }

        //Get all restaurants 
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Get Data from Database via Domain model
            var restaurants = await restaurantRepository.GetAllAsync();

            //Map Domain modela to DTOs
            var restaurantDto = new List<RestaurantDto>();
            foreach (var restaurant in restaurants)
            {
                restaurantDto.Add(new RestaurantDto
                {
                    Id = restaurant.Id,
                    name = restaurant.name,
                    Address = restaurant.Address,
                    description = restaurant.description,
                    logo_url = restaurant.logo_url,
                    rating = restaurant.rating,
                    is_open = restaurant.is_open,
                    created_at = restaurant.created_at ?? DateTime.Now,
                    updated_at = restaurant.updated_at,
                    menuId = restaurant.menuId
                });
            }

            //return DTOs back to client
            return Ok(restaurantDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id) {

            //Get data from Database via - Domain models
            var restaurant = await restaurantRepository.GetRestaurantbyIdAsync(id);

            if (restaurant == null)
            {
                return NotFound();
            }

            //Map Domain models to DTO

            var restaurantDto = new RestaurantDto
            {
                Id = restaurant.Id,
                name = restaurant.name,
                Address = restaurant.Address,
                description = restaurant.description,
                logo_url = restaurant.logo_url,
                rating = restaurant.rating,
                is_open = restaurant.is_open,
                created_at = restaurant.created_at ?? DateTime.Now,
                updated_at = restaurant.updated_at,
                menuId = restaurant.menuId,

            };
            return Ok(restaurantDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRestaurantRequestDto addRestaurantRequestDto)
        {
            if (ModelState.IsValid)
            {
                //Map DTO to Domain model
                var restaurantDomainModel = new Restaurant
                {
                    name = addRestaurantRequestDto.name,
                    Address = addRestaurantRequestDto.Address,
                    description = addRestaurantRequestDto.description,
                    logo_url = addRestaurantRequestDto.logo_url,
                    rating = addRestaurantRequestDto.rating,
                    is_open = addRestaurantRequestDto.is_open,
                    menuId = addRestaurantRequestDto.menuId,
                    created_at = DateTime.UtcNow

                };

                //Use domain model to ceate a restaurant in the DB
                await restaurantRepository.CreateRestaurantAsync(restaurantDomainModel);


                //Map Domain models back to DTOs
                var restaurantDto = new RestaurantDto
                {
                    Id = restaurantDomainModel.Id,
                    name = restaurantDomainModel.name,
                    Address = restaurantDomainModel.Address,
                    description = restaurantDomainModel.description,
                    logo_url = restaurantDomainModel.logo_url,
                    rating = restaurantDomainModel.rating,
                    created_at = (DateTime)restaurantDomainModel.created_at,
                    is_open = restaurantDomainModel.is_open,
                    menuId = restaurantDomainModel.menuId,
                };


                return CreatedAtAction(nameof(GetById), new { id = restaurantDto.Id }, restaurantDto);
            }
            else
            { 
                return BadRequest(ModelState);
            }
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteItem([FromRoute] Guid id) 
        {
            //Get data from Database via - Domain models
            var restaurant = await restaurantRepository.DeleteRestaurantAsync(id);

            if (restaurant == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id,[FromBody] UpdateRestaurantRequestDto updateRestaurantRequestDto)
        {
            if (ModelState.IsValid)
            {
                //Map DTO to Domain Model
                var restaurantDomainModel = new Restaurant
                {
                    name = updateRestaurantRequestDto.name,
                    Address = updateRestaurantRequestDto.Address,
                    description = updateRestaurantRequestDto.description,
                    logo_url = updateRestaurantRequestDto.logo_url,
                    rating = updateRestaurantRequestDto.rating,
                    is_open = updateRestaurantRequestDto.is_open,
                };

                //Check if the restaurant exists
                restaurantDomainModel = await restaurantRepository.UpdateRestaurantAsync(id, restaurantDomainModel);

                if (restaurantDomainModel == null)
                {
                    return NotFound();
                }

                //Convert Domain Model to DTO

                var restaurantDto = new RestaurantDto
                {
                    Id = restaurantDomainModel.Id,
                    name = restaurantDomainModel.name,
                    Address = restaurantDomainModel.Address,
                    description = restaurantDomainModel.description,
                    logo_url = restaurantDomainModel.logo_url,
                    rating = restaurantDomainModel.rating,
                    is_open = restaurantDomainModel.is_open,
                    created_at = (DateTime)restaurantDomainModel.created_at,
                    updated_at = restaurantDomainModel.updated_at,
                    menuId =  restaurantDomainModel.menuId,

                };
                return Ok(restaurantDto);
            }
            else
            { 
                return BadRequest(ModelState);
            }
        }
    }
}