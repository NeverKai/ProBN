using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels
{
	// Token: 0x02000633 RID: 1587
	public class Slot
	{
		// Token: 0x0600289B RID: 10395 RVA: 0x00088528 File Offset: 0x00086928
		public Slot(Vector3Int pos, int maxKeyCount)
		{
			this.pos = pos;
			this.keyCount = new List<Domino>[6, maxKeyCount];
			this.savedKeyCount = new List<Domino>[6, maxKeyCount];
			for (int i = 0; i < 6; i++)
			{
				int j = 0;
				int length = this.keyCount.GetLength(1);
				while (j < length)
				{
					this.keyCount[i, j] = new List<Domino>();
					this.savedKeyCount[i, j] = new List<Domino>();
					j++;
				}
			}
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x0600289C RID: 10396 RVA: 0x000885C7 File Offset: 0x000869C7
		public bool done
		{
			get
			{
				return this.domino != null;
			}
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x0600289D RID: 10397 RVA: 0x000885D5 File Offset: 0x000869D5
		// (set) Token: 0x0600289E RID: 10398 RVA: 0x000885EE File Offset: 0x000869EE
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

		// Token: 0x0600289F RID: 10399 RVA: 0x0008861C File Offset: 0x00086A1C
		public void Clear()
		{
			this.domino = null;
			this.dominos.Clear();
			this.savedDominos.Clear();
			for (int i = 0; i < 6; i++)
			{
				int j = 0;
				int length = this.keyCount.GetLength(1);
				while (j < length)
				{
					this.keyCount[i, j].Clear();
					this.keyCount[i, j] = null;
					this.savedKeyCount[i, j].Clear();
					this.savedKeyCount[i, j] = null;
					j++;
				}
			}
		}

		// Token: 0x060028A0 RID: 10400 RVA: 0x000886B7 File Offset: 0x00086AB7
		private ulong GetPower2(int key)
		{
			return 1UL << key;
		}

		// Token: 0x060028A1 RID: 10401 RVA: 0x000886C0 File Offset: 0x00086AC0
		public void AddDomino(Domino domino, Claim claim)
		{
			this.dominos.Add(domino);
			for (int i = 0; i < 6; i++)
			{
				int num = claim.keys[i];
				this.keyCount[i, num].Add(domino);
			}
		}

		// Token: 0x060028A2 RID: 10402 RVA: 0x00088708 File Offset: 0x00086B08
		public void SaveState()
		{
			this.savedDominos.Clear();
			this.savedDominos.AddRange(this.dominos);
			for (int i = 0; i < this.keyCount.GetLength(0); i++)
			{
				for (int j = 0; j < this.keyCount.GetLength(1); j++)
				{
					this.savedKeyCount[i, j].Clear();
					this.savedKeyCount[i, j].AddRange(this.keyCount[i, j]);
				}
			}
		}

		// Token: 0x060028A3 RID: 10403 RVA: 0x0008879C File Offset: 0x00086B9C
		public void Reset()
		{
			this.dominos.Clear();
			this.dominos.AddRange(this.savedDominos);
			for (int i = 0; i < this.keyCount.GetLength(0); i++)
			{
				for (int j = 0; j < this.keyCount.GetLength(1); j++)
				{
					this.keyCount[i, j].Clear();
					this.keyCount[i, j].AddRange(this.savedKeyCount[i, j]);
				}
			}
			this.navigability = Slot.Navigability.Unclear;
			this.domino = null;
		}

		// Token: 0x04001A26 RID: 6694
		public Domino domino;

		// Token: 0x04001A27 RID: 6695
		public Vector3 normal;

		// Token: 0x04001A28 RID: 6696
		private byte posX;

		// Token: 0x04001A29 RID: 6697
		private byte posY;

		// Token: 0x04001A2A RID: 6698
		private byte posZ;

		// Token: 0x04001A2B RID: 6699
		public Slot.Navigability navigability;

		// Token: 0x04001A2C RID: 6700
		public List<Domino> dominos = new List<Domino>();

		// Token: 0x04001A2D RID: 6701
		public List<Domino>[,] keyCount;

		// Token: 0x04001A2E RID: 6702
		private List<Domino> savedDominos = new List<Domino>();

		// Token: 0x04001A2F RID: 6703
		private List<Domino>[,] savedKeyCount;

		// Token: 0x04001A30 RID: 6704
		public float score;

		// Token: 0x02000634 RID: 1588
		public enum Navigability
		{
			// Token: 0x04001A32 RID: 6706
			Unclear,
			// Token: 0x04001A33 RID: 6707
			Open,
			// Token: 0x04001A34 RID: 6708
			Navigable
		}
	}
}
