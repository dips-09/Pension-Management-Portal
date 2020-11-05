﻿using AuthorizationAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationAPI.Provider
{
    public interface IPensionProvider
    {
        public List<PensionCredentials> list { get;}

        public PensionCredentials GetPensioner(PensionCredentials cred);
    }
}
