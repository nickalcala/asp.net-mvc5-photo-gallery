using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoGallery.Domain
{
    public class Photo
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public string FileExtension { get; set; }

        public ICollection<Tag> Tags { get; set; }
        
        public DateTime CreatedAt { get; set; }
    }
}