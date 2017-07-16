using PhotoGallery.Domain;
using System.Data.Entity;
using System.Diagnostics;

namespace PhotoGallery.Web.DatabaseContexts
{
    public class PhotoGalleryDb : DbContext
    {

        public PhotoGalleryDb()
            : base("DefaultConnection")
        {
        }

        public DbSet<Photo> Photos { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}