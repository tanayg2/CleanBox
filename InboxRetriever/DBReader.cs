using System.Collections.Generic;
using Google.Apis.Gmail.v1.Data;
using GmailInboxLibrary;

namespace InboxRetriever
{
    public class DBReader
    {
        public DBReader()
        {
            //TODO: initialize connection to db
        }

        /// <summary>
        /// "View {group} grouped by {category} sorted {sortType} {order}
        /// </summary>
        /// <param name="group"></param>
        /// <param name="category"></param>
        /// <param name="sortType"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public List<Message> Get(EmailGroup group, Category category, SortingType sortType, Order order)
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="category"></param>
        /// <param name="group"></param>
        /// <param name="???"></param>
        /// <returns></returns>
        public List<Message> Search(Category category, EmailGroup group, string pattern)
        {
            
        }
    }
}