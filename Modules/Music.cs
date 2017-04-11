using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Discord.Commands;
using Newtonsoft.Json;
using UnderBot.Checks;
using UnderBot.Utils;

namespace UnderBot.Modules {
	[Group("music")]
	[RoleCheck]
	public partial class Music : ModuleBase {
		[Command("join")]
		public async Task Join() {
			await new TextMessage().AddMension(Context.User)
				.AddText("This command is currently not enabled")
				.SendMessage(Context.Channel);
		}

		[Command("leave")]
		public async Task Leave() {
			await new TextMessage().AddMension(Context.User)
				.AddText("This command is currently not enabled")
				.SendMessage(Context.Channel);
		}

		[Command("play")]
		public async Task Play() {
			await new TextMessage().AddMension(Context.User)
				.AddText("This command is currently not enabled")
				.SendMessage(Context.Channel);
		}

		[Command("playlist")]
		public async Task Playlist() {
			await new TextMessage().AddMension(Context.User)
				.AddText("This command is currently not enabled")
				.SendMessage(Context.Channel);
		}

		[Command("next")]
		public async Task Next() {
			await new TextMessage().AddMension(Context.User)
				.AddText("This command is currently not enabled")
				.SendMessage(Context.Channel);
		}

		[Command("forcenext")]
		public async Task ForceNext() {
			await new TextMessage().AddMension(Context.User)
				.AddText("This command is currently not enabled")
				.SendMessage(Context.Channel);
		}

		[Command("queue")]
		public async Task Queue() {
			await new TextMessage().AddMension(Context.User)
				.AddText("This command is currently not enabled")
				.SendMessage(Context.Channel);
		}

		[Command("")]
		public async Task Normal() {
			var emoteMessage = new TextMessage();
			var fileStream = new FileStream("Commands.json", FileMode.Open);
			emoteMessage.AddMension(Context.User).AddText("All currently available music commands are:");
			using (var r = new StreamReader(fileStream)) {
				var json = r.ReadToEnd();
				var emotes = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
				//Console.WriteLine(json);
				var emotes2 = JsonConvert.DeserializeObject<Dictionary<string, object>>(emotes["music"].ToString());
				//Console.WriteLine(emotes["music"].ToString());
				var emotes3 = JsonConvert.DeserializeObject<Dictionary<string, object>>(emotes2["subcommands"].ToString());
				//Console.WriteLine(emotes["subcommands"].ToString());
				foreach (var key in emotes3.Keys) {
					var command = new Command("music", key);
					emoteMessage.AddText(command.Usage + " -=- " + command.Desc);
				}
			}
			await emoteMessage.SendMessage(Context.Channel);
		}
	}
}