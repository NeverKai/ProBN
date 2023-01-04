using System;
using ReflexCLI.Attributes;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x0200086F RID: 2159
	public class IslandStyle : MonoBehaviour
	{
		// Token: 0x0600389A RID: 14490 RVA: 0x000F49FB File Offset: 0x000F2DFB
		private void OnValidate()
		{
			if (Application.isPlaying)
			{
				this.ApplyShaderConstants();
			}
		}

		// Token: 0x0600389B RID: 14491 RVA: 0x000F4A0D File Offset: 0x000F2E0D
		[ContextMenu("Apply")]
		[ConsoleCommand("")]
		public void ApplyShaderConstants()
		{
			Shader.SetGlobalVector("_UpColor", this.upColor);
			Shader.SetGlobalVector("_TreeColor", this.treeColor);
		}

		// Token: 0x0400267F RID: 9855
		[Space]
		[SerializeField]
		private Color upColor = Color.yellow;

		// Token: 0x04002680 RID: 9856
		[SerializeField]
		private Color treeColor = Color.green;

		// Token: 0x04002681 RID: 9857
		private Vector4 lutOffset2;

		// Token: 0x04002682 RID: 9858
		private Vector4 _LutLerp;

		// Token: 0x04002683 RID: 9859
		private Vector4 _LutOffset;
	}
}
