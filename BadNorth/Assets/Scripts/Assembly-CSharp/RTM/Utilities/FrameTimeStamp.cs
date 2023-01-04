using System;
using UnityEngine;

namespace RTM.Utilities
{
	// Token: 0x020004E6 RID: 1254
	public struct FrameTimeStamp
	{
		// Token: 0x06002020 RID: 8224 RVA: 0x00056ABA File Offset: 0x00054EBA
		private FrameTimeStamp(int frameCount, float time, float unscaledTime)
		{
			this.frameCount = frameCount;
			this.time = time;
			this.unscaledTime = unscaledTime;
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06002021 RID: 8225 RVA: 0x00056AD1 File Offset: 0x00054ED1
		public int framesSince
		{
			get
			{
				return Time.frameCount - this.frameCount;
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06002022 RID: 8226 RVA: 0x00056ADF File Offset: 0x00054EDF
		public float timeSince
		{
			get
			{
				return Time.time - this.time;
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06002023 RID: 8227 RVA: 0x00056AED File Offset: 0x00054EED
		public float unscaledTimeSince
		{
			get
			{
				return Time.unscaledTime - this.unscaledTime;
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06002024 RID: 8228 RVA: 0x00056AFB File Offset: 0x00054EFB
		public bool isThisFrame
		{
			get
			{
				return Time.frameCount == this.frameCount;
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06002025 RID: 8229 RVA: 0x00056B0A File Offset: 0x00054F0A
		public static FrameTimeStamp now
		{
			get
			{
				return new FrameTimeStamp(Time.frameCount, Time.time, Time.unscaledTime);
			}
		}

		// Token: 0x06002026 RID: 8230 RVA: 0x00056B20 File Offset: 0x00054F20
		public static FrameTimeStamp operator -(FrameTimeStamp a, FrameTimeStamp b)
		{
			return new FrameTimeStamp(a.frameCount - b.frameCount, a.time - b.time, a.unscaledTime - b.unscaledTime);
		}

		// Token: 0x06002027 RID: 8231 RVA: 0x00056B54 File Offset: 0x00054F54
		public override string ToString()
		{
			return string.Format("{0:D5} {1:0.00}s ({2: 0.00}s)", this.frameCount, this.time, this.unscaledTime);
		}

		// Token: 0x040013FB RID: 5115
		public readonly int frameCount;

		// Token: 0x040013FC RID: 5116
		public readonly float time;

		// Token: 0x040013FD RID: 5117
		public readonly float unscaledTime;

		// Token: 0x040013FE RID: 5118
		public static readonly FrameTimeStamp zero = new FrameTimeStamp(0, 0f, 0f);

		// Token: 0x040013FF RID: 5119
		public static readonly FrameTimeStamp infinity = new FrameTimeStamp(int.MaxValue, float.PositiveInfinity, float.PositiveInfinity);

		// Token: 0x04001400 RID: 5120
		public static readonly FrameTimeStamp negativeInfinity = new FrameTimeStamp(-2147483647, float.NegativeInfinity, float.NegativeInfinity);
	}
}
