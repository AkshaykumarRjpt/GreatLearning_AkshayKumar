using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GL.ProjectManagement.API.DTOs
{
    public class TaskInfo
    {
        public string Id { get; set; }
        public string ProjectId { get; set; }
        public int Status { get; set; }
        public string AssignedToUserId { get; set; }
        public string Detail { get; set; }
        public string CreatedOn { get; set; }
    }
}
