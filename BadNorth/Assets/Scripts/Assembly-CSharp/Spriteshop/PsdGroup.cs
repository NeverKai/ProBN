using System;
using System.Collections.Generic;
using UnityEngine;

namespace Spriteshop
{
	// Token: 0x020005D1 RID: 1489
	public class PsdGroup : MonoBehaviour
	{
		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x060026C1 RID: 9921 RVA: 0x0007B75E File Offset: 0x00079B5E
		public Bounds innerBounds
		{
			get
			{
				return new Bounds(this.innerRect.center, this.innerRect.size);
			}
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x060026C2 RID: 9922 RVA: 0x0007B785 File Offset: 0x00079B85
		public Bounds outerBounds
		{
			get
			{
				return new Bounds(this.outerRect.center, this.outerRect.size);
			}
		}

		// Token: 0x060026C3 RID: 9923 RVA: 0x0007B7AC File Offset: 0x00079BAC
		[ContextMenu("SetupSplitName")]
		private void SetupSplitName()
		{
			this.splitName = base.name.Split(new char[]
			{
				' '
			});
		}

		// Token: 0x060026C4 RID: 9924 RVA: 0x0007B7CC File Offset: 0x00079BCC
		public void SetColor(Color color)
		{
			foreach (SpriteRenderer spriteRenderer in this.spriteRenderers)
			{
				spriteRenderer.color = color;
			}
		}

		// Token: 0x060026C5 RID: 9925 RVA: 0x0007B828 File Offset: 0x00079C28
		private void OnDrawGizmos()
		{
			Gizmos.matrix = base.transform.localToWorldMatrix * Matrix4x4.Scale(Vector2.one);
			Gizmos.color = Color.white.SetA(0.5f);
			Gizmos.DrawWireCube(this.innerRect.center, this.innerRect.size);
		}

		// Token: 0x060026C6 RID: 9926 RVA: 0x0007B894 File Offset: 0x00079C94
		private void OnDrawGizmosSelected()
		{
			Gizmos.matrix = base.transform.localToWorldMatrix * Matrix4x4.Scale(Vector2.one);
			Gizmos.color = Color.red;
			Gizmos.DrawWireCube(this.innerRect.center, this.innerRect.size);
			Gizmos.color = Color.blue;
			Gizmos.DrawWireCube(this.outerRect.center, this.outerRect.size);
		}

		// Token: 0x040018BA RID: 6330
		public Rect innerRect;

		// Token: 0x040018BB RID: 6331
		public Rect outerRect;

		// Token: 0x040018BC RID: 6332
		public List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();

		// Token: 0x040018BD RID: 6333
		public string[] splitName;
	}
}
