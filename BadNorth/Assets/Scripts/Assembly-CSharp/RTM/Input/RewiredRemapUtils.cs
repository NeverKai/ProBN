using System;
using System.Collections;
using Rewired;

namespace RTM.Input
{
	// Token: 0x020004BE RID: 1214
	public static class RewiredRemapUtils
	{
		// Token: 0x06001EB2 RID: 7858 RVA: 0x00052534 File Offset: 0x00050934
		public static IEnumerator PollForKeyRoutine(Player player, ActionElementMap aem, int actionId)
		{
			bool gotKey = false;
			ElementAssignment elementAssignment = default(ElementAssignment);
			while (!gotKey)
			{
				yield return null;
				gotKey = RewiredRemapUtils.PollForKey(player, aem, actionId, out elementAssignment);
			}
			if (gotKey && aem != null)
			{
				aem.controllerMap.ReplaceOrCreateElementMap(elementAssignment);
			}
			yield break;
		}

		// Token: 0x06001EB3 RID: 7859 RVA: 0x00052560 File Offset: 0x00050960
		public static bool PollForKey(Player player, ActionElementMap aem, int actionId, out ElementAssignment elementAssignment)
		{
			RewiredRemapUtils.ConflictType conflictType;
			return RewiredRemapUtils.PollForKey(player, aem, actionId, out elementAssignment, out conflictType);
		}

		// Token: 0x06001EB4 RID: 7860 RVA: 0x00052578 File Offset: 0x00050978
		public static bool PollForKey(Player player, ActionElementMap aem, int actionId, out ElementAssignment elementAssignment, out RewiredRemapUtils.ConflictType conflictType)
		{
			ControllerPollingInfo controllerPollingInfo = ReInput.controllers.Keyboard.PollForFirstKeyDown();
			if (aem != null && controllerPollingInfo.success)
			{
				Pole axisContribution = aem.axisContribution;
				ElementAssignment elementAssignment2 = new ElementAssignment(ControllerType.Keyboard, controllerPollingInfo.elementType, controllerPollingInfo.elementIdentifierId, aem.axisRange, controllerPollingInfo.keyboardKey, ModifierKeyFlags.None, actionId, axisContribution, false, (aem == null) ? -1 : aem.id);
				conflictType = RewiredRemapUtils.HasConflict(player, aem, elementAssignment2);
				elementAssignment = elementAssignment2;
				return true;
			}
			conflictType = RewiredRemapUtils.ConflictType.None;
			elementAssignment = default(ElementAssignment);
			return false;
		}

		// Token: 0x06001EB5 RID: 7861 RVA: 0x00052608 File Offset: 0x00050A08
		public static RewiredRemapUtils.ConflictType HasConflict(Player player, ActionElementMap aem)
		{
			return RewiredRemapUtils.HasConflict(player, new ElementAssignmentConflictCheck
			{
				actionId = aem.actionId,
				axisContribution = aem.axisContribution,
				axisRange = aem.axisRange,
				controllerId = aem.controllerMap.controllerId,
				controllerType = aem.controllerMap.controllerType,
				elementAssignmentType = ElementAssignmentType.KeyboardKey,
				elementIdentifierId = aem.elementIdentifierId,
				elementMapId = aem.id,
				invert = aem.invert,
				keyboardKey = aem.keyCode,
				modifierKeyFlags = aem.modifierKeyFlags,
				playerId = player.id
			}, aem.controllerMap);
		}

		// Token: 0x06001EB6 RID: 7862 RVA: 0x000526CC File Offset: 0x00050ACC
		public static RewiredRemapUtils.ConflictType HasConflict(Player player, ActionElementMap aem, ElementAssignment assign)
		{
			ControllerMap controllerMap = aem.controllerMap;
			ElementAssignmentConflictCheck conflictCheck = assign.ToElementAssignmentConflictCheck();
			conflictCheck.playerId = player.id;
			conflictCheck.controllerType = controllerMap.controllerType;
			conflictCheck.controllerId = 0;
			conflictCheck.controllerMapId = controllerMap.id;
			conflictCheck.controllerMapCategoryId = controllerMap.categoryId;
			conflictCheck.elementMapId = assign.elementMapId;
			return RewiredRemapUtils.HasConflict(player, conflictCheck, controllerMap);
		}

		// Token: 0x06001EB7 RID: 7863 RVA: 0x0005273C File Offset: 0x00050B3C
		public static RewiredRemapUtils.ConflictType HasConflict(Player player, ElementAssignmentConflictCheck conflictCheck, ControllerMap controllerMap)
		{
			RewiredRemapUtils.ConflictType conflictType = RewiredRemapUtils.ConflictType.None;
			foreach (ElementAssignmentConflictInfo elementAssignmentConflictInfo in player.controllers.conflictChecking.ElementAssignmentConflicts(conflictCheck, true))
			{
				if (elementAssignmentConflictInfo.controllerMap == controllerMap)
				{
					conflictType |= RewiredRemapUtils.ConflictType.Soft;
				}
				else
				{
					conflictType |= RewiredRemapUtils.ConflictType.Hard;
				}
			}
			return conflictType;
		}

		// Token: 0x06001EB8 RID: 7864 RVA: 0x000527B8 File Offset: 0x00050BB8
		public static ActionElementMap GetActionElementMap(Player player, int actionId, Pole pole)
		{
			InputAction action = ReInput.mapping.GetAction(actionId);
			foreach (KeyboardMap keyboardMap in player.controllers.maps.GetAllMaps<KeyboardMap>())
			{
				ActionElementMap[] elementMapsWithAction = keyboardMap.GetElementMapsWithAction(actionId);
				int i = 0;
				while (i < elementMapsWithAction.Length)
				{
					ActionElementMap actionElementMap = elementMapsWithAction[i];
					if (action.type == InputActionType.Axis && pole == Pole.Negative)
					{
						if (actionElementMap.axisContribution == Pole.Negative)
						{
							goto IL_7E;
						}
					}
					else if (actionElementMap.axisContribution != Pole.Negative)
					{
						goto IL_7E;
					}
					i++;
					continue;
					IL_7E:
					return actionElementMap;
				}
			}
			return null;
		}

		// Token: 0x06001EB9 RID: 7865 RVA: 0x00052890 File Offset: 0x00050C90
		public static void SetDefaults(Player player)
		{
			player.controllers.maps.LoadDefaultMaps(ControllerType.Joystick);
			player.controllers.maps.LoadDefaultMaps(ControllerType.Keyboard);
			player.controllers.maps.LoadDefaultMaps(ControllerType.Mouse);
		}

		// Token: 0x020004BF RID: 1215
		public enum ConflictType
		{
			// Token: 0x0400131C RID: 4892
			None,
			// Token: 0x0400131D RID: 4893
			Soft,
			// Token: 0x0400131E RID: 4894
			Hard
		}
	}
}
