using System;
using System.Diagnostics;
using RTM.OnScreenDebug;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x0200056F RID: 1391
	internal class MobileScreenDetector : MonoBehaviour, IGameSetup
	{
		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06002426 RID: 9254 RVA: 0x00071828 File Offset: 0x0006FC28
		// (set) Token: 0x06002427 RID: 9255 RVA: 0x0007182F File Offset: 0x0006FC2F
		public static Vector4 safeZone { get; private set; }

		// Token: 0x14000081 RID: 129
		// (add) Token: 0x06002428 RID: 9256 RVA: 0x00071838 File Offset: 0x0006FC38
		// (remove) Token: 0x06002429 RID: 9257 RVA: 0x0007186C File Offset: 0x0006FC6C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<Vector4, DeviceOrientation> onSafeZoneChanged;

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x0600242A RID: 9258 RVA: 0x000718A0 File Offset: 0x0006FCA0
		// (set) Token: 0x0600242B RID: 9259 RVA: 0x000718A7 File Offset: 0x0006FCA7
		public static DeviceOrientation deviceOrientation { get; private set; }

		// Token: 0x0600242C RID: 9260 RVA: 0x000718AF File Offset: 0x0006FCAF
		void IGameSetup.OnGameAwake()
		{
		}

		// Token: 0x0600242D RID: 9261 RVA: 0x000718B1 File Offset: 0x0006FCB1
		// Note: this type is marked as 'beforefieldinit'.
		static MobileScreenDetector()
		{
			MobileScreenDetector.onSafeZoneChanged = delegate(Vector4 A_0, DeviceOrientation A_1)
			{
			};
		}

		// Token: 0x040016C3 RID: 5827
		private DebugChannelGroup dbgGroup = new DebugChannelGroup("MobileScreenDetector", EVerbosity.Quiet, 100);

		// Token: 0x040016C7 RID: 5831
		private DeviceOrientation[] allowedOrientations = new DeviceOrientation[]
		{
			DeviceOrientation.LandscapeLeft,
			DeviceOrientation.LandscapeRight
		};

		// Token: 0x040016C8 RID: 5832
		[SerializeField]
		private RectOffset iPhoneDebugOffset = new RectOffset();
	}
}
