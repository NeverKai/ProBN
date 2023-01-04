using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000822 RID: 2082
public class UITextUV2 : MonoBehaviour, IMeshModifier
{
	// Token: 0x0600365A RID: 13914 RVA: 0x000EA250 File Offset: 0x000E8650
	void IMeshModifier.ModifyMesh(VertexHelper vh)
	{
		Vector2 v = Vector2.one * 3f;
		int currentVertCount = vh.currentVertCount;
		for (int i = 0; i < 4; i++)
		{
			for (int j = 0; j < currentVertCount; j++)
			{
				UIVertex v2 = default(UIVertex);
				vh.PopulateUIVertex(ref v2, j);
				v2.uv1 = new Vector2((float)((j + 1) / 2 % 2), (float)((j / 2 + 1) % 2));
				v2.color.a = 0;
				v2.position += v;
				vh.AddVert(v2);
			}
			v = new Vector2(-v.y, v.x);
		}
		for (int k = 0; k < 4; k++)
		{
			for (int l = 0; l < currentVertCount; l += 4)
			{
				int num = (k + 1) * currentVertCount + l;
				vh.AddTriangle(num, num + 1, num + 2);
				vh.AddTriangle(num + 3, num, num + 2);
			}
		}
	}

	// Token: 0x0600365B RID: 13915 RVA: 0x000EA35F File Offset: 0x000E875F
	void IMeshModifier.ModifyMesh(Mesh mesh)
	{
	}
}
