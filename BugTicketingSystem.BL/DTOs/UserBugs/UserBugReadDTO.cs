using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketingSystem.BL
{
    public class UserBugReadDTO
    {
        public Guid UserId { get; set; }
        public Guid BugId { get; set; }
    }
}
