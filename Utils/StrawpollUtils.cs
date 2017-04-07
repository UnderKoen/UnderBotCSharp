using System;
using System.Collections.Generic;
using System.Linq;

namespace UnderBot.Utils {
	public class StrawpollUtils {
		public static Dictionary<string, StrawpollUtils> Strawpolls = new Dictionary<string, StrawpollUtils>();

		public string Poll;

		public List<string> Answers;

		public Dictionary<string, Vote> Votes = new Dictionary<string, Vote>();

		public StrawpollUtils(string poll, List<string> answers) {
			Poll = poll;
			Answers = answers.Select(anwer => anwer.ToLower()).ToList();
			foreach (var anwer in Answers) {
				Votes.Add(anwer, new Vote(anwer, 0));
			}
		}

		public void AddVote(string answer) {
			Votes[answer.ToLower()] = Votes[answer.ToLower()].AddVote();
		}

		public void SubVote(string answer) {
			Votes[answer.ToLower()] = Votes[answer.ToLower()].SubVote();
		}

		public Vote GetVotes(string answer) {
			return Votes[answer.ToLower()];
		}

		public double GetPercentage(string answer) {
			var all = GetTotalVotes();
			if (all == 0) {
				return 0;
			}
			var votes = Votes[answer.ToLower()].Votes;
			return (double) votes / (double) all * 100;
		}

		public int GetTotalVotes() {
			return Votes.Values.Sum(vote => vote.Votes);
		}
	}
}