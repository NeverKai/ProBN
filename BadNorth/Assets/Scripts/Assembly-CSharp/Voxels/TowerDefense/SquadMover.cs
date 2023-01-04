using System;
using System.Collections.Generic;

namespace Voxels.TowerDefense
{
	// Token: 0x0200073F RID: 1855
	public static class SquadMover
	{
		// Token: 0x0600306A RID: 12394 RVA: 0x000C57B4 File Offset: 0x000C3BB4
		public static void MoveTo(EnglishSquad squad, NavSpot newNavSpot)
		{
			if (!squad.navSpotOccupant.canMove)
			{
				return;
			}
			SquadMover.MoveToDirect(squad, newNavSpot, true);
			FabricWrapper.PostEvent(squad.squadTemplate.moveSound);
			FabricWrapper.PostEvent("UI/InGame/UnitMove");
			Singleton<SquadSelector>.instance.SelectSquad(null, false);
		}

		// Token: 0x0600306B RID: 12395 RVA: 0x000C5804 File Offset: 0x000C3C04
		public static void MoveToDirect(EnglishSquad squad, NavSpot newNavSpot, bool showPath = true)
		{
			NavSpotController navSpotOccupant = squad.navSpotOccupant;
			NavSpotController occupant = newNavSpot.occupant;
			if (occupant != null && occupant != navSpotOccupant)
			{
				NavSpot navSpot = navSpotOccupant.navSpot;
				IPathTarget pathTarget = navSpot;
				if (((IPathTarget)newNavSpot).GetDistanceFrom(navSpot.navPos) > 1.5f)
				{
					List<NavSpot> navSpots = squad.faction.island.navSpotter.navSpots;
					float num = float.MaxValue;
					for (int i = 0; i < navSpots.Count; i++)
					{
						NavSpot navSpot2 = navSpots[i];
						if (!(navSpot2.occupant != null) || !(navSpot2.occupant != navSpotOccupant))
						{
							float num2 = ((IPathTarget)newNavSpot).GetDistanceFrom(navSpot2.navPos);
							num2 += pathTarget.GetDistanceFrom(navSpot2.navPos) * 0.2f;
							num2 += ExtraMath.RemapValue(squad.faction.enemy.presence.SampleDistance(navSpot2.vert), 0f, 4f, 1f, 0f);
							if (num2 < num)
							{
								num = num2;
								navSpot = navSpot2;
							}
						}
					}
				}
				occupant.SetNavSpot(navSpot, true);
			}
			squad.navSpotOccupant.SetNavSpot(newNavSpot, showPath);
		}

		// Token: 0x0600306C RID: 12396 RVA: 0x000C5950 File Offset: 0x000C3D50
		public static void SwapWith(EnglishSquad squad, NavSpotController other)
		{
			NavSpotController navSpotOccupant = squad.navSpotOccupant;
			NavSpot navSpot = navSpotOccupant.navSpot;
			NavSpot navSpot2 = other.navSpot;
			other.SetNavSpot(navSpot, true);
			navSpotOccupant.SetNavSpot(navSpot2, true);
			FabricWrapper.PostEvent(squad.squadTemplate.swapSound);
			Singleton<SquadSelector>.instance.SelectSquad(null, false);
		}
	}
}
