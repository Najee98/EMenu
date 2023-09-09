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
using System.Xml.Serialization;

namespace EMenu.Services.Implementations
{
    public class ProductVariantService : IGenericService<ProductVariant>
    {
        private readonly IGenericRepository<ProductVariant> _repository;

        public ProductVariantService(IGenericRepository<ProductVariant> repository)
        {
            _repository = repository;
        }

        public IEnumerable<ProductVariant> GetAll(
        Expression<Func<ProductVariant, bool>> filter = null,
        Func<IQueryable<ProductVariant>, IOrderedQueryable<ProductVariant>> orderBy = null,
        int? pageNumber = null,
        int? pageSize = null)
        {
            IQueryable<ProductVariant> allVariants = (IQueryable<ProductVariant>)_repository.GetAll();

            //Filtering
            if (filter != null)
            {
                allVariants = allVariants.Where(filter);
            }

            //Sorting
            if (orderBy != null)
            {
                allVariants = orderBy(allVariants);
            }

            //Pagination
            if (pageNumber.HasValue && pageSize.HasValue)
            {
                allVariants = allVariants.Skip((pageNumber.Value - 1) * pageSize.Value)
                             .Take(pageSize.Value);
            }

            if (allVariants.Equals(null))
                throw new ResourceNotFoundException("List is empty");

            return allVariants.ToList();
        }

        public ProductVariant GetById(int id)
        {
            ProductVariant variant = _repository.GetById(id);
            if (variant == null)
            {
                throw new ResourceNotFoundException("Product variant doesn't exist");
            }
            else
            {
                return variant;
            }
        }

        public ProductVariant Create(ProductVariant variantToCreate)
        {
            ProductVariant newVariant = new ProductVariant();
            newVariant.variantCreationDate = DateTime.Now;
            newVariant.prodctId = variantToCreate.prodctId;
            newVariant.variantAttributes = variantToCreate.variantAttributes;
            newVariant.variantImages = variantToCreate.variantImages; 
            _repository.Add(newVariant);
            return newVariant;
        }

        public ProductVariant Update(ProductVariant variantToUpdate)
        {
            ProductVariant updatedVariant = _repository.GetById(variantToUpdate.productVariantId);
            if (updatedVariant == null)
                throw new ResourceNotFoundException("Product variant doesn't exist");
            else
            {
                updatedVariant.variantAttributes = variantToUpdate.variantAttributes;
                updatedVariant.variantImages = variantToUpdate.variantImages;
                _repository.Add(updatedVariant);
                return updatedVariant;
            }
        }

        public void Delete(int variantId)
        {
            ProductVariant variantToDelete = _repository.GetById(variantId);
            if (variantToDelete != null)
            {
                _repository.Delete(variantToDelete);
            }
            else
            {
                throw new ResourceNotFoundException("Product variant doesn't exist!");
            }
        }
    }
}
