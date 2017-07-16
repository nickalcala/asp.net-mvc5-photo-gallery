using Microsoft.AspNet.Identity.EntityFramework;
using PhotoGallery.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoGallery.Web.DatabaseContexts
{
    public class IdentityDb : IdentityDbContext<ApplicationUser>
    {
        public IdentityDb()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static IdentityDb Create()
        {
            return new IdentityDb();
        }
    }
}