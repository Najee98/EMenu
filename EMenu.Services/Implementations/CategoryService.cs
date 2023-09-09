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
    public class CategoryService : IGenericService<Category>
    {
        private readonly IGenericRepository<Category> _repository;

        public CategoryService(IGenericRepository<Category> repository)
        {
            _repository = repository;
        }

        public IEnumerable<Category> GetAll(
        Expression<Func<Category, bool>> filter = null,
        Func<IQueryable<Category>, IOrderedQueryable<Category>> orderBy = null,
        int? pageNumber = null,
        int? pageSize = null)
        {
            IQueryable<Category> allCategories = (IQueryable<Category>)_repository.GetAll();

            //Filtering
            if (filter != null)
            {
                allCategories = allCategories.Where(filter);
            }

            //Sorting
            if (orderBy != null)
            {
                allCategories = orderBy(allCategories);
            }

            //Pagination
            if (pageNumber.HasValue && pageSize.HasValue)
            {
                allCategories = allCategories.Skip((pageNumber.Value - 1) * pageSize.Value)
                             .Take(pageSize.Value);
            }

            if (allCategories.Equals(null))
                throw new ResourceNotFoundException("List is empty");

            return allCategories.ToList();
        }

        public Category GetById(int id)
        {
            Category category = _repository.GetById(id);
            if (category == null)
            {
                throw new ResourceNotFoundException("Category doesn't exist");
            }
            else
            {
                return category;
            }
        }

        public Category Create(Category categoryToCreate)
        {
            _repository.Add(categoryToCreate);
            return categoryToCreate;
        }

        public Category Update(Category categoryToUpdate)
        {
            Category updatedCategory = _repository.GetById(categoryToUpdate.categoryId);
            if (updatedCategory == null)
                throw new ResourceNotFoundException("Category doesn't exist");
            else
            {
                updatedCategory.categoryName = categoryToUpdate.categoryName;
                updatedCategory.categoryDescription = categoryToUpdate.categoryDescription;
                _repository.Add(updatedCategory);
                return updatedCategory;
            }

        }

        public void Delete(int categoryId)
        {
            Category categoryToDelete = _repository.GetById(categoryId);
            if (categoryToDelete != null)
            {
                _repository.Delete(categoryToDelete);
            }
            else
            {
                throw new ResourceNotFoundException("Category doesn't exist!");
            }
        }
    }
}
