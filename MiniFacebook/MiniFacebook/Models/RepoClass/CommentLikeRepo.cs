using MiniFacebook.Data;
using MiniFacebook.Models.Entities;
using MiniFacebook.Models.RepoInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniFacebook.Models.RepoClass
{
    public class CommentLikeRepo:ICommentLikeRepo
    {
        private readonly ApplicationDbContext context;
        public CommentLikeRepo(ApplicationDbContext _context)
        {
            context = _context;
        }

        public void addCommentLike(CommentLike comLike)
        {
            context.CommentLikes.Add(comLike);
            context.SaveChanges();
        }

        public CommentLike getCommentLike(int CommentID, string UserID)
        {
           return context.CommentLikes.Where(c => c.CommentID == CommentID && c.UserID == UserID).ToList()[0];
        }
        public void removeCommentLike(CommentLike comLike)
        {
            context.CommentLikes.Remove(comLike);
            context.SaveChanges();
        }
    }
}
