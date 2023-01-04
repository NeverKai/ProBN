using System;

// Token: 0x020005E3 RID: 1507
[Serializable]
public struct Int2
{
	// Token: 0x06002712 RID: 10002 RVA: 0x0007DC75 File Offset: 0x0007C075
	public Int2(int x, int y)
	{
		this = default(Int2);
		this.x = x;
		this.y = y;
	}

	// Token: 0x17000542 RID: 1346
	// (get) Token: 0x06002713 RID: 10003 RVA: 0x0007DC8C File Offset: 0x0007C08C
	public int length
	{
		get
		{
			return 2;
		}
	}

	// Token: 0x17000543 RID: 1347
	private int this[int index]
	{
		get
		{
			if (index == 0)
			{
				return this.x;
			}
			if (index != 1)
			{
				throw new IndexOutOfRangeException();
			}
			return this.y;
		}
		set
		{
			if (index != 0)
			{
				if (index != 1)
				{
					throw new IndexOutOfRangeException();
				}
				this.y = value;
			}
			else
			{
				this.x = value;
			}
		}
	}

	// Token: 0x06002716 RID: 10006 RVA: 0x0007DCE8 File Offset: 0x0007C0E8
	public static bool operator ==(Int2 a, Int2 b)
	{
		return a.x.Equals(b.x) && a.y.Equals(b.y);
	}

	// Token: 0x06002717 RID: 10007 RVA: 0x0007DD18 File Offset: 0x0007C118
	public static bool operator !=(Int2 a, Int2 b)
	{
		return !(a == b);
	}

	// Token: 0x06002718 RID: 10008 RVA: 0x0007DD24 File Offset: 0x0007C124
	public override int GetHashCode()
	{
		return this.x.GetHashCode() * 199 + this.y.GetHashCode();
	}

	// Token: 0x06002719 RID: 10009 RVA: 0x0007DD4F File Offset: 0x0007C14F
	public override bool Equals(object obj)
	{
		return obj != null && obj is Int2 && this == (Int2)obj;
	}

	// Token: 0x04001906 RID: 6406
	public int x;

	// Token: 0x04001907 RID: 6407
	public int y;
}
