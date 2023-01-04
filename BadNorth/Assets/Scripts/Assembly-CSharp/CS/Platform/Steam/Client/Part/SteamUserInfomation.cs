using System;
using CS.Platform.Utils;
using Steamworks;
using UnityEngine;

namespace CS.Platform.Steam.Client.Part
{
	// Token: 0x0200004A RID: 74
	public class SteamUserInfomation : MonoBehaviour
	{
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600030A RID: 778 RVA: 0x0000FD4E File Offset: 0x0000E14E
		public CSteamID LoggedUserID
		{
			get
			{
				return this._UserID;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600030B RID: 779 RVA: 0x0000FD56 File Offset: 0x0000E156
		public Texture2D UserImage
		{
			get
			{
				return this._UserImage;
			}
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000FD60 File Offset: 0x0000E160
		public void Awake()
		{
			this._Manager = base.GetComponent<SteamManager>();
			this._UserImage = CS.Platform.Utils.Random.CreateNewImageF(true, 64, 64, TextureFormat.RGBA32, false);
			this._UserID = SteamUser.GetSteamID();
			this._Manager.Utilities.LoadProfileImage(this._UserID, ref this._UserImage);
		}

		// Token: 0x04000147 RID: 327
		private SteamManager _Manager;

		// Token: 0x04000148 RID: 328
		private CSteamID _UserID;

		// Token: 0x04000149 RID: 329
		private Texture2D _UserImage;
	}
}
