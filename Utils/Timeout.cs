using System;
using System.Collections.Generic;
using Discord;

namespace UnderBot.Utils {
    public class Timeout {
        public static Dictionary<IGuildUser, Timeout> Timeouts = new Dictionary<IGuildUser, Timeout>();

        public IGuildUser User;
        public DateTime Since;
        public DateTime Until;
        public TimeSpan Lenght;
        public string Reason;

        public Timeout(IGuildUser user, DateTime since, DateTime until, string reason) {
            User = user;
            Since = since;
            Until = until;
            Reason = reason;
            Lenght = until.Subtract(since);
        }

        public Timeout(IGuildUser user, DateTime since, TimeSpan lenght, string reason) {
            User = user;
            Since = since;
            Until = since.Add(lenght);
            Reason = reason;
            Lenght = lenght;
        }

        public bool IsTimeouted() {
            return DateTime.Now.Ticks <= Until.Ticks;
        }
    }
}