namespace Resistance.Game.Model
{
	public class MissionSettings
	{

		private int maxVoteRounds;
		private int numberOfFailsNeeded;
		private int numberOfAttendees;

		public MissionSettings(int maxVoteRounds, int numberOfFailsNeeded, int numberOfAttendees)
		{
			this.maxVoteRounds = maxVoteRounds;
			this.numberOfFailsNeeded = numberOfFailsNeeded;
			this.numberOfAttendees = numberOfAttendees;
		}

		public int NumberOfAttendees
		{
			get
			{
				return numberOfAttendees;
			}

			set
			{
				numberOfAttendees = value;
			}
		}

		public int NumberOfFailsNeeded
		{
			get
			{
				return numberOfFailsNeeded;
			}

			set
			{
				numberOfFailsNeeded = value;
			}
		}

		public int MaxVoteRounds
		{
			get
			{
				return maxVoteRounds;
			}

			set
			{
				maxVoteRounds = value;
			}
		}
	}

}