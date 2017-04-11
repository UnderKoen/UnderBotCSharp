using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using UnderBot.Utils;

namespace UnderBot {
	public class CommandHandler {
		private DiscordSocketClient _client;
		private CommandService _cmds;

		public async Task Install(DiscordSocketClient c) {
			_client = c;
			_cmds = new CommandService();

			await _cmds.AddModulesAsync(Assembly.GetEntryAssembly());

			_client.MessageReceived += HandleCommand;
			_client.Ready += async () => {
				Console.WriteLine("Just lauched: " + Program.Version);
			};
		}

		private async Task HandleCommand(SocketMessage s) {
			var msg = s as SocketUserMessage;
			if (msg == null)
				return;
			var context = new CommandContext(_client, msg);

			if (context.User.IsBot) return;

			var user = await Program.GetGuildUser(context);

			if (Timeout.Timeouts.ContainsKey(user)) {
				if (Timeout.Timeouts[user].IsTimeouted()) {
					await context.Message.DeleteAsync();
					return;
				}
			}

			var argPos = 0;
			if (msg.HasStringPrefix("/", ref argPos) ||
			    msg.HasMentionPrefix(_client.CurrentUser, ref argPos)) {
				var result = await _cmds.ExecuteAsync(context, argPos);
				if (!result.IsSuccess) {
					if (Role.StringToRole(result.ErrorReason) != Role.Roles.Everyone) {
						await new TextMessage()
							.AddMension(context.User)
							.AddText("You should atleast have: " + result.ErrorReason + " as your rank.")
							.SendMessage(context.Channel);
					} else {
						//TODO this here better
						await new TextMessage()
							.AddMension(context.User)
							.AddText(result.ToString())
							.AddText("Your input: " + context.Message.Content)
							.AddText("Try using /help.")
							.SendMessage(context.Channel);
					}
				}
			}
		}
	}
}