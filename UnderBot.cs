using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace UnderBot {
	public class UnderBot {
		public static DiscordSocketClient Client;
		private static CommandHandler _handler;

		public static async Task Start(string token) {
			// Define the DiscordSocketClient
			Client = new DiscordSocketClient();

			// Login and connect to Discord.
			await Client.LoginAsync(TokenType.Bot, token);
			await Client.StartAsync();

			_handler = new CommandHandler();
			await _handler.Install(Client);

			await Task.Delay(-1);
		}

		private Task Log(LogMessage msg) {
			Console.WriteLine(msg.ToString());
			return Task.CompletedTask;
		}
	}
}