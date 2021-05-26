using System;
using System.Collections.Generic;
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
        public string Body { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Moderated { get; set; }
        public virtual Upload Upload { get; set; }
        //Still need to make the ToonUser Model
        public virtual ToonUser ToonUser {get; set;}
    }
}
