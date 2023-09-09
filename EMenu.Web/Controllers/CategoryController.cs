using EMenu.Models.Models;
using EMenu.Services.Implementations;
using EMenu.Services.Interfaces;
using EMenu.Utilities.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace EMenu.Web.Controllers
{
    [ApiController]
    [Route("categories")]
    public class CategoryController : ControllerBase
    {
        private readonly IGenericService<Category> _categoryService;

        public CategoryController(IGenericService<Category> categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("all")]
        public ActionResult<IEnumerable<Category>> GetAllCategories(
        [FromQuery] Expression<Func<Category, bool>> filter = null,
        [FromQuery] Func<IQueryable<Category>, IOrderedQueryable<Category>> orderBy = null,
        [FromQuery] int? pageNumber = null,
        [FromQuery] int? pageSize = null)
        {
            try
            {
                var allCategories = _categoryService.GetAll(filter, orderBy, pageNumber, pageSize);
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
        public ActionResult<Category> GetCategoryById(int id)
        {
            try
            {
                var category = _categoryService.GetById(id);
                return Ok(category);
            }
            catch (ResourceNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPatch("create")]
        public ActionResult<Category> CreateCategory(Category category)
        {
            try
            {
                var createdCategory = _categoryService.Create(category);
                return CreatedAtAction(nameof(GetCategoryById), new { id = createdCategory.categoryId }, createdCategory);
            }
            catch (Exception e)
            {
                return BadRequest("Error creating the category: " + e.Message);
            }
        }

        [HttpPut("update")]
        public ActionResult<Category> UpdateCategory(Category category)
        {
            try
            {
                var updatedCategory = _categoryService.Update(category);
                return Ok(updatedCategory);
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
        public IActionResult DeleteCategory(int id)
        {
            try
            {
                _categoryService.Delete(id);
                return NoContent();
            }
            catch (ResourceNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
