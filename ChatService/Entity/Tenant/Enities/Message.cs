using System;
using System.Collections.Generic;

namespace ChatService.Entity.Tenant.Entities
{
    public partial class Message
    {
        public long Id { get; set; }
        public string Message1 { get; set; } = null!;
        public long RoomUserId { get; set; }
        public long ParentMessageId { get; set; }
    }
}
