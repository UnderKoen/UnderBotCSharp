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
            var fileStream = new FileStream("Commands.json", FileMode.Open);
            using (var r = new StreamReader(fileStream)) {
                var json = r.ReadToEnd();
                var commands = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
                foreach (var key in commands.Keys) {
                    if (command.Name != key) continue;
                    var perms = JsonConvert.DeserializeObject<Dictionary<string, object>>(commands[key].ToString());
                    if (!perms.ContainsKey("perm")) return PreconditionResult.FromSuccess();
                    var role = Role.StringToRole(perms["perm"].ToString());
                    return Role.GetHighestRole(await Program.GetGuildUser(context)) >= role ? PreconditionResult.FromSuccess() : PreconditionResult.FromError(role.ToString());
                }
            }
            return PreconditionResult.FromError("");
        }
    }
}