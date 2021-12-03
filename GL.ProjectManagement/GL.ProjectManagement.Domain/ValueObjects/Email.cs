using GL.ProjectManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace GL.ProjectManagement.Domain.ValueObjects
{
    public class Email : ValueObject
    {
        public string EmailAddress { get; set; }
        private Email()
        {

        }
        private Email(string email)
        {
            EmailAddress = email;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return EmailAddress;

        }
        public static Email Address(string email)
        {
           return new Email(email);
        }

        public static implicit operator string(Email email)
        {
            return email.ToString();
        }

        public override string ToString()
        {
            return EmailAddress;
        }
    }
}
