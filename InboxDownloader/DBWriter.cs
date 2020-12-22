using System;
using System.Collections.Generic;
using System.Data.SQLite;
using Google.Apis.Gmail.v1.Data;
using GmailInboxLibrary;

namespace InboxDownloader
{
    public class DBWriter
    {
        private SQLiteConnection dbConnection { get; set; }
        private string userId { get; set; }
        private string connectionString { get; set; }
        // private string inboxTableString = "id VARCHAR(20), "
        //                                 + "threadId VARCHAR(20), "
        //                                 //labelIds
        //                                 + "snippet VARCHAR(100), "
        //                                 + "historyId VARCHAR(20), "
        //                                 + "internalDate VARCHAR(20), "
        //                                 //payload
        //                                 + "sizeEstimate INT, "
        //                                 + "raw VARCHAR(2000)";
        //
        // private string labelIdsTableString = "messageId VARCHAR(20), "
        //                                      + "labelId VARCHAR(20)";
        //
        // private string messagePartTableString = "messageId VARCHAR(20), "
        //                                         + "partId VARCHAR(20), "
        //                                         + "mimeType VARCHAR(20)";
        //
        // private string headerTableString = "messageId VARCHAR(20), "
        //                                    + "partId VARCHAR(20), "
        //                                    + "name VARCHAR(20), "
        //                                    + "value VARCHAR(20)";
        //
        // private string messagePartBodyTableString = "messageId VARCHAR(20), "
        //                                             + "partId VARCHAR(20), "
        //                                             + "attachmentId VARCHAR(20), "
        //                                             + "size INT, "
        //                                             + "data VARCHAR(1000)";
        private string userTableString = "userId VARCHAR(100) PRIMARY KEY"; 
        
        private string messageTableString = "messageId VARCHAR(20) PRIMARY KEY, "
                                            + "internalDate VARCHAR(32)";

        private string labelsTableString = "messageId VARCHAR(20) PRIMARY KEY,"
                                           + "internalDate VARCHAR(32)";

        //TODO: make sure sqlite generates uid
        private string headersTableString = "uid INTEGER PRIMARY KEY, "
                                            + "messageId VARCHAR(20), "
                                            + "name VARCHAR(20), "
                                            + "value VARCHAR(150)";

        private string sendersTableString = "messageId VARCHAR(20), "
                                            + "senderId VARCHAR(150)";
        
        //TODO: moses to look more into this
        private string messagePartsTableString = "messageId VARCHAR(20) PRIMARY KEY, "
                                                 + "partId VARCHAR(2)";

        private string messagePrioritiesTableString = "categoryName VARCHAR(100) PRIMARY KEY, "
                                                      + "categoryPriority INTEGER";

        public DBWriter(string userId)
        {
            this.userId = userId;
            this.connectionString = "Data Source=" + userId + ".sqlite;Version=3;";
            InitializeAndConnectToDB();
        }


        private void InitializeAndConnectToDB()
        {
            //TODO: in the final publish, db shouldn't be created by code
            //Create db
            SQLiteConnection.CreateFile(userId + ".sqlite");
            
            //Connect to db
            dbConnection = new SQLiteConnection(connectionString);
            dbConnection.Open();
            
            //Create tables
            CreateTable("MESSAGES#" + userId, messageTableString);
            CreateTable("HEADERS#" + userId, headersTableString);
            CreateTable("MESSAGEPARTS#" + userId, messagePartsTableString);
            CreateTable("MESSAGEPRIORITIES#" + userId, messagePrioritiesTableString);
        }

        private void CreateTable(string tableName, string tableString)
        {
            string creationString = "CREATE TABLE " + tableName + "\n(" + tableString + ")";
            SQLiteCommand command = new SQLiteCommand(creationString, dbConnection);
            command.ExecuteNonQuery();
        }

        public int Write(List<Message> inbox)
        {
            //TODO: see if there's a way to do this in one efcore write rather than multiple
            int count = 0;
            foreach (Message message in inbox)
            {
                count += Write(message);
            }

            return count;
        }

        public int Write(Message message)
        {
            //TODO: add functionality to write single message to inbox table, return number of lines (should always be 1)
            return 0;
        }

        //TODO: implement
        // public bool RemoveMessage(Message message)
        // {
        // }
        //
        // public bool RemoveMessages(List<Message> messages)
        // {
        //     bool success = true;
        //     foreach (Message message in messages)
        //     {
        //         if (!RemoveMessage(message))
        //         {
        //             success = false;
        //         }
        //     }
        //
        //     return success;
        // }
        
        
    }
}