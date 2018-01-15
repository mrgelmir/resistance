using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour
{

    [Header("Project References")]
    [SerializeField]
    private PlayerView playerViewPrefab;

    [Header("Scene References")]
    [SerializeField]
    private RectTransform playerViewContainer;

    private List<PlayerView> playerViews = new List<PlayerView>();

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
            // Instantiate view and populate with data
            PlayerView v = Instantiate(playerViewPrefab);
            v.transform.SetParent(playerViewContainer, false);
            v.SetData(player);

            // Listen for player input
            v.OnTeamCompositionVote += CompositionVote;
            v.OnMissionVote += MissionVote;

            // Set visual
            v.SetState(PlayerView.ViewState.Idle);

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

        switch (state)
        {
            default:
            case GameController.GameState.Default:
                // Clear all UI
                foreach (PlayerView v in playerViews)
                {
                    v.SetState(PlayerView.ViewState.Idle);
                }
                break;
            case GameController.GameState.TeamAssembly:
                foreach (PlayerView v in playerViews)
                {
                    v.SetState(PlayerView.ViewState.Idle);
                }
                playerViews[GameController.Instance.LeaderId].PopulatePlayerPicker(
                    GameController.Instance.PlayerList,
                    GameController.Instance.GameData.CurrentMission.Settings.NumberOfAttendees);
                playerViews[GameController.Instance.LeaderId].SetState(PlayerView.ViewState.GroupAssembly);
                playerViews[GameController.Instance.LeaderId].OnTeamPicked += TeamPicked;
                break;
            case GameController.GameState.TeamCompositionVote:
                foreach (PlayerView v in playerViews)
                {
                    v.SetState(PlayerView.ViewState.GroupCompositionVote);
                }
                break;
            case GameController.GameState.MissionVote:
                Mission m = GameController.Instance.GameData.CurrentMission;
                for (int i = 0; i < playerViews.Count; i++)
                {
                    if (m.MissionVoteList.Find((PlayerVote v) => { return v.PlayerID == i; }) != null)
                    {
                        playerViews[i].SetState(PlayerView.ViewState.MissionVote);
                    }
                    else
                    {
                        playerViews[i].SetState(PlayerView.ViewState.Idle);
                    }
                }
                break;
        }

    }

    public void TeamPicked(List<int> pickedPlayers)
    {
        // TODO data stuff

        playerViews[GameController.Instance.LeaderId].OnTeamPicked -= TeamPicked;
    }

    public void CompositionVote(int playerIndex, GroupCompositionVoteResult vote)
    {
        print(GameController.Instance.PlayerList[playerIndex].Name + " Voted " + vote.ToString());
    }

    public void MissionVote(int playerIndex, MissionVoteResult vote)
    {
        print(GameController.Instance.PlayerList[playerIndex].Name + " Voted " + vote.ToString());
    }

    // Enums for voting game logic
    public enum GroupCompositionVoteResult
    { Accept, Decline }

    public enum MissionVoteResult
    { Success, Fail }

}