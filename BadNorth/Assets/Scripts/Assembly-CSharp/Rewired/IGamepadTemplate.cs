using System;

namespace Rewired
{
	// Token: 0x0200049B RID: 1179
	public interface IGamepadTemplate : IControllerTemplate
	{
		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06001B85 RID: 7045
		IControllerTemplateButton actionBottomRow1 { get; }

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06001B86 RID: 7046
		IControllerTemplateButton a { get; }

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06001B87 RID: 7047
		IControllerTemplateButton actionBottomRow2 { get; }

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06001B88 RID: 7048
		IControllerTemplateButton b { get; }

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06001B89 RID: 7049
		IControllerTemplateButton actionBottomRow3 { get; }

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06001B8A RID: 7050
		IControllerTemplateButton c { get; }

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06001B8B RID: 7051
		IControllerTemplateButton actionTopRow1 { get; }

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06001B8C RID: 7052
		IControllerTemplateButton x { get; }

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06001B8D RID: 7053
		IControllerTemplateButton actionTopRow2 { get; }

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06001B8E RID: 7054
		IControllerTemplateButton y { get; }

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06001B8F RID: 7055
		IControllerTemplateButton actionTopRow3 { get; }

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06001B90 RID: 7056
		IControllerTemplateButton z { get; }

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06001B91 RID: 7057
		IControllerTemplateButton leftShoulder1 { get; }

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06001B92 RID: 7058
		IControllerTemplateButton leftBumper { get; }

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06001B93 RID: 7059
		IControllerTemplateAxis leftShoulder2 { get; }

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06001B94 RID: 7060
		IControllerTemplateAxis leftTrigger { get; }

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06001B95 RID: 7061
		IControllerTemplateButton rightShoulder1 { get; }

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06001B96 RID: 7062
		IControllerTemplateButton rightBumper { get; }

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06001B97 RID: 7063
		IControllerTemplateAxis rightShoulder2 { get; }

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06001B98 RID: 7064
		IControllerTemplateAxis rightTrigger { get; }

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06001B99 RID: 7065
		IControllerTemplateButton center1 { get; }

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06001B9A RID: 7066
		IControllerTemplateButton back { get; }

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06001B9B RID: 7067
		IControllerTemplateButton center2 { get; }

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06001B9C RID: 7068
		IControllerTemplateButton start { get; }

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06001B9D RID: 7069
		IControllerTemplateButton center3 { get; }

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06001B9E RID: 7070
		IControllerTemplateButton guide { get; }

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06001B9F RID: 7071
		IControllerTemplateThumbStick leftStick { get; }

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06001BA0 RID: 7072
		IControllerTemplateThumbStick rightStick { get; }

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06001BA1 RID: 7073
		IControllerTemplateDPad dPad { get; }
	}
}
