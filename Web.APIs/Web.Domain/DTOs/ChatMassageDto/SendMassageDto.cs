using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Domain.DTOs.ChatMassageDto
{
    public class SendMassageDto
    {
  
        [Required]
        public string ReceiverUserId { get; set; } = default!;

        [Required]
        public string Content { get; set; } = default!;
    }
}
