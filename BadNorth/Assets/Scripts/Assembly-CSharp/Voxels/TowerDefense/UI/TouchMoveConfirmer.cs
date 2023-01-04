using System;
using RTM.Pools;
using UnityEngine;
using UnityEngine.UI;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x0200055E RID: 1374
	public class TouchMoveConfirmer : MonoBehaviour, IslandUIManager.IAwake
	{
		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x060023C6 RID: 9158 RVA: 0x0006F510 File Offset: 0x0006D910
		public EnglishSquad selectedSquad
		{
			get
			{
				return this._selectedSquad;
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x060023C7 RID: 9159 RVA: 0x0006F518 File Offset: 0x0006D918
		// (set) Token: 0x060023C8 RID: 9160 RVA: 0x0006F520 File Offset: 0x0006D920
		public EnglishSquad altSquad
		{
			get
			{
				return this._selectedSquad;
			}
			set
			{
				if (this._altSquad == value)
				{
					return;
				}
				this._altSquad = value;
				if (this._altSquad)
				{
					this.selectGraphic.color = this._altSquad.hero.color;
					this.swapGrahic.color = this._altSquad.hero.color;
					this.selectButton.SetActive(true);
					this.swapButton.SetActive(true);
				}
				else
				{
					this.selectButton.SetActive(false);
					this.swapButton.SetActive(false);
				}
			}
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x060023C9 RID: 9161 RVA: 0x0006F5C1 File Offset: 0x0006D9C1
		public NavSpot navSpot
		{
			get
			{
				return this._navSpot;
			}
		}

		// Token: 0x060023CA RID: 9162 RVA: 0x0006F5CC File Offset: 0x0006D9CC
		void IslandUIManager.IAwake.OnAwake(IslandUIManager manager)
		{
			this.activationState = manager.gameplayManager.states.touchMoveConfirm;
			this.moveGraphic = this.moveButton.GetComponentInChildren<Graphic>();
			this.selectGraphic = this.selectButton.GetComponentInChildren<Graphic>();
			this.swapGrahic = this.swapButton.GetComponentInChildren<Graphic>();
			this.selectButton.gameObject.SetActive(false);
			this.swapButton.gameObject.SetActive(false);
			manager.gameplayManager.squadSelector.onSquadSelectionChanged += this.OnSquadSelectionChanged;
			this.navSpotHighlights = manager.gameplayManager.navSpotPoolManager.GetPool(this.navSpotPrefab);
			this.activationState.OnDeactivate += delegate()
			{
				this.DisplayNavSpotMarker(null);
			};
		}

		// Token: 0x060023CB RID: 9163 RVA: 0x0006F694 File Offset: 0x0006DA94
		public void Activate(EnglishSquad selected, NavSpot target, Vector3 hitPosition)
		{
			this._selectedSquad = selected;
			this._navSpot = target;
			this.displayPosition = hitPosition;
			this.moveGraphic.color = selected.hero.color;
			ConfirmButton.SetCurrent(this);
			this.activationState.SetActive(true);
			this.DisplayNavSpotMarker(target);
			this.screenSpaceOffset = Vector3.zero;
			FabricWrapper.PostEvent("UI/InGame/UnitMarkPlace");
		}

		// Token: 0x060023CC RID: 9164 RVA: 0x0006F6FC File Offset: 0x0006DAFC
		private void DisplayNavSpotMarker(NavSpot target)
		{
			if (this.highlight)
			{
				this.highlight.SetVisible(false);
			}
			if (target)
			{
				this.highlight = this.navSpotHighlights.GetInstance();
				this.highlight.Setup(target);
				this.highlight.SetVisible(true);
			}
		}

		// Token: 0x060023CD RID: 9165 RVA: 0x0006F75C File Offset: 0x0006DB5C
		private void OnEnable()
		{
			this.camRotation = Singleton<LevelCamera>.instance.transform.rotation.eulerAngles.y;
		}

		// Token: 0x060023CE RID: 9166 RVA: 0x0006F78E File Offset: 0x0006DB8E
		private void OnSquadSelectionChanged(EnglishSquad squad)
		{
			if (squad == null || squad != this._selectedSquad)
			{
				this._selectedSquad = null;
				this._navSpot = null;
				this.activationState.SetActive(false);
			}
		}

		// Token: 0x060023CF RID: 9167 RVA: 0x0006F7C7 File Offset: 0x0006DBC7
		private void LateUpdate()
		{
			if (ConfirmButton.currentConfirmMono != this)
			{
				this.activationState.SetActive(false);
			}
			else
			{
				this.UpdateDisplay();
			}
		}

		// Token: 0x060023D0 RID: 9168 RVA: 0x0006F7F0 File Offset: 0x0006DBF0
		private void UpdateDisplay()
		{
			float target = this.camRotation;
			this.camRotation = Singleton<LevelCamera>.instance.transform.rotation.eulerAngles.y;
			float num = Mathf.Abs(Mathf.DeltaAngle(this.camRotation, target));
			Vector3 b = base.transform.position - this.targetPos.position;
			this.screenSpaceOffset = Vector3.Lerp(this.screenSpaceOffset, b, num * 0.05f);
			this.displayPosition = Vector3.Lerp(this.displayPosition, this._navSpot.transform.position, num * 0.05f);
			Vector3 a = Singleton<LevelCamera>.instance.cameraRef.WorldToViewportPoint(this.displayPosition);
			a.x *= (float)Screen.width;
			a.y *= (float)Screen.height;
			base.transform.position = a + this.screenSpaceOffset;
			EnglishSquad altSquad = null;
			if (this.navSpot.isOccupied)
			{
				NavSpotController occupant = this.navSpot.occupant;
				altSquad = ((!occupant) ? null : occupant.enSquad);
			}
			this.altSquad = altSquad;
		}

		// Token: 0x060023D1 RID: 9169 RVA: 0x0006F934 File Offset: 0x0006DD34
		public void DoMove()
		{
			SquadMover.MoveTo(this.selectedSquad, this.navSpot);
		}

		// Token: 0x060023D2 RID: 9170 RVA: 0x0006F948 File Offset: 0x0006DD48
		public void DoSelect()
		{
			if (this._navSpot.isOccupied)
			{
				NavSpotController occupant = this._navSpot.occupant;
				if (occupant && occupant.enSquad)
				{
					Singleton<SquadSelector>.instance.SelectSquad(occupant.enSquad, false);
					this.activationState.SetActive(false);
				}
			}
		}

		// Token: 0x060023D3 RID: 9171 RVA: 0x0006F9A9 File Offset: 0x0006DDA9
		public void DoSwap()
		{
			if (this.navSpot.isOccupied)
			{
				SquadMover.SwapWith(this.selectedSquad, this.navSpot.occupant);
			}
		}

		// Token: 0x0400165D RID: 5725
		[SerializeField]
		public GameObject moveButton;

		// Token: 0x0400165E RID: 5726
		[SerializeField]
		public GameObject selectButton;

		// Token: 0x0400165F RID: 5727
		[SerializeField]
		public GameObject swapButton;

		// Token: 0x04001660 RID: 5728
		[SerializeField]
		public Transform targetPos;

		// Token: 0x04001661 RID: 5729
		[SerializeField]
		public TargetNavSpot navSpotPrefab;

		// Token: 0x04001662 RID: 5730
		private Graphic moveGraphic;

		// Token: 0x04001663 RID: 5731
		private Graphic selectGraphic;

		// Token: 0x04001664 RID: 5732
		private Graphic swapGrahic;

		// Token: 0x04001665 RID: 5733
		private EnglishSquad _selectedSquad;

		// Token: 0x04001666 RID: 5734
		private EnglishSquad _altSquad;

		// Token: 0x04001667 RID: 5735
		private NavSpot _navSpot;

		// Token: 0x04001668 RID: 5736
		private Vector3 displayPosition;

		// Token: 0x04001669 RID: 5737
		private float camRotation;

		// Token: 0x0400166A RID: 5738
		private State activationState;

		// Token: 0x0400166B RID: 5739
		private LocalPool<TargetNavSpot> navSpotHighlights;

		// Token: 0x0400166C RID: 5740
		private TargetNavSpot highlight;

		// Token: 0x0400166D RID: 5741
		private Vector3 screenSpaceOffset = Vector3.zero;
	}
}
