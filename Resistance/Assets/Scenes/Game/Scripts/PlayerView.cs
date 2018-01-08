using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour
{
	[Header("Scene References")]
	[SerializeField]
	private Image portraitView;
	[SerializeField]
	private Text playerNameLabel;
	[SerializeField]
	private Text characterNameLabel;

	[SerializeField]
	private GameObject globalVoteView;
	[SerializeField]
	private GameObject AttendeesVoteView;
	[SerializeField]
	private GameObject LeaderPickerView;

	private int playerId;

	// TODO Add Actions

	public void SetData(Player data)
	{
		playerId = data.Id;

		//portraitView = data.
		playerNameLabel.text = data.GetName();
		characterNameLabel.text = data.GetCharacter().ToString();
	}

	public void WinVote()
	{
		//OnPlayerVoted(playerId, win);
	}

	public void FailVote()
	{

	}

}
