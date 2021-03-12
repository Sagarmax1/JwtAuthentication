using JwtAuthentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtAuthentication.Ripository
{
    public interface ILDAPAuthentication
    {
        UserLdap Login(string userName, string password);
    }
}
