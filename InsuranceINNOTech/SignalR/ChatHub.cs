using Microsoft.AspNetCore.SignalR;


namespace InsuranceINNOTech;

    public class ChatHub : Hub
    {
        public async Task SendMessage(User user, string message)
        {
            await Clients.All.SendAsync("RecieveMessage", user, message);
        }
    }

