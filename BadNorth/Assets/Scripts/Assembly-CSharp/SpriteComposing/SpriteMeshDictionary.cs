using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpriteComposing
{
	// Token: 0x020005C7 RID: 1479
	public static class SpriteMeshDictionary
	{
		// Token: 0x0600269B RID: 9883 RVA: 0x0007AB58 File Offset: 0x00078F58
		public static Mesh GetMesh(Sprite sprite)
		{
			if (!sprite)
			{
				return null;
			}
			Mesh mesh;
			if (!SpriteMeshDictionary.meshDict.TryGetValue(sprite, out mesh))
			{
				mesh = sprite.GetMesh();
				SpriteMeshDictionary.meshDict.Add(sprite, mesh);
			}
			return mesh;
		}

		// Token: 0x040018A4 RID: 6308
		private static Dictionary<Sprite, Mesh> meshDict = new Dictionary<Sprite, Mesh>();
	}
}
