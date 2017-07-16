using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhotoGallery.Web.Models
{
    public class PhotoViewModel
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        public HttpPostedFileBase File { get; set; }
    }
}