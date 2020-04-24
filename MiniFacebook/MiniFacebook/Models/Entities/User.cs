using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace MiniFacebook.Models.Entities
{
    public enum Gender
    {
        Male,Female
    }
    public enum UserState
    {
        blocked,notblocked
    }
    public class User:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        [AllowNull]
        public string Bio { get; set; }
        public DateTime BirthDate { get; set; }
        [AllowNull]
        public string ProfilePic { get; set; }

        public UserState UserState { get; set; }
        public virtual ICollection<Post> Posts{ get; set; }
        public virtual ICollection<Friend> Friends{ get; set; }
        public virtual ICollection<PostLike> PostLikes{ get; set; }
        public virtual ICollection<Comment> Comments{ get; set; }
        public virtual ICollection<CommentLike> CommentLikes{ get; set; }

    }
}
