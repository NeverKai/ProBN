using System;
using UnityEngine;

namespace CS.Platform.Utils.Data
{
	// Token: 0x02000087 RID: 135
	public class PlatformNetworkDatabase : ScriptableObject
	{
		// Token: 0x04000261 RID: 609
		[SerializeField]
		public PlatformNetworkDatabase.XboxInfo XboxNetwork = new PlatformNetworkDatabase.XboxInfo();

		// Token: 0x02000088 RID: 136
		[Serializable]
		public class XboxInfo
		{
			// Token: 0x04000262 RID: 610
			public string SessionTemplateName = "LobbySessions";

			// Token: 0x04000263 RID: 611
			public string TCPTemplate = "PlatformTCP";

			// Token: 0x04000264 RID: 612
			public string UDPTemplate = "PlatformUDP";

			// Token: 0x04000265 RID: 613
			public int TCPPort = 123456;

			// Token: 0x04000266 RID: 614
			public int UDPPort = 123457;

			// Token: 0x04000267 RID: 615
			public string ChatTemplate = "PlatformChat";

			// Token: 0x04000268 RID: 616
			public int ChatPort = 123458;
		}
	}
}
