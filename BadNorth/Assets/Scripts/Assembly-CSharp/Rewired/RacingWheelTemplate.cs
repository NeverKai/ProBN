using System;

namespace Rewired
{
	// Token: 0x020004A2 RID: 1186
	public sealed class RacingWheelTemplate : ControllerTemplate, IRacingWheelTemplate, IControllerTemplate
	{
		// Token: 0x06001CA0 RID: 7328 RVA: 0x0004DEAE File Offset: 0x0004C2AE
		public RacingWheelTemplate(object payload) : base(payload)
		{
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06001CA1 RID: 7329 RVA: 0x0004DEB7 File Offset: 0x0004C2B7
		IControllerTemplateAxis IRacingWheelTemplate.wheel
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(0);
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06001CA2 RID: 7330 RVA: 0x0004DEC0 File Offset: 0x0004C2C0
		IControllerTemplateAxis IRacingWheelTemplate.accelerator
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(1);
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06001CA3 RID: 7331 RVA: 0x0004DEC9 File Offset: 0x0004C2C9
		IControllerTemplateAxis IRacingWheelTemplate.brake
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(2);
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06001CA4 RID: 7332 RVA: 0x0004DED2 File Offset: 0x0004C2D2
		IControllerTemplateAxis IRacingWheelTemplate.clutch
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(3);
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06001CA5 RID: 7333 RVA: 0x0004DEDB File Offset: 0x0004C2DB
		IControllerTemplateButton IRacingWheelTemplate.shiftDown
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(4);
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06001CA6 RID: 7334 RVA: 0x0004DEE4 File Offset: 0x0004C2E4
		IControllerTemplateButton IRacingWheelTemplate.shiftUp
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(5);
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06001CA7 RID: 7335 RVA: 0x0004DEED File Offset: 0x0004C2ED
		IControllerTemplateButton IRacingWheelTemplate.wheelButton1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(6);
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06001CA8 RID: 7336 RVA: 0x0004DEF6 File Offset: 0x0004C2F6
		IControllerTemplateButton IRacingWheelTemplate.wheelButton2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(7);
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06001CA9 RID: 7337 RVA: 0x0004DEFF File Offset: 0x0004C2FF
		IControllerTemplateButton IRacingWheelTemplate.wheelButton3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(8);
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06001CAA RID: 7338 RVA: 0x0004DF08 File Offset: 0x0004C308
		IControllerTemplateButton IRacingWheelTemplate.wheelButton4
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(9);
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06001CAB RID: 7339 RVA: 0x0004DF12 File Offset: 0x0004C312
		IControllerTemplateButton IRacingWheelTemplate.wheelButton5
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(10);
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06001CAC RID: 7340 RVA: 0x0004DF1C File Offset: 0x0004C31C
		IControllerTemplateButton IRacingWheelTemplate.wheelButton6
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(11);
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06001CAD RID: 7341 RVA: 0x0004DF26 File Offset: 0x0004C326
		IControllerTemplateButton IRacingWheelTemplate.wheelButton7
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(12);
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06001CAE RID: 7342 RVA: 0x0004DF30 File Offset: 0x0004C330
		IControllerTemplateButton IRacingWheelTemplate.wheelButton8
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(13);
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06001CAF RID: 7343 RVA: 0x0004DF3A File Offset: 0x0004C33A
		IControllerTemplateButton IRacingWheelTemplate.wheelButton9
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(14);
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06001CB0 RID: 7344 RVA: 0x0004DF44 File Offset: 0x0004C344
		IControllerTemplateButton IRacingWheelTemplate.wheelButton10
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(15);
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06001CB1 RID: 7345 RVA: 0x0004DF4E File Offset: 0x0004C34E
		IControllerTemplateButton IRacingWheelTemplate.consoleButton1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(16);
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06001CB2 RID: 7346 RVA: 0x0004DF58 File Offset: 0x0004C358
		IControllerTemplateButton IRacingWheelTemplate.consoleButton2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(17);
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06001CB3 RID: 7347 RVA: 0x0004DF62 File Offset: 0x0004C362
		IControllerTemplateButton IRacingWheelTemplate.consoleButton3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(18);
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06001CB4 RID: 7348 RVA: 0x0004DF6C File Offset: 0x0004C36C
		IControllerTemplateButton IRacingWheelTemplate.consoleButton4
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(19);
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06001CB5 RID: 7349 RVA: 0x0004DF76 File Offset: 0x0004C376
		IControllerTemplateButton IRacingWheelTemplate.consoleButton5
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(20);
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06001CB6 RID: 7350 RVA: 0x0004DF80 File Offset: 0x0004C380
		IControllerTemplateButton IRacingWheelTemplate.consoleButton6
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(21);
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06001CB7 RID: 7351 RVA: 0x0004DF8A File Offset: 0x0004C38A
		IControllerTemplateButton IRacingWheelTemplate.consoleButton7
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(22);
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06001CB8 RID: 7352 RVA: 0x0004DF94 File Offset: 0x0004C394
		IControllerTemplateButton IRacingWheelTemplate.consoleButton8
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(23);
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06001CB9 RID: 7353 RVA: 0x0004DF9E File Offset: 0x0004C39E
		IControllerTemplateButton IRacingWheelTemplate.consoleButton9
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(24);
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06001CBA RID: 7354 RVA: 0x0004DFA8 File Offset: 0x0004C3A8
		IControllerTemplateButton IRacingWheelTemplate.consoleButton10
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(25);
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06001CBB RID: 7355 RVA: 0x0004DFB2 File Offset: 0x0004C3B2
		IControllerTemplateButton IRacingWheelTemplate.shifter1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(26);
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06001CBC RID: 7356 RVA: 0x0004DFBC File Offset: 0x0004C3BC
		IControllerTemplateButton IRacingWheelTemplate.shifter2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(27);
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06001CBD RID: 7357 RVA: 0x0004DFC6 File Offset: 0x0004C3C6
		IControllerTemplateButton IRacingWheelTemplate.shifter3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(28);
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06001CBE RID: 7358 RVA: 0x0004DFD0 File Offset: 0x0004C3D0
		IControllerTemplateButton IRacingWheelTemplate.shifter4
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(29);
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06001CBF RID: 7359 RVA: 0x0004DFDA File Offset: 0x0004C3DA
		IControllerTemplateButton IRacingWheelTemplate.shifter5
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(30);
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06001CC0 RID: 7360 RVA: 0x0004DFE4 File Offset: 0x0004C3E4
		IControllerTemplateButton IRacingWheelTemplate.shifter6
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(31);
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06001CC1 RID: 7361 RVA: 0x0004DFEE File Offset: 0x0004C3EE
		IControllerTemplateButton IRacingWheelTemplate.shifter7
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(32);
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06001CC2 RID: 7362 RVA: 0x0004DFF8 File Offset: 0x0004C3F8
		IControllerTemplateButton IRacingWheelTemplate.shifter8
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(33);
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06001CC3 RID: 7363 RVA: 0x0004E002 File Offset: 0x0004C402
		IControllerTemplateButton IRacingWheelTemplate.shifter9
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(34);
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06001CC4 RID: 7364 RVA: 0x0004E00C File Offset: 0x0004C40C
		IControllerTemplateButton IRacingWheelTemplate.shifter10
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(35);
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06001CC5 RID: 7365 RVA: 0x0004E016 File Offset: 0x0004C416
		IControllerTemplateButton IRacingWheelTemplate.reverseGear
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(44);
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06001CC6 RID: 7366 RVA: 0x0004E020 File Offset: 0x0004C420
		IControllerTemplateButton IRacingWheelTemplate.select
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(36);
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06001CC7 RID: 7367 RVA: 0x0004E02A File Offset: 0x0004C42A
		IControllerTemplateButton IRacingWheelTemplate.start
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(37);
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06001CC8 RID: 7368 RVA: 0x0004E034 File Offset: 0x0004C434
		IControllerTemplateButton IRacingWheelTemplate.systemButton
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(38);
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06001CC9 RID: 7369 RVA: 0x0004E03E File Offset: 0x0004C43E
		IControllerTemplateButton IRacingWheelTemplate.horn
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(43);
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06001CCA RID: 7370 RVA: 0x0004E048 File Offset: 0x0004C448
		IControllerTemplateDPad IRacingWheelTemplate.dPad
		{
			get
			{
				return base.GetElement<IControllerTemplateDPad>(45);
			}
		}

		// Token: 0x04001152 RID: 4434
		public static readonly Guid typeGuid = new Guid("104e31d8-9115-4dd5-a398-2e54d35e6c83");

		// Token: 0x04001153 RID: 4435
		public const int elementId_wheel = 0;

		// Token: 0x04001154 RID: 4436
		public const int elementId_accelerator = 1;

		// Token: 0x04001155 RID: 4437
		public const int elementId_brake = 2;

		// Token: 0x04001156 RID: 4438
		public const int elementId_clutch = 3;

		// Token: 0x04001157 RID: 4439
		public const int elementId_shiftDown = 4;

		// Token: 0x04001158 RID: 4440
		public const int elementId_shiftUp = 5;

		// Token: 0x04001159 RID: 4441
		public const int elementId_wheelButton1 = 6;

		// Token: 0x0400115A RID: 4442
		public const int elementId_wheelButton2 = 7;

		// Token: 0x0400115B RID: 4443
		public const int elementId_wheelButton3 = 8;

		// Token: 0x0400115C RID: 4444
		public const int elementId_wheelButton4 = 9;

		// Token: 0x0400115D RID: 4445
		public const int elementId_wheelButton5 = 10;

		// Token: 0x0400115E RID: 4446
		public const int elementId_wheelButton6 = 11;

		// Token: 0x0400115F RID: 4447
		public const int elementId_wheelButton7 = 12;

		// Token: 0x04001160 RID: 4448
		public const int elementId_wheelButton8 = 13;

		// Token: 0x04001161 RID: 4449
		public const int elementId_wheelButton9 = 14;

		// Token: 0x04001162 RID: 4450
		public const int elementId_wheelButton10 = 15;

		// Token: 0x04001163 RID: 4451
		public const int elementId_consoleButton1 = 16;

		// Token: 0x04001164 RID: 4452
		public const int elementId_consoleButton2 = 17;

		// Token: 0x04001165 RID: 4453
		public const int elementId_consoleButton3 = 18;

		// Token: 0x04001166 RID: 4454
		public const int elementId_consoleButton4 = 19;

		// Token: 0x04001167 RID: 4455
		public const int elementId_consoleButton5 = 20;

		// Token: 0x04001168 RID: 4456
		public const int elementId_consoleButton6 = 21;

		// Token: 0x04001169 RID: 4457
		public const int elementId_consoleButton7 = 22;

		// Token: 0x0400116A RID: 4458
		public const int elementId_consoleButton8 = 23;

		// Token: 0x0400116B RID: 4459
		public const int elementId_consoleButton9 = 24;

		// Token: 0x0400116C RID: 4460
		public const int elementId_consoleButton10 = 25;

		// Token: 0x0400116D RID: 4461
		public const int elementId_shifter1 = 26;

		// Token: 0x0400116E RID: 4462
		public const int elementId_shifter2 = 27;

		// Token: 0x0400116F RID: 4463
		public const int elementId_shifter3 = 28;

		// Token: 0x04001170 RID: 4464
		public const int elementId_shifter4 = 29;

		// Token: 0x04001171 RID: 4465
		public const int elementId_shifter5 = 30;

		// Token: 0x04001172 RID: 4466
		public const int elementId_shifter6 = 31;

		// Token: 0x04001173 RID: 4467
		public const int elementId_shifter7 = 32;

		// Token: 0x04001174 RID: 4468
		public const int elementId_shifter8 = 33;

		// Token: 0x04001175 RID: 4469
		public const int elementId_shifter9 = 34;

		// Token: 0x04001176 RID: 4470
		public const int elementId_shifter10 = 35;

		// Token: 0x04001177 RID: 4471
		public const int elementId_reverseGear = 44;

		// Token: 0x04001178 RID: 4472
		public const int elementId_select = 36;

		// Token: 0x04001179 RID: 4473
		public const int elementId_start = 37;

		// Token: 0x0400117A RID: 4474
		public const int elementId_systemButton = 38;

		// Token: 0x0400117B RID: 4475
		public const int elementId_horn = 43;

		// Token: 0x0400117C RID: 4476
		public const int elementId_dPadUp = 39;

		// Token: 0x0400117D RID: 4477
		public const int elementId_dPadRight = 40;

		// Token: 0x0400117E RID: 4478
		public const int elementId_dPadDown = 41;

		// Token: 0x0400117F RID: 4479
		public const int elementId_dPadLeft = 42;

		// Token: 0x04001180 RID: 4480
		public const int elementId_dPad = 45;
	}
}
