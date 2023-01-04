using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200054D RID: 1357
public class UiFlairGraphic : MaskableGraphic
{
	// Token: 0x1700048C RID: 1164
	// (get) Token: 0x06002350 RID: 9040 RVA: 0x0006CA65 File Offset: 0x0006AE65
	// (set) Token: 0x06002351 RID: 9041 RVA: 0x0006CA6D File Offset: 0x0006AE6D
	public Sprite sprite
	{
		get
		{
			return this._sprite;
		}
		set
		{
			this._sprite = value;
			this.SetVerticesDirty();
			this.SetMaterialDirty();
		}
	}

	// Token: 0x1700048D RID: 1165
	// (get) Token: 0x06002352 RID: 9042 RVA: 0x0006CA82 File Offset: 0x0006AE82
	// (set) Token: 0x06002353 RID: 9043 RVA: 0x0006CA8A File Offset: 0x0006AE8A
	public float progress
	{
		get
		{
			return this._progress;
		}
		set
		{
			this._progress = value;
			this.SetVerticesDirty();
		}
	}

	// Token: 0x1700048E RID: 1166
	// (get) Token: 0x06002354 RID: 9044 RVA: 0x0006CA99 File Offset: 0x0006AE99
	// (set) Token: 0x06002355 RID: 9045 RVA: 0x0006CAA1 File Offset: 0x0006AEA1
	public float fade
	{
		get
		{
			return this._fade;
		}
		set
		{
			this._fade = value;
			this.SetVerticesDirty();
		}
	}

	// Token: 0x1700048F RID: 1167
	// (get) Token: 0x06002356 RID: 9046 RVA: 0x0006CAB0 File Offset: 0x0006AEB0
	// (set) Token: 0x06002357 RID: 9047 RVA: 0x0006CAB8 File Offset: 0x0006AEB8
	public UiFlairGraphic.Mode mode
	{
		get
		{
			return this._mode;
		}
		set
		{
			this._mode = value;
			this.SetVerticesDirty();
		}
	}

	// Token: 0x06002358 RID: 9048 RVA: 0x0006CAC8 File Offset: 0x0006AEC8
	private void MaybeSetupArrays()
	{
		if (this._lastSprite == this._sprite)
		{
			return;
		}
		this._verts.Clear();
		this._tris.Clear();
		if (this._sprite)
		{
			ushort[] triangles = this._sprite.triangles;
			this._tris.AddRange(this._sprite.triangles);
			Vector2[] vertices = this.sprite.vertices;
			Vector2[] uv = this.sprite.uv;
			Bounds bounds = this._sprite.bounds;
			for (int i = 0; i < vertices.Length; i++)
			{
				Vector2 v = vertices[i];
				v.x = (v.x - bounds.min.x) / bounds.size.x;
				v.y = (v.y - bounds.min.y) / bounds.size.y;
				this._verts.Add(new UIVertex
				{
					position = v,
					uv0 = uv[i]
				});
			}
		}
	}

	// Token: 0x06002359 RID: 9049 RVA: 0x0006CC18 File Offset: 0x0006B018
	private void AddVerts(VertexHelper vh, float minX, float minY, float sizeX, float sizeY)
	{
		for (int i = 0; i < this._verts.Count; i++)
		{
			UIVertex v = this._verts[i];
			v.color = this.color;
			v.uv1.x = this._progress;
			v.uv1.y = this._fade;
			v.position.x = v.position.x * sizeX + minX;
			v.position.y = v.position.y * sizeY + minY;
			vh.AddVert(v);
		}
	}

