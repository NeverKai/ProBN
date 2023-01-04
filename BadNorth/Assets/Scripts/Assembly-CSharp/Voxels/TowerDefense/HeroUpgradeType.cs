using System;
using I2.Loc;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x02000839 RID: 2105
	[Serializable]
	public class HeroUpgradeType : ScriptableObject
	{
		// Token: 0x04002537 RID: 9527
		public HeroUpgradeTypeEnum typeEnum;

		// Token: 0x04002538 RID: 9528
		[TermsPopup("")]
		public string nameTerm;

		// Token: 0x04002539 RID: 9529
		[TermsPopup("")]
		public string descriptionTerm;

		// Token: 0x0400253A RID: 9530
		[TermsPopup("")]
		public string unknownNameTerm;

		// Token: 0x0400253B RID: 9531
		[TermsPopup("")]
		public string unknownDescriptionTerm;

		// Token: 0x0400253C RID: 9532
		[TermsPopup("")]
		public string startItemLockedTerm;

		// Token: 0x0400253D RID: 9533
		[TermsPopup("")]
		public string startItemUnlockedTerm;

		// Token: 0x0400253E RID: 9534
		public bool canBeStartItem;

		// Token: 0x0400253F RID: 9535
		[SpritePreview]
		public Sprite mask;

		// Token: 0x04002540 RID: 9536
		[SpritePreview]
		public Sprite unknownIcon;
	}
}
