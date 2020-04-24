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
    public class PostRepo:IPostRepo
    {
        private readonly ApplicationDbContext context;
        public PostRepo(ApplicationDbContext _context)
        {
            context = _context;
        }

        public void addPost(Post post)
        {
            context.Posts.Add(post);
            context.SaveChanges();
        }

        public void deletePost(int pID)
        {
            var p = context.Posts.Find(pID);
            p.PostState = State.hidden;
            context.SaveChanges();
        }

        public Post getPost(int PostID)
        {
            return context.Posts.Where(p => p.PostID == PostID).Include("User").ToList()[0];
        }

        //Load All posts of the user by user ID
        public IEnumerable<Post> LoadPosts(string ID)
        {
            return context.Posts.Where(p => p.UserID == ID && p.PostState == State.visible)
                .Include("User").Include("PostLikes").Include("Comments")
                .OrderByDescending(p => p.PostDate).ToList();
        }

        public void updatePost(Post post)
        {
            var p = context.Posts.Find(post.PostID);
            if (post.PostPhoto != null)
            {
                p.PostPhoto = post.PostPhoto;
            }
            p.Content = post.Content;
            context.SaveChanges();
        }
    }
}
