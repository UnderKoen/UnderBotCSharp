using Discord.Commands;
using System.Threading.Tasks;
using UnderBot.Modules;

// Inherit from PreconditionAttribute
namespace UnderBot.Checks {
	public class AandachtCheck : PreconditionAttribute {
		public override async Task<PreconditionResult> CheckPermissions(ICommandContext context,
		    CommandInfo command, IDependencyMap map) {
			return Public.LastAandacht != context.User ? PreconditionResult.FromSuccess() : PreconditionResult.FromError("");
		}
	}
}