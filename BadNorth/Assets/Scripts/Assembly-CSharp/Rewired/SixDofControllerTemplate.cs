using System;

namespace Rewired
{
	// Token: 0x020004A6 RID: 1190
	public sealed class SixDofControllerTemplate : ControllerTemplate, ISixDofControllerTemplate, IControllerTemplate
	{
		// Token: 0x06001D5E RID: 7518 RVA: 0x0004E68D File Offset: 0x0004CA8D
		public SixDofControllerTemplate(object payload) : base(payload)
		{
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06001D5F RID: 7519 RVA: 0x0004E696 File Offset: 0x0004CA96
		IControllerTemplateAxis ISixDofControllerTemplate.extraAxis1
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(8);
			}
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06001D60 RID: 7520 RVA: 0x0004E69F File Offset: 0x0004CA9F
		IControllerTemplateAxis ISixDofControllerTemplate.extraAxis2
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(9);
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06001D61 RID: 7521 RVA: 0x0004E6A9 File Offset: 0x0004CAA9
		IControllerTemplateAxis ISixDofControllerTemplate.extraAxis3
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(10);
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06001D62 RID: 7522 RVA: 0x0004E6B3 File Offset: 0x0004CAB3
		IControllerTemplateAxis ISixDofControllerTemplate.extraAxis4
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(11);
			}
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06001D63 RID: 7523 RVA: 0x0004E6BD File Offset: 0x0004CABD
		IControllerTemplateButton ISixDofControllerTemplate.button1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(12);
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06001D64 RID: 7524 RVA: 0x0004E6C7 File Offset: 0x0004CAC7
		IControllerTemplateButton ISixDofControllerTemplate.button2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(13);
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06001D65 RID: 7525 RVA: 0x0004E6D1 File Offset: 0x0004CAD1
		IControllerTemplateButton ISixDofControllerTemplate.button3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(14);
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06001D66 RID: 7526 RVA: 0x0004E6DB File Offset: 0x0004CADB
		IControllerTemplateButton ISixDofControllerTemplate.button4
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(15);
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06001D67 RID: 7527 RVA: 0x0004E6E5 File Offset: 0x0004CAE5
		IControllerTemplateButton ISixDofControllerTemplate.button5
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(16);
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06001D68 RID: 7528 RVA: 0x0004E6EF File Offset: 0x0004CAEF
		IControllerTemplateButton ISixDofControllerTemplate.button6
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(17);
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06001D69 RID: 7529 RVA: 0x0004E6F9 File Offset: 0x0004CAF9
		IControllerTemplateButton ISixDofControllerTemplate.button7
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(18);
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06001D6A RID: 7530 RVA: 0x0004E703 File Offset: 0x0004CB03
		IControllerTemplateButton ISixDofControllerTemplate.button8
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(19);
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06001D6B RID: 7531 RVA: 0x0004E70D File Offset: 0x0004CB0D
		IControllerTemplateButton ISixDofControllerTemplate.button9
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(20);
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06001D6C RID: 7532 RVA: 0x0004E717 File Offset: 0x0004CB17
		IControllerTemplateButton ISixDofControllerTemplate.button10
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(21);
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06001D6D RID: 7533 RVA: 0x0004E721 File Offset: 0x0004CB21
		IControllerTemplateButton ISixDofControllerTemplate.button11
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(22);
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06001D6E RID: 7534 RVA: 0x0004E72B File Offset: 0x0004CB2B
		IControllerTemplateButton ISixDofControllerTemplate.button12
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(23);
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06001D6F RID: 7535 RVA: 0x0004E735 File Offset: 0x0004CB35
		IControllerTemplateButton ISixDofControllerTemplate.button13
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(24);
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06001D70 RID: 7536 RVA: 0x0004E73F File Offset: 0x0004CB3F
		IControllerTemplateButton ISixDofControllerTemplate.button14
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(25);
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06001D71 RID: 7537 RVA: 0x0004E749 File Offset: 0x0004CB49
		IControllerTemplateButton ISixDofControllerTemplate.button15
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(26);
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06001D72 RID: 7538 RVA: 0x0004E753 File Offset: 0x0004CB53
		IControllerTemplateButton ISixDofControllerTemplate.button16
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(27);
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06001D73 RID: 7539 RVA: 0x0004E75D File Offset: 0x0004CB5D
		IControllerTemplateButton ISixDofControllerTemplate.button17
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(28);
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06001D74 RID: 7540 RVA: 0x0004E767 File Offset: 0x0004CB67
		IControllerTemplateButton ISixDofControllerTemplate.button18
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(29);
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06001D75 RID: 7541 RVA: 0x0004E771 File Offset: 0x0004CB71
		IControllerTemplateButton ISixDofControllerTemplate.button19
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(30);
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06001D76 RID: 7542 RVA: 0x0004E77B File Offset: 0x0004CB7B
		IControllerTemplateButton ISixDofControllerTemplate.button20
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(31);
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06001D77 RID: 7543 RVA: 0x0004E785 File Offset: 0x0004CB85
		IControllerTemplateButton ISixDofControllerTemplate.button21
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(55);
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06001D78 RID: 7544 RVA: 0x0004E78F File Offset: 0x0004CB8F
		IControllerTemplateButton ISixDofControllerTemplate.button22
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(56);
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06001D79 RID: 7545 RVA: 0x0004E799 File Offset: 0x0004CB99
		IControllerTemplateButton ISixDofControllerTemplate.button23
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(57);
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06001D7A RID: 7546 RVA: 0x0004E7A3 File Offset: 0x0004CBA3
		IControllerTemplateButton ISixDofControllerTemplate.button24
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(58);
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06001D7B RID: 7547 RVA: 0x0004E7AD File Offset: 0x0004CBAD
		IControllerTemplateButton ISixDofControllerTemplate.button25
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(59);
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06001D7C RID: 7548 RVA: 0x0004E7B7 File Offset: 0x0004CBB7
		IControllerTemplateButton ISixDofControllerTemplate.button26
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(60);
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06001D7D RID: 7549 RVA: 0x0004E7C1 File Offset: 0x0004CBC1
		IControllerTemplateButton ISixDofControllerTemplate.button27
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(61);
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06001D7E RID: 7550 RVA: 0x0004E7CB File Offset: 0x0004CBCB
		IControllerTemplateButton ISixDofControllerTemplate.button28
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(62);
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06001D7F RID: 7551 RVA: 0x0004E7D5 File Offset: 0x0004CBD5
		IControllerTemplateButton ISixDofControllerTemplate.button29
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(63);
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06001D80 RID: 7552 RVA: 0x0004E7DF File Offset: 0x0004CBDF
		IControllerTemplateButton ISixDofControllerTemplate.button30
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(64);
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06001D81 RID: 7553 RVA: 0x0004E7E9 File Offset: 0x0004CBE9
		IControllerTemplateButton ISixDofControllerTemplate.button31
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(65);
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06001D82 RID: 7554 RVA: 0x0004E7F3 File Offset: 0x0004CBF3
		IControllerTemplateButton ISixDofControllerTemplate.button32
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(66);
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06001D83 RID: 7555 RVA: 0x0004E7FD File Offset: 0x0004CBFD
		IControllerTemplateHat ISixDofControllerTemplate.hat1
		{
			get
			{
				return base.GetElement<IControllerTemplateHat>(48);
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06001D84 RID: 7556 RVA: 0x0004E807 File Offset: 0x0004CC07
		IControllerTemplateHat ISixDofControllerTemplate.hat2
		{
			get
			{
				return base.GetElement<IControllerTemplateHat>(49);
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06001D85 RID: 7557 RVA: 0x0004E811 File Offset: 0x0004CC11
		IControllerTemplateThrottle ISixDofControllerTemplate.throttle1
		{
			get
			{
				return base.GetElement<IControllerTemplateThrottle>(52);
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06001D86 RID: 7558 RVA: 0x0004E81B File Offset: 0x0004CC1B
		IControllerTemplateThrottle ISixDofControllerTemplate.throttle2
		{
			get
			{
				return base.GetElement<IControllerTemplateThrottle>(53);
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06001D87 RID: 7559 RVA: 0x0004E825 File Offset: 0x0004CC25
		IControllerTemplateStick6D ISixDofControllerTemplate.stick
		{
			get
			{
				return base.GetElement<IControllerTemplateStick6D>(54);
			}
		}

		// Token: 0x0400127C RID: 4732
		public static readonly Guid typeGuid = new Guid("2599beb3-522b-43dd-a4ef-93fd60e5eafa");

		// Token: 0x0400127D RID: 4733
		public const int elementId_positionX = 1;

		// Token: 0x0400127E RID: 4734
		public const int elementId_positionY = 2;

		// Token: 0x0400127F RID: 4735
		public const int elementId_positionZ = 0;

		// Token: 0x04001280 RID: 4736
		public const int elementId_rotationX = 3;

		// Token: 0x04001281 RID: 4737
		public const int elementId_rotationY = 5;

		// Token: 0x04001282 RID: 4738
		public const int elementId_rotationZ = 4;

		// Token: 0x04001283 RID: 4739
		public const int elementId_throttle1Axis = 6;

		// Token: 0x04001284 RID: 4740
		public const int elementId_throttle1MinDetent = 50;

		// Token: 0x04001285 RID: 4741
		public const int elementId_throttle2Axis = 7;

		// Token: 0x04001286 RID: 4742
		public const int elementId_throttle2MinDetent = 51;

		// Token: 0x04001287 RID: 4743
		public const int elementId_extraAxis1 = 8;

		// Token: 0x04001288 RID: 4744
		public const int elementId_extraAxis2 = 9;

		// Token: 0x04001289 RID: 4745
		public const int elementId_extraAxis3 = 10;

		// Token: 0x0400128A RID: 4746
		public const int elementId_extraAxis4 = 11;

		// Token: 0x0400128B RID: 4747
		public const int elementId_button1 = 12;

		// Token: 0x0400128C RID: 4748
		public const int elementId_button2 = 13;

		// Token: 0x0400128D RID: 4749
		public const int elementId_button3 = 14;

		// Token: 0x0400128E RID: 4750
		public const int elementId_button4 = 15;

		// Token: 0x0400128F RID: 4751
		public const int elementId_button5 = 16;

		// Token: 0x04001290 RID: 4752
		public const int elementId_button6 = 17;

		// Token: 0x04001291 RID: 4753
		public const int elementId_button7 = 18;

		// Token: 0x04001292 RID: 4754
		public const int elementId_button8 = 19;

		// Token: 0x04001293 RID: 4755
		public const int elementId_button9 = 20;

		// Token: 0x04001294 RID: 4756
		public const int elementId_button10 = 21;

		// Token: 0x04001295 RID: 4757
		public const int elementId_button11 = 22;

		// Token: 0x04001296 RID: 4758
		public const int elementId_button12 = 23;

		// Token: 0x04001297 RID: 4759
		public const int elementId_button13 = 24;

		// Token: 0x04001298 RID: 4760
		public const int elementId_button14 = 25;

		// Token: 0x04001299 RID: 4761
		public const int elementId_button15 = 26;

		// Token: 0x0400129A RID: 4762
		public const int elementId_button16 = 27;

		// Token: 0x0400129B RID: 4763
		public const int elementId_button17 = 28;

		// Token: 0x0400129C RID: 4764
		public const int elementId_button18 = 29;

		// Token: 0x0400129D RID: 4765
		public const int elementId_button19 = 30;

		// Token: 0x0400129E RID: 4766
		public const int elementId_button20 = 31;

		// Token: 0x0400129F RID: 4767
		public const int elementId_button21 = 55;

		// Token: 0x040012A0 RID: 4768
		public const int elementId_button22 = 56;

		// Token: 0x040012A1 RID: 4769
		public const int elementId_button23 = 57;

		// Token: 0x040012A2 RID: 4770
		public const int elementId_button24 = 58;

		// Token: 0x040012A3 RID: 4771
		public const int elementId_button25 = 59;

		// Token: 0x040012A4 RID: 4772
		public const int elementId_button26 = 60;

		// Token: 0x040012A5 RID: 4773
		public const int elementId_button27 = 61;

		// Token: 0x040012A6 RID: 4774
		public const int elementId_button28 = 62;

		// Token: 0x040012A7 RID: 4775
		public const int elementId_button29 = 63;

		// Token: 0x040012A8 RID: 4776
		public const int elementId_button30 = 64;

		// Token: 0x040012A9 RID: 4777
		public const int elementId_button31 = 65;

		// Token: 0x040012AA RID: 4778
		public const int elementId_button32 = 66;

		// Token: 0x040012AB RID: 4779
		public const int elementId_hat1Up = 32;

		// Token: 0x040012AC RID: 4780
		public const int elementId_hat1UpRight = 33;

		// Token: 0x040012AD RID: 4781
		public const int elementId_hat1Right = 34;

		// Token: 0x040012AE RID: 4782
		public const int elementId_hat1DownRight = 35;

		// Token: 0x040012AF RID: 4783
		public const int elementId_hat1Down = 36;

		// Token: 0x040012B0 RID: 4784
		public const int elementId_hat1DownLeft = 37;

		// Token: 0x040012B1 RID: 4785
		public const int elementId_hat1Left = 38;

		// Token: 0x040012B2 RID: 4786
		public const int elementId_hat1UpLeft = 39;

		// Token: 0x040012B3 RID: 4787
		public const int elementId_hat2Up = 40;

		// Token: 0x040012B4 RID: 4788
		public const int elementId_hat2UpRight = 41;

		// Token: 0x040012B5 RID: 4789
		public const int elementId_hat2Right = 42;

		// Token: 0x040012B6 RID: 4790
		public const int elementId_hat2DownRight = 43;

		// Token: 0x040012B7 RID: 4791
		public const int elementId_hat2Down = 44;

		// Token: 0x040012B8 RID: 4792
		public const int elementId_hat2DownLeft = 45;

		// Token: 0x040012B9 RID: 4793
		public const int elementId_hat2Left = 46;

		// Token: 0x040012BA RID: 4794
		public const int elementId_hat2UpLeft = 47;

		// Token: 0x040012BB RID: 4795
		public const int elementId_hat1 = 48;

		// Token: 0x040012BC RID: 4796
		public const int elementId_hat2 = 49;

		// Token: 0x040012BD RID: 4797
		public const int elementId_throttle1 = 52;

		// Token: 0x040012BE RID: 4798
		public const int elementId_throttle2 = 53;

		// Token: 0x040012BF RID: 4799
		public const int elementId_stick = 54;
	}
}
