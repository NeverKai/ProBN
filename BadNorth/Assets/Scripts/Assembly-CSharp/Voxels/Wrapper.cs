using System;
using System.Collections.Generic;
using Voxels.SetRules;

namespace Voxels
{
	// Token: 0x02000638 RID: 1592
	public class Wrapper
	{
		// Token: 0x060028B1 RID: 10417 RVA: 0x00088D5C File Offset: 0x0008715C
		public Wrapper(Placement placement)
		{
			this.placement = placement;
		}

		// Token: 0x060028B2 RID: 10418 RVA: 0x00088DC4 File Offset: 0x000871C4
		public void FillRules(object obj)
		{
			if (obj is IOnLast)
			{
				this.onLast.Add(obj as IOnLast);
			}
			if (obj is IOnPlaced)
			{
				this.onPlaced.Add(obj as IOnPlaced);
			}
			if (obj is IOnRemoved)
			{
				this.onRemoved.Add(obj as IOnRemoved);
			}
			if (obj is IModifyScore)
			{
				this.modifyScore.Add(obj as IModifyScore);
			}
			if (obj is IAllowPlacement)
			{
				this.allowPlacement.Add(obj as IAllowPlacement);
			}
			if (obj is IOnDominoCreated)
			{
				this.onDominoAdded.Add(obj as IOnDominoCreated);
			}
		}

		// Token: 0x04001A4C RID: 6732
		public Placement placement;

		// Token: 0x04001A4D RID: 6733
		public float defaultScore = 1f;

		// Token: 0x04001A4E RID: 6734
		public List<IOnLast> onLast = new List<IOnLast>();

		// Token: 0x04001A4F RID: 6735
		public List<IOnPlaced> onPlaced = new List<IOnPlaced>();

		// Token: 0x04001A50 RID: 6736
		public List<IOnRemoved> onRemoved = new List<IOnRemoved>();

		// Token: 0x04001A51 RID: 6737
		public List<IModifyScore> modifyScore = new List<IModifyScore>();

		// Token: 0x04001A52 RID: 6738
		public List<IAllowPlacement> allowPlacement = new List<IAllowPlacement>();

		// Token: 0x04001A53 RID: 6739
		public List<IOnDominoCreated> onDominoAdded = new List<IOnDominoCreated>();
	}
}
