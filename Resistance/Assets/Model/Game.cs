using System.Collections.Generic;

public class Game
{
	private List<Player> playerList;
	private List<Mission> missionList;
	private List<MissionSettings> missionSettingsList;
	private int numberOfMissions;
	private int minPlayers;
	private int numberOfSpies;

	public List<Player> PlayerList
	{
		get { return playerList; }
		set { playerList = value; }
	}

	public List<Mission> MissionList
	{
		get { return missionList; }
		set { missionList = value; }
	}

	public List<MissionSettings> MissionSettingsList
	{
		get
		{
			return missionSettingsList;
		}

		set
		{
			missionSettingsList = value;
		}
	}

	public int NumberOfMissions
	{
		get
		{
			return numberOfMissions;
		}

		set
		{
			numberOfMissions = value;
		}
	}

	public int MinPlayers
	{
		get
		{
			return minPlayers;
		}

		set
		{
			minPlayers = value;
		}
	}

	public int NumberOfSpies
	{
		get
		{
			return numberOfSpies;
		}

		set
		{
			numberOfSpies = value;
		}
	}
}

