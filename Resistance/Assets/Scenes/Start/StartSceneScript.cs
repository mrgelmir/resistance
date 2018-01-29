using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Resistance.Game;
using Resistance.Game.Model;
using Resistance.Characters;

namespace Resistance.Scenes.Start
{
	public class StartSceneScript : MonoBehaviour
	{
		[Header("Scene References")]
		[SerializeField]
		private InputField playerInput;
		[SerializeField]
		private GameObject playerListView;
		[SerializeField]
		private Button startButton;
		[SerializeField]
		private CharacterPickerView characterPickerView;

		[Header("Project References")]
		[SerializeField]
		private CharacterGroup characterGroup;

		protected void Start()
		{
			GameController.Instance.CreateGame();
			GameController.Instance.OnPlayersChanged += UpdatePlayerList;
			UpdatePlayerList();
			characterPickerView.SetCharacterGroup(characterGroup);

			// TEMP
			//GameController.Instance.AddPlayer("p1");
			//GameController.Instance.AddPlayer("p2");
			//GameController.Instance.AddPlayer("p3");
			//StartButton();
		}

		protected void OnDestroy()
		{
			GameController.Instance.OnPlayersChanged -= UpdatePlayerList;
		}

		public void CreatePlayer()
		{
			bool valid = GameController.Instance.AddPlayer(
				playerInput.text,
				characterPickerView.GetSelectedCharacterId());

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

		// Display player list in StartScene
		public void UpdatePlayerList()
		{
			//Clear list
			foreach (Transform child in playerListView.transform)
			{
				Destroy(child.gameObject);
			}

			//Add all players
			for (int i = 0; i < GameController.Instance.PlayerList.Count; i++)
			{
				GameObject player = new GameObject("Text");
				player.transform.SetParent(playerListView.transform, false);
				Text t = player.AddComponent<Text>();
				t.text = string.Format("Player {0}: {1} | Character: {2} ",
					(i + 1).ToString(),
					GameController.Instance.PlayerList[i].PlayerName,
					GameController.Instance.PlayerList[i].CharacterId);
				t.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
                t.fontSize = 50;
			}

			// Enable start button only when three players are created
			startButton.interactable = GameController.Instance.PlayerList.Count > 2;
		}

		//If all players are added, the data is prepared for the start of the game
		//and the scene switches to 'game'
		public void StartButton()
		{
			GameData gameData = GameController.Instance.GameData;

			//Check if there are enough players added
			if (gameData.PlayerList.Count >= gameData.MinPlayers && gameData.PlayerList.Count > gameData.NumberOfSpies)
			{
				// Start the game
				GameController.Instance.StartGame();
				//Open game scene
				SceneManager.LoadScene("Scenes/Game/Game");
			}

		}

	}
}
