using System;

namespace Rewired
{
	// Token: 0x020004A1 RID: 1185
	public sealed class GamepadTemplate : ControllerTemplate, IGamepadTemplate, IControllerTemplate
	{
		// Token: 0x06001C81 RID: 7297 RVA: 0x0004DD7C File Offset: 0x0004C17C
		public GamepadTemplate(object payload) : base(payload)
		{
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06001C82 RID: 7298 RVA: 0x0004DD85 File Offset: 0x0004C185
		IControllerTemplateButton IGamepadTemplate.actionBottomRow1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(4);
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06001C83 RID: 7299 RVA: 0x0004DD8E File Offset: 0x0004C18E
		IControllerTemplateButton IGamepadTemplate.a
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(4);
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06001C84 RID: 7300 RVA: 0x0004DD97 File Offset: 0x0004C197
		IControllerTemplateButton IGamepadTemplate.actionBottomRow2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(5);
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06001C85 RID: 7301 RVA: 0x0004DDA0 File Offset: 0x0004C1A0
		IControllerTemplateButton IGamepadTemplate.b
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(5);
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06001C86 RID: 7302 RVA: 0x0004DDA9 File Offset: 0x0004C1A9
		IControllerTemplateButton IGamepadTemplate.actionBottomRow3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(6);
			}
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06001C87 RID: 7303 RVA: 0x0004DDB2 File Offset: 0x0004C1B2
		IControllerTemplateButton IGamepadTemplate.c
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(6);
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06001C88 RID: 7304 RVA: 0x0004DDBB File Offset: 0x0004C1BB
		IControllerTemplateButton IGamepadTemplate.actionTopRow1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(7);
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06001C89 RID: 7305 RVA: 0x0004DDC4 File Offset: 0x0004C1C4
		IControllerTemplateButton IGamepadTemplate.x
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(7);
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06001C8A RID: 7306 RVA: 0x0004DDCD File Offset: 0x0004C1CD
		IControllerTemplateButton IGamepadTemplate.actionTopRow2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(8);
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06001C8B RID: 7307 RVA: 0x0004DDD6 File Offset: 0x0004C1D6
		IControllerTemplateButton IGamepadTemplate.y
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(8);
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06001C8C RID: 7308 RVA: 0x0004DDDF File Offset: 0x0004C1DF
		IControllerTemplateButton IGamepadTemplate.actionTopRow3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(9);
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06001C8D RID: 7309 RVA: 0x0004DDE9 File Offset: 0x0004C1E9
		IControllerTemplateButton IGamepadTemplate.z
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(9);
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06001C8E RID: 7310 RVA: 0x0004DDF3 File Offset: 0x0004C1F3
		IControllerTemplateButton IGamepadTemplate.leftShoulder1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(10);
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06001C8F RID: 7311 RVA: 0x0004DDFD File Offset: 0x0004C1FD
		IControllerTemplateButton IGamepadTemplate.leftBumper
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(10);
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06001C90 RID: 7312 RVA: 0x0004DE07 File Offset: 0x0004C207
		IControllerTemplateAxis IGamepadTemplate.leftShoulder2
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(11);
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06001C91 RID: 7313 RVA: 0x0004DE11 File Offset: 0x0004C211
		IControllerTemplateAxis IGamepadTemplate.leftTrigger
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(11);
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06001C92 RID: 7314 RVA: 0x0004DE1B File Offset: 0x0004C21B
		IControllerTemplateButton IGamepadTemplate.rightShoulder1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(12);
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06001C93 RID: 7315 RVA: 0x0004DE25 File Offset: 0x0004C225
		IControllerTemplateButton IGamepadTemplate.rightBumper
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(12);
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06001C94 RID: 7316 RVA: 0x0004DE2F File Offset: 0x0004C22F
		IControllerTemplateAxis IGamepadTemplate.rightShoulder2
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(13);
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06001C95 RID: 7317 RVA: 0x0004DE39 File Offset: 0x0004C239
		IControllerTemplateAxis IGamepadTemplate.rightTrigger
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(13);
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06001C96 RID: 7318 RVA: 0x0004DE43 File Offset: 0x0004C243
		IControllerTemplateButton IGamepadTemplate.center1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(14);
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06001C97 RID: 7319 RVA: 0x0004DE4D File Offset: 0x0004C24D
		IControllerTemplateButton IGamepadTemplate.back
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(14);
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06001C98 RID: 7320 RVA: 0x0004DE57 File Offset: 0x0004C257
		IControllerTemplateButton IGamepadTemplate.center2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(15);
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06001C99 RID: 7321 RVA: 0x0004DE61 File Offset: 0x0004C261
		IControllerTemplateButton IGamepadTemplate.start
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(15);
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06001C9A RID: 7322 RVA: 0x0004DE6B File Offset: 0x0004C26B
		IControllerTemplateButton IGamepadTemplate.center3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(16);
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06001C9B RID: 7323 RVA: 0x0004DE75 File Offset: 0x0004C275
		IControllerTemplateButton IGamepadTemplate.guide
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(16);
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06001C9C RID: 7324 RVA: 0x0004DE7F File Offset: 0x0004C27F
		IControllerTemplateThumbStick IGamepadTemplate.leftStick
		{
			get
			{
				return base.GetElement<IControllerTemplateThumbStick>(23);
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06001C9D RID: 7325 RVA: 0x0004DE89 File Offset: 0x0004C289
		IControllerTemplateThumbStick IGamepadTemplate.rightStick
		{
			get
			{
				return base.GetElement<IControllerTemplateThumbStick>(24);
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06001C9E RID: 7326 RVA: 0x0004DE93 File Offset: 0x0004C293
		IControllerTemplateDPad IGamepadTemplate.dPad
		{
			get
			{
				return base.GetElement<IControllerTemplateDPad>(25);
			}
		}

		// Token: 0x0400112A RID: 4394
		public static readonly Guid typeGuid = new Guid("83b427e4-086f-47f3-bb06-be266abd1ca5");

		// Token: 0x0400112B RID: 4395
		public const int elementId_leftStickX = 0;

		// Token: 0x0400112C RID: 4396
		public const int elementId_leftStickY = 1;

		// Token: 0x0400112D RID: 4397
		public const int elementId_rightStickX = 2;

		// Token: 0x0400112E RID: 4398
		public const int elementId_rightStickY = 3;

		// Token: 0x0400112F RID: 4399
		public const int elementId_actionBottomRow1 = 4;

		// Token: 0x04001130 RID: 4400
		public const int elementId_a = 4;

		// Token: 0x04001131 RID: 4401
		public const int elementId_actionBottomRow2 = 5;

		// Token: 0x04001132 RID: 4402
		public const int elementId_b = 5;

		// Token: 0x04001133 RID: 4403
		public const int elementId_actionBottomRow3 = 6;

		// Token: 0x04001134 RID: 4404
		public const int elementId_c = 6;

		// Token: 0x04001135 RID: 4405
		public const int elementId_actionTopRow1 = 7;

		// Token: 0x04001136 RID: 4406
		public const int elementId_x = 7;

		// Token: 0x04001137 RID: 4407
		public const int elementId_actionTopRow2 = 8;

		// Token: 0x04001138 RID: 4408
		public const int elementId_y = 8;

		// Token: 0x04001139 RID: 4409
		public const int elementId_actionTopRow3 = 9;

		// Token: 0x0400113A RID: 4410
		public const int elementId_z = 9;

		// Token: 0x0400113B RID: 4411
		public const int elementId_leftShoulder1 = 10;

		// Token: 0x0400113C RID: 4412
		public const int elementId_leftBumper = 10;

		// Token: 0x0400113D RID: 4413
		public const int elementId_leftShoulder2 = 11;

		// Token: 0x0400113E RID: 4414
		public const int elementId_leftTrigger = 11;

		// Token: 0x0400113F RID: 4415
		public const int elementId_rightShoulder1 = 12;

		// Token: 0x04001140 RID: 4416
		public const int elementId_rightBumper = 12;

		// Token: 0x04001141 RID: 4417
		public const int elementId_rightShoulder2 = 13;

		// Token: 0x04001142 RID: 4418
		public const int elementId_rightTrigger = 13;

		// Token: 0x04001143 RID: 4419
		public const int elementId_center1 = 14;

		// Token: 0x04001144 RID: 4420
		public const int elementId_back = 14;

		// Token: 0x04001145 RID: 4421
		public const int elementId_center2 = 15;

		// Token: 0x04001146 RID: 4422
		public const int elementId_start = 15;

		// Token: 0x04001147 RID: 4423
		public const int elementId_center3 = 16;

		// Token: 0x04001148 RID: 4424
		public const int elementId_guide = 16;

		// Token: 0x04001149 RID: 4425
		public const int elementId_leftStickButton = 17;

		// Token: 0x0400114A RID: 4426
		public const int elementId_rightStickButton = 18;

		// Token: 0x0400114B RID: 4427
		public const int elementId_dPadUp = 19;

		// Token: 0x0400114C RID: 4428
		public const int elementId_dPadRight = 20;

		// Token: 0x0400114D RID: 4429
		public const int elementId_dPadDown = 21;

		// Token: 0x0400114E RID: 4430
		public const int elementId_dPadLeft = 22;

		// Token: 0x0400114F RID: 4431
		public const int elementId_leftStick = 23;

		// Token: 0x04001150 RID: 4432
		public const int elementId_rightStick = 24;

		// Token: 0x04001151 RID: 4433
		public const int elementId_dPad = 25;
	}
}
