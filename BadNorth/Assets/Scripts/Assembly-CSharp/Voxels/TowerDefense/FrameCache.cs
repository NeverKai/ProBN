using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x0200068A RID: 1674
	[Serializable]
	public class FrameCache<Type>
	{
		// Token: 0x06002AC0 RID: 10944 RVA: 0x00098A77 File Offset: 0x00096E77
		public FrameCache(Func<Type> func)
		{
			this.func = func;
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06002AC1 RID: 10945 RVA: 0x00098A86 File Offset: 0x00096E86
		public Type value
		{
			get
			{
				if (this._frame != Time.frameCount)
				{
					this._frame = Time.frameCount;
					this._value = this.func();
				}
				return this._value;
			}
		}

		// Token: 0x06002AC2 RID: 10946 RVA: 0x00098ABA File Offset: 0x00096EBA
		public static implicit operator Type(FrameCache<Type> v)
		{
			return v.value;
		}

		// Token: 0x06002AC3 RID: 10947 RVA: 0x00098AC2 File Offset: 0x00096EC2
		public static implicit operator bool(FrameCache<Type> v)
		{
			return v.value != null;
		}

		// Token: 0x06002AC4 RID: 10948 RVA: 0x00098AD5 File Offset: 0x00096ED5
		public override bool Equals(object obj)
		{
			return obj.Equals(this.value);
		}

		// Token: 0x06002AC5 RID: 10949 RVA: 0x00098AE8 File Offset: 0x00096EE8
		public override int GetHashCode()
		{
			Type value = this.value;
			return value.GetHashCode();
		}

		// Token: 0x04001BC9 RID: 7113
		private Func<Type> func;

		// Token: 0x04001BCA RID: 7114
		[SerializeField]
		private Type _value;

		// Token: 0x04001BCB RID: 7115
		[NonSerialized]
		private int _frame;
	}
}
