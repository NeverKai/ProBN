using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000679 RID: 1657
public class TexturePool : MonoBehaviour
{
	// Token: 0x06002A27 RID: 10791 RVA: 0x0009689C File Offset: 0x00094C9C
	public Texture2D GetTexture(int width, int height, bool linear = true)
	{
		Texture2D texture = this.GetTexture(width, height);
		texture.filterMode = ((!linear) ? FilterMode.Point : FilterMode.Bilinear);
		return texture;
	}

	// Token: 0x06002A28 RID: 10792 RVA: 0x000968C8 File Offset: 0x00094CC8
	private Texture2D GetTexture(int width, int height)
	{
		for (int i = this.pool.Count - 1; i >= 0; i--)
		{
			Texture2D texture2D = this.pool[i];
			if (texture2D.width == width && texture2D.height == texture2D.height)
			{
				this.pool.RemoveAt(i);
				return texture2D;
			}
		}
		if (this.pool.Count > 0)
		{
			Texture2D texture2D2 = this.pool[0];
			this.pool.RemoveAt(0);
			texture2D2.Resize(width, height);
			return texture2D2;
		}
		return new Texture2D(width, height, TextureFormat.ARGB32, false, true);
	}

	// Token: 0x06002A29 RID: 10793 RVA: 0x0009696C File Offset: 0x00094D6C
	public void ReturnTexture(ref Texture2D tex)
	{
		if (tex != null)
		{
			tex.filterMode = FilterMode.Bilinear;
			tex.wrapMode = TextureWrapMode.Repeat;
			this.pool.Add(tex);
			tex = null;
		}
	}

	// Token: 0x06002A2A RID: 10794 RVA: 0x0009699C File Offset: 0x00094D9C
	public void ClearPool()
	{
		for (int i = 0; i < this.pool.Count; i++)
		{
			UnityEngine.Object.Destroy(this.pool[i]);
		}
		this.pool.Clear();
	}

	// Token: 0x04001B7E RID: 7038
	[SerializeField]
	private List<Texture2D> pool = new List<Texture2D>();
}
