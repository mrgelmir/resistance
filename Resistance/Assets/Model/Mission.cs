using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission {
	
	private List<GlobalVote> globalVoteList;
	private List<PlayerVote> attendeesVoteList;
	
	public Mission(){
		globalVoteList = new List<GlobalVote>();
		attendeesVoteList = new List<PlayerVote>();
	}

	public List<GlobalVote> GetGlobalVoteList(){
		return this.globalVoteList;
	}
}
