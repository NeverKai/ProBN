using System;

namespace Rewired
{
	// Token: 0x020004A4 RID: 1188
	public sealed class FlightYokeTemplate : ControllerTemplate, IFlightYokeTemplate, IControllerTemplate
	{
		// Token: 0x06001D26 RID: 7462 RVA: 0x0004E456 File Offset: 0x0004C856
		public FlightYokeTemplate(object payload) : base(payload)
		{
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06001D27 RID: 7463 RVA: 0x0004E45F File Offset: 0x0004C85F
		IControllerTemplateButton IFlightYokeTemplate.leftPaddle
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(59);
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06001D28 RID: 7464 RVA: 0x0004E469 File Offset: 0x0004C869
		IControllerTemplateButton IFlightYokeTemplate.rightPaddle
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(60);
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06001D29 RID: 7465 RVA: 0x0004E473 File Offset: 0x0004C873
		IControllerTemplateButton IFlightYokeTemplate.leftGripButton1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(7);
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06001D2A RID: 7466 RVA: 0x0004E47C File Offset: 0x0004C87C
		IControllerTemplateButton IFlightYokeTemplate.leftGripButton2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(8);
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06001D2B RID: 7467 RVA: 0x0004E485 File Offset: 0x0004C885
		IControllerTemplateButton IFlightYokeTemplate.leftGripButton3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(9);
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06001D2C RID: 7468 RVA: 0x0004E48F File Offset: 0x0004C88F
		IControllerTemplateButton IFlightYokeTemplate.leftGripButton4
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(10);
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06001D2D RID: 7469 RVA: 0x0004E499 File Offset: 0x0004C899
		IControllerTemplateButton IFlightYokeTemplate.leftGripButton5
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(11);
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06001D2E RID: 7470 RVA: 0x0004E4A3 File Offset: 0x0004C8A3
		IControllerTemplateButton IFlightYokeTemplate.leftGripButton6
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(12);
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06001D2F RID: 7471 RVA: 0x0004E4AD File Offset: 0x0004C8AD
		IControllerTemplateButton IFlightYokeTemplate.rightGripButton1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(13);
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06001D30 RID: 7472 RVA: 0x0004E4B7 File Offset: 0x0004C8B7
		IControllerTemplateButton IFlightYokeTemplate.rightGripButton2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(14);
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06001D31 RID: 7473 RVA: 0x0004E4C1 File Offset: 0x0004C8C1
		IControllerTemplateButton IFlightYokeTemplate.rightGripButton3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(15);
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06001D32 RID: 7474 RVA: 0x0004E4CB File Offset: 0x0004C8CB
		IControllerTemplateButton IFlightYokeTemplate.rightGripButton4
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(16);
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06001D33 RID: 7475 RVA: 0x0004E4D5 File Offset: 0x0004C8D5
		IControllerTemplateButton IFlightYokeTemplate.rightGripButton5
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(17);
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06001D34 RID: 7476 RVA: 0x0004E4DF File Offset: 0x0004C8DF
		IControllerTemplateButton IFlightYokeTemplate.rightGripButton6
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(18);
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06001D35 RID: 7477 RVA: 0x0004E4E9 File Offset: 0x0004C8E9
		IControllerTemplateButton IFlightYokeTemplate.centerButton1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(19);
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06001D36 RID: 7478 RVA: 0x0004E4F3 File Offset: 0x0004C8F3
		IControllerTemplateButton IFlightYokeTemplate.centerButton2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(20);
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06001D37 RID: 7479 RVA: 0x0004E4FD File Offset: 0x0004C8FD
		IControllerTemplateButton IFlightYokeTemplate.centerButton3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(21);
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06001D38 RID: 7480 RVA: 0x0004E507 File Offset: 0x0004C907
		IControllerTemplateButton IFlightYokeTemplate.centerButton4
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(22);
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06001D39 RID: 7481 RVA: 0x0004E511 File Offset: 0x0004C911
		IControllerTemplateButton IFlightYokeTemplate.centerButton5
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(23);
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06001D3A RID: 7482 RVA: 0x0004E51B File Offset: 0x0004C91B
		IControllerTemplateButton IFlightYokeTemplate.centerButton6
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(24);
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06001D3B RID: 7483 RVA: 0x0004E525 File Offset: 0x0004C925
		IControllerTemplateButton IFlightYokeTemplate.centerButton7
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(25);
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06001D3C RID: 7484 RVA: 0x0004E52F File Offset: 0x0004C92F
		IControllerTemplateButton IFlightYokeTemplate.centerButton8
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(26);
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06001D3D RID: 7485 RVA: 0x0004E539 File Offset: 0x0004C939
		IControllerTemplateButton IFlightYokeTemplate.wheel1Up
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(53);
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06001D3E RID: 7486 RVA: 0x0004E543 File Offset: 0x0004C943
		IControllerTemplateButton IFlightYokeTemplate.wheel1Down
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(54);
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06001D3F RID: 7487 RVA: 0x0004E54D File Offset: 0x0004C94D
		IControllerTemplateButton IFlightYokeTemplate.wheel1Press
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(55);
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06001D40 RID: 7488 RVA: 0x0004E557 File Offset: 0x0004C957
		IControllerTemplateButton IFlightYokeTemplate.wheel2Up
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(56);
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06001D41 RID: 7489 RVA: 0x0004E561 File Offset: 0x0004C961
		IControllerTemplateButton IFlightYokeTemplate.wheel2Down
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(57);
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06001D42 RID: 7490 RVA: 0x0004E56B File Offset: 0x0004C96B
		IControllerTemplateButton IFlightYokeTemplate.wheel2Press
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(58);
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06001D43 RID: 7491 RVA: 0x0004E575 File Offset: 0x0004C975
		IControllerTemplateButton IFlightYokeTemplate.consoleButton1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(43);
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06001D44 RID: 7492 RVA: 0x0004E57F File Offset: 0x0004C97F
		IControllerTemplateButton IFlightYokeTemplate.consoleButton2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(44);
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06001D45 RID: 7493 RVA: 0x0004E589 File Offset: 0x0004C989
		IControllerTemplateButton IFlightYokeTemplate.consoleButton3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(45);
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06001D46 RID: 7494 RVA: 0x0004E593 File Offset: 0x0004C993
		IControllerTemplateButton IFlightYokeTemplate.consoleButton4
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(46);
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06001D47 RID: 7495 RVA: 0x0004E59D File Offset: 0x0004C99D
		IControllerTemplateButton IFlightYokeTemplate.consoleButton5
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(47);
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06001D48 RID: 7496 RVA: 0x0004E5A7 File Offset: 0x0004C9A7
		IControllerTemplateButton IFlightYokeTemplate.consoleButton6
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(48);
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06001D49 RID: 7497 RVA: 0x0004E5B1 File Offset: 0x0004C9B1
		IControllerTemplateButton IFlightYokeTemplate.consoleButton7
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(49);
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06001D4A RID: 7498 RVA: 0x0004E5BB File Offset: 0x0004C9BB
		IControllerTemplateButton IFlightYokeTemplate.consoleButton8
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(50);
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06001D4B RID: 7499 RVA: 0x0004E5C5 File Offset: 0x0004C9C5
		IControllerTemplateButton IFlightYokeTemplate.consoleButton9
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(51);
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06001D4C RID: 7500 RVA: 0x0004E5CF File Offset: 0x0004C9CF
		IControllerTemplateButton IFlightYokeTemplate.consoleButton10
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(52);
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06001D4D RID: 7501 RVA: 0x0004E5D9 File Offset: 0x0004C9D9
		IControllerTemplateButton IFlightYokeTemplate.mode1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(61);
			}
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06001D4E RID: 7502 RVA: 0x0004E5E3 File Offset: 0x0004C9E3
		IControllerTemplateButton IFlightYokeTemplate.mode2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(62);
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06001D4F RID: 7503 RVA: 0x0004E5ED File Offset: 0x0004C9ED
		IControllerTemplateButton IFlightYokeTemplate.mode3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(63);
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06001D50 RID: 7504 RVA: 0x0004E5F7 File Offset: 0x0004C9F7
		IControllerTemplateYoke IFlightYokeTemplate.yoke
		{
			get
			{
				return base.GetElement<IControllerTemplateYoke>(69);
			}
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06001D51 RID: 7505 RVA: 0x0004E601 File Offset: 0x0004CA01
		IControllerTemplateThrottle IFlightYokeTemplate.lever1
		{
			get
			{
				return base.GetElement<IControllerTemplateThrottle>(70);
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06001D52 RID: 7506 RVA: 0x0004E60B File Offset: 0x0004CA0B
		IControllerTemplateThrottle IFlightYokeTemplate.lever2
		{
			get
			{
				return base.GetElement<IControllerTemplateThrottle>(71);
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06001D53 RID: 7507 RVA: 0x0004E615 File Offset: 0x0004CA15
		IControllerTemplateThrottle IFlightYokeTemplate.lever3
		{
			get
			{
				return base.GetElement<IControllerTemplateThrottle>(72);
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06001D54 RID: 7508 RVA: 0x0004E61F File Offset: 0x0004CA1F
		IControllerTemplateThrottle IFlightYokeTemplate.lever4
		{
			get
			{
				return base.GetElement<IControllerTemplateThrottle>(73);
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06001D55 RID: 7509 RVA: 0x0004E629 File Offset: 0x0004CA29
		IControllerTemplateThrottle IFlightYokeTemplate.lever5
		{
			get
			{
				return base.GetElement<IControllerTemplateThrottle>(74);
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06001D56 RID: 7510 RVA: 0x0004E633 File Offset: 0x0004CA33
		IControllerTemplateHat IFlightYokeTemplate.leftGripHat
		{
			get
			{
				return base.GetElement<IControllerTemplateHat>(75);
			}
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06001D57 RID: 7511 RVA: 0x0004E63D File Offset: 0x0004CA3D
		IControllerTemplateHat IFlightYokeTemplate.rightGripHat
		{
			get
			{
				return base.GetElement<IControllerTemplateHat>(76);
			}
		}

		// Token: 0x0400122A RID: 4650
		public static readonly Guid typeGuid = new Guid("f311fa16-0ccc-41c0-ac4b-50f7100bb8ff");

		// Token: 0x0400122B RID: 4651
		public const int elementId_rotateYoke = 0;

		// Token: 0x0400122C RID: 4652
		public const int elementId_yokeZ = 1;

		// Token: 0x0400122D RID: 4653
		public const int elementId_leftPaddle = 59;

		// Token: 0x0400122E RID: 4654
		public const int elementId_rightPaddle = 60;

		// Token: 0x0400122F RID: 4655
		public const int elementId_lever1Axis = 2;

		// Token: 0x04001230 RID: 4656
		public const int elementId_lever1MinDetent = 64;

		// Token: 0x04001231 RID: 4657
		public const int elementId_lever2Axis = 3;

		// Token: 0x04001232 RID: 4658
		public const int elementId_lever2MinDetent = 65;

		// Token: 0x04001233 RID: 4659
		public const int elementId_lever3Axis = 4;

		// Token: 0x04001234 RID: 4660
		public const int elementId_lever3MinDetent = 66;

		// Token: 0x04001235 RID: 4661
		public const int elementId_lever4Axis = 5;

		// Token: 0x04001236 RID: 4662
		public const int elementId_lever4MinDetent = 67;

		// Token: 0x04001237 RID: 4663
		public const int elementId_lever5Axis = 6;

		// Token: 0x04001238 RID: 4664
		public const int elementId_lever5MinDetent = 68;

		// Token: 0x04001239 RID: 4665
		public const int elementId_leftGripButton1 = 7;

		// Token: 0x0400123A RID: 4666
		public const int elementId_leftGripButton2 = 8;

		// Token: 0x0400123B RID: 4667
		public const int elementId_leftGripButton3 = 9;

		// Token: 0x0400123C RID: 4668
		public const int elementId_leftGripButton4 = 10;

		// Token: 0x0400123D RID: 4669
		public const int elementId_leftGripButton5 = 11;

		// Token: 0x0400123E RID: 4670
		public const int elementId_leftGripButton6 = 12;

		// Token: 0x0400123F RID: 4671
		public const int elementId_rightGripButton1 = 13;

		// Token: 0x04001240 RID: 4672
		public const int elementId_rightGripButton2 = 14;

		// Token: 0x04001241 RID: 4673
		public const int elementId_rightGripButton3 = 15;

		// Token: 0x04001242 RID: 4674
		public const int elementId_rightGripButton4 = 16;

		// Token: 0x04001243 RID: 4675
		public const int elementId_rightGripButton5 = 17;

		// Token: 0x04001244 RID: 4676
		public const int elementId_rightGripButton6 = 18;

		// Token: 0x04001245 RID: 4677
		public const int elementId_centerButton1 = 19;

		// Token: 0x04001246 RID: 4678
		public const int elementId_centerButton2 = 20;

		// Token: 0x04001247 RID: 4679
		public const int elementId_centerButton3 = 21;

		// Token: 0x04001248 RID: 4680
		public const int elementId_centerButton4 = 22;

		// Token: 0x04001249 RID: 4681
		public const int elementId_centerButton5 = 23;

		// Token: 0x0400124A RID: 4682
		public const int elementId_centerButton6 = 24;

		// Token: 0x0400124B RID: 4683
		public const int elementId_centerButton7 = 25;

		// Token: 0x0400124C RID: 4684
		public const int elementId_centerButton8 = 26;

		// Token: 0x0400124D RID: 4685
		public const int elementId_wheel1Up = 53;

		// Token: 0x0400124E RID: 4686
		public const int elementId_wheel1Down = 54;

		// Token: 0x0400124F RID: 4687
		public const int elementId_wheel1Press = 55;

		// Token: 0x04001250 RID: 4688
		public const int elementId_wheel2Up = 56;

		// Token: 0x04001251 RID: 4689
		public const int elementId_wheel2Down = 57;

		// Token: 0x04001252 RID: 4690
		public const int elementId_wheel2Press = 58;

		// Token: 0x04001253 RID: 4691
		public const int elementId_leftGripHatUp = 27;

		// Token: 0x04001254 RID: 4692
		public const int elementId_leftGripHatUpRight = 28;

		// Token: 0x04001255 RID: 4693
		public const int elementId_leftGripHatRight = 29;

		// Token: 0x04001256 RID: 4694
		public const int elementId_leftGripHatDownRight = 30;

		// Token: 0x04001257 RID: 4695
		public const int elementId_leftGripHatDown = 31;

		// Token: 0x04001258 RID: 4696
		public const int elementId_leftGripHatDownLeft = 32;

		// Token: 0x04001259 RID: 4697
		public const int elementId_leftGripHatLeft = 33;

		// Token: 0x0400125A RID: 4698
		public const int elementId_leftGripHatUpLeft = 34;

		// Token: 0x0400125B RID: 4699
		public const int elementId_rightGripHatUp = 35;

		// Token: 0x0400125C RID: 4700
		public const int elementId_rightGripHatUpRight = 36;

		// Token: 0x0400125D RID: 4701
		public const int elementId_rightGripHatRight = 37;

		// Token: 0x0400125E RID: 4702
		public const int elementId_rightGripHatDownRight = 38;

		// Token: 0x0400125F RID: 4703
		public const int elementId_rightGripHatDown = 39;

		// Token: 0x04001260 RID: 4704
		public const int elementId_rightGripHatDownLeft = 40;

		// Token: 0x04001261 RID: 4705
		public const int elementId_rightGripHatLeft = 41;

		// Token: 0x04001262 RID: 4706
		public const int elementId_rightGripHatUpLeft = 42;

		// Token: 0x04001263 RID: 4707
		public const int elementId_consoleButton1 = 43;

		// Token: 0x04001264 RID: 4708
		public const int elementId_consoleButton2 = 44;

		// Token: 0x04001265 RID: 4709
		public const int elementId_consoleButton3 = 45;

		// Token: 0x04001266 RID: 4710
		public const int elementId_consoleButton4 = 46;

		// Token: 0x04001267 RID: 4711
		public const int elementId_consoleButton5 = 47;

		// Token: 0x04001268 RID: 4712
		public const int elementId_consoleButton6 = 48;

		// Token: 0x04001269 RID: 4713
		public const int elementId_consoleButton7 = 49;

		// Token: 0x0400126A RID: 4714
		public const int elementId_consoleButton8 = 50;

		// Token: 0x0400126B RID: 4715
		public const int elementId_consoleButton9 = 51;

		// Token: 0x0400126C RID: 4716
		public const int elementId_consoleButton10 = 52;

		// Token: 0x0400126D RID: 4717
		public const int elementId_mode1 = 61;

		// Token: 0x0400126E RID: 4718
		public const int elementId_mode2 = 62;

		// Token: 0x0400126F RID: 4719
		public const int elementId_mode3 = 63;

		// Token: 0x04001270 RID: 4720
		public const int elementId_yoke = 69;

		// Token: 0x04001271 RID: 4721
		public const int elementId_lever1 = 70;

		// Token: 0x04001272 RID: 4722
		public const int elementId_lever2 = 71;

		// Token: 0x04001273 RID: 4723
		public const int elementId_lever3 = 72;

		// Token: 0x04001274 RID: 4724
		public const int elementId_lever4 = 73;

		// Token: 0x04001275 RID: 4725
		public const int elementId_lever5 = 74;

		// Token: 0x04001276 RID: 4726
		public const int elementId_leftGripHat = 75;

		// Token: 0x04001277 RID: 4727
		public const int elementId_rightGripHat = 76;
	}
}
