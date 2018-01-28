namespace Resistance.Game.Model
{
	public enum CharacterRole { Spy, Resistance }

	public class Player
	{
		/// <summary>
		/// The id of the character, which will be used for getting visuals
		/// </summary>
		public string CharacterId;

		/// <summary>
		/// The role the character has.
		/// The options for now are Spy or Resistance
		/// </summary>
		private CharacterRole character;

		/// <summary>
		/// The name of the person playing this character
		/// </summary>
		private string playerName;

		/// <summary>
		/// The player's ID
		/// (This should be the player's index, or we're in for a world of refactoring)
		/// (but it would be more robust ... )
		/// </summary>
		private int id;

		public CharacterRole CharacterRole
		{
			get { return character; }
			set { character = value; }
		}

		public string PlayerName
		{
			get { return playerName; }
			set { playerName = value; }
		}

		public int Id { get { return id; } }

		public Player(int id, string name)
		{
			this.id = id;
			this.playerName = name;
			this.character = CharacterRole.Resistance;
		}

		public string GetName()
		{
			return this.playerName;
		}

		public CharacterRole GetCharacter()
		{
			return this.character;
		}

		public void SetCharacter(CharacterRole character)
		{
			this.character = character;
		}

	}

}