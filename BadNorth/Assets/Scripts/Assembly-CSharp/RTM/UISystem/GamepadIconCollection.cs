using System;
using RTM.Input;
using UnityEngine;

namespace RTM.UISystem
{
	// Token: 0x020004CD RID: 1229
	[Serializable]
	public class GamepadIconCollection : ScriptableObject
	{
		// Token: 0x06001F06 RID: 7942 RVA: 0x000534A8 File Offset: 0x000518A8
		public Sprite GetSpriteFor(EUIPadAction action)
		{
			switch (action)
			{
			case EUIPadAction.Submit:
				return (!InputHelpers.IsSelectInverted()) ? this.actionBottomRow1 : this.actionBottomRow2;
			case EUIPadAction.Secondary:
				return this.actionTopRow1;
			case EUIPadAction.Tertiary:
				return this.actionTopRow2;
			case EUIPadAction.Cancel:
				return (!InputHelpers.IsSelectInverted()) ? this.actionBottomRow2 : this.actionBottomRow1;
			case EUIPadAction.TabRight:
				return this.bumperRight;
			case EUIPadAction.TabLeft:
				return this.bumperLeft;
			default:
				return null;
			}
		}

		// Token: 0x0400133D RID: 4925
		[Header("Action (face) buttons")]
		[SpritePreview]
		public Sprite actionTopRow1;

		// Token: 0x0400133E RID: 4926
		[SpritePreview]
		public Sprite actionTopRow2;

		// Token: 0x0400133F RID: 4927
		[SpritePreview]
		public Sprite actionBottomRow1;

		// Token: 0x04001340 RID: 4928
		[SpritePreview]
		public Sprite actionBottomRow2;

		// Token: 0x04001341 RID: 4929
		[Header("D-Pad")]
		[SpritePreview]
		public Sprite dPadLeft;

		// Token: 0x04001342 RID: 4930
		[SpritePreview]
		public Sprite dPadRight;

		// Token: 0x04001343 RID: 4931
		[SpritePreview]
		public Sprite dPadUp;

		// Token: 0x04001344 RID: 4932
		[SpritePreview]
		public Sprite dPadDown;

		// Token: 0x04001345 RID: 4933
		[SpritePreview]
		public Sprite dPadLeftRight;

		// Token: 0x04001346 RID: 4934
		[SpritePreview]
		public Sprite dPadUpDown;

		// Token: 0x04001347 RID: 4935
		[Header("Bumpers")]
		[SpritePreview]
		public Sprite bumperLeft;

		// Token: 0x04001348 RID: 4936
		[SpritePreview]
		public Sprite bumperRight;

		// Token: 0x04001349 RID: 4937
		[Header("Triggers")]
		[SpritePreview]
		public Sprite triggerLeft;

		// Token: 0x0400134A RID: 4938
		[SpritePreview]
		public Sprite triggerRight;

		// Token: 0x0400134B RID: 4939
		[Header("Analogue Stick")]
		[SpritePreview]
		public Sprite stickLeft;

		// Token: 0x0400134C RID: 4940
		[SpritePreview]
		public Sprite stickRight;
	}
}
