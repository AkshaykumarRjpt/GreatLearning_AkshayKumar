using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GL.ProjectManagement.API.DTOs
{
    public class UserInfo
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
