using MiniFacebook.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniFacebook.Models.RepoInterface
{
    public interface ICommentLikeRepo
    {
        public CommentLike getCommentLike(int CommentID, string UserID);
        public void removeCommentLike(CommentLike comLike);
        public void addCommentLike(CommentLike comLike);
    }
}
