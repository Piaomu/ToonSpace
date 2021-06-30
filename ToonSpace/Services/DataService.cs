using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToonSpace.Data;
using ToonSpace.Enums;
using ToonSpace.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ToonSpace.Services
{
    public class DataService
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IImageService _imageService;
        private readonly UserManager<ToonUser> _userManager;
        private readonly IConfiguration _configuration;

        public DataService(ApplicationDbContext context, 
                           RoleManager<IdentityRole> roleManager, 
                           IImageService imageService, 
                           UserManager<ToonUser> userManager, 
                           IConfiguration configuration)
        {
            _context = context;
            _roleManager = roleManager;
            _imageService = imageService;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task ManageDataAsync()
        {
            //Make sure the database is present by running through the migrations.
            await _context.Database.MigrateAsync();

            //Task 1: Seed ROLES - Create roles and enter them into the system.
            await SeedRolesAsync();

            //Task 2: Seed USERS - enter some AspNetUsers and enter them into the system.
            await SeedUsersAsync();
        }

        private async Task SeedRolesAsync()
        {
            //Check if there are any roles in the system
            if (_context.Roles.Any())
                return;

            foreach(var stringRole in Enum.GetNames(typeof(ToonerRole)))
            {
                var identityRole = new IdentityRole(stringRole);
                // Create a role in the system for each enumeration
                await _roleManager.CreateAsync(identityRole);
            }
        }

        private async Task SeedUsersAsync()
        {
            var adminUser = new ToonUser()
            {
                Email = "wahl.kasey@gmail.com",
                UserName = "wahl.kasey@gmail.com",
                FirstName = "Kasey",
                LastName = "Wahl",
                PhoneNumber = "555-5555",
                EmailConfirmed = true,
                ImageData = await _imageService.EncodeImageAsync("cryingfrankenstein.jpg"),
                ContentType = "jpg"
            };

            var modUser = new ToonUser()
            {
                Email = "turd@gmail.com",
                UserName = "turd@gmail.com",
                FirstName = "Turd",
                LastName = "Ferguson",
                PhoneNumber = "444-4444",
                EmailConfirmed = true,
                ImageData = await _imageService.EncodeImageAsync("turd_ferguson.jpg"),
                ContentType = "jpg"
            };

            var artistUser1 = new ToonUser()
            {
                Email = "french@gmail.com",
                UserName = "french@gmail.com",
                FirstName = "French",
                LastName = "Stewart",
                PhoneNumber = "333-3333",
                EmailConfirmed = true,
                ImageData = await _imageService.EncodeImageAsync("french_stewart.jpg"),
                ContentType = "jpg"
            };

            var artistUser2 = new ToonUser()
            {
                Email = "sean@gmail.com",
                UserName = "sean@gmail.com",
                FirstName = "Sean",
                LastName = "Connery",
                PhoneNumber = "222-2222",
                EmailConfirmed = true,
                ImageData = await _imageService.EncodeImageAsync("sean_connery.png"),
                ContentType = "png"
            };

            //Create Admin User
            await _userManager.CreateAsync(adminUser, _configuration["AdminPassword"]);
            //Assign User to Admin Role
            await _userManager.AddToRoleAsync(adminUser, ToonerRole.Administrator.ToString());

            //Create Mod User
            await _userManager.CreateAsync(modUser, _configuration["ModPassword"]);
            //Assign User to Mod Role
            await _userManager.AddToRoleAsync(modUser, ToonerRole.Moderator.ToString());

            //Create Artist 1 User
            await _userManager.CreateAsync(artistUser1, _configuration["Artist1Password"]);
            //Assign User to Artist Role
            await _userManager.AddToRoleAsync(artistUser1, ToonerRole.Artist.ToString());

            //Create Artist 2 User
            await _userManager.CreateAsync(artistUser2, _configuration["Artist2Password"]);
            //Assign User to Artist Role
            await _userManager.AddToRoleAsync(artistUser2, ToonerRole.Artist.ToString());
        }



    }
}
