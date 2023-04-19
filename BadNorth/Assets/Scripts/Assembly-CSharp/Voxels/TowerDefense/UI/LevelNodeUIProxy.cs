using RTM.Pools;
using RTM.UISystem;
using RTM.Utilities;
using UnityEngine;
using Voxels.TowerDefense.CampaignGeneration;
using Voxels.TowerDefense.ScriptAnimations;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x020008B2 RID: 2226
	public class LevelNodeUIProxy : MonoBehaviour, IPoolable
	{
		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x06003A69 RID: 14953 RVA: 0x00102D23 File Offset: 0x00101123
		public LevelNode levelNode
		{
			get
			{
				return this._levelNode;
			}
		}

		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x06003A6A RID: 14954 RVA: 0x00102D30 File Offset: 0x00101130
		// (set) Token: 0x06003A6B RID: 14955 RVA: 0x00102D38 File Offset: 0x00101138
		public UINavigable navigable { get; private set; }

		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x06003A6C RID: 14956 RVA: 0x00102D41 File Offset: 0x00101141
		// (set) Token: 0x06003A6D RID: 14957 RVA: 0x00102D49 File Offset: 0x00101149
		public UIClickable clickable { get; private set; }

		// Token: 0x06003A6E RID: 14958 RVA: 0x00102D54 File Offset: 0x00101154
		private void MaybeInit()
		{
			if (this.navigable)
			{
				return;
			}
			this.navigable = base.GetComponent<UINavigable>();
			this.clickable = base.GetComponent<UIClickable>();
			this.clickable.onStateChanged += this.OnStateChanged;
		}

		// Token: 0x06003A6F RID: 14959 RVA: 0x00102DA4 File Offset: 0x001011A4
		private void OnStateChanged(UIInteractable.State state)
		{
			LevelVisuals levelVisuals = (!this.levelNode) ? null : this.levelNode.levelVisuals;
			if (levelVisuals)
			{
				levelVisuals.SetUIState(state);
			}
			if (state == UIInteractable.State.PointerButtonDown)
			{
				this.mapUI.cameraController.SetVelocity(Vector2.zero);
			}
		}

		// Token: 0x06003A70 RID: 14960 RVA: 0x00102E04 File Offset: 0x00101204
		public void Setup(CampaignMapUI mapUI, LevelNode levelNode, float scale)
		{
			this.MaybeInit();
			this.mapUI = mapUI;
			this._levelNode.Target = levelNode;
			AnimatedState cloudState = levelNode.levelVisuals.cloudState;
			cloudState.OnChange = (Action<bool>)Delegate.Combine(cloudState.OnChange, new Action<bool>(delegate(bool x)
			{
				base.gameObject.SetActive(x);
			}));
			base.gameObject.SetActive(levelNode.levelVisuals.cloudState.active);
			base.transform.localScale = Vector3.one * scale;
		}

		// Token: 0x06003A71 RID: 14961 RVA: 0x00102E88 File Offset: 0x00101288
		public void HandleClick()
		{
			this.mapUI.AnyMapClick();
			LevelNode node = this._levelNode.Target;
			Campaign campaign = node.campaign;
			if (node.IsAvailable() && !campaign.trialOver && !campaign.campaignSave.gameOver)
			{
				if (this.levelNode.levelState.checkpointState == LevelState.CheckpointState.Current)
				{
					this.mapUI.ReloadCheckpoint(true, 0f);
				}
				else if (!campaign.heroesAvaliable.active)
				{
					this.mapUI.NoCommandsAvailablePrompt();
				}
				else if (node.IsPlayed())
				{
					ModalOverlay.GetInstance().Initialize("UI/CAMPAIGN/REPLAY/TITLE", "UI/CAMPAIGN/REPLAY/DESCRIPTION", true).AddButton("UI/COMMON/CONTINUE", delegate()
					{
						this.PlayLevel(node);
						return true;
					}, null, null);
				}
				else
				{
					this.PlayLevel(node);
				}
			}
			else
			{
				FabricWrapper.PostEvent(FabricID.uiError);
			}
		}

		// Token: 0x06003A72 RID: 14962 RVA: 0x00102FA4 File Offset: 0x001013A4
		private void PlayLevel(LevelNode node)
		{
			IslandProxy islandProxy = node.islandProxy;
			islandProxy.preparingToPlay = true;
			islandProxy.UpdateBuildPriority();
			if (islandProxy.state == IslandProxy.State.None)
			{
				islandProxy.GenerateIsland();
			}
			node.Play();
			FabricWrapper.PostEvent("UI/InGame/Resume");
		}

		// Token: 0x06003A73 RID: 14963 RVA: 0x00102FE7 File Offset: 0x001013E7
		void IPoolable.SetPool<T>(LocalPool<T> pool)
		{
		}

		// Token: 0x06003A74 RID: 14964 RVA: 0x00102FE9 File Offset: 0x001013E9
		void IPoolable.OnRemoved()
		{
		}

		// Token: 0x06003A75 RID: 14965 RVA: 0x00102FEB File Offset: 0x001013EB
		void IPoolable.OnReturned()
		{
			base.gameObject.SetActive(false);
			this._levelNode.Target = null;
			this.mapUI = null;
		}

		// Token: 0x0400287B RID: 10363
		public CampaignMapUI mapUI;

		// Token: 0x0400287C RID: 10364
		private WeakReference<LevelNode> _levelNode = new WeakReference<LevelNode>(null);
	}
}
