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
    public class HomePageController : Controller
    {
        IPostRepo _Post;
        IFriendRepo _Friends;
        IPostLikeRepo _PostLikes;
        ICommentRepo _PostComments;
        ICommentLikeRepo _CommentLike;
        public HomePageController( ICommentLikeRepo CommentLike, ICommentRepo PostComments, IPostRepo Post,IFriendRepo Friends, IPostLikeRepo PostLikes)
        {
            _Post = Post;
            _Friends = Friends;
            _PostLikes = PostLikes;
            _PostComments = PostComments;
            _CommentLike = CommentLike;
        }
       
        
        //بديهيات
        //Index => Load HomePage With all Posts of user and his/her friend 
        [Authorize]
        public IActionResult index()
        {
            var friends = _Friends.getMyFriends(HttpContext.Session.GetString("ID")).ToList();
            var posts = _Post.LoadPosts(HttpContext.Session.GetString("ID")).ToList();
            List<Post> Allpost;
            foreach (var item in friends)
            {
                Allpost= _Post.LoadPosts(item).ToList();
                posts.AddRange(Allpost);
                Allpost.Clear();
            }
            ViewData["UID"] = HttpContext.Session.GetString("ID");
            return View(posts.OrderByDescending(p=>p.PostDate));
        }
        //addPost => user Can add new Post => بديهيات
        public IActionResult addPost(Post p)
        {
            p.PostDate = DateTime.Now;
            p.UserID = HttpContext.Session.GetString("ID");
            _Post.addPost(p);
            var pts = _Post.LoadPosts(HttpContext.Session.GetString("ID"));
            ViewData["UID"] = HttpContext.Session.GetString("ID");
            return PartialView("Posted", pts);
        }
        //LikePost => User Like a post => بديهيات
        public void LikePost(PostLike like)
        {
            like.UserID = HttpContext.Session.GetString("ID");
            _PostLikes.addPostLike(like);
        }
        //RemoveLikePost => User Unlike a post => بديهيات
        public void RemoveLikePost (PostLike like)
        {
            var getlike = _PostLikes.getPostLike(like.PostID, HttpContext.Session.GetString("ID")).ToList();
            _PostLikes.removePostLike(getlike[0]);
        }
        //AddComment => User add comment to a post
        public IActionResult AddComment(Comment c)
        {
            c.CommentDate = DateTime.Now;
            c.UserID = HttpContext.Session.GetString("ID");
            _PostComments.addComment(c);
            ViewData["UID"] = HttpContext.Session.GetString("ID");
            return PartialView(c);
        }
        //LoadComments => Load All Comments on a post to appeare to the user 
        public IActionResult LoadComments(int postid)
        {
            var res = _PostComments.loadPostComments(postid).ToList();
            ViewData["UID"] = HttpContext.Session.GetString("ID");
            return PartialView(res);
        }
        //updatePost => return modal with the post content to update it
        public IActionResult updatePost(int pid)
        {
            var post = _Post.getPost(pid);
            return PartialView(post);
        }
        //deletePost => بديهيات
        public IActionResult deletePost(int pID)
        {
            _Post.deletePost(pID);
            var friends = _Friends.getMyFriends(HttpContext.Session.GetString("ID")).ToList();
            var posts = _Post.LoadPosts(HttpContext.Session.GetString("ID")).ToList();
            List<Post> Allpost;
            foreach (var item in friends)
            {
                Allpost = _Post.LoadPosts(item).ToList();
                posts.AddRange(Allpost);
                Allpost.Clear();
            }
            ViewData["UID"] = HttpContext.Session.GetString("ID");
            return PartialView("Posted", posts.OrderByDescending(p => p.PostDate));
        }
        //confirmUpdate => take updated post from modal and confirm updates in database
        public void confirmUpdate(Post post)
        {
            _Post.updatePost(post);
        }
        //whoLikePost => all Users who like a post
        public IActionResult whoLikePost(int pid)
        {
            var likes = _PostLikes.getUsersThatLikePost(pid).ToList();
            return PartialView(likes);
        }
        //likeComment => بديهيات
        public void likeComment(CommentLike c)
        {
           
            c.UserID= HttpContext.Session.GetString("ID");
            _CommentLike.addCommentLike(c);
        }
        //removeCommentLike => بديهيات
        public void removeCommentLike(int cid)
        {
            var likeComm = _CommentLike.getCommentLike(cid, HttpContext.Session.GetString("ID"));
            _CommentLike.removeCommentLike(likeComm);
        }
    }
}