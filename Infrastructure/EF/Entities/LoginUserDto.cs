using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EF.Entities
{
    public class LoginUserDto
    {
        public string LoginName { get; set; }
        public string Password { get; set; }
    }
}
