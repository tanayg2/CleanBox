using System;
using System.Collections.Generic;
using Google.Apis.Gmail.v1.Data;

namespace Cleanbox
{
    /// <summary>
    /// Handles the download from google & write to db
    /// </summary>
    public class Cataloger
    {
        public static void Main()
        {
            InboxReceiver inboxReceiver = new InboxReceiver("me");
            List<Message> unsortedInbox = inboxReceiver.SyncMailClient();

            DBWriter writer = new DBWriter("me");
            writer.WriteToMessagesTable(unsortedInbox);

            Console.WriteLine("Wrote unsorted inbox to db");

            
        }
    }
}