using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using UnityEngine;

namespace Voxels.TowerDefense.ProfileInternals
{
	// Token: 0x020005A1 RID: 1441
	[DebuggerDisplay("{uColor}")]
	[ObjectDumper.LeafAttribute]
	[Serializable]
	public class Color
	{
		// Token: 0x06002587 RID: 9607 RVA: 0x00076CCC File Offset: 0x000750CC
		public Color() : this(Color.black)
		{
		}

		// Token: 0x06002588 RID: 9608 RVA: 0x00076CDC File Offset: 0x000750DC
		public Color(Color color)
		{
			this.uColor = color;
			this.a = (this.b = (this.g = (this.r = -1f)));
		}

		// Token: 0x06002589 RID: 9609 RVA: 0x00076D1C File Offset: 0x0007511C
		public static implicit operator Color(Color color)
		{
			return color.uColor;
		}

		// Token: 0x0600258A RID: 9610 RVA: 0x00076D24 File Offset: 0x00075124
		public static implicit operator Color(Color color)
		{
			return new Color(color);
		}

		// Token: 0x0600258B RID: 9611 RVA: 0x00076D2C File Offset: 0x0007512C
		[OnSerializing]
		private void PreSave(StreamingContext context)
		{
			this.a = this.uColor.a;
			this.b = this.uColor.b;
			this.g = this.uColor.g;
			this.r = this.uColor.r;
		}

		// Token: 0x0600258C RID: 9612 RVA: 0x00076D7D File Offset: 0x0007517D
		[OnDeserialized]
		private void PostLoad(StreamingContext context)
		{
			this.uColor = new Color(this.r, this.g, this.b, this.a);
		}

		// Token: 0x0600258D RID: 9613 RVA: 0x00076DA2 File Offset: 0x000751A2
		public override string ToString()
		{
			return this.uColor.ToString();
		}

		// Token: 0x040017CF RID: 6095
		[NonSerialized]
		public Color uColor;

		// Token: 0x040017D0 RID: 6096
		private float a;

		// Token: 0x040017D1 RID: 6097
		private float b;

		// Token: 0x040017D2 RID: 6098
		private float g;

		// Token: 0x040017D3 RID: 6099
		private float r;
	}
}
