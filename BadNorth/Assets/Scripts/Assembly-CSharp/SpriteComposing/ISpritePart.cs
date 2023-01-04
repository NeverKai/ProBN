using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace SpriteComposing
{
	// Token: 0x020005C6 RID: 1478
	public interface ISpritePart
	{
		// Token: 0x0600269A RID: 9882
		void Draw(CommandBuffer buffer, Matrix4x4 matrix);
	}
}
