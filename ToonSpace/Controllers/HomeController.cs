using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ToonSpace.Models;
using ToonSpace.Data;
using ToonSpace.Models.ViewModels;
using ToonSpace.Services.Interfaces;

namespace ToonSpace.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ToonUser> _userManager;
        private readonly IRelationService _relationService;
        private readonly IUploadService _uploadService;

        public HomeController(ILogger<HomeController> logger,
                              ApplicationDbContext context,
                              UserManager<ToonUser> userManager,
                              IRelationService relationService,
                              IUploadService uploadService)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _relationService = relationService;
            _uploadService = uploadService;
        }
        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            var userId = _userManager.GetUserId(User);

            IndexFeedViewModel model = new()
            {
                Followers = await _relationService.GetFollowersAsync(userId),
                Following = await _relationService.GetFollowingAsync(userId),
                User = await _userManager.GetUserAsync(User),
                Uploads = await _uploadService.GetTimelineUploadsAsync(userId)
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
