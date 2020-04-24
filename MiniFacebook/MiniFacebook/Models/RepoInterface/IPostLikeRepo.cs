using MiniFacebook.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniFacebook.Models.RepoInterface
{
    public interface IPostLikeRepo
    {
        public IEnumerable<PostLike> getPostLike(int PostID, string UserID);
        public IEnumerable<User> getUsersThatLikePost(int PostID);
        public void removePostLike(PostLike like);
        public void addPostLike(PostLike like);
    }
}
