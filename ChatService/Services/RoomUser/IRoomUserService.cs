using ChatBot.Common.ViewModels;

namespace ChatService.Services.RoomUser
{
    public interface IRoomUserService
    {
        List<RoomViewModel> GetById(string id);

        List<RoomViewModel> GetAll();

        Task<string> CreateRoomAsync(RoomViewModel roomViewModel);

        //b GetById(string id);
    }
}
