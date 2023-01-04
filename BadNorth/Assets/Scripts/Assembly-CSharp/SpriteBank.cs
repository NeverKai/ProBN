using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005C2 RID: 1474
public class SpriteBank : MonoBehaviour
{
	// Token: 0x06002685 RID: 9861 RVA: 0x00079F64 File Offset: 0x00078364
	public Sprite GetReplacement(Sprite oldSprite)
	{
		Sprite sprite;
		this.spriteDict.TryGetValue(oldSprite, out sprite);
		if (sprite == null)
		{
			int num = int.Parse(oldSprite.name.Substring(oldSprite.name.Length - 4, 4));
			if (num >= 0 && num < this.allSprites.Length)
			{
				sprite = this.allSprites[Mathf.Clamp(num, 0, this.allSprites.Length - 1)];
			}
			else
			{
				sprite = oldSprite;
			}
			this.spriteDict.Add(oldSprite, sprite);
		}
		return sprite;
	}

	// Token: 0x04001884 RID: 6276
	public Sprite sprite;

	// Token: 0x04001885 RID: 6277
	public Sprite[] allSprites;

	// Token: 0x04001886 RID: 6278
	private Dictionary<Sprite, Sprite> spriteDict = new Dictionary<Sprite, Sprite>();
}
