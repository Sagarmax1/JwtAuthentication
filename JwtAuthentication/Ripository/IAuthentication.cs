using JwtAuthentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtAuthentication.Ripository
{
    public interface IAuthentication
    {
        // void Login(Login user);

        //  User Login2(Login user);

        User Login2(string UserName , string Password);

    }
}
