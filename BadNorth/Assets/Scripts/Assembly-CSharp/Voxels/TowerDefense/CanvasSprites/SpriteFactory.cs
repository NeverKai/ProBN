using System;
using System.Collections;
using UnityEngine;

namespace Voxels.TowerDefense.CanvasSprites
{
	// Token: 0x02000728 RID: 1832
	public class SpriteFactory : MonoBehaviour
	{
		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x06002F92 RID: 12178 RVA: 0x000C0F80 File Offset: 0x000BF380
		public Vector2 size
		{
			get
			{
				return new Vector3((float)this.width, (float)this.height);
			}
		}

		// Token: 0x06002F93 RID: 12179 RVA: 0x000C0F9A File Offset: 0x000BF39A
		private void Awake()
		{
			this.canvas = base.GetComponentInChildren<Canvas>();
			this.cameraRef = base.GetComponent<Camera>();
		}

		// Token: 0x06002F94 RID: 12180 RVA: 0x000C0FB4 File Offset: 0x000BF3B4
		private void RenderRect(Rect rect)
		{
			this.cameraRef.Render();
			RenderTexture.active = this.rendTex;
			rect.size += Vector2.one * 2f;
			this.tex.ReadPixels(new Rect(rect.xMin, (float)this.height - rect.yMax, rect.width, rect.height), (int)rect.min.x, (int)rect.min.y);
			this.tex.Apply();
		}

		// Token: 0x06002F95 RID: 12181 RVA: 0x000C1058 File Offset: 0x000BF458
		public void Render(params FactorySprite[] sprites)
		{
			Rect rect = sprites[0].spriteRect;
			foreach (FactorySprite factorySprite in sprites)
			{
				rect = ExtraMath.Encapsulate(rect, factorySprite.spriteRect);
			}
			this.RenderRect(rect);
			foreach (FactorySprite factorySprite2 in sprites)
			{
				factorySprite2.UpdateSprite();
			}
		}

		// Token: 0x06002F96 RID: 12182 RVA: 0x000C10C8 File Offset: 0x000BF4C8
		public IEnumerator RenderEnumerator()
		{
			this.cameraRef.orthographicSize = (float)(this.height / 2);
			yield return null;
			FactorySprite[] sprites = base.GetComponentsInChildren<FactorySprite>();
			Rect bigRect = sprites[0].spriteRect;
			foreach (FactorySprite factorySprite in sprites)
			{
				bigRect = ExtraMath.Encapsulate(bigRect, factorySprite.spriteRect);
			}
			this.width = Mathf.NextPowerOfTwo((int)bigRect.max.x);
			if (this.rendTex == null || this.rendTex.height != this.height || this.rendTex.width != this.width)
			{
				this.rendTex = new RenderTexture(this.width, this.height, 24, RenderTextureFormat.ARGB32);
				this.cameraRef.targetTexture = this.rendTex;
			}
			if (this.tex == null)
			{
				this.tex = new Texture2D(this.width, this.height, TextureFormat.RGBA32, false);
			}
			else if (this.tex.width != this.width || this.tex.height != this.tex.height)
			{
				this.tex.Resize(this.width, this.height);
			}
			yield return null;
			int frame = Time.frameCount;
			while (Time.frameCount == frame)
			{
				yield return null;
			}
			this.Render(sprites);
			yield return null;
			yield break;
		}

		// Token: 0x04001FBB RID: 8123
		private Camera cameraRef;

		// Token: 0x04001FBC RID: 8124
		public Canvas canvas;

		// Token: 0x04001FBD RID: 8125
		public RenderTexture rendTex;

		// Token: 0x04001FBE RID: 8126
		public Texture2D tex;

		// Token: 0x04001FBF RID: 8127
		public int height = 512;

		// Token: 0x04001FC0 RID: 8128
		public int width = 512;
	}
}
