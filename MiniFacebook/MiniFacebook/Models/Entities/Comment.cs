using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniFacebook.Models.Entities
{
    public class Comment
    {
        public int CommentID { get; set; }
        public int PostID { get; set; }
        public string UserID { get; set; }
        public string CommentText { get; set; }
        public DateTime CommentDate { get; set; }
        public State CommentState { get; set; }
        public virtual Post Post { get; set; }
        public virtual User user { get; set; }
        public virtual ICollection<CommentLike> CommentLikes { get; set; }
    }
}
