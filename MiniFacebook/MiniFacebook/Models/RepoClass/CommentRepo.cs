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
    public class CommentRepo:ICommentRepo
    {
        private readonly ApplicationDbContext context;
        public CommentRepo(ApplicationDbContext _context)
        {
            context = _context;
        }

        public void addComment(Comment c)
        {
            context.Comments.Add(c);
            context.SaveChanges();
        }

        // get all comments of post by post id
        public IEnumerable<Comment> loadPostComments(int PostID)
        {
            return context.Comments.Where(c => c.PostID == PostID)
                .Include("CommentLikes").OrderByDescending(c => c.CommentDate);
        }
    }
}
