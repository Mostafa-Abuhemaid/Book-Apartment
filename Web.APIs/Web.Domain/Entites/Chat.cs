using FirebaseAdmin.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Domain.Entites
{
    public class Chat : BaseClass<int>
    {
        public string FirstUserId { get; set; }

        public string SecondUserId { get; set; }

        public List<ChatMessage> Messages { get; set; } = new List<ChatMessage>();

    }
}
