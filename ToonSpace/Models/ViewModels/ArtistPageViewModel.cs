using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToonSpace.Models.ViewModels
{
    public class ArtistPageViewModel
    {
        public List<ToonUser> Followers { get; set; }
        public List<ToonUser> Following { get; set; }
        public List<Upload> ArtistUploads { get; set; }
        public List<Upload> TrendingUploads { get; set; }
        public List<Upload> RecentUploads { get; set; }
        public List<Upload> FeaturedUploads { get; set; }
        public ToonUser Artist { get; set; }
    }
}
