using InvestNinja.Core.Utils;
using System.Collections.Generic;

namespace InvestNinja.Core.Domain
{
    public class AuthorizationToken
    {
        public string Iss { get; private set; }

        public string Aud { get; private set; }

        public string Sub { get; private set; }

        public IList<string> Roles { get; private set; }

        public string UserName { get; private set; }

        public AuthorizationToken(string iss, string aud, string sub, string userName, IList<string> additionalRoles)
        {
            Iss = iss;
            Aud = aud;
            Sub = sub;
            UserName = userName;
            Roles = new List<string>() { "User" };
            if (additionalRoles != null)
                Roles.AddRange(additionalRoles);
        }
    }
}
