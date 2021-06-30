using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToonSpace.Models;
using ToonSpace.Services.Interfaces;
using ToonSpace.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ToonSpace.Services
{
    public class UploadService : IUploadService
    {
        private readonly ApplicationDbContext _context;

        public UploadService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Upload>> GetAllUploadsByArtist(string artistId)
        {
            try 
            {
            ToonUser artist = await _context.Users.FirstOrDefaultAsync(u => u.Id == artistId);
            var uploads = artist.Uploads?.ToList();

            return uploads;

            }
            catch(Exception ex)
            {
                Debug.WriteLine($"*** ERROR *** - Error getting uploads by artist - {ex.Message}");
                throw;
            }
        }

        public async Task<List<Upload>> GetAllUploadsByGenre(int genreId)
        {
            try 
            {
            List<Upload> uploads = new();

            uploads = await _context.Upload
                                    .Include(u => u.Artist)
                                    .Include(u => u.Genre)
                                    .Include(u => u.Likes)
                                    .Include(u => u.Title)
                                    .Include(u => u.Comments)
                                    .Where(u => u.GenreId == genreId).ToListAsync();

            return uploads;

            }
            catch(Exception ex)
            {
                Debug.WriteLine($"*** ERROR *** - Error getting uploads by genre - {ex.Message}");
                throw;
            }
        }

        public async Task<List<Upload>> GetMostPopularUploadsByArtist(string artistId)
        {
            try 
            { 
            List<Upload> uploads = await GetAllUploadsByArtist(artistId);
            var topTen = uploads.OrderByDescending(u => u.Likes.Count).Take(10).ToList();

            return topTen;
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"*** ERROR *** - Error getting top uploads by Artist - {ex.Message}");
                throw;
            }

        }

        public async Task<List<Upload>> GetMostPopularUploadsByDay(string artistId)
        {
            try 
            {
            List<Upload> uploads = await GetAllUploadsByArtist(artistId);
            var topTen = uploads.OrderByDescending(u => u.Created).Take(10).ToList();

            return topTen;
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"*** ERROR *** - Error getting top uploads by Date - {ex.Message}");
                throw;
            }

        }

        public Task<List<Upload>> GetMostPopularUploadsByGenre(string genre)
        {
            throw new NotImplementedException();
        }

        public Task<List<Upload>> GetMostPopularUploadsByMonth(string artistId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Upload>> GetMostPopularUploadsByYear(string artistId)
        {
            throw new NotImplementedException();
        }

        public Task<Upload> GetNewestUpload(string artistId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Upload>> GetNewestUploads(string artistId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> LikeUpload(string myId, int uploadId)
        {
            throw new NotImplementedException();
        }

        public Task UnLikeUpload(string myId, int uploadId)
        {
            throw new NotImplementedException();
        }
    }
}
