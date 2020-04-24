using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniFacebook.Models.Entities
{
    public enum FriendState
    {
        Friend,SendFriendRequest
    }
    public class Friend
    {
        public string UserID { get; set; } //who receive
        public string FriendID { get; set; }   //Who send
        public FriendState State { get; set; }
        public virtual User  User{ get; set; }

    }
}
