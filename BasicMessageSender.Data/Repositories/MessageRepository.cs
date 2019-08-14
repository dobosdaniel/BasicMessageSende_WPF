using BasicMessageSender.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicMessageSender.Data.Repositories
{
    public class MessageRepository
    {
        public MessageRepository()
        {
        }
        public void AddMessage(Message newMessage)
        {
            using (var Context = new BMSContext())
            {
                //Message newMessage = new Message();
                //newMessage.Sender = Context.Users.Where(u => u.Username.Equals(senderUserName)).FirstOrDefault();
                //newMessage.Receiver = Context.Users.Where(u => u.Username.Equals(receiverUserName)).FirstOrDefault();
                //newMessage.IsRead = false;
                //newMessage.Sent = DateTime.Now;


                Context.Messages.Add(newMessage);
                Context.SaveChangesAsync();
            }
        }
        public IQueryable<Message> GetAllReceivedMessagesForUser(string userName)
        {
            using (var Context = new BMSContext())
            {
                return Context.Messages.Where(u => u.Receiver.Username == userName);
            }
        }
        
        public void MarkMessageAsRead(int messageId, bool isRead)
        {
            using (var Context = new BMSContext())
            {
                var message = Context.Messages.Where(m => m.Id == messageId).FirstOrDefault();
                if (message != null)
                    message.IsRead = !isRead;
            }
        }
        public int GetSentMessagesNumberByUserSentToday(string userName)
        {
            using (var Context = new BMSContext())
            {
                return Context.Messages.Where(m => m.Sender.Username == userName && m.Sent == DateTime.Today).Count();
            }
        }
        public Message GetMessageById(int id)
        {
            using (var Context = new BMSContext())
            {
                return Context.Messages.Where(m => m.Id == id).FirstOrDefault();
            }
        }
    }
}
