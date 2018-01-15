using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour
{
	[Header("Scene References")]
	[SerializeField]
	private Image portraitView;
	[SerializeField]
	private TextMeshProUGUI playerNameLabel;
	[SerializeField]
	private TextMeshProUGUI characterNameLabel;

	[SerializeField]
	private GameObject globalVoteView;
	[SerializeField]
	private GameObject attendeesVoteView;
	[SerializeField]
	private GameObject leaderPickerView;

	private int playerId;

	// TODO Add Actions

	public System.Action<int, GameScript.GroupCompositionVoteResult> OnGlobalVote;
	public System.Action<int, GameScript.MissionVoteResult> OnAttendeeVote;

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

	public void AcceptVote()
	{
		if (OnGlobalVote != null)
			OnGlobalVote(playerId, GameScript.GroupCompositionVoteResult.Accept);
	}

	public void DeclineVote()
	{
		if (OnGlobalVote != null)
			OnGlobalVote(playerId, GameScript.GroupCompositionVoteResult.Decline);
	}

	public void SuccessVote()
	{
		if (OnAttendeeVote != null)
			OnAttendeeVote(playerId, GameScript.MissionVoteResult.Success);
	}

	public void FailVote()
	{
		if (OnAttendeeVote != null)
			OnAttendeeVote(playerId, GameScript.MissionVoteResult.Success);
	}

	public enum ViewState
	{
		Idle, GroupAssembly, GroupCompositionVote, MissionVote
	}
}
