using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UnitTestDemo.Models
{
    public class ChatConnection
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public string ConnectionId { get; set; }

        public string UserAgent { get; set; }

        public bool Connected { get; set; }

        public DateTime ConnectedOn { get; set; }

        public DateTime? DisconectedOn { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; }
    }
}