using System;
using RTM.OnScreenDebug;
using RTM.Utilities;
using UnityEngine;
using UnityEngine.EventSystems;
using Voxels.TowerDefense.UI;

namespace Voxels.TowerDefense
{
	// Token: 0x020007A2 RID: 1954
	public class Navigator : MonoBehaviour, IslandGameplayManager.IAwake, IslandGameplayManager.ISetupIsland, IslandGameplayManager.IWipeIsland, CursorManager.IPointerCursor, CursorManager.ICursor
	{
		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x06003287 RID: 12935 RVA: 0x000D6830 File Offset: 0x000D4C30
		private EnglishSquad selectedSquad
		{
			get
			{
				return this.squadSelector.selectedSquad;
			}
		}

		// Token: 0x06003288 RID: 12936 RVA: 0x000D683D File Offset: 0x000D4C3D
		private void ClearHoverSquad()
		{
			this.SetHoverSquad(null, false);
		}

		// Token: 0x06003289 RID: 12937 RVA: 0x000D6848 File Offset: 0x000D4C48
		private void SetHoverSquad(EnglishSquad squad, bool wantHoverEffect)
		{
			if (this.hoverSquad.Target && this.hoverSquad.Target != squad)
			{
				this.hoverSquad.Target.SetHover(false);
			}
			this.hoverSquad.Target = squad;
			if (this.hoverSquad.Target)
			{
				this.hoverSquad.Target.SetHover(wantHoverEffect);
			}
		}

		// Token: 0x0600328A RID: 12938 RVA: 0x000D68C3 File Offset: 0x000D4CC3
		void IslandGameplayManager.IAwake.OnAwake(IslandGameplayManager manager)
		{
			this.cursorManager = manager.cursorManager;
			this.navSpots = base.GetComponentInChildren<NavigatorNavSpotPool>();
		}

		// Token: 0x0600328B RID: 12939 RVA: 0x000D68DD File Offset: 0x000D4CDD
		void IslandGameplayManager.ISetupIsland.OnSetup(Island island)
		{
			this.navSpotter = island.navSpotter;
		}

		// Token: 0x0600328C RID: 12940 RVA: 0x000D68F0 File Offset: 0x000D4CF0
		void IslandGameplayManager.IWipeIsland.OnWipe(Island island)
		{
			this.navSpotter = null;
		}

		// Token: 0x0600328D RID: 12941 RVA: 0x000D68F9 File Offset: 0x000D4CF9
		private void OnEnable()
		{
			this.cursorManager.Add(this);
		}

		// Token: 0x0600328E RID: 12942 RVA: 0x000D6907 File Offset: 0x000D4D07
		private void OnDisable()
		{
			this.cursorManager.Remove(this);
		}

		// Token: 0x0600328F RID: 12943 RVA: 0x000D6915 File Offset: 0x000D4D15
		private void Update()
		{
		}

		// Token: 0x06003290 RID: 12944 RVA: 0x000D6918 File Offset: 0x000D4D18
		private void SelectPC(Vector2 screenPos, PointerEventData.InputButton button)
		{
			using (new ScopedProfiler("Navigator.SelectPC()", null))
			{
				if (button == PointerEventData.InputButton.Left)
				{
					EnglishSquad englishSquad = this.hoverSquad;
					if (englishSquad && englishSquad != this.selectedSquad)
					{
						this.squadSelector.SelectSquad(englishSquad, false);
					}
					else if (this.selectedSquad)
					{
						this.DeselectUnit();
					}
					else
					{
						this.FailedClick();
					}
				}
				else if (button == PointerEventData.InputButton.Right)
				{
					if (this.selectedSquad)
					{
						if (this.hoverNavSpot.Target)
						{
							SquadMover.MoveTo(this.selectedSquad, this.hoverNavSpot);
						}
						else
						{
							this.DeselectUnit();
						}
					}
					else
					{
						this.FailedClick();
					}
				}
			}
		}

		// Token: 0x06003291 RID: 12945 RVA: 0x000D6A14 File Offset: 0x000D4E14
		private void SelectTouch(Vector2 screenPos)
		{
			using (new ScopedProfiler("Navigator.SelectPC()", null))
			{
				if (this.selectedSquad)
				{
					if (this.selectedSquad.upgradeManager.IsBlockingMove())
					{
						this.DeselectUnit();
					}
					else
					{
						NavSpot navSpot = this.hoverNavSpot;
						if (navSpot && navSpot.occupant != this.selectedSquad.navSpotOccupant)
						{
							RaycastHit raycastHit;
							NavSpot.NavSpotCast(screenPos, out raycastHit);
							this.moveConfirmButtonContainer.Activate(this.selectedSquad, navSpot, raycastHit.point);
						}
						else
						{
							this.DeselectUnit();
						}
					}
				}
				else
				{
					EnglishSquad squadFromRaycast = this.squadSelector.GetSquadFromRaycast(screenPos);
					if (squadFromRaycast)
					{
						this.squadSelector.SelectSquad(squadFromRaycast, false);
					}
					else
					{
						this.FailedClick();
					}
				}
			}
		}

