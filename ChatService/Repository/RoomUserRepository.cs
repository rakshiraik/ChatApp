using ChatService.Entity.Tenant.Entities;
using ChatService.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ChatService.Repository
{
    public class RoomUserRepository : Repository<RoomUser>, IRoomUserRepository
    {
        private readonly ApplicationDbContext _tenantContext;
        public RoomUserRepository(ApplicationDbContext context) : base(context)
        {
            _tenantContext = context;
        }
    }
}
