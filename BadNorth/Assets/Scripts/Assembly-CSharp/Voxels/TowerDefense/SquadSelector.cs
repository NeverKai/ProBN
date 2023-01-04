using System;
using System.Collections.Generic;
using System.Diagnostics;
using ReflexCLI.Attributes;
using Rewired;
using RTM.Input;
using RTM.OnScreenDebug;
using RTM.Utilities;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x02000740 RID: 1856
	[ConsoleCommandClassCustomizer("SquadSelector")]
	public class SquadSelector : Singleton<SquadSelector>, IslandGameplayManager.IAwake, IslandGameplayManager.ISetupIsland, IslandGameplayManager.IWipeIsland
	{
		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x0600306E RID: 12398 RVA: 0x000C5A4A File Offset: 0x000C3E4A
		public EnglishSquad selectedSquad
		{
			get
			{
				return (!this.selectionState.active || !this._selectedSquad.Target) ? null : this._selectedSquad.Target;
			}
		}

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x0600306F RID: 12399 RVA: 0x000C5A82 File Offset: 0x000C3E82
		public int selectedSquadIdx
		{
			get
			{
				return (!this.selectedSquad) ? -1 : this.GetSquadIdx(this.selectedSquad);
			}
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x06003070 RID: 12400 RVA: 0x000C5AA6 File Offset: 0x000C3EA6
		public List<Squad> squads
		{
			get
			{
				return this.island.english.livingSquads;
			}
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06003071 RID: 12401 RVA: 0x000C5AB8 File Offset: 0x000C3EB8
		private Island island
		{
			get
			{
				return this._island;
			}
		}

		// Token: 0x1400009C RID: 156
		// (add) Token: 0x06003072 RID: 12402 RVA: 0x000C5AC8 File Offset: 0x000C3EC8
		// (remove) Token: 0x06003073 RID: 12403 RVA: 0x000C5B00 File Offset: 0x000C3F00
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event SquadSelector.OnSquadSelectionChanged onSquadSelectionChanged = delegate(EnglishSquad A_0)
		{
		};

		// Token: 0x06003074 RID: 12404 RVA: 0x000C5B36 File Offset: 0x000C3F36
		void IslandGameplayManager.IAwake.OnAwake(IslandGameplayManager manager)
		{
			Singleton<SquadSelector>._instance = this;
			this.selectionState = manager.states.squadSelected;
			this.selectionState.OnChange += this.OnSelectStateChange;
		}

		// Token: 0x06003075 RID: 12405 RVA: 0x000C5B66 File Offset: 0x000C3F66
		void IslandGameplayManager.ISetupIsland.OnSetup(Island island)
		{
			this._island.Target = island;
		}

		// Token: 0x06003076 RID: 12406 RVA: 0x000C5B74 File Offset: 0x000C3F74
		void IslandGameplayManager.IWipeIsland.OnWipe(Island island)
		{
			this._island.Target = null;
			this._selectedSquad.Target = null;
		}

		// Token: 0x06003077 RID: 12407 RVA: 0x000C5B8E File Offset: 0x000C3F8E
		[ConsoleCommand("")]
		public void SelectNext()
		{
			this.CycleSelection(1);
		}

		// Token: 0x06003078 RID: 12408 RVA: 0x000C5B97 File Offset: 0x000C3F97
		[ConsoleCommand("")]
		public void SelectPrevious()
		{
			this.CycleSelection(-1);
		}

		// Token: 0x06003079 RID: 12409 RVA: 0x000C5BA0 File Offset: 0x000C3FA0
		[ConsoleCommand("")]
		private void CycleSelection(int change)
		{
			if ((!this._selectedSquad.Target || !this._selectedSquad.Target.alive || !this._selectedSquad.Target.selectable) && this.SelectSquad(0))
			{
				return;
			}
			if (!this.selectedSquad && this._selectedSquad.Target.alive && this._selectedSquad.Target.selectable)
			{
				this.SelectSquad(this._selectedSquad, true);
			}
			else
			{
				int count = this.squads.Count;
				int num = this.GetSquadIdx(this._selectedSquad);
				for (int i = 1; i < count; i++)
				{
					num = (num + change + count) % count;
					EnglishSquad englishSquad = this.squads[num] as EnglishSquad;
					if (englishSquad && englishSquad.selectable)
					{
						this.SelectSquad(englishSquad, true);
						return;
					}
				}
			}
			FabricWrapper.PostEvent(FabricID.uiError);
		}

		// Token: 0x0600307A RID: 12410 RVA: 0x000C5CC0 File Offset: 0x000C40C0
		private int GetSquadIdx(EnglishSquad squad)
		{
			int i = 0;
			int count = this.squads.Count;
			while (i < count)
			{
				if (this.squads[i] == squad)
				{
					return i;
				}
				i++;
			}
			return -1;
		}

		// Token: 0x0600307B RID: 12411 RVA: 0x000C5D08 File Offset: 0x000C4108
		[ConsoleCommand("SelectByIndex")]
		public bool SelectSquad(int idx)
		{
			if (this.squads.IsValidIndex(idx))
			{
				this.SelectSquad(this.squads[idx] as EnglishSquad, true);
				return this.selectedSquad == this.squads[idx];
			}
			FabricWrapper.PostEvent(FabricID.uiError);
			return false;
		}

		// Token: 0x0600307C RID: 12412 RVA: 0x000C5D64 File Offset: 0x000C4164
		[ConsoleCommand("")]
		public void SelectSquad(EnglishSquad newSelection, bool showGodray = false)
		{
			using ("SelectSquad")
			{
				EnglishSquad selectedSquad = this.selectedSquad;
				if (!(newSelection == selectedSquad) && (!newSelection || newSelection.selectable))
				{
					if (selectedSquad)
					{
						selectedSquad.ShowSelection(false, false);
					}
					if (newSelection)
					{
						using ("ShowSelection")
						{
							this._selectedSquad.Target = newSelection;
							this._selectedSquad.Target.ShowSelection(true, showGodray);
						}
						using ("stateActivate")
						{
							this.selectionState.SetActive(true);
						}
						using ("Sfx")
						{
							bool flag = !InputHelpers.ControllerTypeIs(ControllerType.Mouse) && selectedSquad;
							FabricEventReference eventId = (!flag) ? this.primarySelectSound : this.secondarySelectSound;
							FabricWrapper.PostEvent(eventId);
						}
					}
					else
					{
						this.selectionState.SetActive(false);
					}
					using ("onSquadSelectionChangedDelegate")
					{
						this.onSquadSelectionChanged(newSelection);
					}
				}
			}
		}

		// Token: 0x0600307D RID: 12413 RVA: 0x000C5F50 File Offset: 0x000C4350
		private void LateUpdate()
		{
			if (this.selectedSquad && !this.selectedSquad.selectable)
			{
				this.SelectSquad(null, false);
			}
			if (this.selectedSquad && Time.unscaledTime - this.lastSelectSoundTime > this.selectionSoundRepeatTime && !CinematicCamera.isActive)
			{
				FabricWrapper.PostEvent(this.selectionRepeatSoundEventId);
				this.lastSelectSoundTime = Time.unscaledTime;
			}
		}

		// Token: 0x0600307E RID: 12414 RVA: 0x000C5FD0 File Offset: 0x000C43D0
		public EnglishSquad GetSquadFromRaycast(Vector2 screenPos)
		{
			bool flag;
			return this.GetSquadFromRaycast(screenPos, out flag);
		}

		// Token: 0x0600307F RID: 12415 RVA: 0x000C5FE8 File Offset: 0x000C43E8
		public EnglishSquad GetSquadFromRaycast(Vector2 screenPos, out bool wantsHoverEffect)
		{
			RaycastHit raycastHit;
			return this.GetSquadFromRaycast(screenPos, out raycastHit, out wantsHoverEffect);
		}

		// Token: 0x06003080 RID: 12416 RVA: 0x000C6000 File Offset: 0x000C4400
		public EnglishSquad GetSquadFromRaycast(Vector2 screenPos, out RaycastHit hit, out bool wantsHoverEffect)
		{
			screenPos.x /= (float)Screen.width;
			screenPos.y /= (float)Screen.height;
			Ray ray = Singleton<LevelCamera>.instance.cameraRef.ViewportPointToRay(screenPos);
			Physics.Raycast(ray, out hit, float.PositiveInfinity, LayerMaster.SquadSelection);
			return this.GetSquadFromRaycast(hit, out wantsHoverEffect);
		}

		// Token: 0x06003081 RID: 12417 RVA: 0x000C6070 File Offset: 0x000C4470
		public EnglishSquad GetSquadFromRaycast(RaycastHit hit, out bool wantsHoverEffect)
		{
			wantsHoverEffect = false;
			if (!hit.collider)
			{
				return null;
			}
			EnglishSquad x = null;
			ISquadSelector squadSelector = null;
			using (new ScopedProfiler("Probably just editor only garbage GetComponent() Failing", null))
			{
				squadSelector = hit.collider.GetComponent<ISquadSelector>();
				x = ((squadSelector != null) ? squadSelector.GetSelectableSquad() : null);
			}
			EnglishSquad result = null;
			float num = 0f;
			for (int i = 0; i < this.squads.Count; i++)
			{
				using (new ScopedProfiler("GetSquadFromRaycast.2", null))
				{
					bool flag = false;
					EnglishSquad englishSquad = this.squads[i] as EnglishSquad;
					if (englishSquad.selectable && !englishSquad.dead)
					{
						float num2 = 0f;
						if (x == englishSquad)
						{
							flag |= squadSelector.wantsHoverEffect;
							num2 += 0.2f;
						}
						Bounds bounds = new Bounds(englishSquad.bounds.center, englishSquad.bounds.size + Vector3.one);
						if (bounds.Contains(hit.point))
						{
							num2 += 0.2f;
						}
						float num3 = Mathf.Clamp01(1f - (hit.point - englishSquad.heroAgent.wPos).magnitude);
						if (num3 > 0f)
						{
							flag = true;
						}
						num2 += num3;
						num2 += ((!englishSquad.navSpotOccupant.navSpot) ? 0f : Mathf.Clamp01(1f - (hit.point - englishSquad.navSpotOccupant.navSpot.navPos.wPos).magnitude));
						if (num2 > num)
						{
							result = englishSquad;
							num = num2;
							wantsHoverEffect = (flag && !englishSquad.isSelected);
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06003082 RID: 12418 RVA: 0x000C62C0 File Offset: 0x000C46C0
		private void OnSelectStateChange(bool squadSelected)
		{
			if (squadSelected)
			{
				TimeManager.RequestTimeScale(this, 0.1f);
				this.lastSelectSoundTime = Time.unscaledTime;
			}
			else
			{
				TimeManager.RemoveTimeScale(this);
				this.lastSelectSoundTime = float.MaxValue;
				if (this._selectedSquad)
				{
					this._selectedSquad.Target.ShowSelection(false, false);
				}
			}
		}

		// Token: 0x04002058 RID: 8280
		private DebugChannelGroup debugGroup = new DebugChannelGroup("Squad Selector", EVerbosity.Quiet, 0);

		// Token: 0x04002059 RID: 8281
		[Header("Audio")]
		[SerializeField]
		private FabricEventReference selectionRepeatSoundEventId = "Sfx/PauseCough";

		// Token: 0x0400205A RID: 8282
		[SerializeField]
		private FabricEventReference primarySelectSound = "UI/InGame/UnitSelect";

		// Token: 0x0400205B RID: 8283
		[SerializeField]
		private FabricEventReference secondarySelectSound = "UI/InGame/UnitSwitch";

		// Token: 0x0400205C RID: 8284
		[SerializeField]
		private float selectionSoundRepeatTime = 3f;

		// Token: 0x0400205D RID: 8285
		private float lastSelectSoundTime = float.MaxValue;

		// Token: 0x0400205E RID: 8286
		private WeakReference<EnglishSquad> _selectedSquad = new WeakReference<EnglishSquad>(null);

		// Token: 0x0400205F RID: 8287
		private State selectionState;

		// Token: 0x04002060 RID: 8288
		private WeakReference<Island> _island = new WeakReference<Island>(null);

		// Token: 0x02000741 RID: 1857
		// (Invoke) Token: 0x06003085 RID: 12421
		public delegate void OnSquadSelectionChanged(EnglishSquad squad);
	}
}
