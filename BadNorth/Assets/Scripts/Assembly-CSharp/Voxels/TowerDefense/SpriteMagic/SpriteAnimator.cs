using System;
using UnityEngine;

namespace Voxels.TowerDefense.SpriteMagic
{
	// Token: 0x020007D4 RID: 2004
	public class SpriteAnimator : BatchedSprite
	{
		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x060033EF RID: 13295 RVA: 0x000E005E File Offset: 0x000DE45E
		private Texture tex
		{
			get
			{
				return (!this._tex) ? ((!this.sprite2) ? null : this.sprite2.texture) : this._tex;
			}
		}

		// Token: 0x060033F0 RID: 13296 RVA: 0x000E009C File Offset: 0x000DE49C
		public override int GetMaterialKey()
		{
			return this.sprite.texture.GetHashCode() * 17 + (this.tex.GetHashCode() + 7) * 151 + (this.tex.GetInstanceID() + 11) * 107 + (base.sharedMaterial.GetHashCode() + 37) * 193;
		}

		// Token: 0x060033F1 RID: 13297 RVA: 0x000E00F8 File Offset: 0x000DE4F8
		public override MaterialPropertyBlock GetMaterialPropertyBlock()
		{
			MaterialPropertyBlock materialPropertyBlock = base.GetMaterialPropertyBlock();
			materialPropertyBlock.SetTexture(SpriteAnimator.partTexId, this.tex);
			return materialPropertyBlock;
		}

		// Token: 0x060033F2 RID: 13298 RVA: 0x000E0123 File Offset: 0x000DE523
		private void OnValidate()
		{
			if (Application.isPlaying)
			{
				return;
			}
			this.SetSpriteEditor();
			if (this.sprite2 != null)
			{
				this.SetSprite2(this.sprite2);
			}
		}

		// Token: 0x060033F3 RID: 13299 RVA: 0x000E0153 File Offset: 0x000DE553
		private void OnDrawGizmos()
		{
			this.SetSpriteEditor();
		}

		// Token: 0x060033F4 RID: 13300 RVA: 0x000E015B File Offset: 0x000DE55B
		private void SetSpriteEditor()
		{
			base.bSprite = this.sprite;
		}

		// Token: 0x060033F5 RID: 13301 RVA: 0x000E0169 File Offset: 0x000DE569
		private void Update()
		{
			this.SetSprite();
		}

		// Token: 0x060033F6 RID: 13302 RVA: 0x000E0171 File Offset: 0x000DE571
		public void SetSprite()
		{
			if (this.lastSprite != this.sprite)
			{
				base.bSprite = this.sprite;
				this.lastSprite = this.sprite;
			}
		}

		// Token: 0x060033F7 RID: 13303 RVA: 0x000E01A1 File Offset: 0x000DE5A1
		private void Start()
		{
			this.UpdateSprite2();
		}

		// Token: 0x060033F8 RID: 13304 RVA: 0x000E01AC File Offset: 0x000DE5AC
		protected override void SetMeshToSprite(Mesh mesh, Sprite sprite)
		{
			base.SetMeshToSprite(mesh, sprite);
			float x = sprite.bounds.size.x;
			mesh.bounds = new Bounds(Vector3.zero, new Vector3(x, 100f, x));
			this.tmpMin.x = -x * 0.5f;
			this.tmpMin.y = -100f;
			this.tmpMin.z = -x * 0.5f;
			this.tmpMax = -this.tmpMin;
			mesh.bounds.SetMinMax(this.tmpMin, this.tmpMax);
		}

		// Token: 0x060033F9 RID: 13305 RVA: 0x000E0255 File Offset: 0x000DE655
		public void UpdateSprite2()
		{
			this.SetSprite2(this.sprite2);
		}

		// Token: 0x060033FA RID: 13306 RVA: 0x000E0264 File Offset: 0x000DE664
		public void SetSprite2(Sprite newSprite)
		{
			if (!newSprite)
			{
				return;
			}
			this._tex = newSprite.texture;
			base.block.SetTexture(SpriteAnimator.partTexId, newSprite.texture);
			this.sprite2 = newSprite;
			Rect textureRect = this.sprite2.textureRect;
			int num = this.sprite2.texture.width / 256;
			int num2 = this.sprite2.texture.height / 256;
			Color32 c = base.color;
			c.r = (byte)((int)textureRect.min.y / num2);
			c.g = (byte)((int)textureRect.min.x / num);
			base.color = c;
			base.ComittBlock();
		}

		// Token: 0x060033FB RID: 13307 RVA: 0x000E0339 File Offset: 0x000DE739
		private void OnEnable()
		{
			this.aimer.transform.localRotation = Quaternion.identity;
		}

		// Token: 0x04002362 RID: 9058
		public Sprite sprite;

		// Token: 0x04002363 RID: 9059
		public Sprite sprite2;

		// Token: 0x04002364 RID: 9060
		private Sprite lastSprite;

		// Token: 0x04002365 RID: 9061
		public Transform aimer;

		// Token: 0x04002366 RID: 9062
		private Texture _tex;

		// Token: 0x04002367 RID: 9063
		private static ShaderId partTexId = "_PartTex";

		// Token: 0x04002368 RID: 9064
		private Vector3 tmpMin = Vector3.zero;

		// Token: 0x04002369 RID: 9065
		private Vector3 tmpMax = Vector3.zero;
	}
}
