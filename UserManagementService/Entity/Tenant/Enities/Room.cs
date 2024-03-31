using System;
using System.Collections.Generic;

namespace ChatService.Entity.Tenant.Entities
{
    public partial class Room
    {
        public long Id { get; set; }
        public string RoomName { get; set; } = null!;
        public int NoOfPeopleAllowed { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
