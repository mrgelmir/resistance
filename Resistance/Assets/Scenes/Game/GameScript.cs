using Resistance.Characters;
using Resistance.Client;
using Resistance.Game.Model;
using Resistance.Helpers;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScript : MonoBehaviour
{

	[Header("Project References")]
	[SerializeField]
	// TODO: abstract this to another class
	private PlayerView playerViewPrefab;

	[Header("Scene References")]
	[SerializeField]
	private RectTransform playerViewContainer;
	[SerializeField]
	private TMPro.TextMeshProUGUI debugLabel;

	[Header("Visual")]
	[SerializeField]
	// TODO: This should be moved elsewhere
	private CharacterGroup defaultCharacterGroup;

	/// <summary>
	/// For now this is passed through data, but might change later
	/// So let's chache it here
	/// </summary>
	private CharacterGroup characterGroup;
	private List<IPlayer> playerViews = new List<IPlayer>();

	private void Start()
	{
		// Check if game is initialized, if not, create a mock game
		// TODO: automise this
		if (!GameController.Instance.Initialized)
		{
			GameController.Instance.CreateMockGame();
		}

		// Cache the character group or use default if not available
		characterGroup = GameController.Instance.GameData.CharacterGoup;
		if (characterGroup == null)
			characterGroup = defaultCharacterGroup;

		// Subscribe to desired events
		GameController.Instance.OnStateChanged += GameStateChanged;
		
		// Clear container and fill with data
		playerViewContainer.DestroyChildren();
		foreach (Player player in GameController.Instance.PlayerList)
		{
			// TODO: move the instantiation part elsewhere: final version will have players as clients, not views

			// Instantiate view and populate with data
			PlayerView v = Instantiate(playerViewPrefab, playerViewContainer, false);

			// TODO: this isn't decoupled for now, but should be handled by a player handler 
			v.SetCharacterGroup(characterGroup);
			v.SetData(player);

			// Listen for player input
			v.OnTeamCompositionVote += CompositionVote;
			v.OnMissionVote += MissionVote;

			// Set visual
			v.SetState(IPlayerState.Idle);

			playerViews.Add(v);
		}

		/// TODO mission views

		// Start Gameplay
		GameController.Instance.NewRound();
	}

	private void OnDestroy()
	{
		GameController.Instance.OnStateChanged -= GameStateChanged;
	}

	private void GameStateChanged(GameController.GameState state)
	{
		System.Text.StringBuilder debugInfo = new System.Text.StringBuilder();
		debugInfo.AppendLine("New game state is " + state.ToString());
		debugInfo.AppendLine(string.Format("Playing mission {0} of {1}",
			GameController.Instance.GameData.MissionList.Count + 1,
			GameController.Instance.GameData.NumberOfMissions));

		switch (state)
		{
			default:
			case GameController.GameState.Default:
				// Clear all UI
				foreach (IPlayer v in playerViews)
				{
					v.SetState(IPlayerState.Idle);
				}
				break;
			case GameController.GameState.TeamAssembly:
				foreach (IPlayer v in playerViews)
				{
					v.SetState(IPlayerState.Idle);
				}
				playerViews[GameController.Instance.LeaderId].PopulatePlayerPicker(
					GameController.Instance.PlayerList,
					GameController.Instance.CurrentMission.Settings.NumberOfAttendees);
				playerViews[GameController.Instance.LeaderId].SetState(IPlayerState.GroupAssembly);
				playerViews[GameController.Instance.LeaderId].OnTeamPicked += TeamPicked;

				debugInfo.AppendLine(string.Format("Going on a mission with {0} players.", GameController.Instance.CurrentMission.Settings.NumberOfAttendees));
				debugInfo.AppendLine(string.Format("This is attempt number {0} of {1}",
					GameController.Instance.CurrentMission.TeamCompositionVoteList.Count,
					GameController.Instance.CurrentMission.Settings.MaxVoteRounds));
				break;
			case GameController.GameState.TeamCompositionVote:
				// TODO show mission view stuff here

				foreach (IPlayer v in playerViews)
				{
					v.SetState(IPlayerState.GroupCompositionVote);
				}
				debugInfo.AppendLine(string.Format("This is voting attempt number {0} of {1}",
				   GameController.Instance.CurrentMission.TeamCompositionVoteList.Count,
				   GameController.Instance.CurrentMission.Settings.MaxVoteRounds));
				break;
			case GameController.GameState.MissionVote:
				Mission m = GameController.Instance.CurrentMission;
				for (int i = 0; i < playerViews.Count; i++)
				{
					if (m.MissionVoteList.Find((PlayerVote v) => { return v.PlayerID == i; }) != null)
					{
						playerViews[i].SetState(IPlayerState.MissionVote);
					}
					else
					{
						playerViews[i].SetState(IPlayerState.Idle);
					}
				}
				break;
			case GameController.GameState.Finished:
				// TODO: Display who won
				debugInfo.AppendLine("Game is finished");
				debugInfo.AppendLine("This should show which team won, but it's not implemented yet");

				SceneManager.LoadScene(0);

				break;
		}

		debugLabel.text = debugInfo.ToString();
	}

	public void TeamPicked(List<int> pickedPlayers)
	{
		// Unsubscribe (maybe clean up too?)
		playerViews[GameController.Instance.LeaderId].OnTeamPicked -= TeamPicked;

		// Inform game controller
		GameController.Instance.StartCompositionVote(pickedPlayers);

	}

	public void CompositionVote(int playerIndex, GroupCompositionVoteResult vote)
	{
		playerViews[playerIndex].SetState(IPlayerState.Idle);
		GameController.Instance.TeamCompositionVote(playerIndex, vote == GroupCompositionVoteResult.Accept);
	}

	public void MissionVote(int playerIndex, MissionVoteResult vote)
	{
		playerViews[playerIndex].SetState(IPlayerState.Idle);
		GameController.Instance.MissionVote(playerIndex, vote == MissionVoteResult.Success);
	}

	// Enums for voting game logic
	public enum GroupCompositionVoteResult
	{ Accept, Decline }

	public enum MissionVoteResult
	{ Success, Fail }

}