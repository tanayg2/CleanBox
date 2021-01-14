using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Sqlite;

namespace InboxDownloader.DatabaseAccess
{
    public class MessagesModel
    {
        [Key]
        public string messageId { get; set; }
        [Required]
        public long internalDate { get; set; }
    }

    public class LabelsModel
    {
        [Key]
        public string messageId { get; set; }
        [Required]
        public long internalDate { get; set; }
    }

    public class HeadersModel
    {
        [Key]
        public string uid { get; set; }
        [Required]
        public string messageId { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string value { get; set; }
    }

    public class SendersModel
    {
        [Key]
        public string messageId { get; set; }
        [Required]
        public string senderId { get; set; }
    }

    public class MessagePartsModel
    {
        [Key]
        public string messageId { get; set; }
        [Required]
        public string partId { get; set; }
    }

    public class MessagePrioritiesModel
    {
        [Key]
        public string categoryName { get; set; }
        [Required]
        public string categoryPriority { get; set; }
    }
}