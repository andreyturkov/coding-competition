using CodingCompetition.Application.Interfaces;
using CodingCompetition.Application.Models.SharedPad;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using CodingCompetition.Web.Models;

namespace CodingCompetition.Web.Hubs
{
	public class SharedPadHub : Hub
	{
		private readonly ISharedPadService _sharedPadService;

		public SharedPadHub(ISharedPadService sharedPadService)
		{
			_sharedPadService = sharedPadService;
		}

		public async Task JoinGroup(string padId, string nickname)
		{
			await Groups.AddToGroupAsync(Context.ConnectionId, padId);

			var user = new CodePadUser(Context.ConnectionId, nickname);
			_sharedPadService.AddPadUser(padId, user);

			await Clients.Group(padId).SendAsync("Join", user);
		}

		public async Task LeaveGroup(string padId)
		{
			await Groups.RemoveFromGroupAsync(Context.ConnectionId, padId);

			_sharedPadService.RemovePadUser(padId, Context.ConnectionId);

			await Clients.Group(padId).SendAsync("Leave", Context.ConnectionId);
		}

		public async Task UpdatePad(string padId, Message<CodePad> message)
		{
			await Clients.Group(padId).SendAsync("Update", message);
		}
	}
}
