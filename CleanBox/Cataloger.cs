using System;
using System.Collections.Generic;
using Google.Apis.Gmail.v1.Data;

namespace CleanBox
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
            
            //TODO: Write inbox to db

        }
    }
}