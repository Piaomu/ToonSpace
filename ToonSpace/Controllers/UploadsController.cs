using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToonSpace.Data;
using ToonSpace.Models;
using ToonSpace.Services;
using ToonSpace.Services.Interfaces;

namespace ToonSpace.Controllers
{
    public class UploadsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IImageService _imageService;
        private readonly IRelationService _relationService;
        private readonly IUploadService _uploadService;
        private readonly UserManager<ToonUser> _userManager;

        public UploadsController(ApplicationDbContext context,
                                 IImageService imageService,
                                 IRelationService relationService,
                                 IUploadService uploadService,
                                 UserManager<ToonUser> userManager)
        {
            _context = context;
            _imageService = imageService;
            _relationService = relationService;
            _uploadService = uploadService;
            _userManager = userManager;
        }

        // GET: Uploads
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Upload.Include(u => u.Artist);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Uploads/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var upload = await _context.Upload
                .Include(u => u.Artist)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (upload == null)
            {
                return NotFound();
            }

            return View(upload);
        }

        // GET: Uploads/Create
        public IActionResult Create()
        {
            ViewData["ArtistId"] = new SelectList(_context.Users, "Id", "Id");
            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
            return View();
        }

        // POST: Uploads/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string returnUrl, [Bind("Id,Title,Description,MediaStatus,ImageFile")] Upload upload)
        {
            if (ModelState.IsValid)
            {
                upload.Created = DateTime.Now;
                upload.ArtistId = _userManager.GetUserId(User);
                upload.Image = await _imageService.EncodeImageAsync(upload.ImageFile);
                upload.ContentType = _imageService.ContentType(upload.ImageFile);

                _context.Add(upload);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["ArtistId"] = new SelectList(_context.Users, "Id", "Name", upload.ArtistId);
            return View(upload);
        }

        // GET: Uploads/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var upload = await _context.Upload.FindAsync(id);
            if (upload == null)
            {
                return NotFound();
            }
            ViewData["ArtistId"] = new SelectList(_context.Users, "Id", "Id", upload.ArtistId);

            return View(upload);
        }

        // POST: Uploads/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,ArtistId,Created,ViewCount,Visible,Image,ContentType,GenreName,MediaStatus")] Upload upload)
        {
            if (id != upload.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(upload);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UploadExists(upload.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtistId"] = new SelectList(_context.Users, "Id", "Id", upload.ArtistId);
            return View(upload);
        }

        // GET: Uploads/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var upload = await _context.Upload
                .Include(u => u.Artist)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (upload == null)
            {
                return NotFound();
            }

            return View(upload);
        }

        // POST: Uploads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var upload = await _context.Upload.FindAsync(id);
            _context.Upload.Remove(upload);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UploadExists(int id)
        {
            return _context.Upload.Any(e => e.Id == id);
        }
    }
}
