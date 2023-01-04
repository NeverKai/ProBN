using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x02000802 RID: 2050
	public class GLDebugDrawer : MonoBehaviour, IGameSetup
	{
		// Token: 0x060035AA RID: 13738 RVA: 0x000E66FF File Offset: 0x000E4AFF
		void IGameSetup.OnGameAwake()
		{
			this.material = new Material(this.shader);
			this.cam = base.GetComponent<Camera>();
		}

		// Token: 0x060035AB RID: 13739 RVA: 0x000E671E File Offset: 0x000E4B1E
		private void OnPostRender()
		{
			this.Draw();
		}

		// Token: 0x060035AC RID: 13740 RVA: 0x000E6728 File Offset: 0x000E4B28
		private void Draw()
		{
			this.material.SetPass(0);
			GL.MultMatrix((-this.cam.transform.forward * 0.1f).GetMoveMatrix());
			GL.Viewport(this.cam.pixelRect);
			GLDebugDrawer.draw();
		}

		// Token: 0x04002471 RID: 9329
		private Camera cam;

		// Token: 0x04002472 RID: 9330
		[SerializeField]
		private Shader shader;

		// Token: 0x04002473 RID: 9331
		private Material material;

		// Token: 0x04002474 RID: 9332
		public static Action draw = delegate()
		{
		};
	}
}
