using System;

namespace Rewired
{
	// Token: 0x0200049E RID: 1182
	public interface IFlightYokeTemplate : IControllerTemplate
	{
		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06001C24 RID: 7204
		IControllerTemplateButton leftPaddle { get; }

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06001C25 RID: 7205
		IControllerTemplateButton rightPaddle { get; }

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06001C26 RID: 7206
		IControllerTemplateButton leftGripButton1 { get; }

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06001C27 RID: 7207
		IControllerTemplateButton leftGripButton2 { get; }

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06001C28 RID: 7208
		IControllerTemplateButton leftGripButton3 { get; }

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06001C29 RID: 7209
		IControllerTemplateButton leftGripButton4 { get; }

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06001C2A RID: 7210
		IControllerTemplateButton leftGripButton5 { get; }

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06001C2B RID: 7211
		IControllerTemplateButton leftGripButton6 { get; }

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06001C2C RID: 7212
		IControllerTemplateButton rightGripButton1 { get; }

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06001C2D RID: 7213
		IControllerTemplateButton rightGripButton2 { get; }

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06001C2E RID: 7214
		IControllerTemplateButton rightGripButton3 { get; }

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06001C2F RID: 7215
		IControllerTemplateButton rightGripButton4 { get; }

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06001C30 RID: 7216
		IControllerTemplateButton rightGripButton5 { get; }

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06001C31 RID: 7217
		IControllerTemplateButton rightGripButton6 { get; }

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06001C32 RID: 7218
		IControllerTemplateButton centerButton1 { get; }

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06001C33 RID: 7219
		IControllerTemplateButton centerButton2 { get; }

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06001C34 RID: 7220
		IControllerTemplateButton centerButton3 { get; }

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06001C35 RID: 7221
		IControllerTemplateButton centerButton4 { get; }

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06001C36 RID: 7222
		IControllerTemplateButton centerButton5 { get; }

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06001C37 RID: 7223
		IControllerTemplateButton centerButton6 { get; }

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06001C38 RID: 7224
		IControllerTemplateButton centerButton7 { get; }

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06001C39 RID: 7225
		IControllerTemplateButton centerButton8 { get; }

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06001C3A RID: 7226
		IControllerTemplateButton wheel1Up { get; }

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06001C3B RID: 7227
		IControllerTemplateButton wheel1Down { get; }

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06001C3C RID: 7228
		IControllerTemplateButton wheel1Press { get; }

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06001C3D RID: 7229
		IControllerTemplateButton wheel2Up { get; }

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06001C3E RID: 7230
		IControllerTemplateButton wheel2Down { get; }

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06001C3F RID: 7231
		IControllerTemplateButton wheel2Press { get; }

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06001C40 RID: 7232
		IControllerTemplateButton consoleButton1 { get; }

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06001C41 RID: 7233
		IControllerTemplateButton consoleButton2 { get; }

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06001C42 RID: 7234
		IControllerTemplateButton consoleButton3 { get; }

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06001C43 RID: 7235
		IControllerTemplateButton consoleButton4 { get; }

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06001C44 RID: 7236
		IControllerTemplateButton consoleButton5 { get; }

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06001C45 RID: 7237
		IControllerTemplateButton consoleButton6 { get; }

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06001C46 RID: 7238
		IControllerTemplateButton consoleButton7 { get; }

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06001C47 RID: 7239
		IControllerTemplateButton consoleButton8 { get; }

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06001C48 RID: 7240
		IControllerTemplateButton consoleButton9 { get; }

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06001C49 RID: 7241
		IControllerTemplateButton consoleButton10 { get; }

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06001C4A RID: 7242
		IControllerTemplateButton mode1 { get; }

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06001C4B RID: 7243
		IControllerTemplateButton mode2 { get; }

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06001C4C RID: 7244
		IControllerTemplateButton mode3 { get; }

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06001C4D RID: 7245
		IControllerTemplateYoke yoke { get; }

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06001C4E RID: 7246
		IControllerTemplateThrottle lever1 { get; }

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06001C4F RID: 7247
		IControllerTemplateThrottle lever2 { get; }

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06001C50 RID: 7248
		IControllerTemplateThrottle lever3 { get; }

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06001C51 RID: 7249
		IControllerTemplateThrottle lever4 { get; }

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06001C52 RID: 7250
		IControllerTemplateThrottle lever5 { get; }

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06001C53 RID: 7251
		IControllerTemplateHat leftGripHat { get; }

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06001C54 RID: 7252
		IControllerTemplateHat rightGripHat { get; }
	}
}
