using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using ToonSpace.Enums;

namespace ToonSpace.Models
{
    public class Invitation
    {
        public int Id { get; set; }

        [DisplayName("Date")]
        public DateTimeOffset InviteDate { get; set; }
        [DisplayName("Code")]
        public Guid Token { get; set; }
        public string InvitorId { get; set; }
        [DisplayName("Email")]
        public string InviteeId { get; set; }
        [DisplayName("First Name")]
        public string InviteeFirstName { get; set; }
        [DisplayName("Last Name")]
        public string InviteeLastName { get; set; }
        public bool IsValid { get; set; }
        public InvitationStatus Status { get; set; }

        public virtual ToonUser Invitor { get; set; }
        public virtual ToonUser Invitee { get; set; }

    }
}
