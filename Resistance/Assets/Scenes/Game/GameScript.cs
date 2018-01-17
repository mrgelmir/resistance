using Resistance.Client;
using Resistance.Game.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour
{

    [Header("Project References")]
    [SerializeField]
	// TODO: abstract this to another class
    private PlayerView playerViewPrefab;

    [Header("Scene References")]
    [SerializeField]
    private RectTransform playerViewContainer;

    private List<IPlayer> playerViews = new List<IPlayer>();

    private void Start()
    {
        GameController.Instance.OnStateChanged += GameStateChanged;

        // Clear container 
        for (int i = 0; i < playerViewContainer.transform.childCount; i++)
        {
            Destroy(playerViewContainer.transform.GetChild(i).gameObject);
        }

        // Fill container with data
        foreach (Player player in GameController.Instance.PlayerList)
        {
			// TODO: move the instantiation part elsewhere: final version will have players as clients, not views

            // Instantiate view and populate with data
            PlayerView v = Instantiate(playerViewPrefab);
            v.transform.SetParent(playerViewContainer, false);
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
		print("New game state is " + state.ToString());
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
                break;
            case GameController.GameState.TeamCompositionVote:
				// TODO show mission view stuff here

                foreach (IPlayer v in playerViews)
                {
                    v.SetState(IPlayerState.GroupCompositionVote);
                }
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
				print("Game is finished");
				break;
        }

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