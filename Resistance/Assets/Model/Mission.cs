using System.Collections.Generic;

public class Mission
{

    private List<GlobalVote> teamCompositionVoteList = new List<GlobalVote>();
    private List<PlayerVote> missionVoteList = new List<PlayerVote>();

    private MissionSettings settings;

    public MissionSettings Settings
    {
        get { return settings; }
        set { settings = value; }
    }

    public Mission(MissionSettings settings)
    {
        this.settings = settings;
    }

    public List<PlayerVote> MissionVoteList
    {
        get { return missionVoteList; }
        set { missionVoteList = value; }
    }

    
}
