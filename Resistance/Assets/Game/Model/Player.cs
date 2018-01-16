namespace Resistance.Game.Model
{
	public enum Character { Spy, Resistance }

	public class Player
	{

		private Character character;
		private string name;
		/// <summary>
		/// The player's ID
		/// (This should be the player's index, or we're in for a world of refactoring)
		/// </summary>
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

		public Player(int id, string name)
		{
			this.id = id;
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

}