using System;
using CS.Platform.Utils.Data;
using UnityEngine;

namespace CS.Platform.Utils
{
	// Token: 0x0200007B RID: 123
	public static class Network
	{
		// Token: 0x06000568 RID: 1384 RVA: 0x00015FE6 File Offset: 0x000143E6
		public static void NetworkSetup(string databaseLocation = null)
		{
			if (string.IsNullOrEmpty(databaseLocation))
			{
				databaseLocation = "Platform/NetworkData";
			}
			Network._ratingInfomation = (Resources.Load(databaseLocation) as PlatformNetworkDatabase);
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0001600C File Offset: 0x0001440C
		public static bool XboxTCPActive()
		{
			return Network._ratingInfomation != null && !string.IsNullOrEmpty(Network._ratingInfomation.XboxNetwork.TCPTemplate) && 0 < Network._ratingInfomation.XboxNetwork.TCPPort;
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x0001605C File Offset: 0x0001445C
		public static bool XboxUDPActive()
		{
			return Network._ratingInfomation != null && !string.IsNullOrEmpty(Network._ratingInfomation.XboxNetwork.UDPTemplate) && 0 < Network._ratingInfomation.XboxNetwork.UDPPort;
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x000160AC File Offset: 0x000144AC
		public static bool XboxChatActive()
		{
			return Network._ratingInfomation != null && !string.IsNullOrEmpty(Network._ratingInfomation.XboxNetwork.ChatTemplate) && 0 < Network._ratingInfomation.XboxNetwork.ChatPort;
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x000160F9 File Offset: 0x000144F9
		public static string XboxTCPTemplate()
		{
			if (Network._ratingInfomation != null)
			{
				return Network._ratingInfomation.XboxNetwork.TCPTemplate;
			}
			return null;
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x0001611C File Offset: 0x0001451C
		public static int XboxTCPPort()
		{
			if (Network._ratingInfomation != null)
			{
				return Network._ratingInfomation.XboxNetwork.TCPPort;
			}
			return 0;
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x0001613F File Offset: 0x0001453F
		public static string XboxUDPTemplate()
		{
			if (Network._ratingInfomation != null)
			{
				return Network._ratingInfomation.XboxNetwork.UDPTemplate;
			}
			return null;
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x00016162 File Offset: 0x00014562
		public static int XboxUDPPort()
		{
			if (Network._ratingInfomation != null)
			{
				return Network._ratingInfomation.XboxNetwork.UDPPort;
			}
			return 0;
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x00016185 File Offset: 0x00014585
		public static string XboxChatTemplate()
		{
			if (Network._ratingInfomation != null)
			{
				return Network._ratingInfomation.XboxNetwork.ChatTemplate;
			}
			return null;
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x000161A8 File Offset: 0x000145A8
		public static int XboxChatPort()
		{
			if (Network._ratingInfomation != null)
			{
				return Network._ratingInfomation.XboxNetwork.ChatPort;
			}
			return 0;
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x000161CB File Offset: 0x000145CB
		public static string XboxSessionTemplateName()
		{
			if (Network._ratingInfomation != null)
			{
				return Network._ratingInfomation.XboxNetwork.SessionTemplateName;
			}
			return null;
		}

		// Token: 0x04000230 RID: 560
		private static PlatformNetworkDatabase _ratingInfomation;
	}
}
