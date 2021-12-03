using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GL.ProjectManagement.API.DTOs
{
    public class ProjectInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public string CreatedOn { get; set; }
    }
}
