using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000823 RID: 2083
public class UIVertexListGraphic : MaskableGraphic
{
	// Token: 0x0600365D RID: 13917 RVA: 0x000EA37F File Offset: 0x000E877F
	public void Clear()
	{
		this.verts.Clear();
		this.tris.Clear();
		this.SetVerticesDirty();
	}

	// Token: 0x0600365E RID: 13918 RVA: 0x000EA39D File Offset: 0x000E879D
	protected override void OnPopulateMesh(VertexHelper vh)
	{
		vh.Clear();
		vh.AddUIVertexStream(this.verts, this.tris);
	}

	// Token: 0x0600365F RID: 13919 RVA: 0x000EA3B7 File Offset: 0x000E87B7
	protected override void UpdateMaterial()
	{
		base.UpdateMaterial();
		base.canvasRenderer.SetTexture(this.texture);
		base.canvasRenderer.SetColor(this.color);
	}

	// Token: 0x040024E0 RID: 9440
	[NonSerialized]
	public List<UIVertex> verts = new List<UIVertex>();

	// Token: 0x040024E1 RID: 9441
	[NonSerialized]
	public List<int> tris = new List<int>();

	// Token: 0x040024E2 RID: 9442
	public Texture texture;
}
