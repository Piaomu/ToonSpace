using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToonSpace.Models
{
    public class Genre
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public IFormFile ImageFile { get; set; }
        public byte[] GenreImage { get; set; }
        public string ContentType { get; set; }
        public virtual ICollection<Upload> Upload { get; set; } = new HashSet<Upload>();

    }
}
