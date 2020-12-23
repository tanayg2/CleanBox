using Microsoft.EntityFrameworkCore;
using DbContext = System.Data.Entity.DbContext;

namespace InboxDownloader.DatabaseAccess
{
    public class MessageDbContext : DbContext
    {
        public System.Data.Entity.DbSet<MessagesModel> MessagesModels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=")
        }
    }
}