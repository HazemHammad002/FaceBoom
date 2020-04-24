using MiniFacebook.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniFacebook.Models.RepoInterface
{
    public interface IUserRepo
    {
        public User getUser(string Uid);

        public void updateProfile(User user);


    }
}
