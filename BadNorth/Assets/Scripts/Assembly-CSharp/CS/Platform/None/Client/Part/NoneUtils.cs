using System;
using CS.Platform.Utils;
using UnityEngine;

namespace CS.Platform.None.Client.Part
{
	// Token: 0x0200003D RID: 61
	public class NoneUtils : MonoBehaviour
	{
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600025F RID: 607 RVA: 0x0000BFBD File Offset: 0x0000A3BD
		// (set) Token: 0x06000260 RID: 608 RVA: 0x0000BFC5 File Offset: 0x0000A3C5
		public bool GUIActive
		{
			get
			{
				return this._guiActive;
			}
			set
			{
				if (!value && this._manager.Dialog.ShowingMessage)
				{
					value = true;
				}
				if (this._guiActive != value)
				{
					this._guiActive = value;
					PlatformEvents.PlatformGamePause(this._guiActive);
				}
			}
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000C003 File Offset: 0x0000A403
		private void Awake()
		{
			this._manager = base.GetComponent<NonePlatformManager>();
			PlatformSystemMessenger.OnMessageChange += this.PlatformSystemMessenger_OnMessageChange;
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000C022 File Offset: 0x0000A422
		private void PlatformSystemMessenger_OnMessageChange(bool obj)
		{
			this.GUIActive = obj;
		}

		// Token: 0x040000F4 RID: 244
		private NonePlatformManager _manager;

		// Token: 0x040000F5 RID: 245
		private bool _guiActive;
	}
}
