using System;
using UnityEngine;

namespace ControlledRandomness
{
	// Token: 0x020004F7 RID: 1271
	[Serializable]
	public struct Tag
	{
		// Token: 0x06002073 RID: 8307 RVA: 0x00057641 File Offset: 0x00055A41
		public Tag(string key, string value)
		{
			this._key = key;
			this._value = value;
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06002074 RID: 8308 RVA: 0x00057651 File Offset: 0x00055A51
		public string key
		{
			get
			{
				return this._key;
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06002075 RID: 8309 RVA: 0x00057659 File Offset: 0x00055A59
		public string value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x06002076 RID: 8310 RVA: 0x00057661 File Offset: 0x00055A61
		public bool Compatible(Tag other)
		{
			return this.key != other.key || this.value == other.value;
		}

		// Token: 0x06002077 RID: 8311 RVA: 0x0005768E File Offset: 0x00055A8E
		public override string ToString()
		{
			return this._key + " : " + this._value;
		}

		// Token: 0x04001424 RID: 5156
		[SerializeField]
		private string _key;

		// Token: 0x04001425 RID: 5157
		[SerializeField]
		private string _value;
	}
}
