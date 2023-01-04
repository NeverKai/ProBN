using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Rewired;
using RTM.OnScreenDebug;
using UnityEngine;

namespace RTM.Input
{
	// Token: 0x020004BA RID: 1210
	public static class RewiredHelpers
	{
		// Token: 0x06001EA7 RID: 7847 RVA: 0x00052274 File Offset: 0x00050674
		static RewiredHelpers()
		{
			if (RewiredHelpers.<>f__mg$cache0 == null)
			{
				RewiredHelpers.<>f__mg$cache0 = new Action<ControllerStatusChangedEventArgs>(RewiredHelpers.OnControllerConnected);
			}
			ReInput.ControllerConnectedEvent += RewiredHelpers.<>f__mg$cache0;
			if (RewiredHelpers.<>f__mg$cache1 == null)
			{
				RewiredHelpers.<>f__mg$cache1 = new Action<ControllerStatusChangedEventArgs>(RewiredHelpers.OnControllerDisconnectedEvent);
			}
			ReInput.ControllerDisconnectedEvent += RewiredHelpers.<>f__mg$cache1;
		}

		// Token: 0x06001EA8 RID: 7848 RVA: 0x000522E1 File Offset: 0x000506E1
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void init()
		{
		}

		// Token: 0x06001EA9 RID: 7849 RVA: 0x000522E3 File Offset: 0x000506E3
		private static void OnControllerConnected(ControllerStatusChangedEventArgs args)
		{
			RewiredHelpers.ApplyPendingSwaps(args.controllerType, args.controllerId);
		}

		// Token: 0x06001EAA RID: 7850 RVA: 0x000522F8 File Offset: 0x000506F8
		public static void ApplyPendingSwaps(ControllerType type, int id)
		{
			for (int i = RewiredHelpers.pendingSwaps.Count - 1; i >= 0; i--)
			{
				RewiredHelpers.ActionSwap swap = RewiredHelpers.pendingSwaps[i];
				if (ReInput.controllers.IsControllerAssignedToPlayer(type, id, swap.player.id))
				{
					IList<ControllerMap> maps = swap.player.controllers.maps.GetMaps(type, id);
					foreach (ControllerMap map in maps)
					{
						RewiredHelpers.SwapActions(swap, map);
					}
				}
			}
		}

		// Token: 0x06001EAB RID: 7851 RVA: 0x000523B0 File Offset: 0x000507B0
		private static void OnControllerDisconnectedEvent(ControllerStatusChangedEventArgs args)
		{
		}

		// Token: 0x06001EAC RID: 7852 RVA: 0x000523B4 File Offset: 0x000507B4
		public static void SwapActions(Player player, int actionA, int actionB)
		{
			RewiredHelpers.ActionSwap actionSwap = new RewiredHelpers.ActionSwap(player, actionA, actionB);
			RewiredHelpers.pendingSwaps.Add(actionSwap);
			IEnumerable<ControllerMap> allMaps = player.controllers.maps.GetAllMaps(ControllerType.Joystick);
			foreach (ControllerMap map in allMaps)
			{
				RewiredHelpers.SwapActions(actionSwap, map);
			}
		}

		// Token: 0x06001EAD RID: 7853 RVA: 0x00052430 File Offset: 0x00050830
		private static void SwapActions(RewiredHelpers.ActionSwap swap, ControllerMap map)
		{
			if (!swap.appliedToMaps.Contains(map.id) && map.ContainsAction(swap.actionA) && map.ContainsAction(swap.actionB))
			{
				ActionElementMap firstElementMapWithAction = map.GetFirstElementMapWithAction(swap.actionA);
				ActionElementMap firstElementMapWithAction2 = map.GetFirstElementMapWithAction(swap.actionB);
				ElementAssignment elementAssignment = new ElementAssignment(firstElementMapWithAction.elementIdentifierId, firstElementMapWithAction2.actionId, Pole.Positive, firstElementMapWithAction.id);
				ElementAssignment elementAssignment2 = new ElementAssignment(firstElementMapWithAction2.elementIdentifierId, firstElementMapWithAction.actionId, Pole.Positive, firstElementMapWithAction2.id);
				map.ReplaceOrCreateElementMap(elementAssignment);
				map.ReplaceOrCreateElementMap(elementAssignment2);
				swap.appliedToMaps.Add(map.id);
			}
		}

		// Token: 0x04001309 RID: 4873
		private static DebugChannelGroup dbgGroup = new DebugChannelGroup("RewiredHelpers", EVerbosity.Quiet, 100);

		// Token: 0x0400130A RID: 4874
		private static List<RewiredHelpers.ActionSwap> pendingSwaps = new List<RewiredHelpers.ActionSwap>();

		// Token: 0x0400130B RID: 4875
		[CompilerGenerated]
		private static Action<ControllerStatusChangedEventArgs> <>f__mg$cache0;

		// Token: 0x0400130C RID: 4876
		[CompilerGenerated]
		private static Action<ControllerStatusChangedEventArgs> <>f__mg$cache1;

		// Token: 0x020004BB RID: 1211
		public static class JoystickGuids
		{
			// Token: 0x0400130D RID: 4877
			public const string Xbox360 = "d74a350e-fe8b-4e9e-bbcd-efff16d34115";

			// Token: 0x0400130E RID: 4878
			public const string XboxOne = "19002688-7406-4f4a-8340-8d25335406c8";

			// Token: 0x0400130F RID: 4879
			public const string SwitchJCLeft = "3eb01142-da0e-4a86-8ae8-a15c2b1f2a04";

			// Token: 0x04001310 RID: 4880
			public const string SwitchJCRight = "605dc720-1b38-473d-a459-67d5857aa6ea";

			// Token: 0x04001311 RID: 4881
			public const string SwitchJCDual = "521b808c-0248-4526-bc10-f1d16ee76bf1";

			// Token: 0x04001312 RID: 4882
			public const string SwitchJCHandheld = "1fbdd13b-0795-4173-8a95-a2a75de9d204";

			// Token: 0x04001313 RID: 4883
			public const string SwitchJCPro = "7bf3154b-9db8-4d52-950f-cd0eed8a5819";

			// Token: 0x04001314 RID: 4884
			public const string DualShock2 = "c3ad3cad-c7cf-4ca8-8c2e-e3df8d9960bb";

			// Token: 0x04001315 RID: 4885
			public const string DualShock3 = "71dfe6c8-9e81-428f-a58e-c7e664b7fbed";

			// Token: 0x04001316 RID: 4886
			public const string DualShock4 = "cd9718bf-a87a-44bc-8716-60a0def28a9f";
		}

		// Token: 0x020004BC RID: 1212
		private struct ActionSwap
		{
			// Token: 0x06001EAE RID: 7854 RVA: 0x000524EF File Offset: 0x000508EF
			public ActionSwap(Player player, int actionA, int actionB)
			{
				this.player = player;
				this.actionA = actionA;
				this.actionB = actionB;
				this.appliedToMaps = new List<int>();
			}

			// Token: 0x04001317 RID: 4887
			public Player player;

			// Token: 0x04001318 RID: 4888
			public int actionA;

			// Token: 0x04001319 RID: 4889
			public int actionB;

			// Token: 0x0400131A RID: 4890
			public List<int> appliedToMaps;
		}
	}
}
