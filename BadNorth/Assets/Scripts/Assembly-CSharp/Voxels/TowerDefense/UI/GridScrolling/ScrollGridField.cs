using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Voxels.TowerDefense.UI.GridScrolling
{
	// Token: 0x02000921 RID: 2337
	[RequireComponent(typeof(RectTransform))]
	public class ScrollGridField : UIBehaviour
	{
		// Token: 0x06003ECF RID: 16079 RVA: 0x0011B7EF File Offset: 0x00119BEF
		protected override void OnRectTransformDimensionsChange()
		{
			base.OnRectTransformDimensionsChange();
		}

		// Token: 0x06003ED0 RID: 16080 RVA: 0x0011B7F8 File Offset: 0x00119BF8
		public IEnumerable<Rect> GetRects()
		{
			RectTransform rt = base.transform as RectTransform;
			RectTransform parentRt = base.transform.parent as RectTransform;
			Rect rect = rt.GetWorldSpaceRect();
			Rect parentRect = parentRt.GetWorldSpaceRect();
			int minX = 0;
			int maxX = 0;
			int minY = 0;
			int maxY = 0;
			int safety = 0;
			while (this.right)
			{
				int num;
				safety = (num = safety) + 1;
				if (num >= 100)
				{
					break;
				}
				if (rect.xMax + (float)(maxX + 1) * rect.width >= parentRect.xMax)
				{
					break;
				}
				maxX++;
			}
			while (this.left)
			{
				int num;
				safety = (num = safety) + 1;
				if (num >= 100)
				{
					break;
				}
				if (rect.xMin + (float)(minX - 1) * rect.width <= parentRect.xMin)
				{
					break;
				}
				minX--;
			}
			while (this.up)
			{
				int num;
				safety = (num = safety) + 1;
				if (num >= 100)
				{
					break;
				}
				if (rect.yMax + (float)(maxY + 1) * rect.height >= parentRect.yMax)
				{
					break;
				}
				maxY++;
			}
			while (this.down)
			{
				int num;
				safety = (num = safety) + 1;
				if (num >= 100)
				{
					break;
				}
				if (rect.yMin + (float)(minY - 1) * rect.height <= parentRect.yMin)
				{
					break;
				}
				minY--;
			}
			for (int x = minX; x <= maxX; x++)
			{
				for (int y = minY; y <= maxY; y++)
				{
					yield return new Rect(rect.position + new Vector2((float)x * rect.width, (float)y * rect.height), rect.size);
				}
			}
			yield break;
		}

		// Token: 0x06003ED1 RID: 16081 RVA: 0x0011B81C File Offset: 0x00119C1C
		private void OnDrawGizmos()
		{
			foreach (Rect rect in this.GetRects())
			{
				Gizmos.DrawWireCube(rect.center, rect.size);
			}
		}

		// Token: 0x04002BE3 RID: 11235
		[Header("Expand")]
		[SerializeField]
		private bool left;

		// Token: 0x04002BE4 RID: 11236
		[SerializeField]
		private bool right;

		// Token: 0x04002BE5 RID: 11237
		[SerializeField]
		private bool up;

		// Token: 0x04002BE6 RID: 11238
		[SerializeField]
		private bool down;
	}
}
