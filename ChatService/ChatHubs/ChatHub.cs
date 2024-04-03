using ChatBot.Common.Dto;
using Microsoft.AspNetCore.SignalR;

namespace ChatService.ChatHubs
{
    public class ChatHub : Hub
    {
        private readonly IDictionary<string, UserRoomConnection> _connection;

        public ChatHub(IDictionary<string, UserRoomConnection> connection)
        {
            _connection = connection;
        }

        public override Task OnConnectedAsync()
        {
            //Context.ConnectionId
            return base.OnConnectedAsync();
        }

        public async Task JoinRoom(UserRoomConnection userRoomConnection)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userRoomConnection.RoomId);
            _connection[Context.ConnectionId] = userRoomConnection;
            await Clients.Group(userRoomConnection.RoomId).SendAsync("ReceiveMessage", "Lets Chat Bot", $"{userRoomConnection.UserId} has Joined the Group", DateTime.Now);
            await SendConnectedUser(userRoomConnection.RoomId);
        }

        public async Task SendMessage(string message)
        {
            if (_connection.TryGetValue(Context.ConnectionId, out UserRoomConnection userRoomConnection))
            {
                await Clients.Group(userRoomConnection.RoomId)
                    .SendAsync("ReceiveMessage", userRoomConnection.UserId, message, DateTime.Now);
            }
        }

        public override Task OnDisconnectedAsync(Exception? exp)
        {
            if (!_connection.TryGetValue(Context.ConnectionId, out UserRoomConnection roomConnection))
            {
                return base.OnDisconnectedAsync(exp);
            }

            _connection.Remove(Context.ConnectionId);
            Clients.Group(roomConnection.RoomId)
                .SendAsync("ReceiveMessage", "Lets Chat bot", $"{roomConnection.UserId} has Left the Group", DateTime.Now);
            SendConnectedUser(roomConnection.RoomId);
            return base.OnDisconnectedAsync(exp);
        }

        public Task SendConnectedUser(string room)
        {
            var users = _connection.Values
                .Where(u => u.RoomId == room)
                .Select(s => s.UserId);
            return Clients.Group(room).SendAsync("ConnectedUser", users);
        }
    }
}
