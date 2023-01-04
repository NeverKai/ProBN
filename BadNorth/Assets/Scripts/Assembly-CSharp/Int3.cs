using System;

// Token: 0x020005E4 RID: 1508
[Serializable]
public struct Int3
{
	// Token: 0x0600271A RID: 10010 RVA: 0x0007DD75 File Offset: 0x0007C175
	public Int3(int x, int y, int z)
	{
		this = default(Int3);
		this.x = x;
		this.y = y;
		this.z = z;
	}

	// Token: 0x17000544 RID: 1348
	// (get) Token: 0x0600271B RID: 10011 RVA: 0x0007DD93 File Offset: 0x0007C193
	public int length
	{
		get
		{
			return 3;
		}
	}

	// Token: 0x17000545 RID: 1349
	private int this[int index]
	{
		get
		{
			switch (index)
			{
			case 0:
				return this.x;
			case 1:
				return this.y;
			case 2:
				return this.z;
			default:
				throw new IndexOutOfRangeException();
			}
		}
		set
		{
			switch (index)
			{
			case 0:
				this.x = value;
				break;
			case 1:
				this.y = value;
				break;
			case 2:
				this.z = value;
				break;
			default:
				throw new IndexOutOfRangeException();
			}
		}
	}

	// Token: 0x0600271E RID: 10014 RVA: 0x0007DE1C File Offset: 0x0007C21C
	public static bool operator ==(Int3 a, Int3 b)
	{
		return a.x.Equals(b.x) && a.y.Equals(b.y) && a.z.Equals(b.z);
	}

	// Token: 0x0600271F RID: 10015 RVA: 0x0007DE6F File Offset: 0x0007C26F
	public static bool operator !=(Int3 a, Int3 b)
	{
		return !(a == b);
	}

	// Token: 0x06002720 RID: 10016 RVA: 0x0007DE7B File Offset: 0x0007C27B
	public override int GetHashCode()
	{
		return this.x.GetHashCode() * 107 + this.y.GetHashCode() * 199 + this.z.GetHashCode();
	}

	// Token: 0x06002721 RID: 10017 RVA: 0x0007DEBB File Offset: 0x0007C2BB
	public override bool Equals(object obj)
	{
		return obj != null && obj is Int3 && this == (Int3)obj;
	}

	// Token: 0x04001908 RID: 6408
	public int x;

	// Token: 0x04001909 RID: 6409
	public int y;

	// Token: 0x0400190A RID: 6410
	public int z;
}
