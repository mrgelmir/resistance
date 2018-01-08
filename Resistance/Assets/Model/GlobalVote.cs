using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVote {
	
	private List<PlayerVote> playerVoteList;
	private int leaderID;
	
	public GlobalVote(int leaderID){
		this.leaderID = leaderID;
		this.playerVoteList = new List<PlayerVote>();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
