using System;
using UnityEngine.UI;

// Token: 0x0200081A RID: 2074
public class EmptyGraphic : Graphic
{
	// Token: 0x06003639 RID: 13881 RVA: 0x000E9CD6 File Offset: 0x000E80D6
	protected override void OnPopulateMesh(VertexHelper vh)
	{
		vh.Clear();
	}

	// Token: 0x0600363A RID: 13882 RVA: 0x000E9CDE File Offset: 0x000E80DE
	protected override void UpdateMaterial()
	{
	}
}
