using EMenu.Models.Models;
using EMenu.Services.Implementations;
using EMenu.Services.Interfaces;
using EMenu.Utilities.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace EMenu.Web.Controllers
{
    [ApiController]
    [Route("image")]
    public class ImageController : ControllerBase
    {
        private readonly IGenericService<Image> _imageService;

        public ImageController(IGenericService<Image> imageService)
        {
            _imageService = imageService;
        }

        [HttpGet("all")]
        public ActionResult<IEnumerable<Image>> GetAllImages(
        [FromQuery] Expression<Func<Image, bool>> filter = null,
        [FromQuery] Func<IQueryable<Image>, IOrderedQueryable<Image>> orderBy = null,
        [FromQuery] int? pageNumber = null,
        [FromQuery] int? pageSize = null)
        {
            try
            {
                var allCategories = _imageService.GetAll(filter, orderBy, pageNumber, pageSize);
                return Ok(allCategories);
            }
            catch (ResourceNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest("Error fetching the index page: " + e.Message);
            }
        }

        [HttpGet("get_by_id/{id}")]
        public ActionResult<Image> GetImageById(int id)
        {
            try
            {
                var image = _imageService.GetById(id);
                return Ok(image);
            }
            catch (ResourceNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPatch("create")]
        public ActionResult<Image> CreateImage(Image image)
        {
            try
            {
                var createdImage = _imageService.Create(image);
                return CreatedAtAction(nameof(GetImageById), new { id = createdImage.imageId }, createdImage);
            }
            catch (Exception e)
            {
                return BadRequest("Error inserting the image: " + e.Message);
            }
        }

        [HttpPut("update")]
        public ActionResult<Image> UpdateImage(Image image)
        {
            try
            {
                var updatedImage = _imageService.Update(image);
                return Ok(updatedImage);
            }
            catch (ResourceNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteImage(int id)
        {
            try
            {
                _imageService.Delete(id);
                return NoContent();
            }
            catch (ResourceNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
