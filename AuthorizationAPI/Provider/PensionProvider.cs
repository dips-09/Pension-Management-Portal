using AuthorizationAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationAPI.Provider
{
    public class PensionProvider : IPensionProvider
    {
        private static List<PensionCredentials> List = new List<PensionCredentials>()
        {
            new PensionCredentials{ Username = "user1", Password = "user1"},
            new PensionCredentials{ Username = "user2", Password = "user2"}
        };
        public List<PensionCredentials> list { get => List; }

        public PensionCredentials GetPensioner(PensionCredentials cred)
        {
            PensionCredentials penCred = List.FirstOrDefault(user => user.Username == cred.Username && user.Password == cred.Password);

            return penCred;
        }
    }
}
