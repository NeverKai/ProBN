using System;
using UnityEngine;

// Token: 0x0200087E RID: 2174
public class VoxelField : MonoBehaviour
{
	// Token: 0x060038F3 RID: 14579 RVA: 0x000F7894 File Offset: 0x000F5C94
	public VoxelField.Type GetType(Vector3 v)
	{
		int num = 0;
		if (v.x % 2f == 0f)
		{
			num++;
		}
		if (v.y % 2f == 0f)
		{
			num++;
		}
		if (v.z % 2f == 0f)
		{
			num++;
		}
		return (VoxelField.Type)num;
	}

	// Token: 0x060038F4 RID: 14580 RVA: 0x000F78F8 File Offset: 0x000F5CF8
	private void OnDrawGizmosSelected()
	{
		Gizmos.matrix = base.transform.localToWorldMatrix;
		Vector3 vector = Vector3.one * 6f;
		int num = 0;
		while ((float)num < vector.x)
		{
			int num2 = 0;
			while ((float)num2 < vector.y)
			{
				int num3 = 0;
				while ((float)num3 < vector.z)
				{
					Vector3 vector2 = new Vector3((float)num, (float)num2, (float)num3);
					VoxelField.Type type = this.GetType(vector2);
					Gizmos.color = Color.HSVToRGB((float)type / 4f, 1f, 1f);
					Gizmos.DrawWireCube(vector2, Vector3.one * 0.3f);
					num3++;
				}
				num2++;
			}
			num++;
		}
	}

	// Token: 0x0200087F RID: 2175
	public enum Type
	{
		// Token: 0x040026E7 RID: 9959
		Box,
		// Token: 0x040026E8 RID: 9960
		Side,
		// Token: 0x040026E9 RID: 9961
		Edge,
		// Token: 0x040026EA RID: 9962
		Corner
	}
}
