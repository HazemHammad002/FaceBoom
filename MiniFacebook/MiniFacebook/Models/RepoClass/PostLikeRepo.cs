using Microsoft.EntityFrameworkCore;
using MiniFacebook.Data;
using MiniFacebook.Models.Entities;
using MiniFacebook.Models.RepoInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniFacebook.Models.RepoClass
{
    public class PostLikeRepo:IPostLikeRepo
    {
        private readonly ApplicationDbContext context;
        public PostLikeRepo(ApplicationDbContext _context)
        {
            context = _context;
        }

        public void addPostLike(PostLike like)
        {
            context.PostLikes.Add(like);
            context.SaveChanges();
        }

        //get Like On Post by post id and user id
        public IEnumerable<PostLike> getPostLike(int PostID, string UserID)
        {
            return context.PostLikes.Where(l => l.PostID == PostID && l.UserID == UserID);
        }
        public IEnumerable<User> getUsersThatLikePost(int PostID)
        {
            return context.PostLikes.Where(p => p.PostID == PostID).Include("User").Select(p => p.User);
        }

        public void removePostLike(PostLike like)
        {
            context.PostLikes.Remove(like);
            context.SaveChanges();
        }
    }
}
