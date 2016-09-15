using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace auth0documentdb.Models.Db
{
    public class User : Entity
    {
        public User():base("user")
        {
        }
    }
}
