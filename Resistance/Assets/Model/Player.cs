using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Character { Spy, Resistance }

public class Player
{

	private Character character;
	private string name;
	private int id;

	public Character Character
	{
		get { return character; }
		set { character = value; }
	}

	public string Name
	{
		get { return name; }
		set { name = value; }
	}

	public int Id { get { return id; } }

	public Player(string name)
	{
		this.name = name;
		this.character = Character.Resistance;
	}

	public string GetName()
	{
		return this.name;
	}

	public Character GetCharacter()
	{
		return this.character;
	}

	public void SetCharacter(Character character)
	{
		this.character = character;
	}

}
