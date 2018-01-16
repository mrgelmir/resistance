namespace Resistance.Game.Model
{
	using System.Collections.Generic;

	public class Game
	{
		// Persistent data 
		// TODO: Encapsulate to maintain state
		public List<MissionSettings> MissionSettingsList;
		public int NumberOfMissions;
		public int MinPlayers;
		public int NumberOfSpies;

		// Changing data 
		public List<Player> PlayerList;

		// This keeps track of game state and history
		public List<Mission> MissionList;
		public Mission CurrentMission;
		public int CurrentLeaderId;
		public int CurrentMissionId;

	}


}