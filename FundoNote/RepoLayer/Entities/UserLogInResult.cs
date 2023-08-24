using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Entities
{
    public class UserLogInResult
    {
        public UserEntity UserEntity { get; set; }
        public string Token { get; set; }
    }
}
