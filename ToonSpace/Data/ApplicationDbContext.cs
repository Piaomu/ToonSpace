using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ToonSpace.Models;

namespace ToonSpace.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ToonSpace.Models.Genre> Genre { get; set; }
        public DbSet<ToonSpace.Models.Upload> Upload { get; set; }
    }
}
