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
    public class ProductService : IGenericService<Product>
    {
        private readonly IGenericRepository<Product> _repository;

        public ProductService(IGenericRepository<Product> repository)
        {
            _repository = repository;
        }

        public IEnumerable<Product> GetAll(
        Expression<Func<Product, bool>> filter = null,
        Func<IQueryable<Product>, IOrderedQueryable<Product>> orderBy = null,
        int? pageNumber = null,
        int? pageSize = null)
        {
            IQueryable<Product> allProducts = (IQueryable<Product>)_repository.GetAll();

            //Filtering
            if (filter != null)
            {
                allProducts = allProducts.Where(filter);
            }

            //Sorting
            if (orderBy != null)
            {
                allProducts = orderBy(allProducts);
            }

            //Pagination
            if (pageNumber.HasValue && pageSize.HasValue)
            {
                allProducts = allProducts.Skip((pageNumber.Value - 1) * pageSize.Value)
                             .Take(pageSize.Value);
            }

            if (allProducts.Equals(null))
                throw new ResourceNotFoundException("List is empty");

            return allProducts.ToList();
        }

        public Product GetById(int id)
        {
            Product product = _repository.GetById(id);
            if (product == null)
            {
                throw new ResourceNotFoundException("Product doesn't exist");
            }
            else
            {
                return product;
            }
        }

        public Product Create(Product productToCreate)
        {
            _repository.Add(productToCreate);
            return productToCreate;
        }

        public Product Update(Product productToUpdate)
        {
            Product updatedProduct = _repository.GetById(productToUpdate.productId);
            if (updatedProduct == null)
                throw new ResourceNotFoundException("Product doesn't exist");
            else
            {
                updatedProduct.productDescription = productToUpdate.productDescription;
                updatedProduct.productName = productToUpdate.productName;
                updatedProduct.productImageId = productToUpdate.productImageId;
                updatedProduct.categoryId = productToUpdate.categoryId;
                _repository.Add(updatedProduct);
                return updatedProduct;
            }

        }

        public void Delete(int categoryId)
        {
            Product productToDelete = _repository.GetById(categoryId);
            if (productToDelete != null)
            {
                _repository.Delete(productToDelete);
            }
            else
            {
                throw new ResourceNotFoundException("Product doesn't exist!");
            }
        }
    }
}
