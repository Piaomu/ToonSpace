using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ToonSpace.Enums
{
    public enum GenreName
    {
        Comedy,
        Romance,
        Superhero,
        [Display(Name = "Sci-Fi")]
        SciFi,
        Fantasy
    }
}


