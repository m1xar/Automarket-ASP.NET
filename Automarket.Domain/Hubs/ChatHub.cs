using Microsoft.AspNetCore.SignalR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Automarket.Domain.Hubs
{
    public class ChatHub : Hub
    {
        private static bool userConnected;
        private static bool adminConnected;
        public async Task Send(string message)
        {
            if (userConnected && adminConnected)
            {
                await Clients.OthersInGroup("Chat").SendAsync("Receive", message);
            }
            else
            {
                await Clients.Caller.SendAsync("Receive", "Please wait until someone connects to the chat");
            }
        }

        public async Task Start()
        {
            if (Context.User.IsInRole("Admin") || Context.User.IsInRole("Moderator"))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, "Chat");
                adminConnected = true;
                if (userConnected)
                {
                    await Clients.Caller.SendAsync("Receive", "You connected to the chat");
                    await Clients.OthersInGroup("Chat").SendAsync("Receive", "Administrator connected to your chat");
                }
                else
                {
                    await Clients.Caller.SendAsync("Receive", "Please wait until someone connects to the chat");
                }
            }
            else
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, "Chat");
                userConnected = true;
                if(adminConnected)
                {
                    await Clients.Caller.SendAsync("Receive", "What is your issue?");
                    await Clients.OthersInGroup("Chat").SendAsync("Receive", "User connected to your chat");
                }
                else
                {
                    await Clients.Caller.SendAsync("Receive", "Please wait until someone connects to the chat");
                }
            }
        }

        public async Task Exit()
        {
            if (Context.User.IsInRole("Admin") || Context.User.IsInRole("Moderator"))
            {
                adminConnected = false;
                await Clients.OthersInGroup("Chat").SendAsync("Receive", "Admin disconnected");
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Chat");
            }
            else
            {
                userConnected = false;
                await Clients.OthersInGroup("Chat").SendAsync("Receive", "User disconnected");
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Chat");
            }
        }
    }
}
