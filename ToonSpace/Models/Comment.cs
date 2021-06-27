using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ToonSpace.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int UploadId { get; set; }
        public string AuthorId { get; set; }
        public string ModeratorId { get; set; }
        [Required]
        [DisplayName("Comment")]
        public string Body { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Moderated { get; set; }


        // Navigational properties
        public virtual ToonUser ToonUser {get; set;}
        public virtual Upload Upload { get; set; }
        public virtual ICollection<UserLike> Likes { get; set; } = new HashSet<UserLike>();

    }
}
