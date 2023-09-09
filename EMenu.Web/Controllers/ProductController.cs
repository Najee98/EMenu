using EMenu.Models.Models;
using EMenu.Services.Implementations;
using EMenu.Services.Interfaces;
using EMenu.Utilities.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace EMenu.Web.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductController : ControllerBase
    {
        private readonly IGenericService<Product> _productService;

        public ProductController(IGenericService<Product> productService)
        {
            _productService = productService;
        }

        [HttpGet("all")]
        public ActionResult<IEnumerable<Product>> GetAllProducts(
        [FromQuery] Expression<Func<Product, bool>> filter = null,
        [FromQuery] Func<IQueryable<Product>, IOrderedQueryable<Product>> orderBy = null,
        [FromQuery] int? pageNumber = null,
        [FromQuery] int? pageSize = null)
        {
            try
            {
                var allProducts = _productService.GetAll(filter, orderBy, pageNumber, pageSize);
                return Ok(allProducts);
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
        public ActionResult<Product> GetProductById(int id)
        {
            try
            {
                var product = _productService.GetById(id);
                return Ok(product);
            }
            catch (ResourceNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPatch("create")]
        public ActionResult<Product> CreateProduct(Product product)
        {
            try
            {
                var createdProduct = _productService.Create(product);
                return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.productId }, createdProduct);
            }
            catch (Exception e)
            {
                return BadRequest("Error creating the product: " + e.Message);
            }
        }

        [HttpPut("update")]
        public ActionResult<Product> UpdateProduct(Product product)
        {
            try
            {
                var updatedProduct = _productService.Update(product);
                return Ok(updatedProduct);
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
        public IActionResult DeleteProduct(int id)
        {
            try
            {
                _productService.Delete(id);
                return NoContent();
            }
            catch (ResourceNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
