using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GL.ProjectManagement.API.DTOs
{
    public class TaskCreation
    {
        public string ProjectId { get; set; }
        public string AssignedToUserId { get; set; }
        public string Detail { get; set; }
    }
}
