using Resistance.Game.Model;
using System.Collections.Generic;

namespace Resistance.Client
{
	interface IPlayer
	{
		event System.Action<int, GameScript.GroupCompositionVoteResult> OnTeamCompositionVote;
		event System.Action<int, GameScript.MissionVoteResult> OnMissionVote;
		event System.Action<List<int>> OnTeamPicked;

		void PopulatePlayerPicker(List<Player> players, int requiredPlayers);
		void SetData(Player data);
		void SetState(IPlayerState state);		
	}

	public enum IPlayerState
	{
		Idle, GroupAssembly, GroupCompositionVote, MissionVote
	}
}
