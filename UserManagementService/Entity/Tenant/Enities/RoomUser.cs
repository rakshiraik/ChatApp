using System;
using System.Collections.Generic;

namespace ChatService.Entity.Tenant.Entities
{
    public partial class RoomUser
    {
        public long Id { get; set; }
        public long RoomId { get; set; }
        public long UserId { get; set; }
    }
}
