using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToonSpace.Models;

namespace ToonSpace.Services.Interfaces
{
    public interface IUploadService
    {
        public Task<List<Upload>> GetAllUploadsByArtist(string artistId);
        public Task<List<Upload>> GetAllUploadsByGenre(int genreId);
        public Task<bool> LikeUpload(string myId, int uploadId);
        public Task UnLikeUpload(string myId, int uploadId);
        public Task<List<Upload>> GetMostPopularUploadsByArtist(string artistId);
        public Task<List<Upload>> GetMostPopularUploadsByGenre(int genreId);
        public Task<List<Upload>> GetMostPopularUploadsByDay(string artistId);
        public Task<List<Upload>> GetMostPopularUploadsByMonth(string artistId);
        public Task<List<Upload>> GetMostPopularUploadsByYear(string artistId);
        public Task<List<Upload>> GetNewestUploads(string artistId);
        public Task<List<Upload>> GetNewestUpload(string artistId);
        public Task<List<Upload>> GetUploadsFromFollowing(string artistId);
        public Task<List<Upload>> GetUploadsFromFollowers(string artistId);

    }
}
