using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000676 RID: 1654
public class StraightMarcher
{
	// Token: 0x06002A22 RID: 10786 RVA: 0x00096678 File Offset: 0x00094A78
	public StraightMarcher(Vector3 extents)
	{
		int num = (int)(-(int)extents.x);
		while ((float)num <= extents.x)
		{
			int num2 = (int)(-(int)extents.y);
			while ((float)num2 <= extents.y)
			{
				int num3 = (int)(-(int)extents.z);
				while ((float)num3 <= extents.z)
				{
					bool flag = false;
					if (num != 0 && (float)Mathf.Abs(num) == extents.x)
					{
						flag = true;
					}
					else if (num2 != 0 && (float)Mathf.Abs(num2) == extents.y)
					{
						flag = true;
					}
					else if (num3 != 0 && (float)Mathf.Abs(num3) == extents.z)
					{
						flag = true;
					}
					if (flag)
					{
						this.rays.Add(new StraightMarcher.Ray(new Vector3Int(num, num2, num3)));
					}
					num3++;
				}
				num2++;
			}
			num++;
		}
	}

	// Token: 0x06002A23 RID: 10787 RVA: 0x0009677C File Offset: 0x00094B7C
	public void Bend(float bend)
	{
		for (int i = 0; i < this.rays.Count; i++)
		{
			for (int j = 0; j < this.rays[i].pos.Length; j++)
			{
				Vector3Int[] pos = this.rays[i].pos;
				int num = j;
				pos[num].y = pos[num].y + Mathf.RoundToInt(this.rays[i].pos[j].magnitude * bend);
			}
		}
	}

	// Token: 0x04001B7B RID: 7035
	public List<StraightMarcher.Ray> rays = new List<StraightMarcher.Ray>();

	// Token: 0x02000677 RID: 1655
	private class Node
	{
		// Token: 0x06002A24 RID: 10788 RVA: 0x0009680F File Offset: 0x00094C0F
		public Node(Vector3Int pos)
		{
			this.pos = pos;
		}

		// Token: 0x04001B7C RID: 7036
		public Vector3Int pos;
	}

	// Token: 0x02000678 RID: 1656
	public class Ray
	{
		// Token: 0x06002A25 RID: 10789 RVA: 0x00096820 File Offset: 0x00094C20
		public Ray(Vector3Int endPos)
		{
			int manhattanMagnitude = endPos.GetManhattanMagnitude();
			this.pos = new Vector3Int[manhattanMagnitude + 1];
			for (int i = 0; i < manhattanMagnitude + 1; i++)
			{
				this.pos[i] = ExtraMath.RoundToInt(Vector3.Lerp(Vector3.zero, endPos, (float)i / (float)manhattanMagnitude));
			}
		}

		// Token: 0x04001B7D RID: 7037
		public Vector3Int[] pos;
	}
}
