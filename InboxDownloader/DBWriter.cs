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
        private string dbName { get; set; }
        private string connectionString { get; set; }
        private string inboxTableString = "id VARCHAR(20), "
                                        + "threadId VARCHAR(20), "
                                        //labelIds
                                        + "snippet VARCHAR(100), "
                                        + "historyId VARCHAR(20), "
                                        + "internalDate VARCHAR(20), "
                                        //payload
                                        + "sizeEstimate INT, "
                                        + "raw VARCHAR(2000)";

        private string labelIdsTableString = "messageId VARCHAR(20), "
                                             + "labelId VARCHAR(20)";

        private string messagePartTableString = "messageId VARCHAR(20), "
                                                + "partId VARCHAR(20), "
                                                + "mimeType VARCHAR(20)";

        private string headerTableString = "messageId VARCHAR(20), "
                                           + "partId VARCHAR(20), "
                                           + "name VARCHAR(20), "
                                           + "value VARCHAR(20)";

        private string messagePartBodyTableString = "messageId VARCHAR(20), "
                                                    + "partId VARCHAR(20), "
                                                    + "attachmentId VARCHAR(20), "
                                                    + "size INT, "
                                                    + "data VARCHAR(1000)";
                                                

        public DBWriter(string dbName)
        {
            this.dbName = dbName;
            this.connectionString = "Data Source=" + dbName + ".sqlite;Version=3;";
        }
        
        
        private void InitializeAndConnectToDB()
        {
            //TODO: in the final publish, db shouldn't be created by code
            //Create db
            SQLiteConnection.CreateFile(dbName + ".sqlite");
            
            //Connect to db
            dbConnection = new SQLiteConnection(connectionString);
            dbConnection.Open();
            
            //Create tables
            CreateTable("inbox", inboxTableString);
        }

        private void CreateTable(string tableName, string tableString)
        {
            string creationString = "CREATE TABLE " + tableName + "\n(" + tableString + ")";
            SQLiteCommand command = new SQLiteCommand(creationString, dbConnection);
            command.ExecuteNonQuery();
        }
        
        
    }
}