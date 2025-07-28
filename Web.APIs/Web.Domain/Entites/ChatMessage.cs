using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Domain.Entites
{
    public class ChatMessage : BaseClass<int>
    {
        [Required]
        public string SenderUserId { get; set; }

        [Required]
        public string ReceiverUserId { get; set; } 

        [Required]
        public string Content { get; set; } 

        public bool IsRead { get; set; } = false;
        [ForeignKey(nameof(Chat))]
        public int ChatId { get; set; }
        public Chat Chat { get; set; }

    }
}
