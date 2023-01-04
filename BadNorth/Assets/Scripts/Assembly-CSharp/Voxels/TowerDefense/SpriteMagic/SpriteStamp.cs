using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.TowerDefense.SpriteMagic
{
	// Token: 0x020007DC RID: 2012
	public class SpriteStamp : MonoBehaviour, ReusableEffect.INavPosEffect
	{
		// Token: 0x06003421 RID: 13345 RVA: 0x000E1A3C File Offset: 0x000DFE3C
		private void Awake()
		{
			this.spriteRenderer = base.GetComponent<SpriteRenderer>();
			if (!this.sprites.Contains(this.spriteRenderer.sprite))
			{
				this.sprites.Add(this.spriteRenderer.sprite);
			}
			this.spriteRenderer.enabled = false;
			this.defaultStamp = new SpriteStampDef(this.spriteRenderer);
			this.defaultStamp.matrix = Matrix4x4.Scale(base.transform.lossyScale);
		}

		// Token: 0x06003422 RID: 13346 RVA: 0x000E1AC0 File Offset: 0x000DFEC0
		public void Stamp(NavPos navPos, float scale = 1f)
		{
			if (!navPos.valid)
			{
				return;
			}
			SpriteStamperRoot componentInParent = navPos.navigationMesh.GetComponentInParent<SpriteStamperRoot>();
			if (!componentInParent)
			{
				return;
			}
			SpriteStampDef stamp = this.defaultStamp;
			Vector3 pos = navPos.pos;
			Quaternion q = Quaternion.LookRotation(navPos.GetNormal(), UnityEngine.Random.insideUnitSphere);
			Vector3 s = Vector3.one * scale * Mathf.Pow(2f, Mathf.Lerp(-this.scaleVariance, this.scaleVariance, (UnityEngine.Random.value + UnityEngine.Random.value) / 4f));
			Matrix4x4 lhs = Matrix4x4.TRS(pos, q, s);
			stamp.sprite = this.sprites[UnityEngine.Random.Range(0, this.sprites.Count)];
			stamp.matrix = lhs * stamp.matrix;
			stamp.navPos = navPos;
			if (this.flipX && UnityEngine.Random.value < 0.5f)
			{
				stamp.flipX = !stamp.flipX;
			}
			if (this.flipY && UnityEngine.Random.value < 0.5f)
			{
				stamp.flipY = !stamp.flipY;
			}
			componentInParent.Add(stamp);
		}

		// Token: 0x06003423 RID: 13347 RVA: 0x000E1BFB File Offset: 0x000DFFFB
		void ReusableEffect.INavPosEffect.PlayAt(NavPos navPos)
		{
			this.Stamp(navPos, 1f);
		}

		// Token: 0x0400238D RID: 9101
		private SpriteRenderer spriteRenderer;

		// Token: 0x0400238E RID: 9102
		[SerializeField]
		private float scaleVariance = 0.1f;

		// Token: 0x0400238F RID: 9103
		[SerializeField]
		private bool flipX = true;

		// Token: 0x04002390 RID: 9104
		[SerializeField]
		private bool flipY = true;

		// Token: 0x04002391 RID: 9105
		[SerializeField]
		private List<Sprite> sprites = new List<Sprite>();

		// Token: 0x04002392 RID: 9106
		private SpriteStampDef defaultStamp;
	}
}
