using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using I2.Loc;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x0200058E RID: 1422
	[ObjectDumper.LeafAttribute]
	[Serializable]
	public class LevelState
	{
		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x060024DF RID: 9439 RVA: 0x00073E53 File Offset: 0x00072253
		public string dbgName
		{
			get
			{
				return ScriptLocalization.Get(this.nameTerm, true, 0, true, false, null, null);
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x060024E0 RID: 9440 RVA: 0x00073E66 File Offset: 0x00072266
		// (set) Token: 0x060024E1 RID: 9441 RVA: 0x00073E84 File Offset: 0x00072284
		public HeroUpgradeDefinition item
		{
			get
			{
				return (this._item == null) ? null : this._item.definition;
			}
			set
			{
				this._item = value;
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x060024E2 RID: 9442 RVA: 0x00073E92 File Offset: 0x00072292
		public int totalBounty
		{
			get
			{
				return (int)((short)this.wavesCount * this.bountyPerWave);
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x060024E3 RID: 9443 RVA: 0x00073EA1 File Offset: 0x000722A1
		// (set) Token: 0x060024E4 RID: 9444 RVA: 0x00073EA9 File Offset: 0x000722A9
		public bool unlocked
		{
			get
			{
				return this._unlocked;
			}
			set
			{
				if (this._unlocked == value)
				{
					return;
				}
				this._unlocked = value;
				if (value)
				{
					this.onUnlocked(this);
				}
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x060024E5 RID: 9445 RVA: 0x00073ED1 File Offset: 0x000722D1
		// (set) Token: 0x060024E6 RID: 9446 RVA: 0x00073EE4 File Offset: 0x000722E4
		public Vector2 pos
		{
			get
			{
				return new Vector2(this.posX, this.posY);
			}
			set
			{
				this.posX = value.x;
				this.posY = value.y;
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x060024E7 RID: 9447 RVA: 0x00073F00 File Offset: 0x00072300
		public Vector3 size
		{
			get
			{
				return new Vector3((float)this.width, (float)this.height, (float)this.width);
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x060024E8 RID: 9448 RVA: 0x00073F1C File Offset: 0x0007231C
		public int rectMaxX
		{
			get
			{
				return this.rectMinX + this.rectWidth;
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x060024E9 RID: 9449 RVA: 0x00073F2B File Offset: 0x0007232B
		public int rectMaxY
		{
			get
			{
				return this.rectMinY + this.rectHeight;
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x060024EA RID: 9450 RVA: 0x00073F3A File Offset: 0x0007233A
		// (set) Token: 0x060024EB RID: 9451 RVA: 0x00073F5D File Offset: 0x0007235D
		public Rect spriteRect
		{
			get
			{
				return new Rect((float)this.rectMinX, (float)this.rectMinY, (float)this.rectWidth, (float)this.rectHeight);
			}
			set
			{
				this.rectMinX = (int)value.xMin;
				this.rectMinY = (int)value.yMin;
				this.rectWidth = (int)value.width;
				this.rectHeight = (int)value.height;
			}
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x060024EC RID: 9452 RVA: 0x00073F97 File Offset: 0x00072397
		public float innerRadius
		{
			get
			{
				return (float)this.width * 0.8f;
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x060024ED RID: 9453 RVA: 0x00073FA6 File Offset: 0x000723A6
		public float outerRadius
		{
			get
			{
				return this.innerRadius + 3f;
			}
		}

		// Token: 0x060024EE RID: 9454 RVA: 0x00073FB4 File Offset: 0x000723B4
		[OnDeserialized]
		private void PostLoad(StreamingContext context)
		{
			if (this.coinTarget == 0)
			{
				this.coinTarget = 7;
			}
		}

		// Token: 0x060024EF RID: 9455 RVA: 0x00073FC8 File Offset: 0x000723C8
		public static implicit operator bool(LevelState state)
		{
			return state != null;
		}

		// Token: 0x060024F0 RID: 9456 RVA: 0x00073FD4 File Offset: 0x000723D4
		public override string ToString()
		{
			return string.Format("{0}:  {1}/{2} coins{3}{4}{5}", new object[]
			{
				this.dbgName,
				this.coinCount,
				this.coinTarget,
				(this.heroId < 0) ? string.Empty : (", hero[" + this.heroId + "]"),
				(!(this.item != null)) ? string.Empty : (", " + this.item.dbgName),
				(this.checkpointState == LevelState.CheckpointState.None) ? string.Empty : (", Checkpoint " + this.checkpointState)
			});
		}

		// Token: 0x0400176C RID: 5996
		public string nameTerm;

		// Token: 0x0400176D RID: 5997
		public int seed;

		// Token: 0x0400176E RID: 5998
		public byte frontierDepth;

		// Token: 0x0400176F RID: 5999
		public byte stepsFromStart;

		// Token: 0x04001770 RID: 6000
		public byte stepsFromEnd;

		// Token: 0x04001771 RID: 6001
		public byte index;

		// Token: 0x04001772 RID: 6002
		public byte width;

		// Token: 0x04001773 RID: 6003
		public byte height;

		// Token: 0x04001774 RID: 6004
		public byte playCount;

		// Token: 0x04001775 RID: 6005
		public byte coinCount;

		// Token: 0x04001776 RID: 6006
		public byte coinTarget;

		// Token: 0x04001777 RID: 6007
		public short bountyPerWave;

		// Token: 0x04001778 RID: 6008
		public byte wavesCount;

		// Token: 0x04001779 RID: 6009
		public byte minWaveSpacing;

		// Token: 0x0400177A RID: 6010
		public byte maxWaveSpacing;

		// Token: 0x0400177B RID: 6011
		public byte relativeDifficulty;

		// Token: 0x0400177C RID: 6012
		public bool goodSeed;

		// Token: 0x0400177D RID: 6013
		public bool hasSprite;

		// Token: 0x0400177E RID: 6014
		public bool _unlocked;

		// Token: 0x0400177F RID: 6015
		public bool metaReward;

		// Token: 0x04001780 RID: 6016
		public HouseState[] houses;

		// Token: 0x04001781 RID: 6017
		private SerializableHeroUpgrade _item;

		// Token: 0x04001782 RID: 6018
		public int heroId = -1;

		// Token: 0x04001783 RID: 6019
		public LevelState.CheckpointState checkpointState;

		// Token: 0x04001784 RID: 6020
		public float posX;

		// Token: 0x04001785 RID: 6021
		public float posY;

		// Token: 0x04001786 RID: 6022
		public List<int> connections = new List<int>();

		// Token: 0x04001787 RID: 6023
		public List<LevelObjectReference> objectReferences = new List<LevelObjectReference>();

		// Token: 0x04001788 RID: 6024
		public int rectMinX;

		// Token: 0x04001789 RID: 6025
		public int rectMinY;

		// Token: 0x0400178A RID: 6026
		public int rectWidth;

		// Token: 0x0400178B RID: 6027
		public int rectHeight;

		// Token: 0x0400178C RID: 6028
		public const float width2InnerRadius = 0.8f;

		// Token: 0x0400178D RID: 6029
		public const float inner2outerRadiusAdd = 3f;

		// Token: 0x0400178E RID: 6030
		[ObjectDumper.HideValuesAttribute]
		[NonSerialized]
		public Action<LevelState> onUnlocked = delegate(LevelState A_0)
		{
		};

		// Token: 0x0200058F RID: 1423
		public enum CheckpointState : byte
		{
			// Token: 0x04001791 RID: 6033
			None,
			// Token: 0x04001792 RID: 6034
			Destroyed,
			// Token: 0x04001793 RID: 6035
			Available,
			// Token: 0x04001794 RID: 6036
			Saved,
			// Token: 0x04001795 RID: 6037
			Current
		}
	}
}
