using Microsoft.AspNetCore.SignalR;

namespace BookHive.Hubs
{
    public class ChatHubs : Hub
    {
        public async Task SendMessage(string username, int bookid,string bookname)
        {
           await Clients.All.SendAsync("Received Message", username, bookid, bookname);
        }
    }
}
