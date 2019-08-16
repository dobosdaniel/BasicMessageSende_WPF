using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicMessageSender.Data.Models
{
    public class BlockedUsers
    {
        public int Id { get; set; }
        public User BlockerUser { get; set; }
        public int BlockerUserId { get; set; }
        public User BlockedUser { get; set; }
        public int BlockedUserId { get; set; }

    }
}
