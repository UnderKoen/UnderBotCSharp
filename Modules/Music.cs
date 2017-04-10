using System.Threading.Tasks;
using Discord.Commands;
using UnderBot.Checks;

namespace UnderBot.Modules {
	public partial class MusicModule : ModuleBase {
		[Group("Music")]
		[RoleCheck]
		public class Music {
		}
	}
}