using System;
using UnityEngine;

namespace RTM.UISystem
{
	// Token: 0x020004D0 RID: 1232
	public interface IUINavigable
	{
		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06001F09 RID: 7945
		Transform transform { get; }

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06001F0A RID: 7946
		bool isNavigable { get; }

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06001F0B RID: 7947
		bool hasFocus { get; }

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06001F0C RID: 7948
		bool allowRepeatNavigation { get; }

		// Token: 0x06001F0D RID: 7949
		void SetFocus(bool focussed, IUINavigable previousFocus = null);

		// Token: 0x06001F0E RID: 7950
		void Click();

		// Token: 0x06001F0F RID: 7951
		bool ConsumeNavigation(Vector2 navDirection);

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06001F10 RID: 7952
		// (set) Token: 0x06001F11 RID: 7953
		FabricEventReference focusAudio { get; set; }

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06001F12 RID: 7954
		// (set) Token: 0x06001F13 RID: 7955
		FabricEventReference selectAudio { get; set; }

		// Token: 0x1400005F RID: 95
		// (add) Token: 0x06001F14 RID: 7956
		// (remove) Token: 0x06001F15 RID: 7957
		event Action onClicked;

		// Token: 0x14000060 RID: 96
		// (add) Token: 0x06001F16 RID: 7958
		// (remove) Token: 0x06001F17 RID: 7959
		event Action<bool> onFocusChanged;

		// Token: 0x14000061 RID: 97
		// (add) Token: 0x06001F18 RID: 7960
		// (remove) Token: 0x06001F19 RID: 7961
		event Action<Vector2> onConsumedNavigation;
	}
}
