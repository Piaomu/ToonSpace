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

        public async Task<List<Upload>> GetUploadsFromFollowing(string artistId)
        {
            try
            {
                ToonUser artist = await _context.Users.FirstOrDefaultAsync(u => u.Id == artistId);
                List<ToonUser> following = artist.Following.ToList();
                List<Upload> followingUploads = new();

                foreach (ToonUser user in following)
                {
                    followingUploads.AddRange(user.Uploads);
                }

                return followingUploads;
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"*** ERROR *** - Error getting your following's uploads - {ex.Message}");
                throw;
            }

        }

        public async Task<List<Upload>> GetUploadsFromFollowers(string artistId)
        {
            try
            {
                ToonUser artist = await _context.Users.FirstOrDefaultAsync(u => u.Id == artistId);
                List<ToonUser> followers = artist.Followers.ToList();
                List<Upload> followerUploads = new();

                foreach (ToonUser user in followers)
                {
                    followerUploads.AddRange(user.Uploads);
                }

                return followerUploads;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"*** ERROR *** - Error getting your followers' uploads - {ex.Message}");
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

        public async Task<List<Upload>> GetMostPopularUploadsByGenre(int genreId)
        {
            try 
            {
            List<Upload> uploads = await GetAllUploadsByGenre(genreId);
            var topTen = uploads.OrderByDescending(u => u.Likes.Count).Take(10).ToList();

            return topTen;
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"*** ERROR *** - Error getting top uploads by Genre - {ex.Message}");
                throw;
            }
        }

        //Implement this if necessary
        public Task<List<Upload>> GetMostPopularUploadsByMonth(string artistId)
        {
            throw new NotImplementedException();
        }

        //Implement this if necessary
        public Task<List<Upload>> GetMostPopularUploadsByYear(string artistId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Upload>> GetNewestUpload(string artistId)
        {
            try 
            {
            ToonUser artist = await _context.Users.FirstOrDefaultAsync(u => u.Id == artistId);
            List<Upload> upload = artist.Uploads.OrderByDescending(u => u.Created).Take(1).ToList();

            return upload;
            } catch(Exception ex)
            {
                Debug.WriteLine($"*** ERROR *** - Error getting most recent upload - {ex.Message}");
                throw;
            }
        }

        public async Task<List<Upload>> GetNewestUploads(string artistId)
        {
            try
            {
                ToonUser artist = await _context.Users.FirstOrDefaultAsync(u => u.Id == artistId);
                List<Upload> upload = artist.Uploads.OrderByDescending(u => u.Created).Take(5).ToList();

                return upload;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"*** ERROR *** - Error getting most recent uploads - {ex.Message}");
                throw;
            }
        }

        public async Task<bool> LikeUpload(string myId, int uploadId)
        {
            try 
            {
            ToonUser user = await _context.Users.FirstOrDefaultAsync(u => u.Id == myId);
            Upload upload = await _context.Upload.FirstOrDefaultAsync(u => u.Id == uploadId);
            UserLike like = new UserLike()
            {
                ToonUser = user,
                Upload = upload
            };

            if(user.Likes.Count(l => l.Id == uploadId) == 0)
            {
                upload.Likes.Add(like);
                await _context.SaveChangesAsync();
                return true;
            }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"*** ERROR *** - Error liking media - {ex.Message}");
                throw;
            }

        }

        public async Task UnLikeUpload(string myId, int uploadId)
        {
            try 
            { 
            ToonUser user = await _context.Users.FirstOrDefaultAsync(u => u.Id == myId);
            Upload upload = await _context.Upload.FirstOrDefaultAsync(u => u.Id == uploadId);
            UserLike like = upload.Likes.FirstOrDefault(l => l.ToonUser.Id == user.Id);

            upload.Likes.Remove(like);
            user.Likes.Remove(like);
            await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"*** ERROR *** - Error unliking media - {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DoesUserAlreadyLike(string userId, int userLikeId)
        {
            ToonUser user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            UserLike like = user.Likes.FirstOrDefault(l => l.Id == userLikeId);

            return like != null;
        }
    }
}
