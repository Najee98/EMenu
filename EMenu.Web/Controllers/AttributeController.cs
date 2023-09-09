using EMenu.Models.Models;
using EMenu.Services.Interfaces;
using EMenu.Utilities.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using Attribute = EMenu.Models.Models.Attribute;

namespace EMenu.Web.Controllers
{
    [ApiController]
    [Route("attributes")]
    public class AttributeController : ControllerBase
    {
        private readonly IGenericService<Attribute> _attributeService;

        public AttributeController(IGenericService<Attribute> attributeService)
        {
            _attributeService = attributeService;
        }

        [HttpGet("all")]
        public ActionResult<IEnumerable<Attribute>> GetAllAttributes(
        [FromQuery] Expression<Func<Attribute, bool>> filter = null,
        [FromQuery] Func<IQueryable<Attribute>, IOrderedQueryable<Attribute>> orderBy = null,
        [FromQuery] int? pageNumber = null,
        [FromQuery] int? pageSize = null)
        {
            try
            {
                var allAttributes = _attributeService.GetAll(filter, orderBy, pageNumber, pageSize);
                return Ok(allAttributes);
            }
            catch(ResourceNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch(Exception e)
            {
                return BadRequest("Error fetching the index page: " + e.Message);
            }
        }

        [HttpGet("get_by_id/{id}")]
        public ActionResult<Attribute> GetAttributeById(int id)
        {
            try
            {
                var attribute = _attributeService.GetById(id);
                return Ok(attribute);
            }
            catch (ResourceNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPatch("create")]
        public ActionResult<Attribute> CreateAttribute(Attribute attribute)
        {
            try
            {
                var createdAttribute = _attributeService.Create(attribute);
                return CreatedAtAction(nameof(GetAttributeById), new { id = createdAttribute.attributeId }, createdAttribute);
            }
            catch (Exception e)
            {
                return BadRequest("Error creating the attribute: " + e.Message);
            }
        }

        [HttpPut("update")]
        public ActionResult<Attribute> UpdateAttribute(Attribute attribute)
        {
            try
            {
                var updatedAttribute = _attributeService.Update(attribute);
                return Ok(updatedAttribute);
            }
            catch (ResourceNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch(Exception e)
            {
                return BadRequest();
            }
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteAttribute(int id)
        {
            try
            {
                _attributeService.Delete(id);
                return NoContent();
            }
            catch (ResourceNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
