using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionSettings {
	
	private int maxVoteRounds;
	private int numberOfFailsNeeded;
	private int numberOfAttendees;
	
	public MissionSettings(int maxVoteRounds, int numberOfFailsNeeded, int numberOfAttendees){
		this.maxVoteRounds = maxVoteRounds;
		this.numberOfFailsNeeded = numberOfFailsNeeded;
		this.numberOfAttendees = numberOfAttendees;
	}
	
	public int GetMaxVoteRounds(){
		return this.maxVoteRounds;
	}

	public int GetNumberOfFailsNeeded(){
		return this.numberOfFailsNeeded;
	}
	
	public int GetNumberOfAttendees(){
		return this.numberOfAttendees;
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
