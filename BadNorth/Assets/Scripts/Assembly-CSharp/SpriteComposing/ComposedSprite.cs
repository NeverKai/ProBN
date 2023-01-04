using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace SpriteComposing
{
	// Token: 0x020005C5 RID: 1477
	public class ComposedSprite : MonoBehaviour
	{
		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x0600268D RID: 9869 RVA: 0x0007A460 File Offset: 0x00078860
		// (set) Token: 0x0600268E RID: 9870 RVA: 0x0007A490 File Offset: 0x00078890
		public Rect rect
		{
			get
			{
				Vector2 position = Vector2.Scale(this.pivot, -this.size);
				return new Rect(position, this.size);
			}
			set
			{
				value.min = ExtraMath.Floor(value.min);
				value.max = ExtraMath.Ceil(value.max);
				this.size = value.size;
				this.pivot = ExtraMath.RemapValueUnclamped(Vector2.zero, value.min, value.max);
			}
		}

		// Token: 0x0600268F RID: 9871 RVA: 0x0007A4EE File Offset: 0x000788EE
		public void SetDirty()
		{
			this.dirty = true;
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06002690 RID: 9872 RVA: 0x0007A4F8 File Offset: 0x000788F8
		public Sprite sprite
		{
			get
			{
				if (!this._sprite || this.dirty)
				{
					using (new ScopedStopwatch("ComposedSprite.Draw", null, 100f))
					{
						this.Draw();
					}
				}
				return this._sprite;
			}
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06002691 RID: 9873 RVA: 0x0007A564 File Offset: 0x00078964
		// (set) Token: 0x06002692 RID: 9874 RVA: 0x0007A56C File Offset: 0x0007896C
		public Square square
		{
			get
			{
				return this._square;
			}
			set
			{
				this._square = value;
			}
		}

		// Token: 0x06002693 RID: 9875 RVA: 0x0007A575 File Offset: 0x00078975
		public static void Initialize(int height = 512, int width = 512)
		{
			ComposedSprite.blitTexture = new RenderTexture(height, width, 24, RenderTextureFormat.ARGB32);
			ComposedSprite.blitTexture.Create();
			ComposedSprite.renderTexture = new RenderTexture(height, width, 24, RenderTextureFormat.ARGB32);
			ComposedSprite.renderTexture.Create();
			ComposedSprite.buffer = new CommandBuffer();
		}

		// Token: 0x06002694 RID: 9876 RVA: 0x0007A5B5 File Offset: 0x000789B5
		public void MaybeRedraw()
		{
			if (this._sprite)
			{
				this.Draw();
			}
		}

		// Token: 0x06002695 RID: 9877 RVA: 0x0007A5D0 File Offset: 0x000789D0
		public static void RecursiveDraw(Transform t, CommandBuffer buffer, Matrix4x4 matrix)
		{
			for (int i = 0; i < t.childCount; i++)
			{
				Transform child = t.GetChild(i);
				if (child.gameObject.activeSelf)
				{
					ComposedSprite.RecursiveDraw(child, buffer, matrix);
				}
			}
			SpriteRenderer component = t.gameObject.GetComponent<SpriteRenderer>();
			if (component)
			{
				component.gameObject.GetOrAddComponent<SpriteRenderProxy>();
			}
			ISpritePart component2 = t.gameObject.GetComponent<ISpritePart>();
			if (component2 != null)
			{
				component2.Draw(buffer, matrix);
			}
		}

		// Token: 0x06002696 RID: 9878 RVA: 0x0007A654 File Offset: 0x00078A54
		[ContextMenu("Draw")]
		public void Draw()
		{
			if (!Application.isPlaying)
			{
				return;
			}
			if (!this.spriteComposer)
			{
				this.spriteComposer = base.gameObject.GetComponentInParentIncludingInactive<SpriteComposer>();
				this.spriteComposer.GetRect(ref this.innerRect, ref this.outerRect, (int)this.size.x, (int)this.size.y, this.padding, this.snapping);
				if (!this.spriteComposer)
				{
					this.spriteComposer = base.gameObject.transform.root.GetComponentInChildren<SpriteComposer>(true);
				}
				if (!this.spriteComposer)
				{
					this.spriteComposer = base.gameObject.AddComponent<SpriteComposer>();
				}
			}
			else if (this.innerRect.size != this.rect.size || this._sprite.border != this.border)
			{
				this.spriteComposer.ReturnRect(this.outerRect);
				this.spriteComposer.GetRect(ref this.innerRect, ref this.outerRect, (int)this.size.x, (int)this.size.y, this.padding, this.snapping);
				this._sprite = null;
			}
			int num = Mathf.NextPowerOfTwo((int)this.outerRect.width);
			int num2 = Mathf.NextPowerOfTwo((int)this.outerRect.height);
			if (ComposedSprite.renderTexture == null)
			{
				ComposedSprite.Initialize(512, 512);
			}
			else if (ComposedSprite.renderTexture.width < num || ComposedSprite.renderTexture.height < num2)
			{
				num = Mathf.Max(num, ComposedSprite.renderTexture.width);
				num2 = Mathf.Max(num2, ComposedSprite.renderTexture.height);
				ComposedSprite.renderTexture.Release();
				UnityEngine.Object.Destroy(ComposedSprite.renderTexture);
				ComposedSprite.renderTexture = new RenderTexture(num, num2, 24, RenderTextureFormat.ARGB32);
				ComposedSprite.blitTexture.Release();
				UnityEngine.Object.Destroy(ComposedSprite.blitTexture);
				ComposedSprite.blitTexture = new RenderTexture(num, num2, 24, RenderTextureFormat.ARGB32);
			}
			Vector2 a = new Vector2((float)ComposedSprite.renderTexture.width, (float)ComposedSprite.renderTexture.height);
			Vector2 a2 = this.rect.center;
			a2 += this.outerRect.center - this.innerRect.center;
			Vector2 vector = (a2 - a / 2f) / this.pixelsPerUnit;
			Vector2 vector2 = (a2 + a / 2f) / this.pixelsPerUnit;
			ComposedSprite.buffer.SetProjectionMatrix(Matrix4x4.Ortho(vector.x, vector2.x, vector.y, vector2.y, -10f, 10f) * base.transform.worldToLocalMatrix);
			ComposedSprite.buffer.SetRenderTarget(new RenderTargetIdentifier(ComposedSprite.renderTexture));
			ComposedSprite.buffer.ClearRenderTarget(true, true, Color.clear);
			Matrix4x4 identity = Matrix4x4.identity;
			ComposedSprite.RecursiveDraw(base.transform, ComposedSprite.buffer, identity);
			Graphics.ExecuteCommandBuffer(ComposedSprite.buffer);
			ComposedSprite.buffer.Clear();
			foreach (Shader shader in this.blitShaders)
			{
				Material material;
				if (!ComposedSprite.blitMaterials.TryGetValue(shader, out material))
				{
					material = new Material(shader);
					ComposedSprite.blitMaterials.Add(shader, material);
				}
				Graphics.Blit(ComposedSprite.renderTexture, ComposedSprite.blitTexture, material);
				RenderTexture renderTexture = ComposedSprite.blitTexture;
				ComposedSprite.blitTexture = ComposedSprite.renderTexture;
				ComposedSprite.renderTexture = renderTexture;
			}
			this.spriteComposer.DrawOnRect(this.outerRect, ComposedSprite.renderTexture);
			if (!this._sprite)
			{
				this._sprite = Sprite.Create(this.spriteComposer.tex, this.innerRect, this.pivot, this.pixelsPerUnit, 1U, SpriteMeshType.FullRect, this.border);
				this._sprite.name = base.name;
			}
			this.dirty = false;
		}

		// Token: 0x06002697 RID: 9879 RVA: 0x0007AAB0 File Offset: 0x00078EB0
		private void OnDrawGizmos()
		{
			Gizmos.matrix = base.transform.localToWorldMatrix;
			Gizmos.DrawWireCube(this.rect.center / this.pixelsPerUnit, this.rect.size / this.pixelsPerUnit);
		}

		// Token: 0x06002698 RID: 9880 RVA: 0x0007AB0E File Offset: 0x00078F0E
		private void OnDestroy()
		{
			if (this.spriteComposer)
			{
				this.spriteComposer.ReturnRect(this.outerRect);
			}
			if (this._sprite)
			{
				UnityEngine.Object.Destroy(this._sprite);
			}
		}

		// Token: 0x04001893 RID: 6291
		public Vector2 size = new Vector2(128f, 128f);

		// Token: 0x04001894 RID: 6292
		public Vector2 pivot = new Vector2(0.5f, 0.5f);

		// Token: 0x04001895 RID: 6293
		public Vector4 border = new Vector4(0f, 0f, 0f, 0f);

		// Token: 0x04001896 RID: 6294
		public float pixelsPerUnit = 100f;

		// Token: 0x04001897 RID: 6295
		public int padding = 2;

		// Token: 0x04001898 RID: 6296
		public int snapping = 1;

		// Token: 0x04001899 RID: 6297
		private static RenderTexture renderTexture;

		// Token: 0x0400189A RID: 6298
		private static RenderTexture blitTexture;

		// Token: 0x0400189B RID: 6299
		private static CommandBuffer buffer;

		// Token: 0x0400189C RID: 6300
		private SpriteComposer spriteComposer;

		// Token: 0x0400189D RID: 6301
		[SerializeField]
		private List<Shader> blitShaders = new List<Shader>();

		// Token: 0x0400189E RID: 6302
		public Rect innerRect;

		// Token: 0x0400189F RID: 6303
		public Rect outerRect;

		// Token: 0x040018A0 RID: 6304
		private static Dictionary<Shader, Material> blitMaterials = new Dictionary<Shader, Material>();

		// Token: 0x040018A1 RID: 6305
		private bool dirty;

		// Token: 0x040018A2 RID: 6306
		[SerializeField]
		[SpritePreview]
		private Sprite _sprite;

		// Token: 0x040018A3 RID: 6307
		private Square _square;
	}
}
