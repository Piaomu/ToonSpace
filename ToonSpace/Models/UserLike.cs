﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToonSpace.Models
{
    public class UserLike
    {
        public int Id { get; set; }
        public ToonUser ToonUser { get; set; }
        public Upload Upload { get; set; }
        public ICollection<ToonUser> ToonUsers = new HashSet<ToonUser>();
        public ICollection<Upload> Uploads = new HashSet<Upload>();
    }
}
