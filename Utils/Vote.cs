using System;

namespace UnderBot.Utils {
    public class Vote {
        public string Answer;

        public int Votes;

        public Vote(string anwer, int votes) {
            Answer = anwer;
            Votes = votes;
        }


        public Vote AddVote() {
            Votes = Votes + 1;
            return this;
        }

        public Vote SubVote() {
            Votes = Votes - 1;
            return this;
        }
    }
}