using GL.ProjectManagement.Domain.Common;
using GL.ProjectManagement.Domain.Enums;
using System;
using System.Text.Json.Serialization;

namespace GL.ProjectManagement.Domain.Entities
{
    public class Task : EntityBase, ITraceableEntity
    {
        
        public int ProjectId { get; set; }
        public Status Status { get; set; }
        public int AssignedToUserId { get; set; }
        public string Detail { get; set; }
        public DateTime CreatedOn { get; set; }

        // Navigations

        [JsonIgnore]
        public Project Project { get; set; }

        [JsonIgnore]
        public User User { get; set; }
        
    }
}
