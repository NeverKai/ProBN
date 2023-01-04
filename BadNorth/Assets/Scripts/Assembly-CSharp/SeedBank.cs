using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000671 RID: 1649
public class SeedBank : MonoBehaviour
{
	// Token: 0x06002A08 RID: 10760 RVA: 0x00096257 File Offset: 0x00094657
	public void AddLevel(string key, int seed, float score)
	{
	}

	// Token: 0x06002A09 RID: 10761 RVA: 0x0009625C File Offset: 0x0009465C
	public int GetSeed(string key)
	{
		int i = 0;
		while (i < this.vaults.Count)
		{
			if (this.vaults[i].key == key)
			{
				if (this.vaults[i].boxes.Count == 0)
				{
					return UnityEngine.Random.Range(int.MinValue, int.MaxValue);
				}
				return this.vaults[i].boxes[UnityEngine.Random.Range(0, this.vaults[i].boxes.Count)].seed;
			}
			else
			{
				i++;
			}
		}
		return UnityEngine.Random.Range(int.MinValue, int.MaxValue);
	}

	// Token: 0x06002A0A RID: 10762 RVA: 0x00096314 File Offset: 0x00094714
	public int GetSeed(string key, int index)
	{
		SeedBank.Vault vault = this.GetVault(key);
		if (vault == null)
		{
			return UnityEngine.Random.Range(int.MinValue, int.MaxValue);
		}
		if (vault.boxes.Count == 0)
		{
			return UnityEngine.Random.Range(int.MinValue, int.MaxValue);
		}
		if (index < vault.boxes.Count)
		{
			return vault.boxes[index].seed;
		}
		return UnityEngine.Random.Range(int.MinValue, int.MaxValue);
	}

	// Token: 0x06002A0B RID: 10763 RVA: 0x00096394 File Offset: 0x00094794
	public void ReportBroken(string key, int seed)
	{
		SeedBank.Vault vault = this.GetVault(key);
		if (vault == null)
		{
			return;
		}
		for (int i = 0; i < vault.boxes.Count; i++)
		{
			if (vault.boxes[i].seed == seed)
			{
				vault.boxes.RemoveAt(i);
				return;
			}
		}
	}

	// Token: 0x06002A0C RID: 10764 RVA: 0x000963F0 File Offset: 0x000947F0
	public SeedBank.Vault GetVault(string key)
	{
		for (int i = 0; i < this.vaults.Count; i++)
		{
			if (key == this.vaults[i].key)
			{
				return this.vaults[i];
			}
		}
		return null;
	}

	// Token: 0x06002A0D RID: 10765 RVA: 0x00096444 File Offset: 0x00094844
	[ContextMenu("OnValidate()")]
	private void OnValidate()
	{
		for (int i = 0; i < this.vaults.Count; i++)
		{
			for (int j = 0; j < this.vaults[i].boxes.Count; j++)
			{
				this.vaults[i].boxes[j].UpdateText();
			}
		}
	}

	// Token: 0x06002A0E RID: 10766 RVA: 0x000964B0 File Offset: 0x000948B0
	private void Update()
	{
	}

	// Token: 0x04001B6E RID: 7022
	public List<SeedBank.Vault> vaults = new List<SeedBank.Vault>();

	// Token: 0x02000672 RID: 1650
	[Serializable]
	public class Vault
	{
		// Token: 0x06002A0F RID: 10767 RVA: 0x000964B2 File Offset: 0x000948B2
		public Vault(string key)
		{
			this.key = key;
			this.maxSize = 12;
		}

		// Token: 0x04001B6F RID: 7023
		public string key;

		// Token: 0x04001B70 RID: 7024
		public int maxSize = 12;

		// Token: 0x04001B71 RID: 7025
		public List<SeedBank.Box> boxes = new List<SeedBank.Box>();
	}

	// Token: 0x02000673 RID: 1651
	[Serializable]
	public class Box
	{
		// Token: 0x06002A10 RID: 10768 RVA: 0x000964DC File Offset: 0x000948DC
		public Box(int seed, float score)
		{
			this.seed = seed;
			this.score = score;
			this.UpdateText();
		}

		// Token: 0x06002A11 RID: 10769 RVA: 0x000964F8 File Offset: 0x000948F8
		public void UpdateText()
		{
			this.text = string.Concat(new object[]
			{
				"Score: ",
				this.score.ToString("F2"),
				", Seed: ",
				this.seed
			});
		}

		// Token: 0x04001B72 RID: 7026
		[HideInInspector]
		public string text;

		// Token: 0x04001B73 RID: 7027
		public int seed;

		// Token: 0x04001B74 RID: 7028
		public float score;
	}
}
