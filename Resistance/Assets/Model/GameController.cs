using System.Collections.Generic;

public class GameController
{
	//Global GameController
	private static GameController instance;

	// CallBacks
	public System.Action OnPlayersChanged;

	//Script objects
	private Game gameData;

	public Game GameData { get { return gameData; } }

	public List<Player> PlayerList
	{
		get { return gameData.PlayerList; }
		set { gameData.PlayerList = value; }
	}


	//Get instance
	public static GameController Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new GameController();
			}
			return instance;
		}
	}

	private GameController()
	{

	}

	//Add player if it doesn't exist yet
	public bool AddPlayer(string name)
	{
		bool exists = false;
		for (int i = 0; i < gameData.PlayerList.Count; i++)
		{
			if (gameData.PlayerList[i].Name.Equals(name))
				exists = true;
		}

		if (!name.Equals("") && !exists)
		{
			gameData.PlayerList.Add(new Player(name));
			if (OnPlayersChanged != null)
				OnPlayersChanged();
		}

		return exists;
	}

	/*
    //Each users screen is build by this function depending on the data
	public void RefreshUserScreens(){
		
	}
    */

	//Button respons methods
	//After the leader picks teams, this method is activated
	public void LeaderPickedMissionTeam()
	{

	}

	public void CreateGame()
	{

		//Init
		gameData = new Game();
		gameData.PlayerList = new List<Player>();
		gameData.MissionList = new List<Mission>();
		gameData.MissionSettingsList = new List<MissionSettings>();

		//Mission settings
		gameData.MinPlayers = 2;
		gameData.NumberOfSpies = 1;
		gameData.MissionSettingsList.Add(new MissionSettings(5, 1, 2));
		gameData.MissionSettingsList.Add(new MissionSettings(5, 1, 3));
		gameData.MissionSettingsList.Add(new MissionSettings(5, 1, 2));
		gameData.MissionSettingsList.Add(new MissionSettings(5, 1, 3));
		gameData.MissionSettingsList.Add(new MissionSettings(5, 1, 3));

		gameData.NumberOfMissions = gameData.MissionSettingsList.Count;
	}

}
