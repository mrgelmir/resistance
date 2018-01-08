
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Resistance.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GameController : MonoBehaviour {

    //Global GameController
    private static GameController instance;

	//Script objects
	private List<Player> playerList;
	private List<Mission> missionList;
	private List<MissionSettings> missionSettingsList;
	private int numberOfMissions;
	private int minPlayers;
	private int numberOfSpies;
	
	//Visual object
	[SerializeField]
	private Text playerInput;
	[SerializeField]
	private GameObject playerListView;
	[SerializeField]
	private GameObject missionsView;
	
	
	/*public GameController(){
	}
	*/

    //Test method
    public void Test(string s)
    {
        print("Test method heeft gewerkt. " + s);
    }

    //Get instance
    public static GameController GetGameController()
    {
        return instance;
    }


    //Add player if it doesn't exist yet
	public void AddPlayerButton(){
		string name = playerInput.text.ToString();
		bool exists = false;
		
		for(int i=0; i<playerList.Count; i++){
			if(playerList[i].GetName().Equals(name))
				exists = true;
		}
		
		if(!name.Equals("") && !exists){
			playerList.Add(new Player(name));
			UpdatePlayerList();
		}
	}
	
    //Display player list in StartScene
	public void UpdatePlayerList(){
		//Clear list
		foreach (Transform child in playerListView.transform) {
             GameObject.Destroy(child.gameObject);
         }
		
		//Add all players
		for(int i=0; i<playerList.Count; i++){
			GameObject player = new GameObject("Text");
			player.transform.parent = playerListView.transform;
			Text t = player.AddComponent<Text>();
			t.text = "Player "+ (i+1).ToString()+": "+playerList[i].GetName();
			t.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
		}
	}

    //If all players are added, the data is prepared for the start of the game
    //and the scene switches to 'game'
	public void StartButton(){		
		//Check if there are enough players added
		if(playerList.Count >= minPlayers && playerList.Count > numberOfSpies){
			
			//Set spies
			for(int i=0; i<numberOfSpies; i++){
				
				int randomNumber = Random.Range(0, playerList.Count);
				print(randomNumber);
				
				if(playerList[randomNumber].GetCharacter() == Character.Spy){
					i--;
				}else{
					playerList[randomNumber].SetCharacter(Character.Spy);
				}
			}
			//Generate missions
			for(int i=0; i<missionSettingsList.Count; i++)
				missionList.Add(new Mission());
			
			//Set Leader for first votinground in first missionList (first leader is random)
			missionList[0].GetGlobalVoteList().Add(new GlobalVote(Random.Range(0, playerList.Count)));
			
			//Open game scene
			Application.LoadLevel("game");
			
			
		}else
			EditorUtility.DisplayDialog("Titel", "Min. aantal spelers is "+minPlayers+".", "Ok", "Cancel");
		
	}

    /*
    //Each users screen is build by this function depending on the data
	public void RefreshUserScreens(){
		
	}
    */
	
    //Button respons methods
    //After the leader picks teams, this method is activated
	public void LeaderPickedMissionTeam(){
		
	}
	
	// Use this for initialization
	void Start () {

        //Make GameController singleton
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);

        //Init
        playerList = new List<Player>();
        missionList = new List<Mission>();
        missionSettingsList = new List<MissionSettings>();
        print("GameController created");

        //Mission settings
        minPlayers = 1;
        numberOfSpies = 0;
        missionSettingsList.Add(new MissionSettings(5, 1, 2));
        missionSettingsList.Add(new MissionSettings(5, 1, 3));
        missionSettingsList.Add(new MissionSettings(5, 1, 2));
        missionSettingsList.Add(new MissionSettings(5, 1, 3));
        missionSettingsList.Add(new MissionSettings(5, 1, 3));

        numberOfMissions = missionSettingsList.Count;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
