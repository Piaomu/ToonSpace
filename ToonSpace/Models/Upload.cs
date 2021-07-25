using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ToonSpace.Enums;

namespace ToonSpace.Models
{
    public class Upload
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }
        [Display(Name = "Artist")]
        public string ArtistId { get; set; }
        public DateTime Created { get; set; }

        public int ViewCount { get; set; }

        public bool Visible { get; set; }
        [NotMapped]
        [Display(Name = "Add Image")]
        public IFormFile ImageFile { get; set; }

        public byte[] Image { get; set; }
        public string ContentType { get; set; }

        [Display(Name = "Public or Private")]
        public MediaStatus MediaStatus { get; set; }

        //Navigational Properties
        public virtual ToonUser Artist { get; set; }
        public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
        public virtual ICollection<UserLike> Likes { get; set; } = new HashSet<UserLike>();
        public virtual ICollection<Notification> Notifications { get; set; } = new HashSet<Notification>();
    }
}
