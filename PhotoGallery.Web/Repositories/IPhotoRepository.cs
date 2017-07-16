using PhotoGallery.Domain;
using PhotoGallery.Web.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoGallery.Web.Repositories
{
    public interface IPhotoRepository
    {

        Task<Photo> FindAsync(int id);
        Task<bool> AddPhotoTagsAsync(int photoId, string[] tags);
        Task<IEnumerable<Photo>> GetPhotosByPageAsync(int pageNumber);
        Task<Photo> SaveAsync(PhotoViewModel photoViewModel);

    }
}