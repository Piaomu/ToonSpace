using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToonSpace.Models;

namespace ToonSpace.Services.Interfaces
{
    interface IRelationService
    {
        public Task<List<ToonUser>> GetFollowersAsync(string myId, string toonUserIds);

        public Task<List<ToonUser>> GetFollowingAsync(string myId, string toonUserIds);

        public Task<bool> FollowUser(string myId, string toonUserId);
        public Task UnfollowUser(string myId, string toonUserId);
        public Task<bool> BlockUser(string myId, string toonUserId);
        public Task<bool> IsUserFollowingMe(string myId, string toonUserId);
        public Task<bool> AmIFollowingUser(string myId, string toonUserId);
    }
}
