using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PhotoGallery.Domain;
using PhotoGallery.Web.DatabaseContexts;
using System.Threading.Tasks;
using System.Data.Entity;
using PhotoGallery.Web.Models;
using System.IO;

namespace PhotoGallery.Web.Repositories
{
    public class DbPhotoRepository : IPhotoRepository
    {

        private PhotoGalleryDb db = new PhotoGalleryDb();

        public async Task<bool> AddPhotoTagsAsync(int photoId, string[] tags)
        {
            Photo _Photo = await FindAsync(photoId);
            var _Tags = _Photo.Tags ?? new List<Tag>();

            foreach (String tag in tags)
            {
                _Tags.Add(new Tag(tag));
            }

            _Photo.Tags = _Tags;
            
            return (await db.SaveChangesAsync()) > 0;
        }

        public async Task<Photo> FindAsync(int id)
        {
            try
            {
                return await db.Photos.Where(p => p.Id == id).SingleAsync();
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<Photo>> GetPhotosByPageAsync(int pageNumber)
        {
            return await db.Photos
                .OrderByDescending(p => p.CreatedAt)
                .Include(p => p.Tags)
                .Skip((pageNumber - 1) * 10)
                .Take(10)
                .ToListAsync();
        }

        public async Task<Photo> SaveAsync(PhotoViewModel photoViewModel)
        {
            Photo photo = new Photo();
            photo.Name = photoViewModel.Name;
            photo.FileName = photoViewModel.File.FileName;
            photo.FileExtension = Path.GetExtension(photoViewModel.File.FileName);
            photo.CreatedAt = DateTime.Now;

            db.Photos.Add(photo);
            await db.SaveChangesAsync();

            return photo;
        }
    }
}