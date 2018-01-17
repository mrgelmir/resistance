using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Resistance.Characters
{
	/// <summary>
	/// Holds all info needed to represent a character in game
	/// </summary>
	[CreateAssetMenu(
		menuName = "Characters/Create a character",
		fileName = "New Character",
		order = 1200)]
	public class CharacterData : ScriptableObject
	{
		[SerializeField]
		[Tooltip("A unique string identifying this character")]
		private string characterID;
		[SerializeField]
		private string characterName;
		[SerializeField]
		private Sprite characterPortrait;

		public string CharacterID
		{
			get { return characterID; }
			set { characterID = value; }
		}

		public string CharacterName
		{
			get { return characterName; }
			set { characterName = value; }
		}

		public Sprite CharacterPortrait
		{
			get { return characterPortrait; }
			set { characterPortrait = value; }
		}
	}

}