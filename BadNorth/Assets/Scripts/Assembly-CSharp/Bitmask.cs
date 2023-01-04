using System;

// Token: 0x020004F2 RID: 1266
public class Bitmask
{
	// Token: 0x06002060 RID: 8288 RVA: 0x00057364 File Offset: 0x00055764
	public Bitmask(int capacity)
	{
		this.longs = new ulong[capacity];
		for (int i = 0; i < this.longs.Length; i++)
		{
			this.longs[i] = 0UL;
		}
	}

	// Token: 0x17000444 RID: 1092
	// (get) Token: 0x06002061 RID: 8289 RVA: 0x000573A6 File Offset: 0x000557A6
	private int capacity
	{
		get
		{
			return this.longs.Length;
		}
	}

	// Token: 0x06002062 RID: 8290 RVA: 0x000573B0 File Offset: 0x000557B0
	private ulong GetPower2(int key)
	{
		return 1UL << key;
	}

	// Token: 0x06002063 RID: 8291 RVA: 0x000573BC File Offset: 0x000557BC
	public static bool SameAs(Bitmask a, Bitmask b)
	{
		for (int i = 0; i < a.longs.Length; i++)
		{
			if (a.longs[i] != b.longs[i])
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06002064 RID: 8292 RVA: 0x000573FC File Offset: 0x000557FC
	public bool SameAs(Bitmask b)
	{
		for (int i = 0; i < this.longs.Length; i++)
		{
			if (this.longs[i] != b.longs[i])
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06002065 RID: 8293 RVA: 0x0005743C File Offset: 0x0005583C
	public static void Copy(Bitmask src, Bitmask dst)
	{
		for (int i = 0; i < src.longs.Length; i++)
		{
			dst.longs[i] = src.longs[i];
		}
	}

	// Token: 0x06002066 RID: 8294 RVA: 0x00057474 File Offset: 0x00055874
	public bool Contains(int key)
	{
		int num = key / 64;
		int key2 = key - num * 64;
		ulong power = this.GetPower2(key2);
		return (this.longs[num] & power) > 0UL;
	}

	// Token: 0x06002067 RID: 8295 RVA: 0x000574A4 File Offset: 0x000558A4
	public void And(int key)
	{
		int num = key / 64;
		int key2 = key - num * 64;
		ulong power = this.GetPower2(key2);
		this.longs[num] = (this.longs[num] & power);
	}

	// Token: 0x06002068 RID: 8296 RVA: 0x000574D8 File Offset: 0x000558D8
	public void Xor(int key)
	{
		int num = key / 64;
		int key2 = key - num * 64;
		ulong power = this.GetPower2(key2);
		this.longs[num] = (this.longs[num] ^ power);
	}

	// Token: 0x06002069 RID: 8297 RVA: 0x0005750C File Offset: 0x0005590C
	public void Or(int key)
	{
		int num = key / 64;
		int key2 = key - num * 64;
		ulong power = this.GetPower2(key2);
		this.longs[num] = (this.longs[num] | power);
	}

	// Token: 0x0400141C RID: 5148
	private ulong[] longs;

	// Token: 0x0400141D RID: 5149
	private const int size = 64;
}
