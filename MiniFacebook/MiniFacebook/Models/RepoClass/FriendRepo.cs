using Microsoft.AspNetCore.Http;
using MiniFacebook.Data;

using MiniFacebook.Models.Entities;
using MiniFacebook.Models.RepoInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniFacebook.Models.RepoClass
{
    public class FriendRepo : IFriendRepo
    {
        private readonly ApplicationDbContext context;
        public FriendRepo(ApplicationDbContext _context)
        {
            context = _context;
        }

        public IEnumerable<string> getFriendRequest(string id)
        {
            return context.Friends.Where(f => f.UserID == id && f.State == FriendState.SendFriendRequest)
                .Select(f => f.FriendID).ToList();
        }

        public IEnumerable<string> getMyFriends(string id)
        {
            return context.Friends.Where(f => f.UserID == id && f.State == FriendState.Friend)
                .Select(f => f.FriendID).ToList();
        }
    }
}
