using Fabric;
using ReflexCLI.Attributes;
using RTM.Utilities;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x0200053D RID: 1341
	[ConsoleCommandClassCustomizer("Raid")]
	public class RaidDriver : MonoBehaviour, IslandGameplayManager.IAwake, IslandGameplayManager.ISetupIsland, IslandGameplayManager.IWipeIsland
	{
		// Token: 0x060022F2 RID: 8946 RVA: 0x0006784C File Offset: 0x00065C4C
		void IslandGameplayManager.IAwake.OnAwake(IslandGameplayManager manager)
		{
			this.notifications = manager.notificationManager;
			manager.states.Selectable.OnDeactivate += delegate()
			{
				FabricWrapper.PostEvent(this.startWave, EventAction.StopSound);
			};
		}

		// Token: 0x060022F3 RID: 8947 RVA: 0x00067876 File Offset: 0x00065C76
		void IslandGameplayManager.ISetupIsland.OnSetup(Island island)
		{
			this.raid.Target = island.raid;
			this.wantFirstWaveSound = false;
		}

		// Token: 0x060022F4 RID: 8948 RVA: 0x00067890 File Offset: 0x00065C90
		void IslandGameplayManager.IWipeIsland.OnWipe(Island island)
		{
			this.raid.Target = null;
			this.wantFirstWaveSound = false;
		}

		// Token: 0x060022F5 RID: 8949 RVA: 0x000678A8 File Offset: 0x00065CA8
		private void Update()
		{
			if (this.wantFirstWaveSound)
			{
				FabricWrapper.PostEvent(this.startWave);
				this.wantFirstWaveSound = false;
			}
			if (this.raid.Target.MaybeLaunchWaves())
			{
				int currentWaveIdx = this.raid.Target.GetCurrentWaveIdx();
				if (currentWaveIdx == 0)
				{
					this.wantFirstWaveSound = true;
				}
				else if (currentWaveIdx == this.raid.Target.finalWaveIdx)
				{
					FabricWrapper.PostEvent("Mus/LastWave");
					this.notifications.PostMessage("UI/GAMEPLAY/WAVES/FINAL", IslandUINotification.Priority.WaveIncoming, this.lastWaveNotificationTime);
				}
			}
		}

		// Token: 0x04001553 RID: 5459
		[SerializeField]
		private float notificationTime = 3.5f;

		// Token: 0x04001554 RID: 5460
		[SerializeField]
		private float firstWaveNotificationTime = 6f;

		// Token: 0x04001555 RID: 5461
		[SerializeField]
		private float lastWaveNotificationTime = 6f;

		// Token: 0x04001556 RID: 5462
		private IslandUINotificationManager notifications;

		// Token: 0x04001557 RID: 5463
		private WeakReference<Raid> raid = new WeakReference<Raid>(null);

		// Token: 0x04001558 RID: 5464
		private FabricEventReference startWave = "Mus/StartWave";

		// Token: 0x04001559 RID: 5465
		private bool wantFirstWaveSound;

		// Token: 0x0400155A RID: 5466
		private const IslandUINotification.Priority prio = IslandUINotification.Priority.WaveIncoming;
	}
}
