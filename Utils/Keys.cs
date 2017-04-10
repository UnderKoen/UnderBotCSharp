using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace UnderBot.Utils {
	public class Keys {
		public enum KeyType {
			Youtube, Discord, DiscordTest
		}

		public static string GetApiKey(KeyType type) {
			string typeName;
			switch (type) {
				case KeyType.Youtube:
					typeName = "youtube";
					break;
				case KeyType.Discord:
					typeName = "discord";
					break;
				case KeyType.DiscordTest:
					typeName = "discordTest";
					break;
				default:
					return "";
			}
			var fileStream = new FileStream("Keys.json", FileMode.Open);
			using (var r = new StreamReader(fileStream)) {
				var json = r.ReadToEnd();
				var objects = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
				foreach (var key in objects.Keys) {
					if (key == typeName) {
						return objects[key].ToString();
					}
				}
			}
			return "";
		}
	}
}
