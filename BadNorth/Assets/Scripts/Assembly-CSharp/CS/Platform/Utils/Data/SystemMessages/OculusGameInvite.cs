using System;

namespace CS.Platform.Utils.Data.SystemMessages
{
	// Token: 0x02000054 RID: 84
	internal class OculusGameInvite : PlatformMessageBase
	{
		// Token: 0x060003F0 RID: 1008 RVA: 0x00012111 File Offset: 0x00010511
		public OculusGameInvite()
		{
			base.MessageType = MessageTypes.GAMEINVITE;
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x00012157 File Offset: 0x00010557
		// (set) Token: 0x060003F1 RID: 1009 RVA: 0x00012136 File Offset: 0x00010536
		public string IP
		{
			get
			{
				return this._IP;
			}
			set
			{
				if (this._IP != value)
				{
					this._IP = value;
					base.Dirty = true;
				}
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x0001217B File Offset: 0x0001057B
		// (set) Token: 0x060003F3 RID: 1011 RVA: 0x0001215F File Offset: 0x0001055F
		public int Port
		{
			get
			{
				return this._Port;
			}
			set
			{
				if (this._Port != value)
				{
					this._Port = value;
					base.Dirty = true;
				}
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x000121A4 File Offset: 0x000105A4
		// (set) Token: 0x060003F5 RID: 1013 RVA: 0x00012183 File Offset: 0x00010583
		public string From
		{
			get
			{
				return this._From;
			}
			set
			{
				if (this._From != value)
				{
					this._From = value;
					base.Dirty = true;
				}
			}
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x000121AC File Offset: 0x000105AC
		protected override void Serialize(DataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteString(this._IP);
			writer.WriteInt32(this._Port);
			writer.WriteString(this._From);
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x000121D9 File Offset: 0x000105D9
		protected override void Deserialize(DataReader reader)
		{
			base.Deserialize(reader);
			this._IP = reader.ReadString();
			this._Port = reader.ReadInt32();
			this._From = reader.ReadString();
		}

		// Token: 0x04000181 RID: 385
		private string _IP = string.Empty;

		// Token: 0x04000182 RID: 386
		private int _Port;

		// Token: 0x04000183 RID: 387
		private string _From = string.Empty;
	}
}
