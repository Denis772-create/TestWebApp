using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using TestWebApp.DAL.Data;
using TestWebApp.DAL.DTO;
using TestWebApp.DAL.Models.Entities;
using TestWebApp.ReviewServer.Signatures;

namespace TestWebApp.ReviewServer.Hubs
{
    public class ReviewHub : Hub<IReviewHub>
    {
        public static readonly List<UserDto> Connections = new List<UserDto>();
        private static readonly Dictionary<string, string> _connectionsMap = new Dictionary<string, string>();

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ReviewHub(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        private string IdentityName => Context.User.Identity.Name;

        public async Task SendMessage()
        {
            await Clients.Others.SendReviewAsync(default);
        }

        public async Task JoinRoom(string roomName)
        {
            try
            {
                var userDto = Connections.FirstOrDefault(u => u.Username == IdentityName);
                if (userDto != null && userDto.CurrentRoom != null)
                {
                    if (!string.IsNullOrEmpty(roomName))
                        await Clients.OthersInGroup(userDto.CurrentRoom).SendInfoAsync("Info", $"{userDto.Username}");

                    await LeaveRoom(userDto.CurrentRoom);
                    await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
                    userDto.CurrentRoom = roomName;

                    await Clients.OthersInGroup(roomName).SendInfoAsync("Info", $"{userDto.Username}");
                }
            }
            catch (Exception e)
            {
                await Clients.Caller.SendErrorAsync(e.Message);
            }
        }

        public async Task LeaveRoom(string roomName)
        {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
        }

        public IEnumerable<UserDto> GetUsers(string roomName)
        {
            return Connections.Where(u => u.CurrentRoom == roomName);
        }

        public override async Task OnConnectedAsync()
        {
            try
            {
                var user = await _context.User.FirstOrDefaultAsync(u => u.UserName == IdentityName);
                var userDto = _mapper.Map<User, UserDto>(user);

                if (Connections.All(u => u.Username != IdentityName))
                {
                    Connections.Add(userDto);
                    _connectionsMap.Add(IdentityName, Context.ConnectionId);
                }   

                await Clients.Caller.SendInfoAsync("Info");

            }
            catch (Exception e)
            {
                await Clients.Caller.SendErrorAsync(e.Message);
            }
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            try
            {
                var user = Connections.First(u => u.Username == IdentityName);
                Connections.Remove(user);

                await Clients.OthersInGroup(user.CurrentRoom).SendInfoAsync($"{user.Username} disconnected", user.Username, user.Avatar);

                _connectionsMap.Remove(user.Username);
            }
            catch (Exception e)
            {
                await Clients.Caller.SendErrorAsync(e.Message);
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}
