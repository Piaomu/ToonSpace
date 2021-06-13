using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ToonSpace.Models
{
    public class Notification
    {
        public int Id { get; set; }
        [DisplayName("Upload")]
        public int UploadId { get; set; }

        [Required]
        [DisplayName("Subject")]
        public string Title { get; set; }
        public string Message { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Date")]
        public DateTimeOffset Created { get; set; }

        [Required]
        [DisplayName("Recipient")]
        public string RecipientId { get; set; }

        [Required]
        [DisplayName("Sender")]
        public string SenderId { get; set; }

        [DisplayName("Has Been Viewed")]
        public bool Viewed { get; set; }


        //Navigational properties
        public virtual Upload Upload { get; set; }
        public virtual ToonUser Recipient { get; set; }
        public virtual ToonUser Sender { get; set; }
    }
}
