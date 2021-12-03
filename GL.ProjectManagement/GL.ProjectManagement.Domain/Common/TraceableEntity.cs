using System;
using System.Collections.Generic;
using System.Text;

namespace GL.ProjectManagement.Domain.Common
{
    public abstract class TraceableEntity
    {
        public DateTime CreatedOn { get; set; }
    }
}
