using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Domain.Entites;

namespace Web.Domain.DTOs.ChatMassageDto
{
    public class GetChatDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string SenderUserId { get; set; }

     
        public string ReceiverUserId { get; set; }

      
        public string Content { get; set; }

        public bool IsRead { get; set; } = false;
   
        public int ChatId { get; set; }
    }
}
