using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;

namespace Cleanbox
{
    public class InboxReceiver
    {
        private static string[] Scopes = { GmailService.Scope.GmailReadonly };
        private static string ApplicationName = "CleanBox";
        private string UserID = "me";
        private GmailService service;

        public InboxReceiver(string user)
        {
            this.UserID = user;
            AuthorizeCredentialsAndInitializeService();
        }
        
        /// <summary>
        ///  Use credentials from token.json to pass OAuthentication & establish http tunnel
        /// </summary>
        /// <returns></returns>
        private bool AuthorizeCredentialsAndInitializeService()
        {
            UserCredential credential;

            using (var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;

                Console.WriteLine("Credential file saved to: " + credPath);

                // Create Gmail API service.
                service = new GmailService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName,
                });
            }

            return true;
        }

        /// <summary>
        /// Does a truncated inbox download, then retrieves full message details using truncated inbox
        /// </summary>
        /// <returns></returns>
        public List<Message> SyncMailClient()
        {
            List<Message> referenceInbox = GetReferenceInbox();
            List<Message> inbox = GetFullInboxFromReference(referenceInbox);

            return inbox;
        }

        /// <summary>
        /// Receives truncated version of inbox with only message headers
        /// </summary>
        /// <returns></returns>
        private List<Message> GetReferenceInbox()
        {
            List<Message> referenceInbox = new List<Message>();

            //Creates request for page of inbox
            UsersResource.MessagesResource.ListRequest request = service.Users.Messages.List(UserID);
            ListMessagesResponse response;

            int count = 1;

            do
            {
                try
                {
                    //Executes request for first page of inbox
                    response = request.Execute();

                    //Loop through each email in request
                    foreach (var email in response.Messages)
                    {
                        Console.WriteLine(count + ": " + email.Id);
                        referenceInbox.Add(email);

                        count++;
                    }

                    //Gets token for next page request
                    request.PageToken = response.NextPageToken;
                }
                catch (Exception e)
                {
                    Console.WriteLine("An error occurred: " + e.Message);
                }
                //Keep looping till out of pages
            } while (!String.IsNullOrEmpty(request.PageToken));

            return referenceInbox;
        }

        /// <summary>
        /// Uses messageIDs from each message in rawInbox to create list of filled out messages
        /// </summary>
        /// <param name="rawInbox"></param>
        /// <returns></returns>
        private List<Message> GetFullInboxFromReference(List<Message> rawInbox)
        {
            List<Message> verboseInbox = new List<Message>();

            int count = 1;

            foreach (Message rawMessage in rawInbox)
            {
                verboseInbox.Add(GetFullMessage(rawMessage.Id));

                Console.WriteLine("Sorted " + count + ": " + rawMessage.Id);

                count++;
            }

            return verboseInbox;
        }

        /// <summary>
        /// Gets full filled out message from gmail using messageID
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        private Message GetFullMessage(string messageId)
        {
            var fullMessageRequest = service.Users.Messages.Get(UserID, messageId);
            return fullMessageRequest.Execute();
        }

    }
}
