using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ToonSpace.Models;

namespace ToonSpace.Data
{
    public class ApplicationDbContext : IdentityDbContext<ToonUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ToonSpace.Models.Upload> Upload { get; set; }
        public DbSet<ToonSpace.Models.Comment> Comment { get; set; }
        public DbSet<ToonSpace.Models.Notification> Notification { get; set; }
        public DbSet<ToonSpace.Models.Invitation> Invitation { get; set; }
    }
}
