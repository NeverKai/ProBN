using System;
using UnityEngine;

namespace Voxels.TowerDefense.Upgrades
{
	// Token: 0x0200084F RID: 2127
	internal class HouseTargetingJoystick : CursorManager.IJoystickCursor, CursorManager.ICursor
	{
		// Token: 0x060037A9 RID: 14249 RVA: 0x000F00AC File Offset: 0x000EE4AC
		public HouseTargetingJoystick(HouseTargetableAbility ability)
		{
			this.ability = ability;
		}

		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x060037AA RID: 14250 RVA: 0x000F00D0 File Offset: 0x000EE4D0
		// (set) Token: 0x060037AB RID: 14251 RVA: 0x000F00D8 File Offset: 0x000EE4D8
		private SquadReplenishLocation joystickTarget
		{
			get
			{
				return this._joystickTarget;
			}
			set
			{
				this._joystickTarget = value;
				this.ability.hoverTarget = ((!value || !value.available) ? null : value);
			}
		}

		// Token: 0x060037AC RID: 14252 RVA: 0x000F010C File Offset: 0x000EE50C
		private SquadReplenishLocation GetDefaultJoystickTarget()
		{
			if (this.joystickTarget)
			{
				return this.joystickTarget;
			}
			NavPos navPos = this.ability.squad.heroAgent.navPos;
			float num = float.MaxValue;
			SquadReplenishLocation result = null;
			foreach (SquadReplenishLocation squadReplenishLocation in this.ability.locations)
			{
				float num2 = ((IPathTarget)squadReplenishLocation).GetDistanceFrom(navPos);
				if (!squadReplenishLocation.available)
				{
					num2 *= 10000f;
				}
				if (num2 < num)
				{
					num = num2;
					result = squadReplenishLocation;
				}
			}
			return result;
		}

		// Token: 0x060037AD RID: 14253 RVA: 0x000F01C8 File Offset: 0x000EE5C8
		private void SetJoystickTarget(SquadReplenishLocation location)
		{
			if (location)
			{
				bool flag = !this.icon;
				if (flag)
				{
					this.icon = this.ability.wsCursorIcons.Target.GetInstance(this.ability.squad);
					if (this.icon.isNew)
					{
						this.icon.SetCurrentPos(location.GetWSCursorPos());
					}
				}
				this.icon.UpdateFloorPos(location.GetWSCursorPos(), false);
				FabricWrapper.PostEvent("UI/InGame/SnapBoat");
			}
			else
			{
				if (this.icon)
				{
					this.icon.Deactivate();
				}
				this.icon = null;
			}
			this.joystickTarget = location;
		}

		// Token: 0x060037AE RID: 14254 RVA: 0x000F0288 File Offset: 0x000EE688
		void CursorManager.ICursor.SetActive(bool active)
		{
			if (active)
			{
				this.SetJoystickTarget(this.GetDefaultJoystickTarget());
				this.joystickInvalid = true;
			}
			else
			{
				this.joystickTarget = null;
				if (this.icon)
				{
					this.icon.Deactivate();
					this.icon = null;
				}
			}
		}

		// Token: 0x060037AF RID: 14255 RVA: 0x000F02DC File Offset: 0x000EE6DC
		void CursorManager.IJoystickCursor.SetMoveInput(Vector2 input)
		{
			input = ((input.magnitude >= 0.5f) ? input : Vector2.zero);
			if (input == Vector2.zero)
			{
				this.joystickInvalid = false;
			}
			else if (!this.joystickInvalid)
			{
				this.joystickInvalid = true;
				if (!this.joystickTarget)
				{
					this.SetJoystickTarget(this.GetDefaultJoystickTarget());
				}
				else
				{
					Vector3 startPos = (!this.joystickTarget) ? this.ability.squad.heroAgent.transform.position : this.joystickTarget.transform.position;
					Vector3 topDownInputVector = Singleton<LevelCamera>.instance.cameraRef.GetTopDownInputVector(input);
					SquadReplenishLocation highestWeighted = this.joystickWeighter.GetHighestWeighted<SquadReplenishLocation>(startPos, this.ability.locations, topDownInputVector.GetZeroY().normalized);
					if (highestWeighted)
					{
						this.SetJoystickTarget(highestWeighted);
					}
					else
					{
						FabricWrapper.PostEvent(FabricID.uiError);
					}
				}
			}
			this.joystickTarget = this.joystickTarget;
		}

		// Token: 0x060037B0 RID: 14256 RVA: 0x000F03FC File Offset: 0x000EE7FC
		void CursorManager.IJoystickCursor.OnSelectButtonDown()
		{
			if (this.ability.IsAvailable() && this.joystickTarget && this.joystickTarget.available)
			{
				this.icon.SetTargetPos(this.joystickTarget.GetWSCursorPos() - Vector3.up * 0.5f, false);
				this.ability.SelectLocation(this.joystickTarget);
			}
			else
			{
				FabricWrapper.PostEvent(FabricID.uiError);
			}
		}

		// Token: 0x060037B1 RID: 14257 RVA: 0x000F0485 File Offset: 0x000EE885
		void CursorManager.IJoystickCursor.OnSelectButtonUp()
		{
		}

		// Token: 0x040025E2 RID: 9698
		private HouseTargetableAbility ability;

		// Token: 0x040025E3 RID: 9699
		private TopDownWeighter joystickWeighter = new TopDownWeighter(50f, float.MaxValue);

		// Token: 0x040025E4 RID: 9700
		private SquadReplenishLocation _joystickTarget;

		// Token: 0x040025E5 RID: 9701
		private WorldSpaceCursorIcon icon;

		// Token: 0x040025E6 RID: 9702
		private bool joystickInvalid;
	}
}
