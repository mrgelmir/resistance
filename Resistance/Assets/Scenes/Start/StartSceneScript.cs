﻿using Resistance.Game.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
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

		// TEMP
		GameController.Instance.AddPlayer("p1");
		GameController.Instance.AddPlayer("p2");
		GameController.Instance.AddPlayer("p3");
		StartButton();
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
			// TODO: Show error message
		}
		else
		{
			// Player name is valid and added, clear field and re-focus
			playerInput.text = "";
			playerInput.ActivateInputField();
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
			// 
			GameController.Instance.StartGame();
			//Open game scene
			SceneManager.LoadScene("Scenes/Game/Game");
		}
		else
		{
			EditorUtility.DisplayDialog("Titel", "Min. aantal spelers is " + gameData.MinPlayers + ".", "Ok", "Cancel");
		}

	}

}
