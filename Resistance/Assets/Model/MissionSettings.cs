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

	public int GetMaxVoteRounds()
	{
		return this.maxVoteRounds;
	}

	public int GetNumberOfFailsNeeded()
	{
		return this.numberOfFailsNeeded;
	}

	public int GetNumberOfAttendees()
	{
		return this.numberOfAttendees;
	}
}
