using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Threading.Tasks;

namespace RockStar_IT_Events.Models
{
    public class RockstarStaff : Guest
    {
        public RockstarStaff(string firstName, string insertion, string lastName, string email, string postalCode) : base(firstName, insertion, lastName, email, postalCode)
        {
            
        }
        
    }
}
