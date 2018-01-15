using System.Collections.Generic;

public class Mission
{

	private List<GlobalVote> globalVoteList = new List<GlobalVote>();
	private List<PlayerVote> attendeesVoteList = new List<PlayerVote>();

	private MissionSettings settings;

	public Mission(MissionSettings settings)
	{
		this.settings = settings;
	}

}
