using System.Collections.Generic;
using System.IO;
using Discord.Commands;
using Newtonsoft.Json;

namespace UnderBot.Utils {
	public class Command {
		public string CommandName;
		public string Subcommands;

		public string Usage;
		public string Desc;
		public Role.Roles Perm;

		public Command(string command) {
			CommandName = command;
			var fileStream = new FileStream("Commands.json", FileMode.Open);
			using (var r = new StreamReader(fileStream)) {
				var json = r.ReadToEnd();
				var commands = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
				foreach (var key in commands.Keys) {
					if (CommandName != key) continue;
					var perms = JsonConvert.DeserializeObject<Dictionary<string, object>>(commands[key].ToString());
					if (perms.ContainsKey("perm")) {
						Perm = Role.StringToRole(perms["perm"].ToString());
					}
					if (perms.ContainsKey("usage")) {
						Usage = perms["usage"].ToString();
					}
					if (perms.ContainsKey("desc")) {
						Desc = perms["desc"].ToString();
					}
				}
			}
		}

		public Command(string command, string subcommand) {
			CommandName = command;
			Subcommands = subcommand;
			var fileStream = new FileStream("Commands.json", FileMode.Open);
			using (var r = new StreamReader(fileStream)) {
				var json = r.ReadToEnd();
				var commands = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
				foreach (var key in commands.Keys) {
					if (CommandName != key) continue;
					var perm =
					    JsonConvert.DeserializeObject<Dictionary<string, object>>(commands[key].ToString());
					if (!perm.ContainsKey("subcommands")) continue;
					var subcommands =
					    JsonConvert.DeserializeObject<Dictionary<string, object>>(perm["subcommands"].ToString());
					foreach (var subcommandKey in subcommands.Keys) {
						if (subcommandKey != subcommand) continue;
						var perms = JsonConvert.DeserializeObject<Dictionary<string, object>>(subcommands[subcommandKey].ToString());
						if (perms.ContainsKey("perm")) {
							Perm = Role.StringToRole(perms["perm"].ToString());
						}
						if (perms.ContainsKey("usage")) {
							Usage = perms["usage"].ToString();
						}
						if (perms.ContainsKey("desc")) {
							Desc = perms["desc"].ToString();
						}
						break;
					}
					break;
				}
			}
		}
	}
}