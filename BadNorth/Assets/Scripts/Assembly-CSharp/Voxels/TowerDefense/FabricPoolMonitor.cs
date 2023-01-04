using System;
using Fabric;
using RTM.OnScreenDebug;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x02000028 RID: 40
	internal class FabricPoolMonitor : MonoBehaviour
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000BB RID: 187 RVA: 0x000056D2 File Offset: 0x00003AD2
		// (set) Token: 0x060000BC RID: 188 RVA: 0x000056D9 File Offset: 0x00003AD9
		public static int poolSize { get; private set; } = int.MaxValue;

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000BD RID: 189 RVA: 0x000056E1 File Offset: 0x00003AE1
		// (set) Token: 0x060000BE RID: 190 RVA: 0x000056E8 File Offset: 0x00003AE8
		public static int availableVoices { get; private set; } = int.MaxValue;

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000BF RID: 191 RVA: 0x000056F0 File Offset: 0x00003AF0
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x000056F7 File Offset: 0x00003AF7
		public static int usedVoices { get; private set; } = 0;

		// Token: 0x060000C1 RID: 193 RVA: 0x00005700 File Offset: 0x00003B00
		private void Start()
		{
			FabricManager componentInParent = base.GetComponentInParent<FabricManager>();
			this.pool = componentInParent.AudioSourcePoolManager;
			this.Update();
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00005728 File Offset: 0x00003B28
		private void Update()
		{
			if (!this.pool)
			{
				return;
			}
			int num = 0;
			int usedVoices = 0;
			this.pool.GetInfo(ref usedVoices, ref num);
			FabricPoolMonitor.availableVoices = this.pool.Size();
			FabricPoolMonitor.usedVoices = usedVoices;
			FabricPoolMonitor.poolSize = FabricPoolMonitor.usedVoices + FabricPoolMonitor.availableVoices;
		}

		// Token: 0x04000050 RID: 80
		private DebugChannelGroup dbgGRoup = new DebugChannelGroup("FabricPoolMonitor", EVerbosity.Quiet, 0);

		// Token: 0x04000054 RID: 84
		private AudioSourcePool pool;
	}
}
