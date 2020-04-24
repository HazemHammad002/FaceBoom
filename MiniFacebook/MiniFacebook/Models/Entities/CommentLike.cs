using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniFacebook.Models.Entities
{
    public class CommentLike
    {
        public int CommentID { get; set; }
        public int PostID { get; set; }
        public string UserID { get; set; }
        public virtual Comment Comment { get; set; }
        public virtual User User { get; set; }

    }
}
