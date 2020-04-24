using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniFacebook.Models.RepoInterface
{
    public interface IFriendRepo
    {
        public IEnumerable<string> getMyFriends(string id);
        public IEnumerable<string> getFriendRequest(string id);

    }
}
