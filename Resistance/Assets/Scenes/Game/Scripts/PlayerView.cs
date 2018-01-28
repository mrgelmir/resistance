using Resistance.Characters;
using Resistance.Client;
using Resistance.Game.Model;
using Resistance.Helpers;
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

	[Header("Visual")]
	[SerializeField]
	// Reference to the charactergroup where this view will get its visuals from. 
	// Should be passed to this view on startup later
	private CharacterGroup characterGroup; 

	// Reference to the player this view represents
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
		PlayerPicker pickerHandler = new PlayerPicker(requiredPlayers, playerPickerConfirmButton);

		// Clear picker container
		playerPickerContainer.DestroyChildren();

		// Populate picker container
		foreach (Player p in players)
		{
			PlayerPickerView ppv = Instantiate(playerPickerPrefab, playerPickerContainer, false);
			ppv.SetData(p);
			pickerHandler.AddPlayerPickerView(ppv);
		}

		pickerHandler.OnComplete += ConfirmPlayerPicker;
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
			OnMissionVote(playerId, GameScript.MissionVoteResult.Fail);
	}

	public void ConfirmPlayerPicker(List<int> selectedIndices)
	{
		if (OnTeamPicked != null)
			OnTeamPicked(selectedIndices);
	}

	/// <summary>
	/// A class responsible for evaluating the picking of players
	/// Should be disposed when picking is complete
	/// </summary>
	private class PlayerPicker
	{
		public System.Action<List<int>> OnComplete;

		private int requiredPlayerCount;
		private Button confirmButton; // I do not like this button being here
		private List<PlayerPickerView> playerPickerViews = new List<PlayerPickerView>();
		private List<int> selectedPlayerIndices;

		public PlayerPicker(int requiredPlayers, Button confirmButton)
		{
			requiredPlayerCount = requiredPlayers;
			this.confirmButton = confirmButton;
			selectedPlayerIndices = new List<int>(requiredPlayers);

			confirmButton.onClick.AddListener(Finish);

			UpdateViews();
		}

		public void AddPlayerPickerView(PlayerPickerView view)
		{
			playerPickerViews.Add(view);
			view.OnSelected += OnPlayerSelected;
			view.OnDeselected += OnPlayerDeselected;
		}

		private void OnPlayerSelected(int playerIndex)
		{
			selectedPlayerIndices.Add(playerIndex);
			UpdateViews();
		}

		private void OnPlayerDeselected(int playerIndex)
		{
			selectedPlayerIndices.Remove(playerIndex);
			UpdateViews();
		}

		private void UpdateViews()
		{
			bool RequiredPlayersReached = selectedPlayerIndices.Count == requiredPlayerCount;

			// Enable/Disable toggles
			if (RequiredPlayersReached)
			{
				for (int i = 0; i < playerPickerViews.Count; i++)
				{
					playerPickerViews[i].SetInteractibility(selectedPlayerIndices.Contains(i));
				}
			}
			else
			{
				foreach (PlayerPickerView ppv in playerPickerViews)
				{
					ppv.SetInteractibility(true);
				}
			}

			// Enable/Disable confirmation button
			confirmButton.interactable = RequiredPlayersReached;
		}

		private void Finish()
		{
			// Unsubscribe from everything
			confirmButton.onClick.RemoveListener(Finish);

			foreach (PlayerPickerView ppv in playerPickerViews)
			{
				ppv.OnSelected -= OnPlayerSelected;
				ppv.OnDeselected -= OnPlayerDeselected;
			}

			if (OnComplete != null)
				OnComplete(selectedPlayerIndices);
			else
				Debug.LogWarning("PlayerPicker: Finished picking players without a listener");
		}
	}


}
