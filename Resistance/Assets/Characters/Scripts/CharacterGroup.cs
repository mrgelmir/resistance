using System.Collections.Generic;
using UnityEngine;

namespace Resistance.Characters
{
	/// <summary>
	/// A group of characters and data beloning to that group
	/// </summary>
	[CreateAssetMenu(
		menuName = "Characters/Create a character group",
		fileName = "New CharacterGroup",
		order = 1201)]
	public class CharacterGroup : ScriptableObject
	{
		[SerializeField]
		private string groupName;
		[SerializeField]
		[TextArea]
		private string groupDescription;
		[SerializeField]
		private Sprite groupLogo;
		[SerializeField]
		private List<CharacterData> characters;

		public string GroupName
		{
			get { return groupName; }
			set { groupName = value; }
		}

		public string GroupDescription
		{
			get { return groupDescription; }
			set { groupDescription = value; }
		}

		public Sprite GroupLogo
		{
			get { return groupLogo; }
			set { groupLogo = value; }
		}

		// This returns an Enumerable to avoid changing the data
		public IEnumerable<CharacterData> Characters
		{
			get { return characters; }
		}

		public CharacterData GetCharacter(string characterID)
		{
			return characters.Find((CharacterData data) =>
			{
				return data.CharacterID.Equals(characterID);
			});
		}

		private void OnValidate()
		{
			HashSet<string> identifiers = new HashSet<string>();
			foreach (CharacterData cd in characters)
			{
				if (!identifiers.Add(cd.CharacterID))
				{
					Debug.LogWarning("Character group " + groupName + "is invalid: the character id " + cd.CharacterID + "is used more than once");
				}
			}
		}
	}
}