using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour
{
	[Header("Player References")]
	[SerializeField]
	private Image portraitView;
	[SerializeField]
	private TextMeshProUGUI playerNameLabel;
	[SerializeField]
	private TextMeshProUGUI characterNameLabel;

    [Header("Views")]
	[SerializeField]
	private GameObject globalVoteView;
	[SerializeField]
	private GameObject attendeesVoteView;
	[SerializeField]
	private GameObject leaderPickerView;

    [Header("Player Picker")]
    [SerializeField]
    private RectTransform playerPickerContainer;
    [SerializeField]
    private PlayerPickerView playerPickerPrefab;
    [SerializeField]
    private Button playerPickerConfirmButton;

	private int playerId;

	// TODO Add Actions

	public System.Action<int, GameScript.GroupCompositionVoteResult> OnTeamCompositionVote;
	public System.Action<int, GameScript.MissionVoteResult> OnMissionVote;
    public System.Action<List<int>> OnTeamPicked;

	public void SetData(Player data)
	{
		playerId = data.Id;

		//portraitView = data.
		playerNameLabel.text = data.GetName();
		characterNameLabel.text = data.GetCharacter().ToString();
	}

	public void SetState(ViewState state)
	{
		globalVoteView.SetActive(state == ViewState.GroupCompositionVote);
		attendeesVoteView.SetActive(state == ViewState.MissionVote);
		leaderPickerView.SetActive(state == ViewState.GroupAssembly);
	}

    public void PopulatePlayerPicker(List<Player> players, int requiredPlayers)
    {
        // Clear picker

        // Populate picker
        foreach (Player p in players)
        {

        }

    }

    public void AcceptVote()
	{
		if (OnTeamCompositionVote != null)
			OnTeamCompositionVote(playerId, GameScript.GroupCompositionVoteResult.Accept);
	}

	public void DeclineVote()
	{
		if (OnTeamCompositionVote != null)
			OnTeamCompositionVote(playerId, GameScript.GroupCompositionVoteResult.Decline);
	}

	public void SuccessVote()
	{
		if (OnMissionVote != null)
			OnMissionVote(playerId, GameScript.MissionVoteResult.Success);
	}

	public void FailVote()
	{
		if (OnMissionVote != null)
			OnMissionVote(playerId, GameScript.MissionVoteResult.Success);
	}

    public void ConfirmPlayerPicker()
    {

    }

	public enum ViewState
	{
		Idle, GroupAssembly, GroupCompositionVote, MissionVote
	}

   
}
