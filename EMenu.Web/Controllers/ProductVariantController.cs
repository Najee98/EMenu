using EMenu.Models.Models;
using EMenu.Services.Implementations;
using EMenu.Services.Interfaces;
using EMenu.Utilities.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace EMenu.Web.Controllers
{
    [ApiController]
    [Route("variants")]
    public class ProductVariantController : ControllerBase
    {
        private readonly IGenericService<ProductVariant> _productVariantService;

        public ProductVariantController(IGenericService<ProductVariant> variantService)
        {
            _productVariantService = variantService;
        }

        [HttpGet("all")]
        public ActionResult<IEnumerable<ProductVariant>> GetAllProductVariants(
        [FromQuery] Expression<Func<ProductVariant, bool>> filter = null,
        [FromQuery] Func<IQueryable<ProductVariant>, IOrderedQueryable<ProductVariant>> orderBy = null,
        [FromQuery] int? pageNumber = null,
        [FromQuery] int? pageSize = null)
        {
            try
            {
                var allProductVariants = _productVariantService.GetAll(filter, orderBy, pageNumber, pageSize);
                return Ok(allProductVariants);
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
        public ActionResult<ProductVariant> GetProductVariantById(int id)
        {
            try
            {
                var productVariant = _productVariantService.GetById(id);
                return Ok(productVariant);
            }
            catch (ResourceNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPatch("create")]
        public ActionResult<ProductVariant> CreateProductVariant(ProductVariant productVariant)
        {
            try
            {
                var createdProductVariant = _productVariantService.Create(productVariant);
                return CreatedAtAction(nameof(GetProductVariantById), new { id = createdProductVariant.productVariantId }, createdProductVariant);
            }
            catch (Exception e)
            {
                return BadRequest("Error creating the product variant: " + e.Message);
            }
        }

        [HttpPut("update")]
        public ActionResult<ProductVariant> UpdateProductVariant(ProductVariant productVariant)
        {
            try
            {
                var updatedProductVariant = _productVariantService.Update(productVariant);
                return Ok(updatedProductVariant);
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
        public IActionResult DeleteProductVariant(int id)
        {
            try
            {
                _productVariantService.Delete(id);
                return NoContent();
            }
            catch (ResourceNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
