using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace RTM
{
	// Token: 0x020004B3 RID: 1203
	[DebuggerDisplay("{Interface} [WeakInterfaceReference]")]
	public struct WeakInterfaceReference<T> where T : class
	{
		// Token: 0x06001E74 RID: 7796 RVA: 0x00051319 File Offset: 0x0004F719
		public WeakInterfaceReference(T interfaceRef)
		{
			this.Comp = (interfaceRef as UnityEngine.Object);
			this.Interface = ((!this.Comp) ? ((T)((object)null)) : interfaceRef);
		}

		// Token: 0x06001E75 RID: 7797 RVA: 0x0005134E File Offset: 0x0004F74E
		public T Get()
		{
			this.Validate();
			return this.Interface;
		}

		// Token: 0x06001E76 RID: 7798 RVA: 0x0005135D File Offset: 0x0004F75D
		public bool Validate()
		{
			if (!this.Comp)
			{
				this.Comp = null;
				this.Interface = (T)((object)null);
			}
			return this.Interface != null;
		}

		// Token: 0x06001E77 RID: 7799 RVA: 0x00051393 File Offset: 0x0004F793
		public static implicit operator bool(WeakInterfaceReference<T> weakRef)
		{
			return weakRef.Validate();
		}

		// Token: 0x06001E78 RID: 7800 RVA: 0x0005139C File Offset: 0x0004F79C
		public static implicit operator T(WeakInterfaceReference<T> weakRef)
		{
			return weakRef.Get();
		}

		// Token: 0x06001E79 RID: 7801 RVA: 0x000513A5 File Offset: 0x0004F7A5
		public static implicit operator WeakInterfaceReference<T>(T interfaceRef)
		{
			return new WeakInterfaceReference<T>(interfaceRef);
		}

		// Token: 0x06001E7A RID: 7802 RVA: 0x000513B0 File Offset: 0x0004F7B0
		public override string ToString()
		{
			this.Get();
			return (this.Interface == null) ? string.Format("null ({0})", typeof(T).FullName) : this.Interface.ToString();
		}

		// Token: 0x06001E7B RID: 7803 RVA: 0x00051404 File Offset: 0x0004F804
		public static List<WeakInterfaceReference<T>> GetList(IEnumerable<T> input)
		{
			List<WeakInterfaceReference<T>> list = new List<WeakInterfaceReference<T>>();
			foreach (T interfaceRef in input)
			{
				list.Add(interfaceRef);
			}
			return list;
		}

		// Token: 0x06001E7C RID: 7804 RVA: 0x00051464 File Offset: 0x0004F864
		public static WeakInterfaceReference<T>[] GetArray(IEnumerable<T> input)
		{
			return WeakInterfaceReference<T>.GetList(input).ToArray();
		}

		// Token: 0x06001E7D RID: 7805 RVA: 0x00051474 File Offset: 0x0004F874
		public static void Convert(List<T> input, List<WeakInterfaceReference<T>> output)
		{
			using (new ScopedProfiler("WeakInterfaceReference.ConvertList", null))
			{
				output.Clear();
				if (output.Capacity < input.Count)
				{
					output.Capacity = input.Count;
				}
				foreach (T interfaceRef in input)
				{
					output.Add(interfaceRef);
				}
			}
		}

		// Token: 0x06001E7E RID: 7806 RVA: 0x00051520 File Offset: 0x0004F920
		public static void Convert(List<WeakInterfaceReference<T>> input, ref List<T> output)
		{
			using (new ScopedProfiler("WeakInterfaceReference.ConvertList", null))
			{
				output.Clear();
				if (output.Capacity < input.Count)
				{
					output.Capacity = input.Count;
				}
				foreach (WeakInterfaceReference<T> weakRef in input)
				{
					output.Add(weakRef);
				}
			}
		}

		// Token: 0x040012F7 RID: 4855
		private T Interface;

		// Token: 0x040012F8 RID: 4856
		private UnityEngine.Object Comp;
	}
}
