using System;
using System.Collections.Generic;
using CS.Platform.Utils;
using CS.Platform.Utils.Data;
using UnityEngine;

namespace CS.Platform.Base.Client.Part
{
	// Token: 0x02000038 RID: 56
	public class SystemStats : MonoBehaviour
	{
		// Token: 0x06000231 RID: 561 RVA: 0x0000B254 File Offset: 0x00009654
		public virtual void ClearStats(bool dirty = false)
		{
			object locker = this._locker;
			lock (locker)
			{
				CS.Platform.Utils.Debug.LogInfo("[SYSTEMSTATS] ClearStats | Total: {0}", new object[]
				{
					this._stats.Count
				});
				if (dirty)
				{
					this._dirty = true;
				}
				this._stats.Clear();
			}
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000B2C8 File Offset: 0x000096C8
		protected void ReadStats(byte[] data)
		{
			object locker = this._locker;
			lock (locker)
			{
				if (data == null)
				{
					this._ready = true;
				}
				else
				{
					bool dirty = this._dirty;
					DataReader dataReader = new DataReader(data, false);
					byte b = dataReader.ReadByte();
					CS.Platform.Utils.Debug.ThreadLogInfo("[SYSTEMSTATS] ReadStats | Total: {0}", new object[]
					{
						b
					});
					for (byte b2 = 0; b2 < b; b2 += 1)
					{
						string text = dataReader.ReadString();
						float num = dataReader.ReadUInt32();
						CS.Platform.Utils.Debug.ThreadLogInfo("[SYSTEMSTATS] ReadStats: New | Key: {0} | Value: {1}", new object[]
						{
							text,
							num
						});
						this.AddStat(text, num);
					}
					this._ready = true;
					if (!dirty)
					{
						this._dirty = false;
					}
					if (this._lastSize < 0)
					{
						this._lastSize = dataReader.RawBufferPoint;
					}
				}
			}
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000B3C0 File Offset: 0x000097C0
		protected void WriteStats()
		{
			object locker = this._locker;
			lock (locker)
			{
				if (this._ready)
				{
					if (this._lastSize < 0)
					{
						this._lastSize = Statistics.StatisticsSaveEstSize();
					}
					DataWriter dataWriter = new DataWriter(this._lastSize);
					dataWriter.WriteByte((byte)this._stats.Count);
					CS.Platform.Utils.Debug.ThreadLogInfo("[SYSTEMSTATS] WriteStats | Total: {0}", new object[]
					{
						(byte)this._stats.Count
					});
					Dictionary<string, float>.Enumerator enumerator = this._stats.GetEnumerator();
					while (enumerator.MoveNext())
					{
						string message = "[SYSTEMSTATS] WriteStats: New | Key: {0} | Value: {1}";
						object[] array = new object[2];
						int num = 0;
						KeyValuePair<string, float> keyValuePair = enumerator.Current;
						array[num] = keyValuePair.Key;
						int num2 = 1;
						KeyValuePair<string, float> keyValuePair2 = enumerator.Current;
						array[num2] = keyValuePair2.Value;
						CS.Platform.Utils.Debug.ThreadLogInfo(message, array);
						DataWriter dataWriter2 = dataWriter;
						KeyValuePair<string, float> keyValuePair3 = enumerator.Current;
						dataWriter2.WriteString(keyValuePair3.Key);
						DataWriter dataWriter3 = dataWriter;
						KeyValuePair<string, float> keyValuePair4 = enumerator.Current;
						dataWriter3.WriteUInt32((uint)keyValuePair4.Value);
					}
					enumerator.Dispose();
					this._manager.Save(Statistics.StatisticsSaveFile(), dataWriter.DataBuffer, dataWriter.RawBufferPoint, false, false);
					if (this._lastSize != dataWriter.RawBufferPoint)
					{
						this._lastSize = dataWriter.RawBufferPoint;
					}
					this._dirty = false;
				}
			}
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000B53C File Offset: 0x0000993C
		public virtual float GetStat(string key)
		{
			object locker = this._locker;
			float result;
			lock (locker)
			{
				if (this._stats.ContainsKey(key))
				{
					result = this._stats[key];
				}
				else
				{
					result = 0f;
				}
			}
			return result;
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000B59C File Offset: 0x0000999C
		public virtual void SetStat(string key, float value)
		{
			object locker = this._locker;
			lock (locker)
			{
				if (this._stats.ContainsKey(key))
				{
					this._dirty = (this._stats[key] != value);
					this._stats[key] = value;
				}
				else
				{
					this._stats.Add(key, value);
				}
			}
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000B61C File Offset: 0x00009A1C
		public virtual void AddStat(string key, float amount)
		{
			if (amount == 0f)
			{
				return;
			}
			object locker = this._locker;
			lock (locker)
			{
				if (this._stats.ContainsKey(key))
				{
					this._stats[key] = this._stats[key] + amount;
				}
				else
				{
					this._stats.Add(key, amount);
				}
				this._dirty = true;
			}
		}

		// Token: 0x040000D2 RID: 210
		protected int _lastSize = -1;

		// Token: 0x040000D3 RID: 211
		protected object _locker = new object();

		// Token: 0x040000D4 RID: 212
		protected bool _dirty;

		// Token: 0x040000D5 RID: 213
		protected bool _ready;

		// Token: 0x040000D6 RID: 214
		protected BasePlatformManager _manager;

		// Token: 0x040000D7 RID: 215
		protected Dictionary<string, float> _stats = new Dictionary<string, float>();
	}
}
