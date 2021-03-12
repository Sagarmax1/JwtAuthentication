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
    public class LDAPAuthentication : ControllerBase
    {

        private readonly ILDAPAuthentication authService;

        ResponseData responsedata;

        public LDAPAuthentication(ILDAPAuthentication authService)
        {
            this.authService = authService;
        }

        [HttpPost]
        public  ActionResult Login(LoginLDAP user)
        {
            //   var user = authService.Login(userName, password);

            UserLdap objuser = authService.Login(user.userName, user.password);
            if (user != null)
            {
                responsedata = new ResponseData(objuser, "success", "");
            }
            else
            {
                responsedata = new ResponseData(null, "ERROR", "ERROR MESSAGE");
            }
            return Ok(responsedata);
        }
    }
}
