using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ToonSpace.Models
{
    public class ToonUser: IdentityUser
    {
        [Required]
        [Display(Name = "First Name")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at most {1} characters.", MinimumLength = 2)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at most {1} characters.", MinimumLength = 2)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at most {1} characters.", MinimumLength = 2)]
        public string Nickname { get; set; }

        public DateTime RegisterDate { get; set; }
        public DateTime LastLogin { get; set; }
        public string FollowerId { get; set; }
        public string FollowingId { get; set; }
        public byte[] ImageData { get; set; }
        public string ContentType { get; set; }
        [NotMapped]
        public string FullName 
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        //Navigational properties
        public ICollection<ToonUser> Followers { get; set; } = new HashSet<ToonUser>();
        public ICollection<ToonUser> Following { get; set; } = new HashSet<ToonUser>();
        public ICollection<UserRelations> Relations { get; set; } = new HashSet<UserRelations>();
        public ICollection<Upload> Uploads { get; set; } = new HashSet<Upload>();

    }
}
