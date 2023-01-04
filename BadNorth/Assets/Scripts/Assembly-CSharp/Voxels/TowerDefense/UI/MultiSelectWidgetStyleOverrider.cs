using System;
using UnityEngine;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x020008CD RID: 2253
	public class MultiSelectWidgetStyleOverrider : MonoBehaviour
	{
		// Token: 0x06003B87 RID: 15239 RVA: 0x00108CA0 File Offset: 0x001070A0
		public void SetStyles(PolygonStyle backgroundStyle, PolygonStyle arrowStyle, PolygonStyle arrowShade)
		{
			if (backgroundStyle && this.backgroundPolygon)
			{
				this.backgroundPolygon.style = backgroundStyle;
			}
			if (arrowStyle)
			{
				if (this.leftArrowPolygon)
				{
					this.leftArrowPolygon.style = arrowStyle;
				}
				if (this.rightArrowPolygon)
				{
					this.rightArrowPolygon.style = arrowStyle;
				}
			}
			if (arrowShade)
			{
				if (this.leftArrowShade)
				{
					this.leftArrowShade.style = arrowShade;
				}
				if (this.rightArrowShade)
				{
					this.rightArrowShade.style = arrowShade;
				}
			}
		}

		// Token: 0x0400296D RID: 10605
		[SerializeField]
		private PolygonMask backgroundPolygon;

		// Token: 0x0400296E RID: 10606
		[SerializeField]
		private PolygonMask leftArrowPolygon;

		// Token: 0x0400296F RID: 10607
		[SerializeField]
		private PolygonMask rightArrowPolygon;

		// Token: 0x04002970 RID: 10608
		[SerializeField]
		private PolygonMask leftArrowShade;

		// Token: 0x04002971 RID: 10609
		[SerializeField]
		private PolygonMask rightArrowShade;
	}
}
