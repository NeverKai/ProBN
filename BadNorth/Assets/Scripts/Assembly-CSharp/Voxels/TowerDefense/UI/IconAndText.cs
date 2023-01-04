using System;
using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x020008D6 RID: 2262
	public class IconAndText : MonoBehaviour
	{
		// Token: 0x04002993 RID: 10643
		[SerializeField]
		public Image[] images;

		// Token: 0x04002994 RID: 10644
		[SerializeField]
		public Localize[] terms;

		// Token: 0x04002995 RID: 10645
		[SerializeField]
		public MaskedSprite[] maskedSprites;
	}
}
