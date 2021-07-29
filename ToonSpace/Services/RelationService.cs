using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ToonSpace.Data;
using ToonSpace.Models;
using ToonSpace.Services.Interfaces;

namespace ToonSpace.Services
{
    public class RelationService : IRelationService
    {
        private readonly ApplicationDbContext _context;

        public RelationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> FollowUser(string myId, string toonUserId)
        {
            //ToonUser user = await _context.Users.FirstOrDefaultAsync(u => u.Id == myId);
            //ToonUser toonUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == toonUserId);

            //user?.Following.Add(toonUser);
            //toonUser?.Followers.Add(user);
            //await _context.SaveChangesAsync();
            //return true;
            try
            {
                ToonUser user = await _context.Users.FirstOrDefaultAsync(u => u.Id == myId);
                if (user is not null)
                {
                    ToonUser toonUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == toonUserId);
                    if (!await AmIFollowingUser(myId, toonUserId))
                    {
                        try
                        {
                            user?.Following.Add(toonUser);
                            toonUser?.Followers.Add(user);
                            await _context.SaveChangesAsync();
                            return true;
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"*** ERROR *** - Error following user. --> {ex.Message}");
                return false;
            }
        }

        public async Task<List<ToonUser>> GetFollowersAsync(string myId)
        {
            ToonUser user = await _context.Users
                            .Include(u =>u.Followers)
                            .FirstOrDefaultAsync(u => u.Id == myId);

            return user?.Followers.ToList();
        }

        public async Task<List<ToonUser>> GetFollowingAsync(string myId)
        {
            ToonUser user = await _context.Users
                            .Include(u => u.Following)
                            .FirstOrDefaultAsync(u => u.Id == myId);

            return user?.Following.ToList();
        }

        public async Task UnfollowUser(string myId, string toonUserId)
        {
            try
            {
                ToonUser user = await _context.Users.FirstOrDefaultAsync(u => u.Id == myId);
                ToonUser toonUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == toonUserId);

                user?.Following.Remove(toonUser);
                toonUser?.Followers.Remove(user);
                await _context.SaveChangesAsync();
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task<bool> IsUserFollowingMe(string myId, string toonUserId)
        {
            ToonUser toonUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == toonUserId);

            bool result = toonUser.Following.Any(u => u.Id == myId);

            return result;
        }

        public async Task<bool> AmIFollowingUser(string myId, string toonUserId)
        {
            ToonUser user = await _context.Users.FirstOrDefaultAsync(u => u.Id == myId);
            ToonUser toonUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == toonUserId);

            bool result = user.Following.Any(u => u.Id == toonUserId);

            return result;
        }
    }
}
