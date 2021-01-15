using Microsoft.EntityFrameworkCore;
using DbContext = System.Data.Entity.DbContext;

namespace Cleanbox.DatabaseAccess
{
    public class MessageDbContext : DbContext
    {
        public System.Data.Entity.DbSet<MessagesModel> MessagesModels { get; set; }
        public System.Data.Entity.DbSet<MessagePrioritiesModel> MessagePrioritiesModels { get; set; }

        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}