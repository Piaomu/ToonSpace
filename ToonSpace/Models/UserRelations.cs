using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToonSpace.Models
{
    public class UserRelations
    {
        public ICollection<string> FollowerIds = new HashSet<string>();
        public ICollection<string> FollowingIds = new HashSet<string>();
        public ICollection<Invitation> Invitations = new HashSet<Invitation>();
    }
}
