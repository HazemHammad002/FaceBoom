using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniFacebook.Data;
using MiniFacebook.Models.Entities;
using MiniFacebook.Models.RepoInterface;

namespace MiniFacebook.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        
        IPostRepo _Post;
        IFriendRepo _Friends;
        IPostLikeRepo _PostLikes;
        ICommentRepo _PostComments;
        ICommentLikeRepo _CommentLike;
        IUserRepo _User;
        public ProfileController(ICommentLikeRepo CommentLike ,IUserRepo User , ICommentRepo PostComments, IPostRepo Post, IFriendRepo Friends, IPostLikeRepo PostLikes)
        {
            _Post = Post;
            _Friends = Friends;
            _PostLikes = PostLikes;
            _PostComments = PostComments;
            _CommentLike = CommentLike;
            _User = User;
        }
        public IActionResult ProfilePage()
        {
            var posts = _Post.LoadPosts(HttpContext.Session.GetString("ID")).ToList();
            ViewData["UID"] = HttpContext.Session.GetString("ID");
            ViewData["User"] = _User.getUser(HttpContext.Session.GetString("ID"));

            var friend = _Friends.getMyFriends(HttpContext.Session.GetString("ID")).ToList();

            var friendReq = _Friends.getFriendRequest(HttpContext.Session.GetString("ID")).ToList();

            List<User> AllFriends = new List<User>();

            List<User> FriendRequests = new List<User>();

            foreach (var item in friend)
            {
                var f= _User.getUser(item);
                AllFriends.Add(f);

            }

            foreach (var item2 in friendReq)
            {
                var fr = _User.getUser(item2);
                FriendRequests.Add(fr);

            }
            ViewData["Friends"] = AllFriends;
            ViewData["FriendRequest"] = FriendRequests;
            return View(posts.OrderByDescending(p => p.PostDate));

        }

        public IActionResult addPost(Post p)
        {
            p.PostDate = DateTime.Now;
            p.UserID = HttpContext.Session.GetString("ID");
            _Post.addPost(p);
            var pts = _Post.LoadPosts(HttpContext.Session.GetString("ID"));
            ViewData["UID"] = HttpContext.Session.GetString("ID");
            return PartialView("PostedProfile", pts);
        }

        public void LikePost(PostLike like)
        {
            like.UserID = HttpContext.Session.GetString("ID");
            _PostLikes.addPostLike(like);
        }

        public void RemoveLikePost(PostLike like)
        {
            var getlike = _PostLikes.getPostLike(like.PostID, HttpContext.Session.GetString("ID")).ToList();
            _PostLikes.removePostLike(getlike[0]);
        }
        public IActionResult AddComment(Comment c)
        {
            c.CommentDate = DateTime.Now;
            c.UserID = HttpContext.Session.GetString("ID");
            _PostComments.addComment(c);
            ViewData["UID"] = HttpContext.Session.GetString("ID");
            return PartialView(c);
        }
        public IActionResult LoadComments(int postid)
        {
            var res = _PostComments.loadPostComments(postid).ToList();
            ViewData["UID"] = HttpContext.Session.GetString("ID");
            return PartialView(res);
        }

        public IActionResult updatePost(int pid)
        {
            var post = _Post.getPost(pid);
            return PartialView(post);
        }
        public IActionResult updateProfile(string id)
        {
            var user = _User.getUser(id);
            return PartialView(user);
        }

        [HttpPost]
        public void confirmProfileUpdate(User user)
        {
            _User.updateProfile(user);
        }
        public IActionResult deletePost(int pID)
        {
            _Post.deletePost(pID);
            var posts = _Post.LoadPosts(HttpContext.Session.GetString("ID")).ToList();            
            ViewData["UID"] = HttpContext.Session.GetString("ID");
            return PartialView("Posted", posts.OrderByDescending(p => p.PostDate));
        }

        public void confirmUpdate(Post post)
        {
            _Post.updatePost(post);
        }
        public IActionResult whoLikePost(int pid)
        {
            var likes = _PostLikes.getUsersThatLikePost(pid).ToList();
            return PartialView(likes);
        }
        public void likeComment(CommentLike c)
        {

            c.UserID = HttpContext.Session.GetString("ID");
            _CommentLike.addCommentLike(c);
        }
        public void removeCommentLike(int cid)
        {
            var likeComm = _CommentLike.getCommentLike(cid, HttpContext.Session.GetString("ID"));
            _CommentLike.removeCommentLike(likeComm);
        }
    }
}