using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RockStar_IT_Events.Models
{
    public class MailPerson
    {
        private string Email;

        private string PostalCode;

        public MailPerson(string email, string postalCode)
        {
            Email = email;
            PostalCode = postalCode;
        }


        public void ChangeEmail(string newemail)
        {
            Email = newemail;
        }

        public void ChangePostalCode(string newPostalCode)
        {
            PostalCode = newPostalCode;
        }
    }
}
