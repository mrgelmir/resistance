using Resistance.Client;
using Resistance.Game.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour, IPlayer
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


	// Actions

	#region IPlayer interface implementation

	public event Action<int, GameScript.GroupCompositionVoteResult> OnTeamCompositionVote;
	public event Action<int, GameScript.MissionVoteResult> OnMissionVote;
	public event Action<List<int>> OnTeamPicked;


	public void SetData(Player data)
	{
		playerId = data.Id;

		//portraitView = data.
		playerNameLabel.text = data.GetName();
		characterNameLabel.text = data.GetCharacter().ToString();
	}

	public void SetState(IPlayerState state)
	{
		globalVoteView.SetActive(state == IPlayerState.GroupCompositionVote);
		attendeesVoteView.SetActive(state == IPlayerState.MissionVote);
		leaderPickerView.SetActive(state == IPlayerState.GroupAssembly);
	}

	public void PopulatePlayerPicker(List<Player> players, int requiredPlayers)
	{
		// Clear picker

		// Populate picker
		foreach (Player p in players)
		{

		}

	}

	#endregion

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

	


}
