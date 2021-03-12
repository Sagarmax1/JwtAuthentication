using JwtAuthentication.Models;
using JwtAuthentication.Ripository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthentication _authentication;

        ResponseData responsedata;

        public AuthenticationController(IAuthentication authentication)
        {
            this._authentication = authentication;
        }


        [HttpPost("login")]
        public ActionResult Login(Login user)
        {
            if(ModelState.IsValid)
            {
                //  _authentication.Login2(user);
                User objuser = _authentication.Login2(user.UserName, user.Password);

                if (user != null)
                {
                    responsedata = new ResponseData(objuser, "success", "");
                }
                else
                {
                    responsedata = new ResponseData(null, "ERROR", "ERROR MESSAGE");
                }

            }
            return Ok(responsedata);
        }

    }
}
