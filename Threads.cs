using System;
using System.Linq;
using System.Threading;
using Discord.Rest;
using UnderBot.Utils;

namespace UnderBot {
    public class Threads {

        public static Thread LiveThread;
        public static bool LiveThreadB;

        public void Live() {
            var before = "";
            var now = Youtube.GetTimLivestream();
            var last = "";
            while (LiveThreadB) {
                if (before != now && now != "" && last != now) {
                    last = now;
                    foreach (var channel in UnderBot.Client.Guilds.FirstOrDefault().TextChannels) {
                        if (channel.Name != "meldingen") continue;
                        channel.SendMessageAsync("@everyone Tim is live https://www.youtube.com/watch?v=" + now);
                    }
                }
                System.Threading.Thread.Sleep(60 * 1000);
                before = now;
                var ehm = Youtube.GetTimLivestream();
                if (ehm != null) {
                    now = ehm;
                }
            }
        }

        public void Delete(object o) {
            if (o.GetType() != typeof(RestUserMessage)) return;
            System.Threading.Thread.Sleep(5 * 60 * 1000);
            ((RestUserMessage) o).DeleteAsync();
        }

        //public void Console() {
            /*while (false) {
                foreach (var server in DiscordBot.Discord.Servers) {
                    server.FindChannels("console", ChannelType.Text)
                        .FirstOrDefault()
                        ?.SendMessage(System.Console.OpenStandardOutput().ToString());
                }
                System.Threading.Thread.Sleep(10 * 1000);
            }*/
        //}
    }
}