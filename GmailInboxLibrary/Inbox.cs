using Google.Apis.Gmail.v1.Data;
using System.Collections.Generic;

namespace GmailInboxLibrary
{
    public enum EmailGroup
    {
        Inbox,
        All,
        Label
    }
    
    public enum Category
    {
        None,
        Sender,
        Subject,
        Date,
    }

    public enum SortingType
    {
        Date,
        Alphabetically,
        GroupSize
    }

    public enum Order
    {
        Ascending,
        Descending
    }
    
    public class Inbox
    {
        public List<Message> Messages { get; set; }
    }
}