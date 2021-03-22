using CodingCompetition.Web.Controllers;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace CodingCompetition.Web.Hubs
{
	public class SharedPadHub : Hub
	{
		public async Task JoinGroup(string groupName)
		{
			await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

			await Clients.Group(groupName).SendAsync("Join", $"{Context.ConnectionId} has joined the group {groupName}.");
		}

		public async Task LeaveGroup(string groupName)
		{
			await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

			await Clients.Group(groupName).SendAsync("Leave", $"{Context.ConnectionId} has left the group {groupName}.");
		}

		public async Task SendCodePad(string groupName, CodePad codePad)
		{
			await Clients.Group(groupName).SendAsync("CodePad", codePad);
		}
	}
}
