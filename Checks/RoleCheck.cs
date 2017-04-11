using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Discord.Commands;
using Newtonsoft.Json;
using UnderBot.Utils;

namespace UnderBot.Checks {
	public class RoleCheck : PreconditionAttribute {
		public override async Task<PreconditionResult> CheckPermissions(ICommandContext context,
		    CommandInfo command, IDependencyMap map) {
			await context.Message.DeleteAsync();
			Command commandI;
			if (command.Module.Aliases[0] == "") {
				commandI = new Command(command.Name);
			} else {
				commandI = new Command(command.Module.Aliases[0], command.Name);
			}
			var role = commandI.Perm;
			return Role.GetHighestRole(await Program.GetGuildUser(context)) >= role ? PreconditionResult.FromSuccess() : PreconditionResult.FromError(role.ToString());
		}
	}
}