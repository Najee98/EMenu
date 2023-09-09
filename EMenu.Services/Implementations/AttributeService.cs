using EMenu.Models.Models;
using EMenu.Repositories.Interfaces;
using EMenu.Services.Interfaces;
using EMenu.Utilities.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Attribute = EMenu.Models.Models.Attribute;

namespace EMenu.Services.Implementations
{
    public class AttributeService : IGenericService<Attribute>
    {
        private readonly IGenericRepository<Attribute> _repository;

        public AttributeService(IGenericRepository<Attribute> repository)
        {
            _repository = repository;
        }

        public IEnumerable<Attribute> GetAll(
        Expression<Func<Attribute, bool>> filter = null,
        Func<IQueryable<Attribute>, IOrderedQueryable<Attribute>> orderBy = null,
        int? pageNumber = null,
        int? pageSize = null)
        {
            IQueryable<Attribute> allAttributes = (IQueryable<Attribute>)_repository.GetAll();

            //Filtering
            if (filter != null)
            {
                allAttributes = allAttributes.Where(filter);
            }

            //Sorting
            if (orderBy != null)
            {
                allAttributes = orderBy(allAttributes);
            }

            //Pagination
            if (pageNumber.HasValue && pageSize.HasValue)
            {
                allAttributes = allAttributes.Skip((pageNumber.Value - 1) * pageSize.Value)
                             .Take(pageSize.Value);
            }

            if (allAttributes.Equals(null))
                throw new ResourceNotFoundException("List is empty");

            return allAttributes.ToList();
        }

        public Attribute GetById(int id)
        {
            Attribute attribute = _repository.GetById(id);
            if(attribute == null)
            {
                throw new ResourceNotFoundException("Attribute doesn't exist");
            }
            else
            {
                return attribute;
            }
        }

        public Attribute Create(Attribute attributeToCreate)
        {
            _repository.Add(attributeToCreate);
            return attributeToCreate;
        }

        public Attribute Update(Attribute attributeToUpdate)
        {
            Attribute updatedAttribute = _repository.GetById(attributeToUpdate.attributeId);
            if (updatedAttribute == null)
                throw new ResourceNotFoundException("Attribute doesn't exist");
            else
            {
                updatedAttribute.attributeName = attributeToUpdate.attributeName;
                updatedAttribute.description = attributeToUpdate.description;
                _repository.Add(updatedAttribute);
                return updatedAttribute;
            }

        }

        public void Delete(int attributeId)
        {
            Attribute attributeToDelete = _repository.GetById(attributeId);
            if (attributeToDelete != null)
            {
                _repository.Delete(attributeToDelete);
            }
            else
            {
                throw new ResourceNotFoundException("Attribute doesn't exist!");
            }
                
            
        }
    }
}
