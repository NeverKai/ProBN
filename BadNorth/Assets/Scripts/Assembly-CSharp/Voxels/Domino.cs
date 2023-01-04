using System;
using UnityEngine;

namespace Voxels
{
	// Token: 0x0200063F RID: 1599
	public class Domino : IComparable<Domino>
	{
		// Token: 0x060028B9 RID: 10425 RVA: 0x00088E79 File Offset: 0x00087279
		public Domino(Wrapper placementWrapper, Vector3Int offset)
		{
			this.defaultScore = placementWrapper.defaultScore;
			this.placementWrapper = placementWrapper;
			this.offset = offset;
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x060028BA RID: 10426 RVA: 0x00088EA6 File Offset: 0x000872A6
		// (set) Token: 0x060028BB RID: 10427 RVA: 0x00088EBF File Offset: 0x000872BF
		public Vector3Int offset
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

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x060028BC RID: 10428 RVA: 0x00088EEB File Offset: 0x000872EB
		public Placement placement
		{
			get
			{
				return this.placementWrapper.placement;
			}
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x060028BD RID: 10429 RVA: 0x00088EF8 File Offset: 0x000872F8
		public float cost
		{
			get
			{
				return -this.score;
			}
		}

		// Token: 0x060028BE RID: 10430 RVA: 0x00088F01 File Offset: 0x00087301
		public OrientedModule GetOrientedModule()
		{
			return this.placementWrapper.placement.modules[(int)(this.fraction * (float)this.placementWrapper.placement.modules.Count)];
		}

		// Token: 0x060028BF RID: 10431 RVA: 0x00088F36 File Offset: 0x00087336
		public void Clear()
		{
			this.placementWrapper = null;
			this.placedModule = null;
		}

		// Token: 0x060028C0 RID: 10432 RVA: 0x00088F46 File Offset: 0x00087346
		public Claim GetClaim(Vector3Int pos)
		{
			return this.placement.GetClaimAt(pos - this.offset);
		}

		// Token: 0x060028C1 RID: 10433 RVA: 0x00088F5F File Offset: 0x0008735F
		public Bounds GetBounds()
		{
			return new Bounds(this.placement.bounds.center + this.offset, this.placement.bounds.size);
		}

		// Token: 0x060028C2 RID: 10434 RVA: 0x00088F96 File Offset: 0x00087396
		public Bounds GetNavigableBounds()
		{
			return new Bounds(this.placement.navigableBounds.center + this.offset, this.placement.navigableBounds.size);
		}

		// Token: 0x060028C3 RID: 10435 RVA: 0x00088FCD File Offset: 0x000873CD
		public Bounds GetOpenBounds()
		{
			return new Bounds(this.placement.openBounds.center + this.offset, this.placement.openBounds.size);
		}

		// Token: 0x060028C4 RID: 10436 RVA: 0x00089004 File Offset: 0x00087404
		public void Reset()
		{
			this.state = this.savedState;
		}

		// Token: 0x060028C5 RID: 10437 RVA: 0x00089012 File Offset: 0x00087412
		public void SaveState()
		{
			this.savedState = this.state;
		}

		// Token: 0x060028C6 RID: 10438 RVA: 0x00089020 File Offset: 0x00087420
		int IComparable<Domino>.CompareTo(Domino other)
		{
			return this.cost.CompareTo(other.cost);
		}

		// Token: 0x04001A54 RID: 6740
		private byte posX;

		// Token: 0x04001A55 RID: 6741
		private byte posY;

		// Token: 0x04001A56 RID: 6742
		private byte posZ;

		// Token: 0x04001A57 RID: 6743
		public Wrapper placementWrapper;

		// Token: 0x04001A58 RID: 6744
		public Domino.State state;

		// Token: 0x04001A59 RID: 6745
		public Domino.State savedState;

		// Token: 0x04001A5A RID: 6746
		public OrientedModule placedModule;

		// Token: 0x04001A5B RID: 6747
		public float defaultScore = 1f;

		// Token: 0x04001A5C RID: 6748
		public float score;

		// Token: 0x04001A5D RID: 6749
		public float fraction;

		// Token: 0x02000640 RID: 1600
		public enum State
		{
			// Token: 0x04001A5F RID: 6751
			idle,
			// Token: 0x04001A60 RID: 6752
			done,
			// Token: 0x04001A61 RID: 6753
			removing
		}
	}
}
