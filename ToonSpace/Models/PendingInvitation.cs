using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ToonSpace.Models
{
    public class PendingInvitation : Invitation
    {
        [Display(Name = "Accept Invitation")]
        public bool AcceptInvitation { get; set; }
        [Display(Name = "Decline Invitation")]
        public bool RejectInvitation { get; set; }

        //Navigational Properties
        public virtual Invitation Invitation { get; set; }
    }
}
