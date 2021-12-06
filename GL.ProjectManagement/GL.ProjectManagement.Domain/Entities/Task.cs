using GL.ProjectManagement.Domain.Common;
using GL.ProjectManagement.Domain.Enums;
using System;
using System.Text.Json.Serialization;

namespace GL.ProjectManagement.Domain.Entities
{
    public class Task : TraceableEntity
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public Status Status { get; set; }
        public int AssignedToUserId { get; set; }
        public string Detail { get; set; }

        // Navigations

        [JsonIgnore]
        public Project Project { get; set; }

        [JsonIgnore]
        public User User { get; set; }
    }
}
