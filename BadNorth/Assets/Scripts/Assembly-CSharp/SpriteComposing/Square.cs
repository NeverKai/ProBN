using System;
using UnityEngine;

namespace SpriteComposing
{
	// Token: 0x020005CD RID: 1485
	public struct Square
	{
		// Token: 0x060026AD RID: 9901 RVA: 0x0007B380 File Offset: 0x00079780
		public Square(Sheet sheet, Rect rect)
		{
			this.rect = rect;
			this.sheet = sheet;
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x060026AE RID: 9902 RVA: 0x0007B390 File Offset: 0x00079790
		public float xMin
		{
			get
			{
				return this.rect.xMin;
			}
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x060026AF RID: 9903 RVA: 0x0007B3AC File Offset: 0x000797AC
		public float yMin
		{
			get
			{
				return this.rect.yMin;
			}
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x060026B0 RID: 9904 RVA: 0x0007B3C8 File Offset: 0x000797C8
		public float xMax
		{
			get
			{
				return this.rect.xMax;
			}
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x060026B1 RID: 9905 RVA: 0x0007B3E4 File Offset: 0x000797E4
		public float yMax
		{
			get
			{
				return this.rect.yMax;
			}
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x060026B2 RID: 9906 RVA: 0x0007B400 File Offset: 0x00079800
		public float width
		{
			get
			{
				return this.rect.width;
			}
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x060026B3 RID: 9907 RVA: 0x0007B41C File Offset: 0x0007981C
		public float height
		{
			get
			{
				return this.rect.height;
			}
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x060026B4 RID: 9908 RVA: 0x0007B437 File Offset: 0x00079837
		public float area
		{
			get
			{
				return this.width * this.height;
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x060026B5 RID: 9909 RVA: 0x0007B448 File Offset: 0x00079848
		public Vector2 min
		{
			get
			{
				return this.rect.min;
			}
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x060026B6 RID: 9910 RVA: 0x0007B464 File Offset: 0x00079864
		public Vector2 max
		{
			get
			{
				return this.rect.max;
			}
		}

		// Token: 0x040018AE RID: 6318
		[NonSerialized]
		private readonly Sheet sheet;

		// Token: 0x040018AF RID: 6319
		private readonly Rect rect;
	}
}
