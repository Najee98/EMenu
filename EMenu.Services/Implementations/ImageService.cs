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
    public class ImageService : IGenericService<Image>
    {
        private readonly IGenericRepository<Image> _repository;

        public ImageService(IGenericRepository<Image> repository)
        {
            _repository = repository;
        }

        public IEnumerable<Image> GetAll(
        Expression<Func<Image, bool>> filter = null,
        Func<IQueryable<Image>, IOrderedQueryable<Image>> orderBy = null,
        int? pageNumber = null,
        int? pageSize = null)
        {
            IQueryable<Image> allImages = (IQueryable<Image>)_repository.GetAll();

            //Filtering
            if (filter != null)
            {
                allImages = allImages.Where(filter);
            }

            //Sorting
            if (orderBy != null)
            {
                allImages = orderBy(allImages);
            }

            //Pagination
            if (pageNumber.HasValue && pageSize.HasValue)
            {
                allImages = allImages.Skip((pageNumber.Value - 1) * pageSize.Value)
                             .Take(pageSize.Value);
            }

            if (allImages.Equals(null))
                throw new ResourceNotFoundException("List is empty");

            return allImages.ToList();
        }

        public Image GetById(int id)
        {
            Image image = _repository.GetById(id);
            if (image == null)
            {
                throw new ResourceNotFoundException("Image doesn't exist");
            }
            else
            {
                return image;
            }
        }

        public Image Create(Image imageToInsert)
        {
            _repository.Add(imageToInsert);
            return imageToInsert;
        }

        public Image Update(Image imageToUpdate)
        {
            Image updatedImage = _repository.GetById(imageToUpdate.imageId);
            if (updatedImage == null)
                throw new ResourceNotFoundException("Image doesn't exist");
            else
            {
                updatedImage.imageURL = imageToUpdate.imageURL;
                _repository.Add(updatedImage);
                return updatedImage;
            }

        }

        public void Delete(int imageId)
        {
            Image imageToDelete = _repository.GetById(imageId);
            if (imageToDelete != null)
            {
                _repository.Delete(imageToDelete);
            }
            else
            {
                throw new ResourceNotFoundException("Image doesn't exist!");
            }
        }
    }
}
