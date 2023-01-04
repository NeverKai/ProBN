using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels
{
	// Token: 0x02000635 RID: 1589
	public class CornerSlot
	{
		// Token: 0x060028A4 RID: 10404 RVA: 0x00088840 File Offset: 0x00086C40
		public CornerSlot(Vector3Int pos, int angles)
		{
			this.pos = pos;
			this.occludedAngles = new bool[angles];
			this.savedOccludedAngles = new bool[angles];
			this.occlusionCount = 0;
			this.dominos = new List<Domino>[8, 2];
			this.savedDominos = new List<Domino>[8, 2];
			for (int i = 0; i < 8; i++)
			{
				this.dominos[i, 0] = new List<Domino>();
				this.dominos[i, 1] = new List<Domino>();
				this.savedDominos[i, 0] = new List<Domino>();
				this.savedDominos[i, 1] = new List<Domino>();
			}
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x060028A5 RID: 10405 RVA: 0x00088905 File Offset: 0x00086D05
		// (set) Token: 0x060028A6 RID: 10406 RVA: 0x0008891E File Offset: 0x00086D1E
		public Vector3Int pos
		{
			get
			{
				return new Vector3Int((int)this.posX, (int)this.posY, (int)this.posZ);
			}
			set
			{
				this.posX = (byte)value.x;
				this.posY = (byte)value.y;
				this.posZ = (byte)value.z;
			}
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x060028A7 RID: 10407 RVA: 0x0008894A File Offset: 0x00086D4A
		public float visiblity
		{
			get
			{
				return 1f - (float)this.occlusionCount / (float)this.occludedAngles.Length;
			}
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x060028A8 RID: 10408 RVA: 0x00088963 File Offset: 0x00086D63
		public CornerSlot.State state
		{
			get
			{
				if (this.stateCount[1] == 0)
				{
					return CornerSlot.State.Outside;
				}
				if (this.stateCount[0] == 0)
				{
					return CornerSlot.State.Inside;
				}
				return CornerSlot.State.Unclear;
			}
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x060028A9 RID: 10409 RVA: 0x00088984 File Offset: 0x00086D84
		public bool inside
		{
			get
			{
				return this.stateCount[0] == 0;
			}
		}

		// Token: 0x060028AA RID: 10410 RVA: 0x00088991 File Offset: 0x00086D91
		public bool Occlude(int i)
		{
			if (this.occludedAngles[i])
			{
				return false;
			}
			this.occludedAngles[i] = true;
			this.occlusionCount++;
			return true;
		}

		// Token: 0x060028AB RID: 10411 RVA: 0x000889BA File Offset: 0x00086DBA
		public Color GetColor()
		{
			return (!this.inside) ? Color.white : new Color(0.5f, 0.5f, 0.5f, 0f);
		}

		// Token: 0x060028AC RID: 10412 RVA: 0x000889EC File Offset: 0x00086DEC
		public void Clear()
		{
			for (int i = 0; i < 8; i++)
			{
				this.dominos[i, 0].Clear();
				this.dominos[i, 1].Clear();
				this.savedDominos[i, 0].Clear();
				this.savedDominos[i, 1].Clear();
				this.dominos[i, 0] = null;
				this.dominos[i, 1] = null;
				this.savedDominos[i, 0] = null;
				this.savedDominos[i, 1] = null;
			}
		}

		// Token: 0x060028AD RID: 10413 RVA: 0x00088A8C File Offset: 0x00086E8C
		public void AddDomino(Domino domino, int index, bool inside)
		{
			List<Domino> list = this.dominos[index, (!inside) ? 0 : 1];
			this.stateCount[(!inside) ? 0 : 1]++;
			list.Add(domino);
		}

		// Token: 0x060028AE RID: 10414 RVA: 0x00088AD8 File Offset: 0x00086ED8
		public CornerSlot.State RemoveDomino(Domino domino, int index, bool inside)
		{
			CornerSlot.State result;
			using (new ScopedProfiler("Remove Domino from Corner Slot", null))
			{
				List<Domino> list = this.dominos[index, (!inside) ? 0 : 1];
				if (list.Remove(domino))
				{
					this.stateCount[(!inside) ? 0 : 1]--;
					if (list.Count == 0 && !this.collapsed)
					{
						this.collapsed = true;
						return (!inside) ? CornerSlot.State.Outside : CornerSlot.State.Inside;
					}
				}
				result = CornerSlot.State.Unclear;
			}
			return result;
		}

		// Token: 0x060028AF RID: 10415 RVA: 0x00088B8C File Offset: 0x00086F8C
		public void SaveState()
		{
			this.savedCollapsed = this.collapsed;
			this.savedStateCount[0] = this.stateCount[0];
			this.savedStateCount[1] = this.stateCount[1];
			for (int i = 0; i < this.occludedAngles.Length; i++)
			{
				this.savedOccludedAngles[i] = this.occludedAngles[i];
			}
			this.savedOcclusionCount = this.occlusionCount;
			for (int j = 0; j < 8; j++)
			{
				this.savedDominos[j, 0].Clear();
				this.savedDominos[j, 1].Clear();
				this.savedDominos[j, 0].AddRange(this.dominos[j, 0]);
				this.savedDominos[j, 1].AddRange(this.dominos[j, 1]);
			}
		}

		// Token: 0x060028B0 RID: 10416 RVA: 0x00088C70 File Offset: 0x00087070
		public void Reset()
		{
			this.collapsed = this.savedCollapsed;
			this.forcedVisible = false;
			for (int i = 0; i < this.occludedAngles.Length; i++)
			{
				this.occludedAngles[i] = this.savedOccludedAngles[i];
			}
			this.occlusionCount = this.savedOcclusionCount;
			this.stateCount[0] = this.savedStateCount[0];
			this.stateCount[1] = this.savedStateCount[1];
			for (int j = 0; j < 8; j++)
			{
				this.dominos[j, 0].Clear();
				this.dominos[j, 1].Clear();
				this.dominos[j, 0].AddRange(this.savedDominos[j, 0]);
				this.dominos[j, 1].AddRange(this.savedDominos[j, 1]);
			}
		}

		// Token: 0x04001A35 RID: 6709
		private byte posX;

		// Token: 0x04001A36 RID: 6710
		private byte posY;

		// Token: 0x04001A37 RID: 6711
		private byte posZ;

		// Token: 0x04001A38 RID: 6712
		public CornerSlot.Mode mode;

		// Token: 0x04001A39 RID: 6713
		public List<Domino>[,] dominos;

		// Token: 0x04001A3A RID: 6714
		private List<Domino>[,] savedDominos;

		// Token: 0x04001A3B RID: 6715
		public int[] stateCount = new int[2];

		// Token: 0x04001A3C RID: 6716
		public int[] savedStateCount = new int[2];

		// Token: 0x04001A3D RID: 6717
		private bool collapsed;

		// Token: 0x04001A3E RID: 6718
		private bool savedCollapsed;

		// Token: 0x04001A3F RID: 6719
		public bool forcedVisible;

		// Token: 0x04001A40 RID: 6720
		public int occlusionCount;

		// Token: 0x04001A41 RID: 6721
		public int savedOcclusionCount;

		// Token: 0x04001A42 RID: 6722
		public bool[] occludedAngles;

		// Token: 0x04001A43 RID: 6723
		public bool[] savedOccludedAngles;

		// Token: 0x02000636 RID: 1590
		public enum Mode
		{
			// Token: 0x04001A45 RID: 6725
			any,
			// Token: 0x04001A46 RID: 6726
			inside,
			// Token: 0x04001A47 RID: 6727
			outside
		}

		// Token: 0x02000637 RID: 1591
		public enum State
		{
			// Token: 0x04001A49 RID: 6729
			Outside,
			// Token: 0x04001A4A RID: 6730
			Inside,
			// Token: 0x04001A4B RID: 6731
			Unclear
		}
	}
}
