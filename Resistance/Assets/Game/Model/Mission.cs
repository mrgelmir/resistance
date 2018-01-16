namespace Resistance.Game.Model
{
	using System.Collections.Generic;

	public class Mission
	{

		private List<GlobalVote> teamCompositionVoteList = new List<GlobalVote>();
		private List<PlayerVote> missionVoteList = new List<PlayerVote>();

		private MissionSettings settings;


		public List<GlobalVote> TeamCompositionVoteList
		{
			get { return teamCompositionVoteList; }
			set { teamCompositionVoteList = value; }
		}

		public List<PlayerVote> MissionVoteList
		{
			get { return missionVoteList; }
			set { missionVoteList = value; }
		}

		public GlobalVote CurrentTeamCompositionVote
		{
			get
			{
				return TeamCompositionVoteList.Count > 0 ?
				  TeamCompositionVoteList[TeamCompositionVoteList.Count - 1] :
				  null;
			}
		}

		public MissionSettings Settings
		{
			get { return settings; }
			set { settings = value; }
		}

		public Mission(MissionSettings settings)
		{
			this.settings = settings;
		}

	}

}