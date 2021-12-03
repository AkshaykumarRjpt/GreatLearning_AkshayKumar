using GL.ProjectManagement.Domain.Enums;
using System;

namespace GL.ProjectManagement.Domain.Entities
{
    public class Task
    {
        public string Id { get; set; }
        public string ProjectId { get; set; }
        public Status Status { get; set; }
        public string AssignedToUserId { get; set; }
        public string Detail { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
