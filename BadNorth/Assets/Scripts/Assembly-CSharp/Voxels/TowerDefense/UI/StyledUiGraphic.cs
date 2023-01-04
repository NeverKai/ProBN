using System;
using UnityEngine;
using UnityEngine.UI;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x02000820 RID: 2080
	[RequireComponent(typeof(Graphic))]
	public class StyledUiGraphic : BaseMeshEffect
	{
		// Token: 0x06003653 RID: 13907 RVA: 0x000EA120 File Offset: 0x000E8520
		public override void ModifyMesh(VertexHelper vh)
		{
			if (!this.colorObject)
			{
				return;
			}
			int i = 0;
			int currentVertCount = vh.currentVertCount;
			while (i < currentVertCount)
			{
				UIVertex vertex = default(UIVertex);
				vh.PopulateUIVertex(ref vertex, i);
				vertex.color *= this.colorObject.color;
				vh.SetUIVertex(vertex, i);
				i++;
			}
		}

		// Token: 0x040024DD RID: 9437
		[SerializeField]
		private ColorObject colorObject;
	}
}
