using System;

namespace CS.Platform
{
	// Token: 0x0200005B RID: 91
	public struct BaseUserInfo
	{
		// Token: 0x060003FD RID: 1021 RVA: 0x00012260 File Offset: 0x00010660
		public BaseUserInfo(ulong userID, string platform, string name)
		{
			this._hasGenedHas = false;
			this._hashCode = 0;
			this._userID = userID;
			this._platformKey = platform;
			this._userName = name;
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00012288 File Offset: 0x00010688
		public BaseUserInfo(string fromString)
		{
			this._hasGenedHas = false;
			this._hashCode = 0;
			this._userID = 0UL;
			this._platformKey = "no platform";
			this._userName = string.Empty;
			int num = 0;
			int num2 = fromString.IndexOf(':', num);
			this._platformKey = fromString.Substring(num, num2 - num);
			if (num + 1 > fromString.Length)
			{
				return;
			}
			num = num2 + 1;
			num2 = fromString.IndexOf(':', num);
			this._userID = Convert.ToUInt64(fromString.Substring(num, num2 - num));
			if (num + 1 > fromString.Length)
			{
				return;
			}
			num = num2 + 1;
			this._userName = fromString.Substring(num);
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00012330 File Offset: 0x00010730
		public BaseUserInfo(byte[] target, int readPoint, out int readEnd, int size = 0)
		{
			this._hasGenedHas = false;
			this._hashCode = 0;
			readEnd = readPoint;
			this._userID = 0UL;
			this._platformKey = string.Empty;
			this._userName = string.Empty;
			if (size == 0)
			{
				size = target.Length;
			}
			if (size < readEnd + 8)
			{
				return;
			}
			this._userID = BitConverter.ToUInt64(target, readEnd);
			readEnd += 8;
			if (size < readEnd + 1)
			{
				return;
			}
			byte b = target[readEnd];
			readEnd++;
			if (size < readEnd + (int)(b * 2))
			{
				return;
			}
			for (byte b2 = 0; b2 < b; b2 += 1)
			{
				this._platformKey += BitConverter.ToChar(target, readEnd);
				readEnd += 2;
			}
			if (size < readEnd + 1)
			{
				return;
			}
			b = target[readEnd];
			readEnd++;
			if (size < readEnd + (int)(b * 2))
			{
				return;
			}
			for (byte b3 = 0; b3 < b; b3 += 1)
			{
				this._userName += BitConverter.ToChar(target, readEnd);
				readEnd += 2;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x00012452 File Offset: 0x00010852
		public ulong userID
		{
			get
			{
				return this._userID;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000401 RID: 1025 RVA: 0x0001245A File Offset: 0x0001085A
		public string platformKey
		{
			get
			{
				return this._platformKey;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x00012462 File Offset: 0x00010862
		public string userName
		{
			get
			{
				return this._userName;
			}
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0001246A File Offset: 0x0001086A
		public void Blank()
		{
			this._hasGenedHas = false;
			this._hashCode = 0;
			this._userID = 0UL;
			this._platformKey = "no platform";
			this._userName = string.Empty;
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00012498 File Offset: 0x00010898
		public void Set(ulong userID, string platform, string name)
		{
			this._hasGenedHas = false;
			this._hashCode = 0;
			this._userID = userID;
			if (platform != null)
			{
				this._platformKey = platform;
			}
			this._userName = name;
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x000124C4 File Offset: 0x000108C4
		public void Set(BaseUserInfo copy)
		{
			this._hasGenedHas = copy._hasGenedHas;
			this._hashCode = copy._hashCode;
			this._userID = copy._userID;
			this._platformKey = copy._platformKey;
			this._userName = copy._userName;
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00012512 File Offset: 0x00010912
		public static bool operator ==(BaseUserInfo left, BaseUserInfo right)
		{
			return left.platformKey == right.platformKey && left.userID == right.userID;
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0001253E File Offset: 0x0001093E
		public static bool operator !=(BaseUserInfo left, BaseUserInfo right)
		{
			return !(left == right);
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0001254A File Offset: 0x0001094A
		public override bool Equals(object obj)
		{
			return obj is BaseUserInfo && this == (BaseUserInfo)obj;
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0001256A File Offset: 0x0001096A
		public bool RawEquals(BaseUserInfo check)
		{
			return this.userID == check.userID && this.platformKey == check.platformKey && this.userName == check.userName;
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x000125AC File Offset: 0x000109AC
		public override int GetHashCode()
		{
			if (!this._hasGenedHas)
			{
				this._hasGenedHas = true;
				if (this.platformKey != null)
				{
					this._hashCode = string.Format("{0}-{1}", this.platformKey, this.userID).GetHashCode();
				}
				else
				{
					this._hashCode = string.Format("{0}-{1}", string.Empty, string.Empty).GetHashCode();
				}
			}
			return this._hashCode;
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x00012626 File Offset: 0x00010A26
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				this.platformKey,
				":",
				this.userID,
				":",
				this.userName
			});
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x00012663 File Offset: 0x00010A63
		public int GetByteSize()
		{
			return 10 + 2 * (this.platformKey.Length + this.userName.Length);
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x00012684 File Offset: 0x00010A84
		public int AddToArray(byte[] target, int startAt)
		{
			if (target.Length < startAt + this.GetByteSize())
			{
				return startAt;
			}
			Buffer.BlockCopy(BitConverter.GetBytes(this.userID), 0, target, startAt, 8);
			startAt += 8;
			target[startAt] = (byte)this.platformKey.Length;
			startAt++;
			Buffer.BlockCopy(this.platformKey.ToCharArray(), 0, target, startAt, 2 * this.platformKey.Length);
			startAt += 2 * this.platformKey.Length;
			target[startAt] = (byte)this.userName.Length;
			startAt++;
			Buffer.BlockCopy(this.userName.ToCharArray(), 0, target, startAt, 2 * this.userName.Length);
			startAt += 2 * this.userName.Length;
			return startAt;
		}

		// Token: 0x040001A6 RID: 422
		private ulong _userID;

		// Token: 0x040001A7 RID: 423
		private string _platformKey;

		// Token: 0x040001A8 RID: 424
		private string _userName;

		// Token: 0x040001A9 RID: 425
		private bool _hasGenedHas;

		// Token: 0x040001AA RID: 426
		private int _hashCode;
	}
}
