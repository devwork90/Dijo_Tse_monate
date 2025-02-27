using Dijo.API.Data;
using Dijo.API.Models.Domain;
using Dijo.API.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> GetAll()
        {
            //Get Data from Database via Domain model
            var restaurants = await dbContext.restaurants.ToListAsync();

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
                    updated_at = restaurant.updated_at
                });
            }

            //return DTOs back to client
            return Ok(restaurantDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id) {

            //Get data from Database via - Domain models
            var restaurant = await dbContext.restaurants.FirstOrDefaultAsync(x => x.Id == id);

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
        public async Task<IActionResult> Create([FromBody] AddRestaurantRequestDto addRestaurantRequestDto)
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
            await dbContext.restaurants.AddAsync(restaurantDomainModel);
            await dbContext.SaveChangesAsync();


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
        public async Task<IActionResult> DeleteItem([FromRoute] Guid id) 
        {
            //Get data from Database via - Domain models
            var restaurant = await dbContext.restaurants.FirstOrDefaultAsync(x => x.Id == id);

            if (restaurant == null)
            {
                return NotFound();
            }

            dbContext.Remove(restaurant);
            await dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id,[FromBody] UpdateRestaurantRequestDto updateRestaurantRequestDto)
        {
            //Check if the restaurant exists
            var restaurantDomainModel = await dbContext.restaurants.FirstOrDefaultAsync(x => x.Id == id);

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

            await dbContext.SaveChangesAsync();

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

            };

            return Ok(restaurantDto);
        }
    }
}