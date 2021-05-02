using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToonSpace.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string genre { get; set; }
        public byte[] GenreImage { get; set; }
        public ICollection<Upload> Upload { get; set; } = new HashSet<Upload>();

    }
}
