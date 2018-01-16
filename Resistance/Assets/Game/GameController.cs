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

		AssignNextLeader();

		// Activate mission
		gameData.CurrentMission = new Mission(gameData.MissionSettingsList[gameData.MissionList.Count]);

		SetNewState(GameState.TeamAssembly);
	}

	public void StartCompositionVote(List<int> pickedPlayers)
	{
		// Make players able to vote for the current team configuration
		GlobalVote newCompositionVote = new GlobalVote(gameData.CurrentLeaderId, PlayerList.Count, pickedPlayers);
		CurrentMission.TeamCompositionVoteList.Add(newCompositionVote);

		SetNewState(GameState.TeamCompositionVote);
	}

	public void TeamCompositionVote(int playerIndex, bool vote)
	{
		// TODO: Check if this is a valid action in the current game state

		// Accept player vorte
		PlayerVote playerVote = CurrentMission.CurrentTeamCompositionVote.PlayerVoteList[playerIndex];

		// Do we allow changing votes? For now we don't
		if (playerVote.Voted == false)
		{
			playerVote.PlaceVote(vote);
		}
		else return;


		// Check if all players have voted
		bool votesComplete = true;
		int pro = 0, con = 0;
		foreach (PlayerVote pv in CurrentMission.CurrentTeamCompositionVote.PlayerVoteList)
		{
			// Count placed votes
			if (!pv.Voted)
				votesComplete = false;

			// Count pro and con votes
			if (pv.Vote)
				++pro;
			else
				++con;

		}

		// Process votes
		if (votesComplete)
		{
			bool compositionAccepted = pro > con;

			if (compositionAccepted)
			{
				UnityEngine.Debug.Log("Team Composition Accepted");
				// Current team is accepted, start mission
				StartMissionVote();
			}
			else
			{
				UnityEngine.Debug.Log("Team Composition Declined");
				// TODO: See if the amount of attempts hasn't exceeded the maximum

				// Let new leader pick another team
				AssignNextLeader();
				SetNewState(GameState.TeamAssembly);
			}
		}
	}

	public void StartMissionVote()
	{
		// Create the mission vote, only including the picked team members
		for (int i = 0; i < CurrentMission.CurrentTeamCompositionVote.TeamConfiguration.Count; i++)
		{
			CurrentMission.MissionVoteList.Add(new PlayerVote(CurrentMission.CurrentTeamCompositionVote.TeamConfiguration[i]));
		}

		SetNewState(GameState.MissionVote);
	}

	public void MissionVote(int playerIndex, bool vote)
	{
		// TODO: Check if this is a valid action in the current game state

		// If player is on the current mission, accept his vote
		var playerVote = CurrentMission.MissionVoteList.Find((PlayerVote v) => { return v.PlayerID.Equals(playerIndex); });
		if (playerVote != null)
		{
			playerVote.PlaceVote(vote);
		}

		// Check if all votes are in
		bool votesComplete = true;
		foreach (PlayerVote v in CurrentMission.MissionVoteList)
		{
			if (!v.Voted)
			{
				votesComplete = false;
				break;
			}
		}

		if (votesComplete)
		{
			MissionVoteComplete();
		}
	}

	public void MissionVoteComplete()
	{
		// Add current mission to completed missions
		gameData.MissionList.Add(gameData.CurrentMission);

		// Check if mission is a success or failure and change data
		bool missionSuccess = CurrentMission.MissionVoteList.FindAll((PlayerVote vote) => { return !vote.Vote; }).Count < CurrentMission.Settings.NumberOfFailsNeeded;

		UnityEngine.Debug.Log("Mission ended successfully: " + missionSuccess);

		// Remove reference to finished mission
		gameData.CurrentMission = null;

		// Check for end of game conditions
		bool endGame = false;

		if (endGame)
		{
			// Check who won
			SetNewState(GameState.Finished);
		}
		else
		{
			NewRound();
		}
	}

	private void AssignNextLeader()
	{
		// Assign new leader
		gameData.CurrentLeaderId = (gameData.CurrentLeaderId += 1) % gameData.PlayerList.Count;
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
		MissionVote,
		// The game is finished
		Finished
	}
}
