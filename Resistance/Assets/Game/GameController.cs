using Resistance.Game.Model;
using System.Collections.Generic;

public class GameController
{

	#region Singleton implementation

	//Global GameController
	private static GameController instance;
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

	#endregion

	#region CallBacks

	/// <summary>
	/// Gets called when the player composition gets changed. This can be:
	/// - Adding or removing a player
	/// - Changing player data
	/// </summary>
	public System.Action OnPlayersChanged;
	/// <summary>
	/// Gets called when the game state changes and the view should update accordingly
	/// </summary>
	public System.Action<GameState> OnStateChanged;

	#endregion

	#region Data
	private Game gameData;
	private GameState currentState = GameState.Default;

	[System.Obsolete("Let's not use this, unless temporarily")]
	public Game GameData { get { return gameData; } }

	public int LeaderId
	{
		get { return gameData.CurrentLeaderId; }
	}

	public List<Player> PlayerList
	{
		get { return gameData.PlayerList; }
		set { gameData.PlayerList = value; }
	}

	public Mission CurrentMission
	{ get { return gameData.CurrentMission; } }

	#endregion

	// Flow methods

	public void NewRound()
	{
		// TODO test if valid 

		// Assign new leader
		gameData.CurrentLeaderId = (gameData.CurrentLeaderId += 1) % gameData.PlayerList.Count;

		// Activate mission
		gameData.CurrentMission = new Mission(gameData.MissionSettingsList[gameData.MissionList.Count]);

		SetNewState(GameState.TeamAssembly);
	}

	public void StartCompositionVote(List<int> pickedPlayers)
	{
		// Make players able to vote for the current team configuration


		SetNewState(GameState.TeamCompositionVote);
	}



	public void StartMissionVote()
	{
		// 
	}

	public void MissionVoteComplete()
	{
		// Add current mission to completed missions
		gameData.MissionList.Add(gameData.CurrentMission);
		// Check if mission is a success or failure and change data

		// Remove reference to finished mission
		gameData.CurrentMission = null;
	}



	private void SetNewState(GameState newState)
	{
		currentState = newState;

		// Notify listeners of state change
		if (OnStateChanged != null)
			OnStateChanged(currentState);
	}

	/// <summary>
	/// Add player if it doesn't exist yet
	/// </summary>
	/// <returns> Has the player been added succesfully </returns>
	public bool AddPlayer(string name)
	{
		bool valid = true;
		for (int i = 0; i < gameData.PlayerList.Count; i++)
		{
			if (gameData.PlayerList[i].Name.Equals(name))
				valid = false;
		}

		if (!name.Equals("") && valid)
		{
			gameData.PlayerList.Add(new Player(gameData.PlayerList.Count, name));
			if (OnPlayersChanged != null)
				OnPlayersChanged();
		}

		return valid;
	}

	// Helper
	public void CreateGame()
	{
		// Create a game here, this is temporary

		// Init
		gameData = new Game()
		{
			CurrentLeaderId = -1,
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

		gameData.NumberOfMissions = gameData.MissionSettingsList.Count;
	}

	public void StartGame()
	{
		// Assign Roles
		for (int i = 0; i < gameData.NumberOfSpies; i++)
		{
			int randomNumber = UnityEngine.Random.Range(0, gameData.PlayerList.Count);

			if (gameData.PlayerList[randomNumber].GetCharacter() == Character.Spy)
			{
				i--;
			}
			else
			{
				gameData.PlayerList[randomNumber].SetCharacter(Character.Spy);
			}
		}

		// TODO pseudo-randomize player order

	}


	public enum GameState
	{
		// everything else
		Default,
		// Leader picks team composition
		TeamAssembly,
		// Players vote for team composition
		TeamCompositionVote,
		// Team members vote for mission success
		MissionVote
	}
}
