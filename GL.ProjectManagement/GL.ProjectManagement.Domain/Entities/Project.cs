using GL.ProjectManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace GL.ProjectManagement.Domain.Entities
{
    public class Project : TraceableEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
    }
}
