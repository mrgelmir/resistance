namespace Resistance.Game.Model
{
	public class PlayerVote
	{

		public bool Vote = false;
		public bool Voted = false;
		public int PlayerID = -1;

		public PlayerVote(int playerId)
		{
			PlayerID = playerId;			
		}

		public void PlaceVote(bool vote)
		{
			Vote = vote;
			Voted = true;
		}
	}

}