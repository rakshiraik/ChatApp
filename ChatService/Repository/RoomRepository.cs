using ChatService.Entity.Tenant.Entities;
using ChatService.Repository.Contracts;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ChatService.Repository
{
    public class RoomRepository : Repository<Room>, IRoomRepository
    {
        private readonly ApplicationDbContext _tenantContext;
        public RoomRepository(ApplicationDbContext context) : base(context)
        {
            _tenantContext = context;
        }

    }
}
