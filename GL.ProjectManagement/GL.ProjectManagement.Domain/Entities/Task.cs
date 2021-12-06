using GL.ProjectManagement.Domain.Common;
using GL.ProjectManagement.Domain.Enums;
using System;

namespace GL.ProjectManagement.Domain.Entities
{
    public class Task : TraceableEntity
    {
        public int Id { get; set; }
        public string ProjectId { get; set; }
        public Status Status { get; set; }
        public string AssignedToUserId { get; set; }
        public string Detail { get; set; }        
    }
}
