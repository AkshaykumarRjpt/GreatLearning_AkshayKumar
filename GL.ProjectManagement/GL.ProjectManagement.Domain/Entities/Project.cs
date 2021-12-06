using GL.ProjectManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace GL.ProjectManagement.Domain.Entities
{
    public class Project : TraceableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }

        // Navigations
        [JsonIgnore]
        public List<Task> Task { get; set; }
    }
}
