using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ToonSpace.Enums;

namespace ToonSpace.Models
{
    public class Upload
    {
        public int Id { get; set; }
        public int GenreId { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public DateTime Created { get; set; }
        public byte[] Image { get; set; }
        public string ContentType { get; set; }
        public Genre Genre { get; set; }

        [Display(Name = "Select Genre")]
        public GenreName GenreName { get; set; }
    }
}
