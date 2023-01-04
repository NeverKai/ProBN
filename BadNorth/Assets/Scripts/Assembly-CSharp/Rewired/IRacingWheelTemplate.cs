using System;

namespace Rewired
{
	// Token: 0x0200049C RID: 1180
	public interface IRacingWheelTemplate : IControllerTemplate
	{
		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06001BA2 RID: 7074
		IControllerTemplateAxis wheel { get; }

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06001BA3 RID: 7075
		IControllerTemplateAxis accelerator { get; }

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06001BA4 RID: 7076
		IControllerTemplateAxis brake { get; }

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06001BA5 RID: 7077
		IControllerTemplateAxis clutch { get; }

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06001BA6 RID: 7078
		IControllerTemplateButton shiftDown { get; }

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06001BA7 RID: 7079
		IControllerTemplateButton shiftUp { get; }

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06001BA8 RID: 7080
		IControllerTemplateButton wheelButton1 { get; }

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06001BA9 RID: 7081
		IControllerTemplateButton wheelButton2 { get; }

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06001BAA RID: 7082
		IControllerTemplateButton wheelButton3 { get; }

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06001BAB RID: 7083
		IControllerTemplateButton wheelButton4 { get; }

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06001BAC RID: 7084
		IControllerTemplateButton wheelButton5 { get; }

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06001BAD RID: 7085
		IControllerTemplateButton wheelButton6 { get; }

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06001BAE RID: 7086
		IControllerTemplateButton wheelButton7 { get; }

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06001BAF RID: 7087
		IControllerTemplateButton wheelButton8 { get; }

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06001BB0 RID: 7088
		IControllerTemplateButton wheelButton9 { get; }

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06001BB1 RID: 7089
		IControllerTemplateButton wheelButton10 { get; }

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06001BB2 RID: 7090
		IControllerTemplateButton consoleButton1 { get; }

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06001BB3 RID: 7091
		IControllerTemplateButton consoleButton2 { get; }

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06001BB4 RID: 7092
		IControllerTemplateButton consoleButton3 { get; }

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06001BB5 RID: 7093
		IControllerTemplateButton consoleButton4 { get; }

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06001BB6 RID: 7094
		IControllerTemplateButton consoleButton5 { get; }

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06001BB7 RID: 7095
		IControllerTemplateButton consoleButton6 { get; }

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06001BB8 RID: 7096
		IControllerTemplateButton consoleButton7 { get; }

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06001BB9 RID: 7097
		IControllerTemplateButton consoleButton8 { get; }

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06001BBA RID: 7098
		IControllerTemplateButton consoleButton9 { get; }

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06001BBB RID: 7099
		IControllerTemplateButton consoleButton10 { get; }

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06001BBC RID: 7100
		IControllerTemplateButton shifter1 { get; }

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06001BBD RID: 7101
		IControllerTemplateButton shifter2 { get; }

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06001BBE RID: 7102
		IControllerTemplateButton shifter3 { get; }

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06001BBF RID: 7103
		IControllerTemplateButton shifter4 { get; }

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06001BC0 RID: 7104
		IControllerTemplateButton shifter5 { get; }

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06001BC1 RID: 7105
		IControllerTemplateButton shifter6 { get; }

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06001BC2 RID: 7106
		IControllerTemplateButton shifter7 { get; }

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06001BC3 RID: 7107
		IControllerTemplateButton shifter8 { get; }

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06001BC4 RID: 7108
		IControllerTemplateButton shifter9 { get; }

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06001BC5 RID: 7109
		IControllerTemplateButton shifter10 { get; }

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06001BC6 RID: 7110
		IControllerTemplateButton reverseGear { get; }

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06001BC7 RID: 7111
		IControllerTemplateButton select { get; }

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06001BC8 RID: 7112
		IControllerTemplateButton start { get; }

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06001BC9 RID: 7113
		IControllerTemplateButton systemButton { get; }

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06001BCA RID: 7114
		IControllerTemplateButton horn { get; }

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06001BCB RID: 7115
		IControllerTemplateDPad dPad { get; }
	}
}
