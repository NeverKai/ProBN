using System;
using System.Collections;
using System.Collections.Generic;
using RTM.OnScreenDebug;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x02000527 RID: 1319
	public class AudioEventBuffer : MonoBehaviour, IslandGameplayManager.IAwake, IslandGameplayManager.ISetupIslandCoroutine, IslandGameplayManager.IWipeIsland
	{
		// Token: 0x06002224 RID: 8740 RVA: 0x000629D7 File Offset: 0x00060DD7
		void IslandGameplayManager.IAwake.OnAwake(IslandGameplayManager manager)
		{
			base.enabled = false;
			this.nameCache = base.name;
		}

		// Token: 0x06002225 RID: 8741 RVA: 0x000629EC File Offset: 0x00060DEC
		IEnumerator IslandGameplayManager.ISetupIslandCoroutine.OnSetup(Island island)
		{
			if (this.pool == null)
			{
				this.removeNulls = ((GameObject g) => !g);
				this.pool = new List<AudioEventBuffer.EventData>(this.poolSize);
				this.inUse = new List<AudioEventBuffer.EventData>(this.poolSize);
				this.nameLookup = new Dictionary<string, FabricEventReference>(this.nameLookupSize);
				this.prefixSuffixLookup = new Dictionary<string, Dictionary<string, FabricEventReference>>(this.prefixLookupSize);
				for (int i = 0; i < this.poolSize; i++)
				{
					yield return null;
					this.pool.Add(new AudioEventBuffer.EventData(this.maxRequestersPerEvent));
				}
			}
			base.enabled = true;
			yield break;
		}

		// Token: 0x06002226 RID: 8742 RVA: 0x00062A08 File Offset: 0x00060E08
		void IslandGameplayManager.IWipeIsland.OnWipe(Island island)
		{
			foreach (AudioEventBuffer.EventData eventData in this.inUse)
			{
				eventData.Clear();
				this.pool.Add(eventData);
			}
			this.inUse.Clear();
			base.enabled = false;
		}

		// Token: 0x06002227 RID: 8743 RVA: 0x00062A84 File Offset: 0x00060E84
		private void Update()
		{
			int frameCount = Time.frameCount;
			int num = 0;
			int b = FabricPoolMonitor.availableVoices - this.minFabricPoolAvailablity;
			int num2 = Mathf.Min(this.maxSoundsPerFrame, b);
			using ("SortSounds")
			{
				this.inUse.Sort();
			}
			using ("PostSounds")
			{
				foreach (AudioEventBuffer.EventData eventData in this.inUse)
				{
					if (num >= num2)
					{
						break;
					}
					List<GameObject> gameObjects = eventData.gameObjects;
					gameObjects.RemoveAll(this.removeNulls);
					if (gameObjects.Count != 0)
					{
						int pseudorandomIndex = AudioEventBuffer.GetPseudorandomIndex(eventData.gameObjects.Count);
						GameObject parentGameObject = gameObjects[pseudorandomIndex];
						gameObjects.RemoveAt(pseudorandomIndex);
						eventData.playFrame = frameCount;
						num++;
						FabricWrapper.PostEvent(eventData.id, parentGameObject);
					}
				}
			}
			using ("RemoveSounds")
			{
				for (int i = this.inUse.Count - 1; i >= 0; i--)
				{
					AudioEventBuffer.EventData eventData2 = this.inUse[i];
					if (frameCount >= eventData2.reqFrame + this.expiryFrames)
					{
						if (eventData2.playFrame == 0)
						{
						}
						eventData2.gameObjects.Clear();
					}
					int count = eventData2.gameObjects.Count;
					if (count > 1)
					{
						int num3 = Mathf.FloorToInt((float)count * 0.5f);
						eventData2.gameObjects.RemoveRange(num3, count - num3);
					}
					if (this.inUse[i].gameObjects.Count == 0)
					{
						eventData2.Clear();
						this.inUse.RemoveAt(i);
						this.pool.Add(eventData2);
					}
				}
			}
		}

		// Token: 0x06002228 RID: 8744 RVA: 0x00062CD4 File Offset: 0x000610D4
		private AudioEventBuffer.EventData GetData(FabricEventReference eventRef)
		{
			AudioEventBuffer.EventData result;
			using ("GetData")
			{
				int num = (eventRef == null) ? 0 : eventRef.id;
				if (num == 0)
				{
					result = null;
				}
				else
				{
					foreach (AudioEventBuffer.EventData eventData in this.inUse)
					{
						if (eventData.id == num)
						{
							return eventData;
						}
					}
					if (this.pool.Count > 0)
					{
						int index = this.pool.Count - 1;
						AudioEventBuffer.EventData eventData2 = this.pool[index];
						eventData2.id = num;
						this.pool.RemoveAt(index);
						this.inUse.Add(eventData2);
						result = eventData2;
					}
					else
					{
						result = null;
					}
				}
			}
			return result;
		}

		// Token: 0x06002229 RID: 8745 RVA: 0x00062DEC File Offset: 0x000611EC
		public void RequestEvent(FabricEventReference eventRef, GameObject gameObject)
		{
			using ("AudioEventBuffer.RequestEvent")
			{
				AudioEventBuffer.EventData data = this.GetData(eventRef);
				if (data != null)
				{
					List<GameObject> gameObjects = data.gameObjects;
					data.reqFrame = Time.frameCount;
					if (gameObjects.Capacity > data.gameObjects.Count)
					{
						gameObjects.Add(gameObject);
					}
					else
					{
						gameObjects[AudioEventBuffer.GetPseudorandomIndex(gameObjects.Count)] = gameObject;
					}
				}
			}
		}

		// Token: 0x0600222A RID: 8746 RVA: 0x00062E7C File Offset: 0x0006127C
		public void RequestEvent(string eventName, GameObject gameObject)
		{
			using ("AudioEventBuffer.RequestEventByName")
			{
				FabricEventReference fabricEventReference = null;
				if (!this.nameLookup.TryGetValue(eventName, out fabricEventReference))
				{
					fabricEventReference = eventName;
					this.nameLookup.Add(eventName, fabricEventReference);
				}
				this.RequestEvent(fabricEventReference, gameObject);
			}
		}

		// Token: 0x0600222B RID: 8747 RVA: 0x00062EE8 File Offset: 0x000612E8
		public void RequestEvent(string namePrefix, string nameSuffix, GameObject gameObject)
		{
			using ("AudioEventBuffer.RequestEventByPrefix+Suffix")
			{
				Dictionary<string, FabricEventReference> dictionary = null;
				if (!this.prefixSuffixLookup.TryGetValue(namePrefix, out dictionary))
				{
					dictionary = new Dictionary<string, FabricEventReference>(this.suffixLookupSize);
					this.prefixSuffixLookup.Add(namePrefix, dictionary);
				}
				FabricEventReference fabricEventReference = null;
				if (!dictionary.TryGetValue(nameSuffix, out fabricEventReference))
				{
					fabricEventReference = string.Format("{0}/{1}", namePrefix, nameSuffix);
					dictionary.Add(nameSuffix, fabricEventReference);
				}
				this.RequestEvent(fabricEventReference, gameObject);
			}
		}

		// Token: 0x0600222C RID: 8748 RVA: 0x00062F84 File Offset: 0x00061384
		public static int GetPseudorandomIndex(int count)
		{
			if (count == 0)
			{
				return -1;
			}
			if (count != 1)
			{
				AudioEventBuffer.prIdx = (AudioEventBuffer.prIdx + 9941) % count;
				return AudioEventBuffer.prIdx;
			}
			return 0;
		}

		// Token: 0x040014D5 RID: 5333
		private DebugChannelGroup dbgGroup = new DebugChannelGroup("AudioEventBuffer", EVerbosity.Quiet, 300);

		// Token: 0x040014D6 RID: 5334
		[SerializeField]
		[Header("Pool Sizes")]
		private int poolSize = 8;

		// Token: 0x040014D7 RID: 5335
		[SerializeField]
		private int maxRequestersPerEvent = 8;

		// Token: 0x040014D8 RID: 5336
		[SerializeField]
		private int nameLookupSize = 8;

		// Token: 0x040014D9 RID: 5337
		[SerializeField]
		private int prefixLookupSize = 16;

		// Token: 0x040014DA RID: 5338
		[SerializeField]
		private int suffixLookupSize = 8;

		// Token: 0x040014DB RID: 5339
		[SerializeField]
		[Header("Limits")]
		private int maxSoundsPerFrame = 2;

		// Token: 0x040014DC RID: 5340
		[SerializeField]
		private int expiryFrames = 3;

		// Token: 0x040014DD RID: 5341
		[SerializeField]
		private int minFabricPoolAvailablity;

		// Token: 0x040014DE RID: 5342
		private List<AudioEventBuffer.EventData> pool;

		// Token: 0x040014DF RID: 5343
		private List<AudioEventBuffer.EventData> inUse;

		// Token: 0x040014E0 RID: 5344
		private Predicate<GameObject> removeNulls;

		// Token: 0x040014E1 RID: 5345
		private Dictionary<string, FabricEventReference> nameLookup;

		// Token: 0x040014E2 RID: 5346
		private Dictionary<string, Dictionary<string, FabricEventReference>> prefixSuffixLookup;

		// Token: 0x040014E3 RID: 5347
		private string nameCache;

		// Token: 0x040014E4 RID: 5348
		private static int prIdx;

		// Token: 0x040014E5 RID: 5349
		private const int bigPrime = 9941;

		// Token: 0x02000528 RID: 1320
		private class EventData : IComparable<AudioEventBuffer.EventData>
		{
			// Token: 0x0600222E RID: 8750 RVA: 0x00062FB5 File Offset: 0x000613B5
			public EventData(int objectCount)
			{
				this.gameObjects = new List<GameObject>(objectCount);
				this.Clear();
			}

			// Token: 0x0600222F RID: 8751 RVA: 0x00062FCF File Offset: 0x000613CF
			public void Clear()
			{
				this.id = 0;
				this.reqFrame = 0;
				this.playFrame = 0;
				this.gameObjects.Clear();
			}

			// Token: 0x06002230 RID: 8752 RVA: 0x00062FF4 File Offset: 0x000613F4
			int IComparable<AudioEventBuffer.EventData>.CompareTo(AudioEventBuffer.EventData other)
			{
				int num = this.playFrame.CompareTo(other.playFrame);
				return (num != 0) ? num : this.reqFrame.CompareTo(other.reqFrame);
			}

			// Token: 0x040014E6 RID: 5350
			public int id;

			// Token: 0x040014E7 RID: 5351
			public int reqFrame;

			// Token: 0x040014E8 RID: 5352
			public int playFrame;

			// Token: 0x040014E9 RID: 5353
			public List<GameObject> gameObjects;
		}
	}
}
