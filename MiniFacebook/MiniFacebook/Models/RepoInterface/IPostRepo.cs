using MiniFacebook.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniFacebook.Models.RepoInterface
{
    public interface IPostRepo
    {
        public IEnumerable<Post> LoadPosts(string ID);
        public void updatePost(Post post);
        public void deletePost(int pID);
        public Post getPost(int PostID);
        public void addPost(Post post);
    }
}
