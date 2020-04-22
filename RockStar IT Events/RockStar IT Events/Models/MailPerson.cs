using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RockStar_IT_Events.Models
{
    public class MailPerson
    {
        private string eMail;

        private string postalCode;

        public MailPerson(string email, string postalcode)
        {
            eMail = email;
            postalCode = postalcode;
        }

        public void ChangeEmail(string newemail)
        {
            eMail = newemail;
        }

        public void ChangePostalCode(string newPostalCode)
        {
            postalCode = newPostalCode;
        }
    }
}


