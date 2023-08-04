using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodePulseAPI.Repositories.Interface
{
  public  interface ITokenRepository
    {
        string CreateJwtToken(IdentityUser user, List<string> roles);

    }
}
