using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ToonSpace.Models
{
    public class Invite
    {
        public int Id { get; set; }

        [DisplayName("Date")]
        public DateTimeOffset InviteDate { get; set; }
        [DisplayName("Code")]
        public Guid Token { get; set; }
        public string Invitor { get; set; }
        [DisplayName("Email")]
        public string Invitee { get; set; }
        [DisplayName("First Name")]
        public string InviteeFirstName { get; set; }
        [DisplayName("Last Name")]
        public string InviteeLastName { get; set; }
        public bool IsValid { get; set; }
    }
}
