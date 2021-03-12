using JwtAuthentication.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.DirectoryServices;

namespace JwtAuthentication.Ripository
{
    public class Authentication : IAuthentication
    {

        private readonly AppSettings _appSettings;

        public Authentication(IOptions<AppSettings> appSeting)
        {
            _appSettings = appSeting.Value;
        }


        string ConnectionString = "Server=localhost; user=root; password=Path@123;Database=jwtauthentication";


        public User Login2(string UserName , string Password)
        {
            User userModel = new User()
            {
                //Username = user.UserName,
                //Password = user.Password
                Username = UserName,
                Password = Password

            };

            try
            {

                using (MySqlConnection con = new MySqlConnection(ConnectionString))
                {
                    //if (user.UserName == "sagar" & user.Password == "SA123")
                    if (UserName == "sagar" & Password == "SA123")
                    {

                        // authentication successful so generate jwt token
                        var tokenHandler = new JwtSecurityTokenHandler();
                        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                        var tokenDescriptor = new SecurityTokenDescriptor
                        {
                            Subject = new ClaimsIdentity(new Claim[]
                            {
                                new Claim(ClaimTypes.Name, userModel.Username),
                                new Claim(ClaimTypes.Email, userModel.Username)
                            }),
                            Expires = DateTime.UtcNow.AddDays(7),
                            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                        };

                        //if (AuthenticateActiveDirectoryUser(UserName, Password))   // For Ldap Authentication
                        //{

                        //    User log = new User();
                        //    log.Username = UserName;
                        //    log.Password = Password;

                        //}

                        var token = tokenHandler.CreateToken(tokenDescriptor);
                        userModel.Token = tokenHandler.WriteToken(token);


                    }

                }
                return userModel;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }


        // Ldap Code Start for checking Purpose put system password and email  For Ldap Authentication

        //private bool AuthenticateActiveDirectoryUser(string UserName, string Password)   // For LDAP Authentication
        //{
        //    try
        //    {
        //        //DirectoryEntry de = new DirectoryEntry(CompassConnection.GetCompassLDAPConnection(), username, password);
        //        DirectoryEntry de = new DirectoryEntry("LDAP://pathinfotech.com", UserName, Password);
        //        de.AuthenticationType = AuthenticationTypes.Secure;
        //        string strName = de.Name;
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        // Ldap Code End 



        public string DecryptString(string encrString)  // DecryptString Syntax
        {
            byte[] b;
            string decrypted;
            try
            {
                b = Convert.FromBase64String(encrString);
                decrypted = System.Text.ASCIIEncoding.ASCII.GetString(b);
            }
            catch (FormatException )
            {
                decrypted = "";
            }
            return decrypted;
        }


    }
}
