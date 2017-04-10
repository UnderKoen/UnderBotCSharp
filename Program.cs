using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using UnderBot.Utils;

namespace UnderBot {
	internal class Program {

		public static string Version = "0.0.1";
		public static bool TestBot = true;

		public static void Main(string[] args) =>
		    new Program().Start().GetAwaiter().GetResult();

		public async Task Start() {
			Console.WriteLine("Now lauching: " + Version);
			if (TestBot) {
				await UnderBot.Start(Keys.GetApiKey(Keys.KeyType.DiscordTest));
			} else {
				await UnderBot.Start(Keys.GetApiKey(Keys.KeyType.Discord));
			}
		}

		public static async Task<IGuildUser> GetGuildUser(ICommandContext context) {
			return await context.Guild.GetUserAsync(context.User.Id);
		}
	}
}