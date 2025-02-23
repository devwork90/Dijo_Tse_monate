using Dijo.API.Data;
using Dijo.API.Models.Domain;
using Dijo.API.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Dijo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private readonly DijoDbContext dbContext;

        public RestaurantsController(DijoDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //Get all restaurants 
        [HttpGet]
        public IActionResult GetAll()
        {
            //Get Data from Database via Domain model
            var restaurants = dbContext.restaurants.ToList();

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
                    created_at = restaurant.created_at ?? DateTime.Now,
                    updated_at = restaurant.updated_at
                });
            }

            //return DTOs back to client
            return Ok(restaurantDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id) {

            //Get data from Database via - Domain models
            var restaurant = dbContext.restaurants.FirstOrDefault(x => x.Id == id);

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
                updated_at = restaurant.updated_at

            };
            return Ok(restaurantDto);
        }

        [HttpPost]
        public IActionResult Create([FromBody] AddRestaurantRequestDto addRestaurantRequestDto)
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
                created_at = DateTime.UtcNow

            };

            //Use domain model to ceate a restaurant in the DB
            dbContext.restaurants.Add(restaurantDomainModel);
            dbContext.SaveChanges();


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
                is_open = restaurantDomainModel.is_open

            };


            return CreatedAtAction(nameof(GetById), new { id = restaurantDto.Id }, restaurantDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult DeleteItem([FromRoute] Guid id) 
        {
            //Get data from Database via - Domain models
            var restaurant = dbContext.restaurants.FirstOrDefault(x => x.Id == id);

            if (restaurant == null)
            {
                return NotFound();
            }

            dbContext.Remove(restaurant);
            dbContext.SaveChanges();

            return NoContent();
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult Update([FromRoute] Guid id,[FromBody] UpdateRestaurantRequestDto updateRestaurantRequestDto)
        {
            //Check if the restaurant exists
            var restaurantDomainModel = dbContext.restaurants.FirstOrDefault(x => x.Id == id);

            if(restaurantDomainModel == null) 
            { 
                return NotFound();
            }

            //Map DTO's to Domain Model
            restaurantDomainModel.name = updateRestaurantRequestDto.name;
            restaurantDomainModel.Address = updateRestaurantRequestDto.Address;
            restaurantDomainModel.description = updateRestaurantRequestDto.description;
            restaurantDomainModel.logo_url = updateRestaurantRequestDto.logo_url;
            restaurantDomainModel.rating = updateRestaurantRequestDto.rating;
            restaurantDomainModel.is_open = updateRestaurantRequestDto.is_open;
            restaurantDomainModel.updated_at = DateTime.UtcNow;

            dbContext.SaveChanges();

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
                updated_at = DateTime.UtcNow

            };

            return Ok(restaurantDto);
        }
    }
}