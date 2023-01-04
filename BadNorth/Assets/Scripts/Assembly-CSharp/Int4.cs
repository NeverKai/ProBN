using System;

// Token: 0x020005E5 RID: 1509
[Serializable]
public struct Int4
{
	// Token: 0x06002722 RID: 10018 RVA: 0x0007DEE1 File Offset: 0x0007C2E1
	public Int4(int x, int y, int z, int w)
	{
		this = default(Int4);
		this.x = x;
		this.y = y;
		this.z = z;
		this.w = w;
	}

	// Token: 0x17000546 RID: 1350
	// (get) Token: 0x06002723 RID: 10019 RVA: 0x0007DF07 File Offset: 0x0007C307
	public int length
	{
		get
		{
			return 4;
		}
	}

	// Token: 0x17000547 RID: 1351
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
			case 3:
				return this.w;
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
			case 3:
				this.w = value;
				break;
			default:
				throw new IndexOutOfRangeException();
			}
		}
	}

	// Token: 0x06002726 RID: 10022 RVA: 0x0007DFA8 File Offset: 0x0007C3A8
	public static bool operator ==(Int4 a, Int4 b)
	{
		return a.x.Equals(b.x) && a.y.Equals(b.y) && a.z.Equals(b.z) && a.w.Equals(b.w);
	}

	// Token: 0x06002727 RID: 10023 RVA: 0x0007E013 File Offset: 0x0007C413
	public static bool operator !=(Int4 a, Int4 b)
	{
		return !(a == b);
	}

	// Token: 0x06002728 RID: 10024 RVA: 0x0007E020 File Offset: 0x0007C420
	public override int GetHashCode()
	{
		return this.x.GetHashCode() * 107 + this.y.GetHashCode() * 199 + this.z.GetHashCode() + this.w.GetHashCode() * 47;
	}

	// Token: 0x06002729 RID: 10025 RVA: 0x0007E080 File Offset: 0x0007C480
	public override bool Equals(object obj)
	{
		return obj != null && obj is Int4 && this == (Int4)obj;
	}

	// Token: 0x0400190B RID: 6411
	public int x;

	// Token: 0x0400190C RID: 6412
	public int y;

	// Token: 0x0400190D RID: 6413
	public int z;

	// Token: 0x0400190E RID: 6414
	public int w;
}
