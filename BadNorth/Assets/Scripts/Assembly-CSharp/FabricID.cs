using System;
using Rewired;
using RTM.Input;

// Token: 0x02000396 RID: 918
public static class FabricID
{
	// Token: 0x170000F9 RID: 249
	// (get) Token: 0x060014F2 RID: 5362 RVA: 0x0002B7BA File Offset: 0x00029BBA
	private static ControllerType controllerType
	{
		get
		{
			return InputHelpers.GetControllerType();
		}
	}

	// Token: 0x170000FA RID: 250
	// (get) Token: 0x060014F3 RID: 5363 RVA: 0x0002B7C4 File Offset: 0x00029BC4
	public static FabricEventReference uiButtonClick
	{
		get
		{
			ControllerType controllerType = FabricID.controllerType;
			if (controllerType != ControllerType.Mouse)
			{
				return FabricID.uiButtonClickGamePad;
			}
			return FabricID.uiButtonClickMouse;
		}
	}

	// Token: 0x04000D02 RID: 3330
	private static readonly FabricEventReference uiButtonClickMouse = "UI/ButtonClick";

	// Token: 0x04000D03 RID: 3331
	private static readonly FabricEventReference uiButtonClickGamePad = "UI/ButtonClickGamepad";

	// Token: 0x04000D04 RID: 3332
	public static readonly FabricEventReference uiError = "UI/InGame/Error";

	// Token: 0x04000D05 RID: 3333
	public static readonly FabricEventReference uiFocus = "UI/Menu/HoverGamepad";

	// Token: 0x04000D06 RID: 3334
	public static readonly FabricEventReference uiHover = "UI/Menu/Hover";

	// Token: 0x04000D07 RID: 3335
	public static readonly FabricEventReference uiBack = "UI/Menu/Back";

	// Token: 0x04000D08 RID: 3336
	public static readonly FabricEventReference settingChange = "UI/Menu/Toogle";

	// Token: 0x04000D09 RID: 3337
	public static readonly FabricEventReference exitLevel = "UI/InGame/ExitLevel";
}
