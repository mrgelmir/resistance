namespace Resistance.Game.Model
{
	using System.Collections.Generic;

	public class GlobalVote
	{

		private List<PlayerVote> playerVoteList = new List<PlayerVote>();
		private List<int> teamConfiguration;
		private int leaderID;

		public List<PlayerVote> PlayerVoteList
		{
			get { return playerVoteList; }
			set { playerVoteList = value; }
		}

		public List<int> TeamConfiguration
		{
			get { return teamConfiguration; }
			set { teamConfiguration = value; }
		}

		public int LeaderID
		{ get { return leaderID; } set { leaderID = value; } }

		public GlobalVote(int leaderID, int playerCount, List<int> teamConfiguration)
		{
			this.leaderID = leaderID;
			this.teamConfiguration = teamConfiguration;
			for (int i = 0; i < playerCount; i++)
			{
				playerVoteList.Add(new PlayerVote(i));
			}
		}

	}

}