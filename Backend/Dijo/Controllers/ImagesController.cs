using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Models.DTO;
using RestaurantAPI.Service;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageService imageService;
        public ImagesController(IImageService imageService)
        {
            this.imageService = imageService;
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var ImageItem = await imageService.GetImageByIdAsync(id);
            if (ImageItem == null) { return NotFound(); }
            return Ok(ImageItem);
        }

        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto imageUploadRequestDto)
        {
            if (ModelState.IsValid)
            {
                var uploadImage = await imageService.UploadImageAsync(imageUploadRequestDto);
                return CreatedAtAction(nameof(GetById), new { id = uploadImage.Id }, uploadImage);
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deletedImage = await imageService.DeleteImageAsync(id);
            if (deletedImage == null) { return NotFound(); }
            return Ok(deletedImage);
        }
    }
}
