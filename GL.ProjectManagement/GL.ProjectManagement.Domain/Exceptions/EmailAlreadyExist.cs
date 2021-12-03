using System;
using System.Collections.Generic;
using System.Text;

namespace GL.ProjectManagement.Domain.Exceptions
{
    public class EmailAlreadyExist : Exception
    {
        public EmailAlreadyExist(string email):
            base($"Email {email} already exist!!")
        {

        }
    }
}
