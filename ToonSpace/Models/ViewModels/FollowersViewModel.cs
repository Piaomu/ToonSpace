using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToonSpace.Models.ViewModels
{
    public class FollowersViewModel
    {
        public List<ToonUser> Followers { get; set; } 
        public ToonUser Artist { get; set; }
    }
}
