using UnderBot.Checks;
using UnderBot.Utils;
using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Timeout = UnderBot.Utils.Timeout;

namespace UnderBot.Modules {
	[RoleCheck]
	public partial class Mod : ModuleBase {
		[Command("timeout")]
		public async Task Timeout(string name, string reason, string timeoutLenght) {
			var lenght = timeoutLenght.ToLower();
			var daysR = new Regex(@"(\d*)d");
			var hoursR = new Regex(@"(\d*)h");
			var minutesR = new Regex(@"(\d*)m");
			var secondsR = new Regex(@"(\d*)s");
			var days = 0;
			days = days + daysR.Matches(lenght)
				   .Cast<Match>()
				   .Aggregate(0,
				       (current, time) => current + int.Parse(
							      time.Value.Remove(time.Value.Length - 1)));
			var hours = 0;
			hours = hours + hoursR.Matches(lenght)
				    .Cast<Match>()
				    .Aggregate(0,
					(current, time) => current + int.Parse(
							       time.Value.Remove(time.Value.Length - 1)));
			var minutes = 0;
			minutes = minutes + minutesR.Matches(lenght)
				      .Cast<Match>()
				      .Aggregate(0,
					  (current, time) => current + int.Parse(
								 time.Value.Remove(time.Value.Length - 1)));
			var seconds = 0;
			seconds = seconds + secondsR.Matches(lenght)
				      .Cast<Match>()
				      .Aggregate(0,
					  (current, time) => current + int.Parse(
								 time.Value.Remove(time.Value.Length - 1)));
			var user = (IGuildUser)null;
			foreach (var userT in await Context.Guild.GetUsersAsync()) {
				if (userT.Mention.Replace("!", "") == name || userT.Nickname == name || userT.Username == name) {
					user = userT;
				}
			}
			await Context.Guild.GetUsersAsync();
			if (user == null) {
				await new TextMessage().AddText(name + " is not a valid person.")
				    .AddMension(Context.User)
				    .SendMessage(Context.Channel);
				return;
			}
			var timeout = new Timeout(user, DateTime.Now,
			    new TimeSpan(days, hours, minutes, seconds), reason.Replace('_', ' '));
			if (Utils.Timeout.Timeouts.ContainsKey(user)) {
				Utils.Timeout.Timeouts.Remove(user);
			}
			Utils.Timeout.Timeouts.Add(user, timeout);
			await new TextMessage().AddText(timeout.User.Username + " is timedout for: " + timeout.Reason)
			    .AddText(timeout.User.Username + " will be not able to talk until:")
			    .AddText(timeout.Until.ToString(CultureInfo.CurrentCulture))
			    .AddMension(Context.User)
			    .SendMessage(Context.Channel);
		}


		[Command("startlivestreamcheck")]
		public async Task StartLivestreamCheck() {
			var threads = new Threads();
			Threads.LiveThread = new Thread(threads.Live);
			Threads.LiveThreadB = true;
			Threads.LiveThread.Start();
			var startMessage = new TextMessage();
			startMessage.AddText("Started checking for livestreams");
			await startMessage.AddMension(Context.User).SendMessage(Context.Channel);
		}

		[Command("stoplivestreamcheck")]
		public async Task StopLivestreamCheck() {
			var threads = new Threads();
			Threads.LiveThread = new Thread(threads.Live);
			Threads.LiveThreadB = false;
			var startMessage = new TextMessage();
			startMessage.AddText("Stopped checking for livestreams");
			await startMessage.AddMension(Context.User).SendMessage(Context.Channel);
		}
	}
}