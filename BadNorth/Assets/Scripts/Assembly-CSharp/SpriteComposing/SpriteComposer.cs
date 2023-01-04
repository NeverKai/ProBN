using System;
using UnityEngine;

namespace SpriteComposing
{
	// Token: 0x020005CA RID: 1482
	public class SpriteComposer : MonoBehaviour
	{
		// Token: 0x060026A1 RID: 9889 RVA: 0x0007ABE4 File Offset: 0x00078FE4
		public void MaybeInitalize()
		{
			if (!this.tex)
			{
				this.tex = new Texture2D(this.width, this.height, TextureFormat.ARGB32, false, true);
				Color32[] array = new Color32[this.width * this.height];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = new Color32(0, 0, 0, 0);
				}
				this.tex.SetPixels32(array);
				this.sheet = new Sheet(this.tex);
			}
		}

		// Token: 0x060026A2 RID: 9890 RVA: 0x0007AC74 File Offset: 0x00079074
		public void ReturnRect(Rect rect)
		{
			this.sheet.ReturnRect(rect);
		}

		// Token: 0x060026A3 RID: 9891 RVA: 0x0007AC84 File Offset: 0x00079084
		public void GetRect(ref Rect innerRect, ref Rect outerRect, int width, int height, int padding = 2, int snapping = 1)
		{
			this.MaybeInitalize();
			RectRequest occupant = new RectRequest(width, height, padding, snapping);
			outerRect = this.sheet.GetSquare(occupant);
			innerRect = occupant.GetSpriteRect(outerRect.min);
		}

		// Token: 0x060026A4 RID: 9892 RVA: 0x0007ACCC File Offset: 0x000790CC
		public void DrawOnRect(Rect rect, RenderTexture renderTexture)
		{
			this.MaybeInitalize();
			Vector2 a = new Vector2((float)renderTexture.width, (float)renderTexture.height);
			Rect rect2 = new Rect((a - rect.size) / 2f, rect.size);
			RenderTexture.active = renderTexture;
			this.tex.ReadPixels(new Rect(rect2.min.x, rect2.min.y, rect.width, rect.height), (int)rect.xMin, (int)rect.yMin);
			this.tex.Apply();
		}

		// Token: 0x060026A5 RID: 9893 RVA: 0x0007AD76 File Offset: 0x00079176
		private void OnDestroy()
		{
			UnityEngine.Object.Destroy(this.tex);
			this.sheet.avaliableSquares.Clear();
		}

		// Token: 0x040018A5 RID: 6309
		[SerializeField]
		private int width = 2048;

		// Token: 0x040018A6 RID: 6310
		[SerializeField]
		private int height = 2048;

		// Token: 0x040018A7 RID: 6311
		public Sheet sheet;

		// Token: 0x040018A8 RID: 6312
		public Texture2D tex;
	}
}
