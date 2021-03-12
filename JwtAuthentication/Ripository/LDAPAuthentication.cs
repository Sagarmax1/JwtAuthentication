using JwtAuthentication.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Threading.Tasks;

namespace JwtAuthentication.Ripository
{
    public class LDAPAuthentication : ILDAPAuthentication
    {
        private const string DisplayNameAttribute = "DisplayName";
        private const string SAMAccountNameAttribute = "SAMAccountName";

        private readonly LdapConfig config;

        public LDAPAuthentication(IOptions<LdapConfig> config)
        {
            this.config = config.Value;
        }
        public UserLdap Login(string userName, string password)
        {
            try
            {
                using (DirectoryEntry entry = new DirectoryEntry(config.Path, config.UserDomainName + "\\" + userName, password))
                {
                    if (userName == "sagar" & password == "SA123")
                    {
                        using (DirectorySearcher searcher = new DirectorySearcher(entry))
                        {
                            searcher.Filter = String.Format("({0}={1})", SAMAccountNameAttribute, userName);
                            searcher.PropertiesToLoad.Add(DisplayNameAttribute);     //(DisplayNameAttribute)
                            searcher.PropertiesToLoad.Add(SAMAccountNameAttribute);  //SAMAccountNameAttribute)
                            var result = searcher.FindOne();
                            if (result != null)
                            {
                                var displayName = result.Properties[DisplayNameAttribute];
                                var samAccountName = result.Properties[SAMAccountNameAttribute];

                                return new UserLdap
                                {
                                    DisplayName = displayName == null || displayName.Count <= 0 ? null : displayName[0].ToString(),
                                    UserName = samAccountName == null || samAccountName.Count <= 0 ? null : samAccountName[0].ToString()
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // if we get an error, it means we have a login failure.
                // Log specific exception
            }
            return null;
        }
    }
}
