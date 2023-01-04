using System;
using UnityEngine;

namespace Voxels.TowerDefense.CampaignGeneration
{
	// Token: 0x0200071C RID: 1820
	public struct Node
	{
		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x06002F35 RID: 12085 RVA: 0x000BD164 File Offset: 0x000BB564
		public int totalSteps
		{
			get
			{
				return (int)(this.stepsFromStart + this.stepsFromEnd);
			}
		}

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x06002F36 RID: 12086 RVA: 0x000BD173 File Offset: 0x000BB573
		public float frontierCost
		{
			get
			{
				return (float)this.stepsFromStart + this.pos.x * 0.01f;
			}
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x06002F37 RID: 12087 RVA: 0x000BD18E File Offset: 0x000BB58E
		public int connectionCount
		{
			get
			{
				return (int)(this.connectionIndex1 - this.connectionIndex0);
			}
		}

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x06002F38 RID: 12088 RVA: 0x000BD19D File Offset: 0x000BB59D
		public int idleNeighbours
		{
			get
			{
				return this.connectionCount - (int)this.chosenNeigbours;
			}
		}

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x06002F39 RID: 12089 RVA: 0x000BD1AC File Offset: 0x000BB5AC
		public float innerRadius
		{
			get
			{
				return 0.8f * (float)this.width;
			}
		}

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x06002F3A RID: 12090 RVA: 0x000BD1BB File Offset: 0x000BB5BB
		public float outerRadius
		{
			get
			{
				return this.innerRadius + 3f;
			}
		}

		// Token: 0x06002F3B RID: 12091 RVA: 0x000BD1CC File Offset: 0x000BB5CC
		public Color GetColor(Node.ColorType colorType)
		{
			Node.State state = this.state;
			if (state == Node.State.Forbidden)
			{
				return Color.white.SetA(0.1f);
			}
			if (state == Node.State.Idle)
			{
				return Color.white.SetA(0.7f);
			}
			switch (colorType)
			{
			case Node.ColorType.Type:
				return Color.HSVToRGB(((float)this.type / 7f + 0.33333334f) % 1f, 0.5f, 1f);
			case Node.ColorType.StepsFromStart:
				return Color.HSVToRGB((float)this.stepsFromStart / 8f % 1f, 0.5f, 1f);
			case Node.ColorType.StepsFromEnd:
				return Color.HSVToRGB((float)this.stepsFromEnd / 8f % 1f, 0.5f, 1f);
			case Node.ColorType.TotalSteps:
				return Color.HSVToRGB((float)this.totalSteps / 6f % 1f, 0.5f, 1f);
			case Node.ColorType.Chokepointness:
				return Color.HSVToRGB((float)this.chokepointLevel / 4f % 1f, 0.5f, 1f);
			case Node.ColorType.Reward:
				return Color.HSVToRGB((float)this.reward / 6f % 1f, 0.5f, 1f);
			case Node.ColorType.Difficulty:
				return new Color(1f, this.difficulty, 0f);
			case Node.ColorType.FrontierDepth:
				return Color.HSVToRGB((float)this.frontierDepth / 6f % 1f, 0.5f, 1f);
			default:
				return Color.white;
			}
		}

		// Token: 0x06002F3C RID: 12092 RVA: 0x000BD354 File Offset: 0x000BB754
		public LevelState GetLevelState()
		{
			return new LevelState
			{
				width = this.width,
				height = this.height,
				frontierDepth = this.frontierDepth,
				stepsFromStart = this.stepsFromStart,
				stepsFromEnd = this.stepsFromEnd,
				stepsFromEnd = this.stepsFromEnd,
				pos = this.pos,
				seed = UnityEngine.Random.Range(int.MinValue, int.MaxValue)
			};
		}

		// Token: 0x06002F3D RID: 12093 RVA: 0x000BD3D4 File Offset: 0x000BB7D4
		public static int Compare(Node node0, Node node1)
		{
			int num = node0.stepsFromStart.CompareTo(node1.stepsFromStart);
			if (num != 0)
			{
				return num;
			}
			num = node1.stepsFromEnd.CompareTo(node0.stepsFromEnd);
			if (num != 0)
			{
				return num;
			}
			return node0.pos.x.CompareTo(node1.pos.x);
		}

		// Token: 0x04001F6E RID: 8046
		public byte width;

		// Token: 0x04001F6F RID: 8047
		public byte height;

		// Token: 0x04001F70 RID: 8048
		public Vector2 pos;

		// Token: 0x04001F71 RID: 8049
		public Vector2 move;

		// Token: 0x04001F72 RID: 8050
		public byte stepsFromStart;

		// Token: 0x04001F73 RID: 8051
		public byte stepsFromEnd;

		// Token: 0x04001F74 RID: 8052
		public byte frontierDepth;

		// Token: 0x04001F75 RID: 8053
		public byte rewardNeighbours;

		// Token: 0x04001F76 RID: 8054
		public short connectionIndex0;

		// Token: 0x04001F77 RID: 8055
		public short connectionIndex1;

		// Token: 0x04001F78 RID: 8056
		public byte chosenNeigbours;

		// Token: 0x04001F79 RID: 8057
		public float rawDifficulty;

		// Token: 0x04001F7A RID: 8058
		public float difficulty;

		// Token: 0x04001F7B RID: 8059
		public byte enemyTypes;

		// Token: 0x04001F7C RID: 8060
		public Node.State state;

		// Token: 0x04001F7D RID: 8061
		public Node.Type type;

		// Token: 0x04001F7E RID: 8062
		public RewardType reward;

		// Token: 0x04001F7F RID: 8063
		public byte chokepointLevel;

		// Token: 0x0200071D RID: 1821
		public enum State
		{
			// Token: 0x04001F81 RID: 8065
			Forbidden,
			// Token: 0x04001F82 RID: 8066
			Idle,
			// Token: 0x04001F83 RID: 8067
			Chosen
		}

		// Token: 0x0200071E RID: 1822
		public enum Type
		{
			// Token: 0x04001F85 RID: 8069
			Normal,
			// Token: 0x04001F86 RID: 8070
			Buffer,
			// Token: 0x04001F87 RID: 8071
			Chokepoint,
			// Token: 0x04001F88 RID: 8072
			Offshoot,
			// Token: 0x04001F89 RID: 8073
			Detour
		}

		// Token: 0x0200071F RID: 1823
		public enum ColorType
		{
			// Token: 0x04001F8B RID: 8075
			Type,
			// Token: 0x04001F8C RID: 8076
			StepsFromStart,
			// Token: 0x04001F8D RID: 8077
			StepsFromEnd,
			// Token: 0x04001F8E RID: 8078
			TotalSteps,
			// Token: 0x04001F8F RID: 8079
			Chokepointness,
			// Token: 0x04001F90 RID: 8080
			Reward,
			// Token: 0x04001F91 RID: 8081
			Difficulty,
			// Token: 0x04001F92 RID: 8082
			FrontierDepth
		}
	}
}
