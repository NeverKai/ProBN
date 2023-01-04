using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Steamworks
{
	// Token: 0x0200035A RID: 858
	[Serializable]
	[StructLayout(LayoutKind.Sequential, Pack = 4, Size = 372)]
	public class gameserveritem_t
	{
		// Token: 0x060012C6 RID: 4806 RVA: 0x00027CAB File Offset: 0x000260AB
		public string GetGameDir()
		{
			return Encoding.UTF8.GetString(this.m_szGameDir, 0, Array.IndexOf<byte>(this.m_szGameDir, 0));
		}

		// Token: 0x060012C7 RID: 4807 RVA: 0x00027CCA File Offset: 0x000260CA
		public void SetGameDir(string dir)
		{
			this.m_szGameDir = Encoding.UTF8.GetBytes(dir + '\0');
		}

		// Token: 0x060012C8 RID: 4808 RVA: 0x00027CE8 File Offset: 0x000260E8
		public string GetMap()
		{
			return Encoding.UTF8.GetString(this.m_szMap, 0, Array.IndexOf<byte>(this.m_szMap, 0));
		}

		// Token: 0x060012C9 RID: 4809 RVA: 0x00027D07 File Offset: 0x00026107
		public void SetMap(string map)
		{
			this.m_szMap = Encoding.UTF8.GetBytes(map + '\0');
		}

		// Token: 0x060012CA RID: 4810 RVA: 0x00027D25 File Offset: 0x00026125
		public string GetGameDescription()
		{
			return Encoding.UTF8.GetString(this.m_szGameDescription, 0, Array.IndexOf<byte>(this.m_szGameDescription, 0));
		}

		// Token: 0x060012CB RID: 4811 RVA: 0x00027D44 File Offset: 0x00026144
		public void SetGameDescription(string desc)
		{
			this.m_szGameDescription = Encoding.UTF8.GetBytes(desc + '\0');
		}

		// Token: 0x060012CC RID: 4812 RVA: 0x00027D62 File Offset: 0x00026162
		public string GetServerName()
		{
			if (this.m_szServerName[0] == 0)
			{
				return this.m_NetAdr.GetConnectionAddressString();
			}
			return Encoding.UTF8.GetString(this.m_szServerName, 0, Array.IndexOf<byte>(this.m_szServerName, 0));
		}

		// Token: 0x060012CD RID: 4813 RVA: 0x00027D9A File Offset: 0x0002619A
		public void SetServerName(string name)
		{
			this.m_szServerName = Encoding.UTF8.GetBytes(name + '\0');
		}

		// Token: 0x060012CE RID: 4814 RVA: 0x00027DB8 File Offset: 0x000261B8
		public string GetGameTags()
		{
			return Encoding.UTF8.GetString(this.m_szGameTags, 0, Array.IndexOf<byte>(this.m_szGameTags, 0));
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x00027DD7 File Offset: 0x000261D7
		public void SetGameTags(string tags)
		{
			this.m_szGameTags = Encoding.UTF8.GetBytes(tags + '\0');
		}

		// Token: 0x04000C74 RID: 3188
		public servernetadr_t m_NetAdr;

		// Token: 0x04000C75 RID: 3189
		public int m_nPing;

		// Token: 0x04000C76 RID: 3190
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bHadSuccessfulResponse;

		// Token: 0x04000C77 RID: 3191
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bDoNotRefresh;

		// Token: 0x04000C78 RID: 3192
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
		private byte[] m_szGameDir;

		// Token: 0x04000C79 RID: 3193
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
		private byte[] m_szMap;

		// Token: 0x04000C7A RID: 3194
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
		private byte[] m_szGameDescription;

		// Token: 0x04000C7B RID: 3195
		public uint m_nAppID;

		// Token: 0x04000C7C RID: 3196
		public int m_nPlayers;

		// Token: 0x04000C7D RID: 3197
		public int m_nMaxPlayers;

		// Token: 0x04000C7E RID: 3198
		public int m_nBotPlayers;

		// Token: 0x04000C7F RID: 3199
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bPassword;

		// Token: 0x04000C80 RID: 3200
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bSecure;

		// Token: 0x04000C81 RID: 3201
		public uint m_ulTimeLastPlayed;

		// Token: 0x04000C82 RID: 3202
		public int m_nServerVersion;

		// Token: 0x04000C83 RID: 3203
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
		private byte[] m_szServerName;

		// Token: 0x04000C84 RID: 3204
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
		private byte[] m_szGameTags;

		// Token: 0x04000C85 RID: 3205
		public CSteamID m_steamID;
	}
}
