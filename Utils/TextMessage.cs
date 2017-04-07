using System.Threading;
using System.Threading.Tasks;
using Discord;

namespace UnderBot.Utils {
	public class TextMessage {
		public string Message = "";

		private IUser _user;
		private bool _autoDelete = true;

		public TextMessage AddText(string text) {
			Message = Message + "\n" + text;
			return this;
		}

		public TextMessage AddMension(IUser user) {
			_user = user;
			return this;
		}

		public TextMessage AutoDelete(bool autoDelete) {
			_autoDelete = autoDelete;
			return this;
		}

		public async Task SendMessage(IMessageChannel channel) {
			var message = _user != null
			    ? await channel.SendMessageAsync(_user.Mention + "```" + Message + "```")
			    : await channel.SendMessageAsync("```" + Message + "```");
			if (!_autoDelete) return;
			var threads = new Threads();
			var delete = new Thread(new ParameterizedThreadStart(threads.Delete));
			delete.Start(message);
		}

		public async Task SendNormalMessage(IMessageChannel channel) {
			var message = _user != null
			    ? await channel.SendMessageAsync(_user.Mention + Message)
			    : await channel.SendMessageAsync(Message);
			if (!_autoDelete) return;
			var threads = new Threads();
			var delete = new Thread(new ParameterizedThreadStart(threads.Delete));
			delete.Start(message);
		}
	}
}