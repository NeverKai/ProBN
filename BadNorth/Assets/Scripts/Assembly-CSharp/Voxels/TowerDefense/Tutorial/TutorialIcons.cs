using System;
using RTM.UISystem;
using UnityEngine;

namespace Voxels.TowerDefense.Tutorial
{
	// Token: 0x02000543 RID: 1347
	public class TutorialIcons : MonoBehaviour
	{
		// Token: 0x06002315 RID: 8981 RVA: 0x00069C14 File Offset: 0x00068014
		public Sprite GetSprite(TutorialIcons.ID id)
		{
			switch (id)
			{
			case TutorialIcons.ID.Mouse1:
				return this.GetMouseIcons().Mouse1;
			case TutorialIcons.ID.Mouse2:
				return this.GetMouseIcons().Mouse2;
			case TutorialIcons.ID.MouseWheel:
				return this.GetMouseIcons().MouseWheel;
			case TutorialIcons.ID.TouchTap:
				return this.touchTap;
			case TutorialIcons.ID.TouchDrag:
				return this.touchDrag;
			case TutorialIcons.ID.JoystickTriggerLeft:
				return Singleton<UIManager>.instance.GetIconCollection().triggerLeft;
			case TutorialIcons.ID.JoystickTriggerRight:
				return Singleton<UIManager>.instance.GetIconCollection().triggerRight;
			case TutorialIcons.ID.JoystickBumperLeft:
				return Singleton<UIManager>.instance.GetIconCollection().bumperLeft;
			case TutorialIcons.ID.JoystickBumperRight:
				return Singleton<UIManager>.instance.GetIconCollection().bumperRight;
			case TutorialIcons.ID.JoystickStickLeft:
				return Singleton<UIManager>.instance.GetIconCollection().stickLeft;
			case TutorialIcons.ID.JoystickStickRight:
				return Singleton<UIManager>.instance.GetIconCollection().stickRight;
			case TutorialIcons.ID.JoystickSubmit:
				return Singleton<UIManager>.instance.GetActionIcon(EUIPadAction.Submit);
			case TutorialIcons.ID.JoystickDPadUp:
				return Singleton<UIManager>.instance.GetIconCollection().dPadUp;
			case TutorialIcons.ID.JoystickDPadDown:
				return Singleton<UIManager>.instance.GetIconCollection().dPadDown;
			case TutorialIcons.ID.JoystickDPadLeft:
				return Singleton<UIManager>.instance.GetIconCollection().dPadLeft;
			case TutorialIcons.ID.JoystickDPadRight:
				return Singleton<UIManager>.instance.GetIconCollection().dPadLeft;
			default:
				return null;
			}
		}

		// Token: 0x06002316 RID: 8982 RVA: 0x00069D52 File Offset: 0x00068152
		private TutorialIcons.MouseIcons GetMouseIcons()
		{
			return (!Platform.Is(EPlatform.Mac)) ? this.WinMouse : this.macMouse;
		}

		// Token: 0x04001590 RID: 5520
		[Header("Mouse")]
		[SerializeField]
		[SpritePreview]
		private TutorialIcons.MouseIcons WinMouse;

		// Token: 0x04001591 RID: 5521
		[SerializeField]
		[SpritePreview]
		private TutorialIcons.MouseIcons macMouse;

		// Token: 0x04001592 RID: 5522
		[Header("Touch")]
		[SerializeField]
		[SpritePreview]
		public Sprite touchTap;

		// Token: 0x04001593 RID: 5523
		[SerializeField]
		[SpritePreview]
		public Sprite touchDrag;

		// Token: 0x02000544 RID: 1348
		[Serializable]
		private class MouseIcons
		{
			// Token: 0x04001594 RID: 5524
			[SpritePreview]
			public Sprite Mouse1;

			// Token: 0x04001595 RID: 5525
			[SpritePreview]
			public Sprite Mouse2;

			// Token: 0x04001596 RID: 5526
			[SpritePreview]
			public Sprite MouseWheel;
		}

		// Token: 0x02000545 RID: 1349
		public enum ID
		{
			// Token: 0x04001598 RID: 5528
			None,
			// Token: 0x04001599 RID: 5529
			Mouse1,
			// Token: 0x0400159A RID: 5530
			Mouse2,
			// Token: 0x0400159B RID: 5531
			MouseWheel,
			// Token: 0x0400159C RID: 5532
			TouchTap,
			// Token: 0x0400159D RID: 5533
			TouchDrag,
			// Token: 0x0400159E RID: 5534
			JoystickTriggerLeft,
			// Token: 0x0400159F RID: 5535
			JoystickTriggerRight,
			// Token: 0x040015A0 RID: 5536
			JoystickBumperLeft,
			// Token: 0x040015A1 RID: 5537
			JoystickBumperRight,
			// Token: 0x040015A2 RID: 5538
			JoystickStickLeft,
			// Token: 0x040015A3 RID: 5539
			JoystickStickRight,
			// Token: 0x040015A4 RID: 5540
			JoystickSubmit,
			// Token: 0x040015A5 RID: 5541
			JoystickDPadUp,
			// Token: 0x040015A6 RID: 5542
			JoystickDPadDown,
			// Token: 0x040015A7 RID: 5543
			JoystickDPadLeft,
			// Token: 0x040015A8 RID: 5544
			JoystickDPadRight
		}
	}
}
