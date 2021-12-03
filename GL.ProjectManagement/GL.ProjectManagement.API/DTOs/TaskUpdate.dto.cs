using GL.ProjectManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GL.ProjectManagement.API.DTOs
{
    public class TaskUpdate
    {
        public string Id { get; set; }
        public string ProjectId { get; set; }
        public string AssignedToUserId { get; set; }
        public string Detail { get; set; }
        public Status Status { get; set; }
    }
}
