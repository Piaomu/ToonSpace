using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ToonSpace.Enums;

namespace ToonSpace.Models
{
    public class Upload
    {
        public int Id { get; set; }

        [DisplayName("Genre")]
        public int GenreId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }
        public string Artist { get; set; }
        public DateTime Created { get; set; }

        public int ViewCount { get; set; }

        public bool Visible { get; set; }
        public IFormFile ImageFile { get; set; }

        [Required]
        public byte[] Image { get; set; }
        public string ContentType { get; set; }
        public Genre Genre { get; set; }

        [Display(Name = "Select Genre")]
        public GenreName GenreName { get; set; }
        public MediaStatus MediaStatus { get; set; }

        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
        public ICollection<UserLike> Likes { get; set; } = new HashSet<UserLike>();
    }
}
