using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace UnderBot {
    internal class Program {

        public static string Version = "0.0.1";
        public static bool TestBot = true;

        public static void Main(string[] args) =>
            new Program().Start().GetAwaiter().GetResult();

        public async Task Start() {
            Console.WriteLine("Now lauching: " + Version);
            if (TestBot) {
                await UnderBot.Start("MjkxODQ1NzAwNTIwNjQwNTEy.C70M4g.7CknOO_xBv5adCQcieMJ3twOBlI");
            } else {
                await UnderBot.Start("Mjg3MzMzNzI5NTU3NDEzODg5.C7xF8w.U4PXJuyWu-nfGiR5tpBWGiuqsd8");
            }
        }

        public static async Task<IGuildUser> GetGuildUser(ICommandContext context) {
            return await context.Guild.GetUserAsync(context.User.Id);
        }
    }
}