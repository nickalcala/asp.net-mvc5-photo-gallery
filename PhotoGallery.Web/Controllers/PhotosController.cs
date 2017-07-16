using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using PhotoGallery.Domain;
using PhotoGallery.Web.DatabaseContexts;
using System.Web;
using System.IO;
using PhotoGallery.Web.Models;
using System.Linq;
using System;
using System.Collections.Generic;
using PhotoGallery.Web.Repositories;

namespace PhotoGallery.Web.Controllers
{
    public class PhotosController : Controller
    {
        private PhotoGalleryDb db = new PhotoGalleryDb();
        private IPhotoRepository photoRepository;

        public PhotosController(IPhotoRepository photoRepository)
        {
            this.photoRepository = photoRepository;
        }

        public async Task<ActionResult> Page(int id)
        {
            var _Photos = await photoRepository.GetPhotosByPageAsync(id);

            return Json(new
            {
                success = true,
                data = _Photos
            }, JsonRequestBehavior.AllowGet);
        }

        // GET: Photos
        public async Task<ActionResult> Index()
        {
            var _Photos = await photoRepository.GetPhotosByPageAsync(1);

            return View(_Photos);
        }
        
        // GET: Photos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photo photo = await db.Photos.FindAsync(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        // GET: Photos/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // GET: Photos/Delete/5
        [Authorize]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photo photo = await db.Photos.FindAsync(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        // POST: Photos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Photo photo = await db.Photos.FindAsync(id);
            db.Photos.Remove(photo);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // POST: Photos/UploadPhoto
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UploadPhoto(PhotoViewModel Model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    success = false,
                    data = ViewData.ModelState.Values.Where(E => E.Errors.Count > 0)
                        .SelectMany(E => E.Errors)
                        .Select(E => E.ErrorMessage)
                        .ToArray()
                });
            }

            HttpPostedFileBase _File = Model.File;

            try
            {
                if (_File.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(_File.FileName);
                    string _Path = Path.Combine(Server.MapPath("~/Uploads"), _FileName);
                    _File.SaveAs(_Path);

                    await photoRepository.SaveAsync(Model);
                }
            }
            catch (Exception e)
            {
                return Json(new
                {
                    success = false,
                    data = e.Message
                });
            }

            return Json(new {
                success = true
            });
        }

        // POST: Photos/UploadPhoto
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> TagPhoto(int id, String[] tags)
        {
            Photo _Photo = await photoRepository.FindAsync(id);
            if (_Photo == null)
            {
                return HttpNotFound();
            }

            await photoRepository.AddPhotoTagsAsync(id, tags);
            
            return Json(new { success = true });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}