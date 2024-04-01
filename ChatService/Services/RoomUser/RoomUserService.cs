using ChatBot.Common.Entry;
using ChatBot.Common.ViewModels;
using ChatService.Repository.Contracts;
using ChatService.Services.Error;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ChatService.Services.RoomUser
{
    public class RoomUserService : IRoomUserService
    {
        public IErrorService _errorService;
        public IRoomRepository _roomRepository;
        public IRoomUserRepository _roomUserRepository;

        public RoomUserService(IErrorService errorService, IRoomRepository roomRepository, IRoomUserRepository roomUserRepository)
        {
            this._roomUserRepository = roomUserRepository;
            this._roomRepository = roomRepository;
            this._errorService = errorService;
        }

        public List<RoomViewModel> GetById(string id)
        {

            var RoomViewModel = _roomRepository.GetAll()
           .Join(_roomUserRepository.GetAll(),
               room => room.Id,
               roomUser => roomUser.RoomId,
               (room, roomUser) => new { Room = room, RoomUser = roomUser })
           .Where(joinResult => joinResult.RoomUser.UserId == id)
           .Select(joinResult => new RoomViewModel
           {
               Id = joinResult.Room.Id,
               RoomName = joinResult.Room.RoomName
           })
           .ToList();

            return RoomViewModel;

        }

        public List<RoomViewModel> GetAll()
        {
            var RoomViewModel = _roomRepository.GetAll()

                   .Select(joinResult => new RoomViewModel
                   {
                       Id = joinResult.Id,
                       RoomName = joinResult.RoomName
                   })
           .ToList();

            return RoomViewModel;

        }

        public async Task<string> CreateRoomAsync(RoomViewModel roomViewModel)
        {
            string resultStr = "";
            ChatService.Entity.Tenant.Entities.Room room = new ChatService.Entity.Tenant.Entities.Room
            {
                RoomName = roomViewModel.RoomName,
                NoOfPeopleAllowed = roomViewModel.NoOfPeopleAllowed,
                CreatedOn = DateTime.Now
            };

            this._roomRepository.Add(room);
            this._roomRepository.SaveChanges();
            resultStr = JsonConvert.SerializeObject(new EntryResponse { IsValid = true });

            return resultStr;

        }
    }
}
