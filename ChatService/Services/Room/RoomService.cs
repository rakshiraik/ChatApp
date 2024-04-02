using ChatService.Entity.Tenant.Entities;
using ChatBot.Common.ViewModels;
using ChatService.Repository;
using ChatService.Repository.Contracts;

namespace ChatService.Services.Room
{
    public class RoomService : IRoomService
    {
        public IRoomRepository _roomRepository { get; set; }
        public RoomService(IRoomRepository roomRepository)
        {

            _roomRepository = roomRepository;

        }
        public Task<List<RoomViewModel>> GetRoomsAsync(string Email)
        {
            List<RoomViewModel> list = new List<RoomViewModel>();
            return Task.FromResult(list);
        }
    }
}
