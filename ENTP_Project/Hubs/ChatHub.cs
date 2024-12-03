using System;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Auth0.ManagementApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ENTP_Project.Models;
using ENTP_Project.Data;

namespace ENTP_Project.Hubs
{
    public class ChatHub : Hub
    {
        private readonly AppDbContext _context;
        public ChatHub(AppDbContext context)
        {
            _context = context;
        }
        public override async Task OnConnectedAsync()
        {
            var claims = Context.User?.Claims;
            var email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email || c.Type == "email")?.Value;
            var userCheck = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            var user = _context.Users.Find(userCheck.Id);
            //await Clients.Others.SendAsync("ReceiveMessage", $"{user.Name} has joined the chat!");

            //await base.OnConnectedAsync();
            await Clients.All.SendAsync("ReceiveMessage", $"{user.Name} has joined the chat!");
        }
        public async Task SendMessage(string message)
        {
            var claims = Context.User?.Claims;
            var email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email || c.Type == "email")?.Value;
            var userCheck = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            var user = _context.Users.Find(userCheck.Id);

            await Clients.Client(Context.ConnectionId).SendAsync("ReceiveMessage", $"{user.Name}: {message}");
        }
    }
}
