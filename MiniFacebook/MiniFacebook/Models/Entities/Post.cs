using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace MiniFacebook.Models.Entities
{
    public enum Feeling
    {
        normal,happy,sad, optimistic, pessimistic
    }
    public enum State
    {
        visible,hidden
    }
    public class Post
    {
        public int PostID { get; set; }
        public string Content { get; set; }
        public DateTime PostDate { get; set; }
        
        public Feeling? Feeling { get; set; }
        [AllowNull]
        public string PostPhoto { get; set; }
        public string UserID { get; set; }
        public State PostState { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<PostLike> PostLikes{ get; set; }
        public virtual ICollection<Comment> Comments{ get; set; }
    }
}
//Likes,
//,Ienumerable<comment> comment
