using System;
using System.Collections;
using System.Collections.Generic;

namespace CS.VT
{
	// Token: 0x02000383 RID: 899
	public class Callback<T> : IEnumerator<T>, IEnumerator, IDisposable
	{
		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600148F RID: 5263 RVA: 0x00029E2C File Offset: 0x0002822C
		// (set) Token: 0x06001490 RID: 5264 RVA: 0x00029E6C File Offset: 0x0002826C
		public Action<Callback<T>> OnComplete
		{
			get
			{
				object @lock = this._lock;
				Action<Callback<T>> onComplete;
				lock (@lock)
				{
					onComplete = this._onComplete;
				}
				return onComplete;
			}
			set
			{
				object @lock = this._lock;
				lock (@lock)
				{
					this._onComplete = value;
				}
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06001491 RID: 5265 RVA: 0x00029EAC File Offset: 0x000282AC
		// (set) Token: 0x06001492 RID: 5266 RVA: 0x00029EEC File Offset: 0x000282EC
		public Callback<T>.CallbackState State
		{
			get
			{
				object @lock = this._lock;
				Callback<T>.CallbackState state;
				lock (@lock)
				{
					state = this._state;
				}
				return state;
			}
			set
			{
				object @lock = this._lock;
				lock (@lock)
				{
					this._state = value;
					if (this._state != Callback<T>.CallbackState.Active && this._onComplete != null)
					{
						this._onComplete(this);
					}
				}
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06001493 RID: 5267 RVA: 0x00029F4C File Offset: 0x0002834C
		// (set) Token: 0x06001494 RID: 5268 RVA: 0x00029F8C File Offset: 0x0002838C
		public T Result
		{
			get
			{
				object @lock = this._lock;
				T result;
				lock (@lock)
				{
					result = this._result;
				}
				return result;
			}
			set
			{
				object @lock = this._lock;
				lock (@lock)
				{
					this._result = value;
				}
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06001495 RID: 5269 RVA: 0x00029FCC File Offset: 0x000283CC
		public object Current
		{
			get
			{
				return this.Result;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06001496 RID: 5270 RVA: 0x00029FD9 File Offset: 0x000283D9
		T IEnumerator<!0>.Current
		{
			get
			{
				return this.Result;
			}
		}

		// Token: 0x06001497 RID: 5271 RVA: 0x00029FE1 File Offset: 0x000283E1
		public bool MoveNext()
		{
			return this.State == Callback<T>.CallbackState.Active;
		}

		// Token: 0x06001498 RID: 5272 RVA: 0x00029FEC File Offset: 0x000283EC
		public void Reset()
		{
		}

		// Token: 0x06001499 RID: 5273 RVA: 0x00029FEE File Offset: 0x000283EE
		public void Dispose()
		{
		}

		// Token: 0x04000CCB RID: 3275
		private Callback<T>.CallbackState _state;

		// Token: 0x04000CCC RID: 3276
		private T _result;

		// Token: 0x04000CCD RID: 3277
		private object _lock = new object();

		// Token: 0x04000CCE RID: 3278
		private Action<Callback<T>> _onComplete;

		// Token: 0x02000384 RID: 900
		public enum CallbackState
		{
			// Token: 0x04000CD0 RID: 3280
			Active,
			// Token: 0x04000CD1 RID: 3281
			Failed,
			// Token: 0x04000CD2 RID: 3282
			Complete
		}
	}
}
