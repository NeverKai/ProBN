using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.TowerDefense.SpriteMagic
{
	// Token: 0x020007D6 RID: 2006
	public class SpriteBoundsPool
	{
		// Token: 0x06003406 RID: 13318 RVA: 0x000E0C9C File Offset: 0x000DF09C
		public static SpriteBounds GetSpriteBoundsStatic(Sprite sprite)
		{
			SpriteBounds result;
			using (new ScopedProfiler("GetSpriteBoundsStatic", null))
			{
				if (!Application.isPlaying)
				{
					result = new SpriteBounds(sprite);
				}
				else
				{
					SpriteBounds spriteBounds;
					if (!SpriteBoundsPool.staticBoundDict.TryGetValue(sprite, out spriteBounds))
					{
						spriteBounds = new SpriteBounds(sprite);
						SpriteBoundsPool.staticBoundDict.Add(sprite, spriteBounds);
					}
					result = spriteBounds;
				}
			}
			return result;
		}

		// Token: 0x06003407 RID: 13319 RVA: 0x000E0D18 File Offset: 0x000DF118
		public SpriteBounds GetSpriteBounds(Sprite sprite)
		{
			SpriteBounds result;
			using (new ScopedProfiler("GetSpriteBounds", null))
			{
				if (!Application.isPlaying)
				{
					result = new SpriteBounds(sprite);
				}
				else
				{
					SpriteBounds spriteBounds;
					if (!this.boundDict.TryGetValue(sprite, out spriteBounds))
					{
						if (!SpriteBoundsPool.staticBoundDict.TryGetValue(sprite, out spriteBounds))
						{
							spriteBounds = new SpriteBounds(sprite);
							SpriteBoundsPool.staticBoundDict.Add(sprite, spriteBounds);
						}
						this.boundDict.Add(sprite, spriteBounds);
					}
					result = spriteBounds;
				}
			}
			return result;
		}

		// Token: 0x04002374 RID: 9076
		private static Dictionary<Sprite, SpriteBounds> staticBoundDict = new Dictionary<Sprite, SpriteBounds>();

		// Token: 0x04002375 RID: 9077
		private Dictionary<Sprite, SpriteBounds> boundDict = new Dictionary<Sprite, SpriteBounds>();
	}
}