		// Token: 0x06003292 RID: 12946 RVA: 0x000D6B18 File Offset: 0x000D4F18
		private void DeselectUnit()
		{
			this.squadSelector.SelectSquad(null, false);
			FabricWrapper.PostEvent("UI/InGame/UnitDeselect");
		}

		// Token: 0x06003293 RID: 12947 RVA: 0x000D6B32 File Offset: 0x000D4F32
		private void FailedClick()
		{
			FabricWrapper.PostEvent("UI/InGame/Error");
		}

		// Token: 0x06003294 RID: 12948 RVA: 0x000D6B40 File Offset: 0x000D4F40
		private NavSpot GetNavSpotFromSquadSelector()
		{
			EnglishSquad selectedSquad = this.squadSelector.selectedSquad;
			if (!selectedSquad)
			{
				return null;
			}
			return selectedSquad.pather.target as NavSpot;
		}

		// Token: 0x06003295 RID: 12949 RVA: 0x000D6B76 File Offset: 0x000D4F76
		void CursorManager.ICursor.SetActive(bool active)
		{
			if (!active)
			{
				this.navSpots.SetHover(null);
				this.ClearHoverSquad();
			}
		}

		// Token: 0x06003296 RID: 12950 RVA: 0x000D6B90 File Offset: 0x000D4F90
		void CursorManager.IPointerCursor.OnButtonDown(PointerEventData.InputButton button, Vector2 screenPos)
		{
		}

		// Token: 0x06003297 RID: 12951 RVA: 0x000D6B94 File Offset: 0x000D4F94
		void CursorManager.IPointerCursor.OnButtonUp(PointerEventData.InputButton button, Vector2 screenPos)
		{
			switch (Profile.userSettings.cursorBehaviour)
			{
			case UserSettings.CursorBehaviour.TwoButton:
				this.SelectPC(screenPos, button);
				break;
			case UserSettings.CursorBehaviour.OneButton:
				this.SelectPC(screenPos, (!this.selectedSquad) ? PointerEventData.InputButton.Left : PointerEventData.InputButton.Right);
				break;
			case UserSettings.CursorBehaviour.Touch:
				this.SelectTouch(screenPos);
				break;
			default:
				Debug.LogWarningFormat("Unhandled Navigator.OnPointerClick() - platform '{0}' not handled", new object[]
				{
					Platform.platform
				});
				break;
			}
		}

		// Token: 0x06003298 RID: 12952 RVA: 0x000D6C24 File Offset: 0x000D5024
		void CursorManager.IPointerCursor.UpdateHoverTarget(PointerRationalizer.State state, Vector2 screenPos)
		{
			UserSettings.CursorBehaviour cursorBehaviour = Profile.userSettings.cursorBehaviour;
			if (cursorBehaviour == UserSettings.CursorBehaviour.Touch && state == PointerRationalizer.State.Hover)
			{
				state = PointerRationalizer.State.None;
			}
			if (state == PointerRationalizer.State.None || state == PointerRationalizer.State.Dragging)
			{
				this.ClearHoverSquad();
				this.hoverNavSpot.Target = null;
			}
			else
			{
				bool flag = this.selectedSquad && !this.selectedSquad.upgradeManager.IsBlockingMove();
				bool flag2 = !this.selectedSquad || (this.selectedSquad && cursorBehaviour == UserSettings.CursorBehaviour.TwoButton);
				if (flag2)
				{
					using (new ScopedProfiler("hoverSquad", null))
					{
						bool wantHoverEffect;
						this.SetHoverSquad(this.squadSelector.GetSquadFromRaycast(screenPos, out wantHoverEffect), wantHoverEffect);
					}
				}
				using (new ScopedProfiler("hoverNavSpot", null))
				{
					this.hoverNavSpot.Target = ((!flag) ? null : NavSpot.NavSpotCast(screenPos));
				}
			}
			this.navSpots.SetHover(this.hoverNavSpot);
		}

		// Token: 0x06003299 RID: 12953 RVA: 0x000D6D70 File Offset: 0x000D5170
		void CursorManager.IPointerCursor.OverrideCursorTexture(PointerRationalizer.State state, ref Texture2D texture, ref Vector2 position)
		{
		}

		// Token: 0x04002258 RID: 8792
		private DebugChannelGroup dbgGroup = new DebugChannelGroup("Navigator", EVerbosity.Quiet, 0);

		// Token: 0x04002259 RID: 8793
		[Header("Scene References")]
		[SerializeField]
		private TouchMoveConfirmer moveConfirmButtonContainer;

		// Token: 0x0400225A RID: 8794
		[SerializeField]
		public SquadSelector squadSelector;

		// Token: 0x0400225B RID: 8795
		private CursorManager cursorManager;

		// Token: 0x0400225C RID: 8796
		private NavigatorNavSpotPool navSpots;

		// Token: 0x0400225D RID: 8797
		private WeakReference<NavSpotter> navSpotter = new WeakReference<NavSpotter>(null);

		// Token: 0x0400225E RID: 8798
		private WeakReference<NavSpot> hoverNavSpot = new WeakReference<NavSpot>(null);

		// Token: 0x0400225F RID: 8799
		private WeakReference<EnglishSquad> hoverSquad = new WeakReference<EnglishSquad>(null);
	}
}
