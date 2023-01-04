using System;
using UnityEngine;

// Token: 0x02000795 RID: 1941
public static class LayerMaster
{
	// Token: 0x040021EF RID: 8687
	public static LayerMaster.LayerRef moduleLayer = new LayerMaster.LayerRef("Modules");

	// Token: 0x040021F0 RID: 8688
	public static LayerMaster.LayerRef backgroundLayer = new LayerMaster.LayerRef("Background");

	// Token: 0x040021F1 RID: 8689
	public static LayerMaster.LayerRef longShipModulesLayer = new LayerMaster.LayerRef("LongshipModules");

	// Token: 0x040021F2 RID: 8690
	public static LayerMaster.LayerRef houseLayer = new LayerMaster.LayerRef("Houses");

	// Token: 0x040021F3 RID: 8691
	public static LayerMaster.LayerRef debrisLayer = new LayerMaster.LayerRef("Debris");

	// Token: 0x040021F4 RID: 8692
	public static LayerMaster.LayerRef enBodies = new LayerMaster.LayerRef("EnBodies");

	// Token: 0x040021F5 RID: 8693
	public static LayerMaster.LayerRef viBodies = new LayerMaster.LayerRef("ViBodies");

	// Token: 0x040021F6 RID: 8694
	public static LayerMaster.LayerRef enShields = new LayerMaster.LayerRef("EnShields");

	// Token: 0x040021F7 RID: 8695
	public static LayerMaster.LayerRef viShields = new LayerMaster.LayerRef("ViShields");

	// Token: 0x040021F8 RID: 8696
	public static LayerMaster.LayerRef enProjectiles = new LayerMaster.LayerRef("EnProjectiles");

	// Token: 0x040021F9 RID: 8697
	public static LayerMaster.LayerRef viProjectiles = new LayerMaster.LayerRef("ViProjectiles");

	// Token: 0x040021FA RID: 8698
	public static LayerMaster.LayerRef landings = new LayerMaster.LayerRef("Landings");

	// Token: 0x040021FB RID: 8699
	public static LayerMaster.LayerRef mirror = new LayerMaster.LayerRef("Mirror");

	// Token: 0x040021FC RID: 8700
	public static LayerMaster.LayerRef blood = new LayerMaster.LayerRef("Blood");

	// Token: 0x040021FD RID: 8701
	public static LayerMaster.LayerRef arrowBlocker = new LayerMaster.LayerRef("ArrowBlocker");

	// Token: 0x040021FE RID: 8702
	public static LayerMask moduleMask = LayerMask.GetMask(new string[]
	{
		"Modules"
	});

	// Token: 0x040021FF RID: 8703
	public static LayerMask voxelMask = LayerMask.GetMask(new string[]
	{
		"Voxels"
	});

	// Token: 0x04002200 RID: 8704
	public static LayerMask landingsMask = LayerMask.GetMask(new string[]
	{
		"Landings"
	});

	// Token: 0x04002201 RID: 8705
	public static LayerMask houseMask = LayerMask.GetMask(new string[]
	{
		"Houses"
	});

	// Token: 0x04002202 RID: 8706
	public static LayerMask campaignMask = LayerMask.GetMask(new string[]
	{
		"Campaign"
	});

	// Token: 0x04002203 RID: 8707
	public static LayerMask longshipModulesMask = LayerMask.GetMask(new string[]
	{
		"LongshipModules"
	});

	// Token: 0x04002204 RID: 8708
	public static LayerMask arrowLow = LayerMask.GetMask(new string[]
	{
		"Voxels",
		"Houses",
		"ArrowBlocker"
	});

	// Token: 0x04002205 RID: 8709
	public static LayerMask arrowHigh = LayerMask.GetMask(new string[]
	{
		"Modules",
		"LongshipModules"
	});

	// Token: 0x04002206 RID: 8710
	public static LayerMask SquadSelection = LayerMask.GetMask(new string[]
	{
		"Modules",
		"EnBodies",
		"Longships",
		"Houses",
		"Standards"
	});

	// Token: 0x04002207 RID: 8711
	public static LayerMask cursorFloorMask = LayerMask.GetMask(new string[]
	{
		"Modules"
	});

	// Token: 0x04002208 RID: 8712
	public static LayerMask cursorShadowMask = LayerMask.GetMask(new string[]
	{
		"Modules",
		"Houses"
	});

	// Token: 0x04002209 RID: 8713
	public static LayerMask waterfallVisibilityMask = LayerMask.GetMask(new string[]
	{
		"Modules"
	});

	// Token: 0x02000796 RID: 1942
	public class LayerRef
	{
		// Token: 0x0600321B RID: 12827 RVA: 0x000D4902 File Offset: 0x000D2D02
		public LayerRef(string name)
		{
			this.name = name;
		}

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x0600321C RID: 12828 RVA: 0x000D4911 File Offset: 0x000D2D11
		public int id
		{
			get
			{
				if (!this.hasId)
				{
					this._id = LayerMask.NameToLayer(this.name);
					this.hasId = true;
				}
				return this._id;
			}
		}

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x0600321D RID: 12829 RVA: 0x000D493C File Offset: 0x000D2D3C
		public LayerMask mask
		{
			get
			{
				if (!this.hasMask)
				{
					this._mask = LayerMask.GetMask(new string[]
					{
						this.name
					});
					this.hasMask = true;
				}
				return this._mask;
			}
		}

		// Token: 0x0400220A RID: 8714
		private string name;

		// Token: 0x0400220B RID: 8715
		private bool hasId;

		// Token: 0x0400220C RID: 8716
		private int _id;

		// Token: 0x0400220D RID: 8717
		private bool hasMask;

		// Token: 0x0400220E RID: 8718
		private LayerMask _mask;
	}
}