	// Token: 0x0600235A RID: 9050 RVA: 0x0006CCC8 File Offset: 0x0006B0C8
	protected override void OnPopulateMesh(VertexHelper vh)
	{
		if (this._progress > 0f && this._fade < 1f && this.color.a > 0f)
		{
			if (this._sprite)
			{
				this.MaybeSetupArrays();
				vh.Clear();
				Rect rect = base.rectTransform.rect;
				int num = 1;
				switch (this.mode)
				{
				case UiFlairGraphic.Mode.None:
					num = 1;
					this.AddVerts(vh, rect.xMin, rect.yMin, rect.width, rect.height);
					break;
				case UiFlairGraphic.Mode.Mirror:
					num = 2;
					this.AddVerts(vh, rect.xMin, rect.yMin, rect.width / 2f, rect.height);
					this.AddVerts(vh, rect.xMax, rect.yMin, -rect.width / 2f, rect.height);
					break;
				case UiFlairGraphic.Mode.Quadrants:
					num = 4;
					this.AddVerts(vh, rect.xMin, rect.center.x, rect.width / 2f, rect.height / 2f);
					this.AddVerts(vh, rect.xMax, rect.center.x, -rect.width / 2f, rect.height / 2f);
					this.AddVerts(vh, rect.xMin, rect.center.x, rect.width / 2f, -rect.height / 2f);
					this.AddVerts(vh, rect.xMax, rect.center.x, -rect.width / 2f, -rect.height / 2f);
					break;
				}
				for (int i = 0; i < num; i++)
				{
					int num2 = this._verts.Count * i;
					for (int j = 0; j < this._tris.Count; j += 3)
					{
						vh.AddTriangle((int)this._tris[j] + num2, (int)this._tris[j + 1] + num2, (int)this._tris[j + 2] + num2);
					}
				}
			}
			else
			{
				base.OnPopulateMesh(vh);
			}
		}
		else
		{
			vh.Clear();
		}
	}

	// Token: 0x0600235B RID: 9051 RVA: 0x0006CF60 File Offset: 0x0006B360
	protected override void UpdateMaterial()
	{
		base.UpdateMaterial();
		if (this._sprite)
		{
			base.canvasRenderer.SetTexture(this._sprite.texture);
		}
	}

	// Token: 0x0600235C RID: 9052 RVA: 0x0006CF90 File Offset: 0x0006B390
	public IEnumerator<object> PlayOnceEnumerator(float inTime = 1f, float waitTime = 1f, float outTime = 1f)
	{
		for (float t = 0f; t < inTime; t += Time.unscaledDeltaTime)
		{
			this.progress = t;
			this.SetVerticesDirty();
			yield return null;
		}
		this.progress = 1f;
		for (float t2 = 0f; t2 < waitTime; t2 += Time.unscaledDeltaTime)
		{
			this.SetVerticesDirty();
			yield return null;
		}
		for (float t3 = 0f; t3 < outTime; t3 += Time.unscaledDeltaTime)
		{
			this.fade = t3;
			this.SetVerticesDirty();
			yield return null;
		}
		this.fade = 1f;
		yield return null;
		yield break;
	}

	// Token: 0x040015F7 RID: 5623
	[SerializeField]
	private Sprite _sprite;

	// Token: 0x040015F8 RID: 5624
	[SerializeField]
	[Range(0f, 1f)]
	private float _progress = 1f;

	// Token: 0x040015F9 RID: 5625
	[SerializeField]
	[Range(0f, 1f)]
	private float _fade;

	// Token: 0x040015FA RID: 5626
	[SerializeField]
	private UiFlairGraphic.Mode _mode;

	// Token: 0x040015FB RID: 5627
	private List<ushort> _tris = new List<ushort>();

	// Token: 0x040015FC RID: 5628
	private List<UIVertex> _verts = new List<UIVertex>();

	// Token: 0x040015FD RID: 5629
	private Sprite _lastSprite;

	// Token: 0x0200054E RID: 1358
	public enum Mode
	{
		// Token: 0x040015FF RID: 5631
		None,
		// Token: 0x04001600 RID: 5632
		Mirror,
		// Token: 0x04001601 RID: 5633
		Quadrants
	}
}
