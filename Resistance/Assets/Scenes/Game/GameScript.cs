using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour
{

	[Header("Project References")]
	public PlayerView playerViewPrefab;

	[Header("Scene References")]
	public RectTransform playerViewContainer;

	private void Start()
	{
		GameController gc = GameController.Instance;

		// Clear container 
		for (int i = 0; i < playerViewContainer.transform.childCount; i++)
		{
			Destroy(playerViewContainer.transform.GetChild(i).gameObject);
		}

		// Fill container with data
		foreach (Player player in gc.PlayerList)
		{
			// Instantiate view and populate with data
			PlayerView v = Instantiate(playerViewPrefab);
			v.transform.SetParent(playerViewContainer, false);
			v.SetData(player);

			// Listen for player input
			v.OnGlobalVote += CompositionVote;
			v.OnAttendeeVote += MissionVote;
		}
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
