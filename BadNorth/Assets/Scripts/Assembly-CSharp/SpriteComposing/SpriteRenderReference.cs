using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace SpriteComposing
{
	// Token: 0x020005CF RID: 1487
	public class SpriteRenderReference : MonoBehaviour, ISpritePart
	{
		// Token: 0x060026BC RID: 9916 RVA: 0x0007B5B0 File Offset: 0x000799B0
		void ISpritePart.Draw(CommandBuffer buffer, Matrix4x4 matrix)
		{
			float num = this.width * (float)this.count / (float)(this.count + this.extraCount);
			for (int i = 0; i < this.count + this.extraCount; i++)
			{
				Matrix4x4 rhs = Matrix4x4.Translate(new Vector3(-(((float)i + this.offset) / (float)this.count) * num, 0f, 0f));
				ComposedSprite.RecursiveDraw(this.reference, buffer, matrix * base.transform.localToWorldMatrix * rhs * this.reference.worldToLocalMatrix);
			}
		}

		// Token: 0x060026BD RID: 9917 RVA: 0x0007B658 File Offset: 0x00079A58
		private void OnDrawGizmos()
		{
			Gizmos.matrix = base.transform.localToWorldMatrix;
			float num = this.width * (float)this.count / (float)(this.count + this.extraCount);
			for (int i = 0; i < this.count; i++)
			{
				ExtraGizmos.DrawCircleZ(new Vector3(-(((float)i + this.offset) / (float)this.count) * num, 0f, 0f), 0.1f, 8);
			}
		}

		// Token: 0x040018B2 RID: 6322
		[SerializeField]
		private Transform reference;

		// Token: 0x040018B3 RID: 6323
		[SerializeField]
		private int count;

		// Token: 0x040018B4 RID: 6324
		[SerializeField]
		private float width = 1f;

		// Token: 0x040018B5 RID: 6325
		[SerializeField]
		private float offset = 0.5f;

		// Token: 0x040018B6 RID: 6326
		[SerializeField]
		public int extraCount;
	}
}
