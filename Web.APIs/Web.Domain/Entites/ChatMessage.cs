using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Domain.Entites
{
    public class ChatMessage : BaseClass<int>
    {
        [Required]
        public string SenderUserId { get; set; } = default!;

        [Required]
        public string ReceiverUserId { get; set; } = default!;

        [Required]
        public string Content { get; set; } = default!;
    
        public bool IsRead { get; set; }

    
    }
}
