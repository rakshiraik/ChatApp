using ChatService.Entity.Tenant.Entities;

namespace ChatService.Services.Room
{
    public interface IRoomService
    {

        Task<List<ChatBot.Common.ViewModels.RoomViewModel>> GetRoomsAsync(string Email);
    }
}
