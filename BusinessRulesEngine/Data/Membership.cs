using BusinessRulesEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessRulesEngine.Data
{
    public class Membership
    {
        public Guid Id { get; set; }
        public bool Active { get; set; }
        public string Email { get; set; }
        public string MembershipType { get; set; }
        public DateTime Created { get; set; }
        public DateTime Activated { get; set; }
    }
}
