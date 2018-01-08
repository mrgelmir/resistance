using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour
{

	[Header("Project References")]
	public PlayerView playerViewPrefab;

	[Header("Scene References")]
	public RectTransform playerViewContainer;

	void Start()
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
			PlayerView v = Instantiate(playerViewPrefab);
			v.transform.SetParent(playerViewContainer, false);
			v.SetData(player);

			// TODO: Listen for player input
		}
	}

	public void PlayerVoted(Player p)
	{

	}

}
