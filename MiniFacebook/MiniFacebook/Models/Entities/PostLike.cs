using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniFacebook.Models.Entities
{
    public class PostLike
    {
        public int PostID { get; set; }
        public string UserID { get; set; }
        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
    }
}
