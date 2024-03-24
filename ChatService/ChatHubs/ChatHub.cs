using Microsoft.AspNetCore.SignalR;

namespace ChatService.ChatHubs
{
    public class ChatHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            //Context.ConnectionId
            return base.OnConnectedAsync();
        }

        public async Task Join(string roomId)
        {
            
        }

        public async Task GetRoomInfo()
        {

        }


    }
}
