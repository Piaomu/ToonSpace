using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToonSpace.Models.ViewModels
{
    public class IndexFeedViewModel
    {
       public List<ToonUser> Followers { get; set; }
       public List<ToonUser> Following { get; set; }
       public List<Upload> Uploads { get; set; }
       public ToonUser User { get; set; }
    }
}
