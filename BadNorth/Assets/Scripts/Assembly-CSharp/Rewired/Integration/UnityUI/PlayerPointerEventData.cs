using System;
using System.Text;
using Rewired.UI;
using UnityEngine.EventSystems;

namespace Rewired.Integration.UnityUI
{
	// Token: 0x02000492 RID: 1170
	public class PlayerPointerEventData : PointerEventData
	{
		// Token: 0x06001AE3 RID: 6883 RVA: 0x0004AEDD File Offset: 0x000492DD
		public PlayerPointerEventData(EventSystem eventSystem) : base(eventSystem)
		{
			this.playerId = -1;
			this.inputSourceIndex = -1;
			this.buttonIndex = -1;
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06001AE4 RID: 6884 RVA: 0x0004AEFB File Offset: 0x000492FB
		// (set) Token: 0x06001AE5 RID: 6885 RVA: 0x0004AF03 File Offset: 0x00049303
		public int playerId { get; set; }

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06001AE6 RID: 6886 RVA: 0x0004AF0C File Offset: 0x0004930C
		// (set) Token: 0x06001AE7 RID: 6887 RVA: 0x0004AF14 File Offset: 0x00049314
		public int inputSourceIndex { get; set; }

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06001AE8 RID: 6888 RVA: 0x0004AF1D File Offset: 0x0004931D
		// (set) Token: 0x06001AE9 RID: 6889 RVA: 0x0004AF25 File Offset: 0x00049325
		public IMouseInputSource mouseSource { get; set; }

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06001AEA RID: 6890 RVA: 0x0004AF2E File Offset: 0x0004932E
		// (set) Token: 0x06001AEB RID: 6891 RVA: 0x0004AF36 File Offset: 0x00049336
		public ITouchInputSource touchSource { get; set; }

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06001AEC RID: 6892 RVA: 0x0004AF3F File Offset: 0x0004933F
		// (set) Token: 0x06001AED RID: 6893 RVA: 0x0004AF47 File Offset: 0x00049347
		public PointerEventType sourceType { get; set; }

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06001AEE RID: 6894 RVA: 0x0004AF50 File Offset: 0x00049350
		// (set) Token: 0x06001AEF RID: 6895 RVA: 0x0004AF58 File Offset: 0x00049358
		public int buttonIndex { get; set; }

		// Token: 0x06001AF0 RID: 6896 RVA: 0x0004AF64 File Offset: 0x00049364
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("<b>Player Id</b>: " + this.playerId);
			stringBuilder.AppendLine("<b>Mouse Source</b>: " + this.mouseSource);
			stringBuilder.AppendLine("<b>Input Source Index</b>: " + this.inputSourceIndex);
			stringBuilder.AppendLine("<b>Touch Source/b>: " + this.touchSource);
			stringBuilder.AppendLine("<b>Source Type</b>: " + this.sourceType);
			stringBuilder.AppendLine("<b>Button Index</b>: " + this.buttonIndex);
			stringBuilder.Append(base.ToString());
			return stringBuilder.ToString();
		}
	}
}
