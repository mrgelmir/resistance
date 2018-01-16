namespace Resistance.Game.Model
{
	using System.Collections.Generic;

	public class GlobalVote
	{

		private List<PlayerVote> playerVoteList;
		private int leaderID;

		public GlobalVote(int leaderID)
		{
			this.leaderID = leaderID;
			this.playerVoteList = new List<PlayerVote>();
		}

	}

}