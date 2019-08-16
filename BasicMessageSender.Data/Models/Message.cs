using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicMessageSender.Data.Models
{
    public class Message
    {
        public int Id { get; set; }
        public DateTime Sent { get; set; }
        public string Data { get; set; }
        public bool IsRead { get; set; }

        [ForeignKey("Sender")]
        public int SenderId { get; set; }
        public User Sender { get; set; }

        [ForeignKey("Receiver")]
        public int ReceiverId { get; set; }
        public User Receiver { get; set; }



    }
}
