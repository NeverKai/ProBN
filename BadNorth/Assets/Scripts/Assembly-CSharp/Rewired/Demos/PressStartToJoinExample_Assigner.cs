using System;
using System.Collections.Generic;
using UnityEngine;

namespace Rewired.Demos
{
	// Token: 0x02000489 RID: 1161
	[AddComponentMenu("")]
	public class PressStartToJoinExample_Assigner : MonoBehaviour
	{
		// Token: 0x06001AB5 RID: 6837 RVA: 0x00049974 File Offset: 0x00047D74
		public static Player GetRewiredPlayer(int gamePlayerId)
		{
			if (!ReInput.isReady)
			{
				return null;
			}
			if (PressStartToJoinExample_Assigner.instance == null)
			{
				Debug.LogError("Not initialized. Do you have a PressStartToJoinPlayerSelector in your scehe?");
				return null;
			}
			for (int i = 0; i < PressStartToJoinExample_Assigner.instance.playerMap.Count; i++)
			{
				if (PressStartToJoinExample_Assigner.instance.playerMap[i].gamePlayerId == gamePlayerId)
				{
					return ReInput.players.GetPlayer(PressStartToJoinExample_Assigner.instance.playerMap[i].rewiredPlayerId);
				}
			}
			return null;
		}

		// Token: 0x06001AB6 RID: 6838 RVA: 0x00049A05 File Offset: 0x00047E05
		private void Awake()
		{
			this.playerMap = new List<PressStartToJoinExample_Assigner.PlayerMap>();
			PressStartToJoinExample_Assigner.instance = this;
		}

		// Token: 0x06001AB7 RID: 6839 RVA: 0x00049A18 File Offset: 0x00047E18
		private void Update()
		{
			for (int i = 0; i < ReInput.players.playerCount; i++)
			{
				if (ReInput.players.GetPlayer(i).GetButtonDown("JoinGame"))
				{
					this.AssignNextPlayer(i);
				}
			}
		}

		// Token: 0x06001AB8 RID: 6840 RVA: 0x00049A64 File Offset: 0x00047E64
		private void AssignNextPlayer(int rewiredPlayerId)
		{
			if (this.playerMap.Count >= this.maxPlayers)
			{
				Debug.LogError("Max player limit already reached!");
				return;
			}
			int nextGamePlayerId = this.GetNextGamePlayerId();
			this.playerMap.Add(new PressStartToJoinExample_Assigner.PlayerMap(rewiredPlayerId, nextGamePlayerId));
			Player player = ReInput.players.GetPlayer(rewiredPlayerId);
			player.controllers.maps.SetMapsEnabled(false, "Assignment");
			player.controllers.maps.SetMapsEnabled(true, "Default");
			Debug.Log(string.Concat(new object[]
			{
				"Added Rewired Player id ",
				rewiredPlayerId,
				" to game player ",
				nextGamePlayerId
			}));
		}

		// Token: 0x06001AB9 RID: 6841 RVA: 0x00049B18 File Offset: 0x00047F18
		private int GetNextGamePlayerId()
		{
			return this.gamePlayerIdCounter++;
		}

		// Token: 0x040010B5 RID: 4277
		private static PressStartToJoinExample_Assigner instance;

		// Token: 0x040010B6 RID: 4278
		public int maxPlayers = 4;

		// Token: 0x040010B7 RID: 4279
		private List<PressStartToJoinExample_Assigner.PlayerMap> playerMap;

		// Token: 0x040010B8 RID: 4280
		private int gamePlayerIdCounter;

		// Token: 0x0200048A RID: 1162
		private class PlayerMap
		{
			// Token: 0x06001ABA RID: 6842 RVA: 0x00049B36 File Offset: 0x00047F36
			public PlayerMap(int rewiredPlayerId, int gamePlayerId)
			{
				this.rewiredPlayerId = rewiredPlayerId;
				this.gamePlayerId = gamePlayerId;
			}

			// Token: 0x040010B9 RID: 4281
			public int rewiredPlayerId;

			// Token: 0x040010BA RID: 4282
			public int gamePlayerId;
		}
	}
}
