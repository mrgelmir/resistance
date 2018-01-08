using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Character {Spy, Resistance}

public class Player {
	
	private Character character;
	private string name;
	
	
	public Player(string name){
		this.name = name;
		this.character = Character.Resistance;
	}
	
	public string GetName(){
		return this.name;
	}
	
	public Character GetCharacter(){
		return this.character;
	}
	
	public void SetCharacter(Character character){
		this.character = character;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
