using Microsoft.AspNet.SignalR;

namespace SignalRUnityWeb
{
    public class SignalRSampleHub : Hub
    {
        public void Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.broadcastMessage(name, message);
        }
    }
}