using System;
using RTM.Utilities;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020006C5 RID: 1733
	public abstract class LevelCameraComponent : MonoBehaviour, IslandGameplayManager.IAwake, IslandGameplayManager.ISetupIsland, IslandGameplayManager.IWipeIsland
	{
		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x06002D03 RID: 11523 RVA: 0x000A4BE4 File Offset: 0x000A2FE4
		protected ILevelCamera levelCamera
		{
			get
			{
				return this._levelCamera;
			}
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x06002D04 RID: 11524 RVA: 0x000A4BEC File Offset: 0x000A2FEC
		public Island island
		{
			get
			{
				return this._island;
			}
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06002D05 RID: 11525 RVA: 0x000A4BF9 File Offset: 0x000A2FF9
		protected float deltaTime
		{
			get
			{
				return Mathf.Min(0.05f, Mathf.Lerp(Time.unscaledDeltaTime, Time.deltaTime, this.timeScaleEffect));
			}
		}

		// Token: 0x06002D06 RID: 11526 RVA: 0x000A4C1A File Offset: 0x000A301A
		void IslandGameplayManager.IAwake.OnAwake(IslandGameplayManager manager)
		{
			this._levelCamera = base.GetComponent<ILevelCamera>();
			this.pauser = manager.levelPauser;
		}

		// Token: 0x06002D07 RID: 11527 RVA: 0x000A4C34 File Offset: 0x000A3034
		void IslandGameplayManager.ISetupIsland.OnSetup(Island island)
		{
			this._island.Target = island;
			this.ResetLevelView();
		}

		// Token: 0x06002D08 RID: 11528 RVA: 0x000A4C48 File Offset: 0x000A3048
		void IslandGameplayManager.IWipeIsland.OnWipe(Island island)
		{
			this._island.Target = null;
		}

		// Token: 0x06002D09 RID: 11529 RVA: 0x000A4C56 File Offset: 0x000A3056
		private void Update()
		{
			if (this.CanUpdate())
			{
				this.UpdateInternal();
			}
		}

		// Token: 0x06002D0A RID: 11530 RVA: 0x000A4C69 File Offset: 0x000A3069
		private bool CanUpdate()
		{
			return this.ignorePause || !this.pauser.isPaused;
		}

		// Token: 0x06002D0B RID: 11531
		protected abstract void UpdateInternal();

		// Token: 0x06002D0C RID: 11532 RVA: 0x000A4C87 File Offset: 0x000A3087
		protected virtual void ResetLevelView()
		{
		}

		// Token: 0x04001D85 RID: 7557
		[SerializeField]
		private bool ignorePause;

		// Token: 0x04001D86 RID: 7558
		[SerializeField]
		[Range(0f, 1f)]
		private float timeScaleEffect;

		// Token: 0x04001D87 RID: 7559
		private ILevelCamera _levelCamera;

		// Token: 0x04001D88 RID: 7560
		private WeakReference<Island> _island = new WeakReference<Island>(null);

		// Token: 0x04001D89 RID: 7561
		private LevelPauser pauser;
	}
}
