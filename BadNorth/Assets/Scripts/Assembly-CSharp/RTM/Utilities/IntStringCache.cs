using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace RTM.Utilities
{
	// Token: 0x020004E7 RID: 1255
	public class IntStringCache
	{
		// Token: 0x06002029 RID: 8233 RVA: 0x00056BD8 File Offset: 0x00054FD8
		public IntStringCache(string format)
		{
			this.format = format;
			this.builder = ((!string.IsNullOrEmpty(format)) ? new StringBuilder(format.Length + 32) : null);
		}

		// Token: 0x0600202A RID: 8234 RVA: 0x00056C17 File Offset: 0x00055017
		public IntStringCache(int initMin, int initMax, string format) : this(format)
		{
			this.cache = new Dictionary<int, string>(initMax - initMin + 1);
			this.AddRange(initMin, initMax);
		}

		// Token: 0x0600202B RID: 8235 RVA: 0x00056C38 File Offset: 0x00055038
		public static string GetClean(int i)
		{
			return IntStringCache.clean.Get(i);
		}

		// Token: 0x0600202C RID: 8236 RVA: 0x00056C48 File Offset: 0x00055048
		public string Get(int value)
		{
			string result;
			using (new ScopedProfiler("IntStringCache.Get()", null))
			{
				string text = null;
				if (!this.cache.TryGetValue(value, out text))
				{
					text = this.Add(value);
				}
				result = text;
			}
			return result;
		}

		// Token: 0x0600202D RID: 8237 RVA: 0x00056CA8 File Offset: 0x000550A8
		public void AddRange(int min, int max)
		{
			for (int i = min; i <= max; i++)
			{
				if (!this.cache.ContainsKey(i))
				{
					this.Add(i);
				}
			}
		}

		// Token: 0x0600202E RID: 8238 RVA: 0x00056CE0 File Offset: 0x000550E0
		private string Add(int value)
		{
			string text;
			if (this.builder != null)
			{
				text = this.builder.AppendFormat(this.format, value).ToString();
				this.builder.Length = 0;
			}
			else
			{
				text = value.ToString();
			}
			this.cache.Add(value, text);
			return text;
		}

		// Token: 0x0600202F RID: 8239 RVA: 0x00056D44 File Offset: 0x00055144
		public static implicit operator IntStringCache(string format)
		{
			return new IntStringCache(format);
		}

		// Token: 0x06002030 RID: 8240 RVA: 0x00056D4C File Offset: 0x0005514C
		[RuntimeInitializeOnLoadMethod]
		private static void Init()
		{
		}

		// Token: 0x04001401 RID: 5121
		public readonly string format;

		// Token: 0x04001402 RID: 5122
		private StringBuilder builder;

		// Token: 0x04001403 RID: 5123
		private Dictionary<int, string> cache = new Dictionary<int, string>();

		// Token: 0x04001404 RID: 5124
		public static IntStringCache clean = new IntStringCache(0, 31, null);

		// Token: 0x04001405 RID: 5125
		public static IntStringCache percent = new IntStringCache(0, 100, "{0}%");
	}
}
