namespace InboxRetriever
{
    public class AppInterface
    {
        private DBReader dbReader { get; set; }
        public AppInterface(DBReader reader)
        {
            dbReader = reader;
        }
    }
}