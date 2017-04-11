using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Newtonsoft.Json;
using UnderBot.Checks;
using UnderBot.Utils;

namespace UnderBot.Modules {
	[RoleCheck]
	public class Public : ModuleBase {
		[Command("info")]
		public async Task Info() {
			await new TextMessage()
				.AddMension(Context.User)
			    	.AddText("The current version of the bot is: " + Program.Version)
			    	.AddText("Use /changelog for the more detail")
			    	.SendMessage(Context.Channel);
		}

		[Command("changelog")]
		public async Task Changelog() {
			var fileStream = new FileStream("Changelog.txt", FileMode.Open);
			using (var reader = new StreamReader(fileStream)) {
				string line;
				var msg = new TextMessage().AddMension(Context.User);
				while ((line = reader.ReadLine()) != null) {
					msg.AddText(line);
				}
				await msg.SendMessage(Context.Channel);
			}
		}

		public static IUser LastAandacht;
		private static int _aandachtInt = 0;
		private static List<string> _aandacht;

		[Command("aandacht"), AandachtCheck]
		public async Task Aandacht() {
			if (_aandacht == null) {
				SetupAandacht();
			}
			LastAandacht = Context.User;
			await new TextMessage().AddMension(Context.User)
			    .AddText(_aandacht?[_aandachtInt])
			    .SendMessage(Context.Channel);
			_aandachtInt++;
			if (_aandachtInt == _aandacht?.Count) _aandachtInt = 0;
		}

		private static void SetupAandacht() {
			_aandacht = new List<string>();
			var fileStream = new FileStream("Aandacht.txt", FileMode.Open);
			using (var r = new StreamReader(fileStream)) {
				string line;
				while ((line = r.ReadLine()) != null) {
					_aandacht.Add(line);
				}
			}
		}

		[Command("emote")]
		public async Task Emote([Remainder] string arg) {
			var message = "";
			var emoteMessgae = new TextMessage();
			var fileStream = new FileStream("Emote.json", FileMode.Open);
			using (var r = new StreamReader(fileStream)) {
				var json = r.ReadToEnd();
				var emotes = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
				foreach (var _char in arg.ToLower().ToCharArray()) {
					if (emotes.ContainsKey(_char.ToString())) {
						message = message + emotes[_char.ToString()];
					} else {
						message = message + emotes["none"];
					}
				}
			}
			emoteMessgae.AddText(message);
			await new TextMessage().AddMension(Context.User).SendNormalMessage(Context.Channel);
			await emoteMessgae.SendNormalMessage(Context.Channel);
		}

		[Command("help")]
		public async Task Help() {
			var emoteMessage = new TextMessage();
			var fileStream = new FileStream("Commands.json", FileMode.Open);
			emoteMessage.AddMension(Context.User).AddText("All currently available commands are:");
			using (var r = new StreamReader(fileStream)) {
				var json = r.ReadToEnd();
				var emotes = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
				foreach (var key in emotes.Keys) {
					var command = new Command(key);
					emoteMessage.AddText(command.Usage + " -=- " + command.Desc);
				}
			}
			await emoteMessage.SendMessage(Context.Channel);
		}
	}
}