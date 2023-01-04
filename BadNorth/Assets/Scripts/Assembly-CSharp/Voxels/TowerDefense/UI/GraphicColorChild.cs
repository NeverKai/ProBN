using System;
using UnityEngine;
using UnityEngine.UI;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x0200081B RID: 2075
	[RequireComponent(typeof(Graphic))]
	public class GraphicColorChild : BaseMeshEffect
	{
		// Token: 0x0600363C RID: 13884 RVA: 0x000E9CE8 File Offset: 0x000E80E8
		public override void ModifyMesh(VertexHelper vh)
		{
			if (this.type != GraphicColorParent.Type.None)
			{
				if (Application.isPlaying)
				{
					if (!this.parent)
					{
						this.parent = base.gameObject.GetComponentInParentIncludingInactive<GraphicColorParent>();
						this.color = GraphicColorParent.GetColor(this.type, this.parent);
					}
				}
				else
				{
					GraphicColorParent componentInParentIncludingInactive = base.gameObject.GetComponentInParentIncludingInactive<GraphicColorParent>();
					this.color = GraphicColorParent.GetColor(this.type, componentInParentIncludingInactive);
				}
			}
			int i = 0;
			int currentVertCount = vh.currentVertCount;
			while (i < currentVertCount)
			{
				UIVertex vertex = default(UIVertex);
				vh.PopulateUIVertex(ref vertex, i);
				vertex.color *= this.color;
				vh.SetUIVertex(vertex, i);
				i++;
			}
		}

		// Token: 0x0600363D RID: 13885 RVA: 0x000E9DBC File Offset: 0x000E81BC
		public void SetType(GraphicColorParent.Type type)
		{
			this.type = type;
			Graphic component = base.GetComponent<Graphic>();
			if (component)
			{
				component.SetAllDirty();
			}
		}

		// Token: 0x0600363E RID: 13886 RVA: 0x000E9DE8 File Offset: 0x000E81E8
		public void SetColor(Color color)
		{
			this.color = color;
			this.SetType(GraphicColorParent.Type.None);
		}

		// Token: 0x0600363F RID: 13887 RVA: 0x000E9DF8 File Offset: 0x000E81F8
		protected override void OnTransformParentChanged()
		{
			base.OnTransformParentChanged();
			this.parent = null;
		}

		// Token: 0x040024CD RID: 9421
		[SerializeField]
		private GraphicColorParent.Type type;

		// Token: 0x040024CE RID: 9422
		private GraphicColorParent parent;

		// Token: 0x040024CF RID: 9423
		private Color color;
	}
}
