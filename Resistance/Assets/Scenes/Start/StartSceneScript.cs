using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class StartSceneScript : MonoBehaviour
{

	//Visual object
	[SerializeField]
	private InputField playerInput;
	[SerializeField]
	private GameObject playerListView;

	protected void Start()
	{
		GameController.Instance.CreateGame();
		GameController.Instance.OnPlayersChanged += UpdatePlayerList;
	}

	protected void OnDestroy()
	{
		GameController.Instance.OnPlayersChanged -= UpdatePlayerList;
	}

	public void CreatePlayer()
	{
		bool valid = GameController.Instance.AddPlayer(playerInput.text);

		if (!valid)
		{
			// TODO
		}
		else
		{
			playerInput.text = "";
		}
	}

	//Display player list in StartScene
	public void UpdatePlayerList()
	{
		//Clear list
		foreach (Transform child in playerListView.transform)
		{
			GameObject.Destroy(child.gameObject);
		}

		//Add all players
		for (int i = 0; i < GameController.Instance.PlayerList.Count; i++)
		{
			GameObject player = new GameObject("Text");
			player.transform.parent = playerListView.transform;
			Text t = player.AddComponent<Text>();
			t.text = "Player " + (i + 1).ToString() + ": " + GameController.Instance.PlayerList[i].Name;
			t.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
		}
	}

	//If all players are added, the data is prepared for the start of the game
	//and the scene switches to 'game'
	public void StartButton()
	{

		Game gameData = GameController.Instance.GameData;

		//Check if there are enough players added
		if (gameData.PlayerList.Count >= gameData.MinPlayers && gameData.PlayerList.Count > gameData.NumberOfSpies)
		{

			//Set spies
			for (int i = 0; i < gameData.NumberOfSpies; i++)
			{

				int randomNumber = Random.Range(0, gameData.PlayerList.Count);
				print(randomNumber);

				if (gameData.PlayerList[randomNumber].GetCharacter() == Character.Spy)
				{
					i--;
				}
				else
				{
					gameData.PlayerList[randomNumber].SetCharacter(Character.Spy);
				}
			}
			//Generate missions
			for (int i = 0; i < gameData.MissionSettingsList.Count; i++)
				gameData.MissionList.Add(new Mission());

			//Set Leader for first votinground in first missionList (first leader is random)
			gameData.MissionList[0].GetGlobalVoteList().Add(new GlobalVote(Random.Range(0, gameData.PlayerList.Count)));

			//Open game scene
			Application.LoadLevel("game");


		}
		else
			EditorUtility.DisplayDialog("Titel", "Min. aantal spelers is " + gameData.MinPlayers + ".", "Ok", "Cancel");

	}

}
