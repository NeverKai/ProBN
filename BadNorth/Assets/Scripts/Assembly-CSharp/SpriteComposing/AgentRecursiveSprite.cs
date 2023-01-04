using System;
using UnityEngine;
using UnityEngine.Rendering;
using Voxels.TowerDefense.HeroGeneration;

namespace SpriteComposing
{
	// Token: 0x020005C3 RID: 1475
	public class AgentRecursiveSprite : MonoBehaviour, ISpritePart
	{
		// Token: 0x06002687 RID: 9863 RVA: 0x00079FF8 File Offset: 0x000783F8
		void ISpritePart.Draw(CommandBuffer buffer, Matrix4x4 m)
		{
			if (!this.monoHero)
			{
				this.monoHero = base.gameObject.GetComponentInParentIncludingInactive<MonoHero>();
			}
			buffer.DrawMesh(MeshPrimitives.quad, m * base.transform.localToWorldMatrix, this.material, 0, 0, this.monoHero.agentBlock);
		}

		// Token: 0x04001887 RID: 6279
		[SerializeField]
		private Material material;

		// Token: 0x04001888 RID: 6280
		private MonoHero monoHero;
	}
}
