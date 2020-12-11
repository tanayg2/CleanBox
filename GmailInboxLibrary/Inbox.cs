using Google.Apis.Gmail.v1.Data;
using System.Collections.Generic;

namespace GmailInboxLibrary
{
    public class Inbox
    {
        public List<Message> Messages { get; set; }
    }
}