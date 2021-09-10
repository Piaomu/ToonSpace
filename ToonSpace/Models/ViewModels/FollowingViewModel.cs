using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToonSpace.Models.ViewModels
{
    public class FollowingViewModel
    {
        public List<ToonUser> Following { get; set; }
        public ToonUser Artist { get; set; }
    }
}
