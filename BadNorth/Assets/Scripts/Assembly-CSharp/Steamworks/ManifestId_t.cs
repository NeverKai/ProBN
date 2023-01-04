using System;

namespace Steamworks
{
	// Token: 0x0200037B RID: 891
	[Serializable]
	public struct ManifestId_t : IEquatable<ManifestId_t>, IComparable<ManifestId_t>
	{
		// Token: 0x0600143F RID: 5183 RVA: 0x000298CE File Offset: 0x00027CCE
		public ManifestId_t(ulong value)
		{
			this.m_ManifestId = value;
		}

		// Token: 0x06001440 RID: 5184 RVA: 0x000298D7 File Offset: 0x00027CD7
		public override string ToString()
		{
			return this.m_ManifestId.ToString();
		}

		// Token: 0x06001441 RID: 5185 RVA: 0x000298EA File Offset: 0x00027CEA
		public override bool Equals(object other)
		{
			return other is ManifestId_t && this == (ManifestId_t)other;
		}

		// Token: 0x06001442 RID: 5186 RVA: 0x0002990B File Offset: 0x00027D0B
		public override int GetHashCode()
		{
			return this.m_ManifestId.GetHashCode();
		}

		// Token: 0x06001443 RID: 5187 RVA: 0x0002991E File Offset: 0x00027D1E
		public static bool operator ==(ManifestId_t x, ManifestId_t y)
		{
			return x.m_ManifestId == y.m_ManifestId;
		}

		// Token: 0x06001444 RID: 5188 RVA: 0x00029930 File Offset: 0x00027D30
		public static bool operator !=(ManifestId_t x, ManifestId_t y)
		{
			return !(x == y);
		}

		// Token: 0x06001445 RID: 5189 RVA: 0x0002993C File Offset: 0x00027D3C
		public static explicit operator ManifestId_t(ulong value)
		{
			return new ManifestId_t(value);
		}

		// Token: 0x06001446 RID: 5190 RVA: 0x00029944 File Offset: 0x00027D44
		public static explicit operator ulong(ManifestId_t that)
		{
			return that.m_ManifestId;
		}

		// Token: 0x06001447 RID: 5191 RVA: 0x0002994D File Offset: 0x00027D4D
		public bool Equals(ManifestId_t other)
		{
			return this.m_ManifestId == other.m_ManifestId;
		}

		// Token: 0x06001448 RID: 5192 RVA: 0x0002995E File Offset: 0x00027D5E
		public int CompareTo(ManifestId_t other)
		{
			return this.m_ManifestId.CompareTo(other.m_ManifestId);
		}

		// Token: 0x04000CBF RID: 3263
		public static readonly ManifestId_t Invalid = new ManifestId_t(0UL);

		// Token: 0x04000CC0 RID: 3264
		public ulong m_ManifestId;
	}
}
