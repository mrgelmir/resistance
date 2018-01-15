using System.Collections.Generic;

public class GameController
{
	//Global GameController
	private static GameController instance;

	// CallBacks

	/// <summary>
	/// Gets called when the player composition gets changed. This can be:
	/// - Adding or removing a player
	/// - Changing player data
	/// </summary>
	public System.Action OnPlayersChanged;
	/// <summary>
	/// Gets called when the game state changes and the view should update accordingly
	/// </summary>
	public System.Action OnStateChanged;

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


	// Flow methods

	public void StartRound()
	{
		// Assign new leader
		gameData.CurrentLeaderId = (gameData.CurrentLeaderId += 1) % gameData.PlayerList.Count;

		// Activate mission
		gameData.CurrentMission = new Mission(gameData.MissionSettingsList[gameData.CurrentMissionId]);
		
		if (OnStateChanged != null)
			OnStateChanged();
	}

	public void StartConfigurationVote()
	{
		// Make players able to vote for the current team configuration



		if (OnStateChanged != null)
			OnStateChanged();
	}

	public void ConfigurationVoteComplete()
	{

	}

	public void StartMissionVote()
	{
		// 
	}

	public void MissionVoteComplete()
	{
		// Check if mission is a success or failure and change data
	}



	/// <summary>
	/// Add player if it doesn't exist yet
	/// </summary>
	/// <returns> Has the player been added succesfully </returns>
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
			gameData.PlayerList.Add(new Player(gameData.PlayerList.Count, name));
			if (OnPlayersChanged != null)
				OnPlayersChanged();
		}

		return exists;
	}




	public void CreateGame()
	{
		// Create a game here, this is temporary

		// Init
		gameData = new Game()
		{
			CurrentLeaderId = 0,
			PlayerList = new List<Player>(),
			MissionList = new List<Mission>(),
			MinPlayers = 2,
			NumberOfSpies = 1,
			MissionSettingsList = new List<MissionSettings>()
			{
				new MissionSettings(5, 1, 2),
				new MissionSettings(5, 1, 3),
				new MissionSettings(5, 1, 2),
				new MissionSettings(5, 1, 3),
				new MissionSettings(5, 1, 3)
			},
		};

		// Mission settings (should depend on amount of players)
		gameData.MissionSettingsList.Add(new MissionSettings(5, 1, 2));
		gameData.MissionSettingsList.Add(new MissionSettings(5, 1, 3));
		gameData.MissionSettingsList.Add(new MissionSettings(5, 1, 2));
		gameData.MissionSettingsList.Add(new MissionSettings(5, 1, 3));
		gameData.MissionSettingsList.Add(new MissionSettings(5, 1, 3));
		gameData.NumberOfMissions = gameData.MissionSettingsList.Count;
	}

}
