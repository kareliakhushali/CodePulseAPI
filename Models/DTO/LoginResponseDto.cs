using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodePulseAPI.Models.DTO
{
    public class LoginResponseDto
    {
      public string Email { get; set; }
        public string Token { get; set; }
        public List<string> Roles { get; set; }
    }
}
