using System;

namespace Rewired
{
	// Token: 0x020004A3 RID: 1187
	public sealed class HOTASTemplate : ControllerTemplate, IHOTASTemplate, IControllerTemplate
	{
		// Token: 0x06001CCC RID: 7372 RVA: 0x0004E063 File Offset: 0x0004C463
		public HOTASTemplate(object payload) : base(payload)
		{
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06001CCD RID: 7373 RVA: 0x0004E06C File Offset: 0x0004C46C
		IControllerTemplateButton IHOTASTemplate.stickTrigger
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(3);
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06001CCE RID: 7374 RVA: 0x0004E075 File Offset: 0x0004C475
		IControllerTemplateButton IHOTASTemplate.stickTriggerStage2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(4);
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06001CCF RID: 7375 RVA: 0x0004E07E File Offset: 0x0004C47E
		IControllerTemplateButton IHOTASTemplate.stickPinkyButton
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(5);
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06001CD0 RID: 7376 RVA: 0x0004E087 File Offset: 0x0004C487
		IControllerTemplateButton IHOTASTemplate.stickPinkyTrigger
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(154);
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06001CD1 RID: 7377 RVA: 0x0004E094 File Offset: 0x0004C494
		IControllerTemplateButton IHOTASTemplate.stickButton1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(6);
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06001CD2 RID: 7378 RVA: 0x0004E09D File Offset: 0x0004C49D
		IControllerTemplateButton IHOTASTemplate.stickButton2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(7);
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06001CD3 RID: 7379 RVA: 0x0004E0A6 File Offset: 0x0004C4A6
		IControllerTemplateButton IHOTASTemplate.stickButton3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(8);
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06001CD4 RID: 7380 RVA: 0x0004E0AF File Offset: 0x0004C4AF
		IControllerTemplateButton IHOTASTemplate.stickButton4
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(9);
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06001CD5 RID: 7381 RVA: 0x0004E0B9 File Offset: 0x0004C4B9
		IControllerTemplateButton IHOTASTemplate.stickButton5
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(10);
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06001CD6 RID: 7382 RVA: 0x0004E0C3 File Offset: 0x0004C4C3
		IControllerTemplateButton IHOTASTemplate.stickButton6
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(11);
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06001CD7 RID: 7383 RVA: 0x0004E0CD File Offset: 0x0004C4CD
		IControllerTemplateButton IHOTASTemplate.stickButton7
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(12);
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06001CD8 RID: 7384 RVA: 0x0004E0D7 File Offset: 0x0004C4D7
		IControllerTemplateButton IHOTASTemplate.stickButton8
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(13);
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06001CD9 RID: 7385 RVA: 0x0004E0E1 File Offset: 0x0004C4E1
		IControllerTemplateButton IHOTASTemplate.stickButton9
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(14);
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06001CDA RID: 7386 RVA: 0x0004E0EB File Offset: 0x0004C4EB
		IControllerTemplateButton IHOTASTemplate.stickButton10
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(15);
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06001CDB RID: 7387 RVA: 0x0004E0F5 File Offset: 0x0004C4F5
		IControllerTemplateButton IHOTASTemplate.stickBaseButton1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(18);
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06001CDC RID: 7388 RVA: 0x0004E0FF File Offset: 0x0004C4FF
		IControllerTemplateButton IHOTASTemplate.stickBaseButton2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(19);
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06001CDD RID: 7389 RVA: 0x0004E109 File Offset: 0x0004C509
		IControllerTemplateButton IHOTASTemplate.stickBaseButton3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(20);
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06001CDE RID: 7390 RVA: 0x0004E113 File Offset: 0x0004C513
		IControllerTemplateButton IHOTASTemplate.stickBaseButton4
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(21);
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06001CDF RID: 7391 RVA: 0x0004E11D File Offset: 0x0004C51D
		IControllerTemplateButton IHOTASTemplate.stickBaseButton5
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(22);
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06001CE0 RID: 7392 RVA: 0x0004E127 File Offset: 0x0004C527
		IControllerTemplateButton IHOTASTemplate.stickBaseButton6
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(23);
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06001CE1 RID: 7393 RVA: 0x0004E131 File Offset: 0x0004C531
		IControllerTemplateButton IHOTASTemplate.stickBaseButton7
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(24);
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06001CE2 RID: 7394 RVA: 0x0004E13B File Offset: 0x0004C53B
		IControllerTemplateButton IHOTASTemplate.stickBaseButton8
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(25);
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06001CE3 RID: 7395 RVA: 0x0004E145 File Offset: 0x0004C545
		IControllerTemplateButton IHOTASTemplate.stickBaseButton9
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(26);
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06001CE4 RID: 7396 RVA: 0x0004E14F File Offset: 0x0004C54F
		IControllerTemplateButton IHOTASTemplate.stickBaseButton10
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(27);
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06001CE5 RID: 7397 RVA: 0x0004E159 File Offset: 0x0004C559
		IControllerTemplateButton IHOTASTemplate.stickBaseButton11
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(161);
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06001CE6 RID: 7398 RVA: 0x0004E166 File Offset: 0x0004C566
		IControllerTemplateButton IHOTASTemplate.stickBaseButton12
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(162);
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06001CE7 RID: 7399 RVA: 0x0004E173 File Offset: 0x0004C573
		IControllerTemplateButton IHOTASTemplate.mode1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(44);
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06001CE8 RID: 7400 RVA: 0x0004E17D File Offset: 0x0004C57D
		IControllerTemplateButton IHOTASTemplate.mode2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(45);
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06001CE9 RID: 7401 RVA: 0x0004E187 File Offset: 0x0004C587
		IControllerTemplateButton IHOTASTemplate.mode3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(46);
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06001CEA RID: 7402 RVA: 0x0004E191 File Offset: 0x0004C591
		IControllerTemplateButton IHOTASTemplate.throttleButton1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(50);
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06001CEB RID: 7403 RVA: 0x0004E19B File Offset: 0x0004C59B
		IControllerTemplateButton IHOTASTemplate.throttleButton2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(51);
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06001CEC RID: 7404 RVA: 0x0004E1A5 File Offset: 0x0004C5A5
		IControllerTemplateButton IHOTASTemplate.throttleButton3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(52);
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06001CED RID: 7405 RVA: 0x0004E1AF File Offset: 0x0004C5AF
		IControllerTemplateButton IHOTASTemplate.throttleButton4
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(53);
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06001CEE RID: 7406 RVA: 0x0004E1B9 File Offset: 0x0004C5B9
		IControllerTemplateButton IHOTASTemplate.throttleButton5
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(54);
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06001CEF RID: 7407 RVA: 0x0004E1C3 File Offset: 0x0004C5C3
		IControllerTemplateButton IHOTASTemplate.throttleButton6
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(55);
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06001CF0 RID: 7408 RVA: 0x0004E1CD File Offset: 0x0004C5CD
		IControllerTemplateButton IHOTASTemplate.throttleButton7
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(56);
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06001CF1 RID: 7409 RVA: 0x0004E1D7 File Offset: 0x0004C5D7
		IControllerTemplateButton IHOTASTemplate.throttleButton8
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(57);
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06001CF2 RID: 7410 RVA: 0x0004E1E1 File Offset: 0x0004C5E1
		IControllerTemplateButton IHOTASTemplate.throttleButton9
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(58);
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06001CF3 RID: 7411 RVA: 0x0004E1EB File Offset: 0x0004C5EB
		IControllerTemplateButton IHOTASTemplate.throttleButton10
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(59);
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06001CF4 RID: 7412 RVA: 0x0004E1F5 File Offset: 0x0004C5F5
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(60);
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06001CF5 RID: 7413 RVA: 0x0004E1FF File Offset: 0x0004C5FF
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(61);
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06001CF6 RID: 7414 RVA: 0x0004E209 File Offset: 0x0004C609
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(62);
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06001CF7 RID: 7415 RVA: 0x0004E213 File Offset: 0x0004C613
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton4
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(63);
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06001CF8 RID: 7416 RVA: 0x0004E21D File Offset: 0x0004C61D
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton5
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(64);
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06001CF9 RID: 7417 RVA: 0x0004E227 File Offset: 0x0004C627
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton6
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(65);
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06001CFA RID: 7418 RVA: 0x0004E231 File Offset: 0x0004C631
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton7
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(66);
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06001CFB RID: 7419 RVA: 0x0004E23B File Offset: 0x0004C63B
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton8
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(67);
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06001CFC RID: 7420 RVA: 0x0004E245 File Offset: 0x0004C645
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton9
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(68);
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06001CFD RID: 7421 RVA: 0x0004E24F File Offset: 0x0004C64F
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton10
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(69);
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06001CFE RID: 7422 RVA: 0x0004E259 File Offset: 0x0004C659
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton11
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(132);
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06001CFF RID: 7423 RVA: 0x0004E266 File Offset: 0x0004C666
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton12
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(133);
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06001D00 RID: 7424 RVA: 0x0004E273 File Offset: 0x0004C673
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton13
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(134);
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06001D01 RID: 7425 RVA: 0x0004E280 File Offset: 0x0004C680
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton14
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(135);
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06001D02 RID: 7426 RVA: 0x0004E28D File Offset: 0x0004C68D
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton15
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(136);
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06001D03 RID: 7427 RVA: 0x0004E29A File Offset: 0x0004C69A
		IControllerTemplateAxis IHOTASTemplate.throttleSlider1
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(70);
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06001D04 RID: 7428 RVA: 0x0004E2A4 File Offset: 0x0004C6A4
		IControllerTemplateAxis IHOTASTemplate.throttleSlider2
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(71);
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06001D05 RID: 7429 RVA: 0x0004E2AE File Offset: 0x0004C6AE
		IControllerTemplateAxis IHOTASTemplate.throttleSlider3
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(72);
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06001D06 RID: 7430 RVA: 0x0004E2B8 File Offset: 0x0004C6B8
		IControllerTemplateAxis IHOTASTemplate.throttleSlider4
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(73);
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06001D07 RID: 7431 RVA: 0x0004E2C2 File Offset: 0x0004C6C2
		IControllerTemplateAxis IHOTASTemplate.throttleDial1
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(74);
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06001D08 RID: 7432 RVA: 0x0004E2CC File Offset: 0x0004C6CC
		IControllerTemplateAxis IHOTASTemplate.throttleDial2
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(142);
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06001D09 RID: 7433 RVA: 0x0004E2D9 File Offset: 0x0004C6D9
		IControllerTemplateAxis IHOTASTemplate.throttleDial3
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(143);
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06001D0A RID: 7434 RVA: 0x0004E2E6 File Offset: 0x0004C6E6
		IControllerTemplateAxis IHOTASTemplate.throttleDial4
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(144);
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06001D0B RID: 7435 RVA: 0x0004E2F3 File Offset: 0x0004C6F3
		IControllerTemplateButton IHOTASTemplate.throttleWheel1Forward
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(145);
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06001D0C RID: 7436 RVA: 0x0004E300 File Offset: 0x0004C700
		IControllerTemplateButton IHOTASTemplate.throttleWheel1Back
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(146);
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06001D0D RID: 7437 RVA: 0x0004E30D File Offset: 0x0004C70D
		IControllerTemplateButton IHOTASTemplate.throttleWheel1Press
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(147);
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06001D0E RID: 7438 RVA: 0x0004E31A File Offset: 0x0004C71A
		IControllerTemplateButton IHOTASTemplate.throttleWheel2Forward
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(148);
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06001D0F RID: 7439 RVA: 0x0004E327 File Offset: 0x0004C727
		IControllerTemplateButton IHOTASTemplate.throttleWheel2Back
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(149);
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06001D10 RID: 7440 RVA: 0x0004E334 File Offset: 0x0004C734
		IControllerTemplateButton IHOTASTemplate.throttleWheel2Press
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(150);
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06001D11 RID: 7441 RVA: 0x0004E341 File Offset: 0x0004C741
		IControllerTemplateButton IHOTASTemplate.throttleWheel3Forward
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(151);
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06001D12 RID: 7442 RVA: 0x0004E34E File Offset: 0x0004C74E
		IControllerTemplateButton IHOTASTemplate.throttleWheel3Back
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(152);
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06001D13 RID: 7443 RVA: 0x0004E35B File Offset: 0x0004C75B
		IControllerTemplateButton IHOTASTemplate.throttleWheel3Press
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(153);
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06001D14 RID: 7444 RVA: 0x0004E368 File Offset: 0x0004C768
		IControllerTemplateAxis IHOTASTemplate.leftPedal
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(168);
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06001D15 RID: 7445 RVA: 0x0004E375 File Offset: 0x0004C775
		IControllerTemplateAxis IHOTASTemplate.rightPedal
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(169);
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06001D16 RID: 7446 RVA: 0x0004E382 File Offset: 0x0004C782
		IControllerTemplateAxis IHOTASTemplate.slidePedals
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(170);
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06001D17 RID: 7447 RVA: 0x0004E38F File Offset: 0x0004C78F
		IControllerTemplateStick IHOTASTemplate.stick
		{
			get
			{
				return base.GetElement<IControllerTemplateStick>(171);
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06001D18 RID: 7448 RVA: 0x0004E39C File Offset: 0x0004C79C
		IControllerTemplateThumbStick IHOTASTemplate.stickMiniStick1
		{
			get
			{
				return base.GetElement<IControllerTemplateThumbStick>(172);
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06001D19 RID: 7449 RVA: 0x0004E3A9 File Offset: 0x0004C7A9
		IControllerTemplateThumbStick IHOTASTemplate.stickMiniStick2
		{
			get
			{
				return base.GetElement<IControllerTemplateThumbStick>(173);
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06001D1A RID: 7450 RVA: 0x0004E3B6 File Offset: 0x0004C7B6
		IControllerTemplateHat IHOTASTemplate.stickHat1
		{
			get
			{
				return base.GetElement<IControllerTemplateHat>(174);
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06001D1B RID: 7451 RVA: 0x0004E3C3 File Offset: 0x0004C7C3
		IControllerTemplateHat IHOTASTemplate.stickHat2
		{
			get
			{
				return base.GetElement<IControllerTemplateHat>(175);
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06001D1C RID: 7452 RVA: 0x0004E3D0 File Offset: 0x0004C7D0
		IControllerTemplateHat IHOTASTemplate.stickHat3
		{
			get
			{
				return base.GetElement<IControllerTemplateHat>(176);
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06001D1D RID: 7453 RVA: 0x0004E3DD File Offset: 0x0004C7DD
		IControllerTemplateHat IHOTASTemplate.stickHat4
		{
			get
			{
				return base.GetElement<IControllerTemplateHat>(177);
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06001D1E RID: 7454 RVA: 0x0004E3EA File Offset: 0x0004C7EA
		IControllerTemplateThrottle IHOTASTemplate.throttle1
		{
			get
			{
				return base.GetElement<IControllerTemplateThrottle>(178);
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06001D1F RID: 7455 RVA: 0x0004E3F7 File Offset: 0x0004C7F7
		IControllerTemplateThrottle IHOTASTemplate.throttle2
		{
			get
			{
				return base.GetElement<IControllerTemplateThrottle>(179);
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06001D20 RID: 7456 RVA: 0x0004E404 File Offset: 0x0004C804
		IControllerTemplateThumbStick IHOTASTemplate.throttleMiniStick
		{
			get
			{
				return base.GetElement<IControllerTemplateThumbStick>(180);
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06001D21 RID: 7457 RVA: 0x0004E411 File Offset: 0x0004C811
		IControllerTemplateHat IHOTASTemplate.throttleHat1
		{
			get
			{
				return base.GetElement<IControllerTemplateHat>(181);
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06001D22 RID: 7458 RVA: 0x0004E41E File Offset: 0x0004C81E
		IControllerTemplateHat IHOTASTemplate.throttleHat2
		{
			get
			{
				return base.GetElement<IControllerTemplateHat>(182);
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06001D23 RID: 7459 RVA: 0x0004E42B File Offset: 0x0004C82B
		IControllerTemplateHat IHOTASTemplate.throttleHat3
		{
			get
			{
				return base.GetElement<IControllerTemplateHat>(183);
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06001D24 RID: 7460 RVA: 0x0004E438 File Offset: 0x0004C838
		IControllerTemplateHat IHOTASTemplate.throttleHat4
		{
			get
			{
				return base.GetElement<IControllerTemplateHat>(184);
			}
		}

		// Token: 0x04001181 RID: 4481
		public static readonly Guid typeGuid = new Guid("061a00cf-d8c2-4f8d-8cb5-a15a010bc53e");

		// Token: 0x04001182 RID: 4482
		public const int elementId_stickX = 0;

		// Token: 0x04001183 RID: 4483
		public const int elementId_stickY = 1;

		// Token: 0x04001184 RID: 4484
		public const int elementId_stickRotate = 2;

		// Token: 0x04001185 RID: 4485
		public const int elementId_stickMiniStick1X = 78;

		// Token: 0x04001186 RID: 4486
		public const int elementId_stickMiniStick1Y = 79;

		// Token: 0x04001187 RID: 4487
		public const int elementId_stickMiniStick1Press = 80;

		// Token: 0x04001188 RID: 4488
		public const int elementId_stickMiniStick2X = 81;

		// Token: 0x04001189 RID: 4489
		public const int elementId_stickMiniStick2Y = 82;

		// Token: 0x0400118A RID: 4490
		public const int elementId_stickMiniStick2Press = 83;

		// Token: 0x0400118B RID: 4491
		public const int elementId_stickTrigger = 3;

		// Token: 0x0400118C RID: 4492
		public const int elementId_stickTriggerStage2 = 4;

		// Token: 0x0400118D RID: 4493
		public const int elementId_stickPinkyButton = 5;

		// Token: 0x0400118E RID: 4494
		public const int elementId_stickPinkyTrigger = 154;

		// Token: 0x0400118F RID: 4495
		public const int elementId_stickButton1 = 6;

		// Token: 0x04001190 RID: 4496
		public const int elementId_stickButton2 = 7;

		// Token: 0x04001191 RID: 4497
		public const int elementId_stickButton3 = 8;

		// Token: 0x04001192 RID: 4498
		public const int elementId_stickButton4 = 9;

		// Token: 0x04001193 RID: 4499
		public const int elementId_stickButton5 = 10;

		// Token: 0x04001194 RID: 4500
		public const int elementId_stickButton6 = 11;

		// Token: 0x04001195 RID: 4501
		public const int elementId_stickButton7 = 12;

		// Token: 0x04001196 RID: 4502
		public const int elementId_stickButton8 = 13;

		// Token: 0x04001197 RID: 4503
		public const int elementId_stickButton9 = 14;

		// Token: 0x04001198 RID: 4504
		public const int elementId_stickButton10 = 15;

		// Token: 0x04001199 RID: 4505
		public const int elementId_stickBaseButton1 = 18;

		// Token: 0x0400119A RID: 4506
		public const int elementId_stickBaseButton2 = 19;

		// Token: 0x0400119B RID: 4507
		public const int elementId_stickBaseButton3 = 20;

		// Token: 0x0400119C RID: 4508
		public const int elementId_stickBaseButton4 = 21;

		// Token: 0x0400119D RID: 4509
		public const int elementId_stickBaseButton5 = 22;

		// Token: 0x0400119E RID: 4510
		public const int elementId_stickBaseButton6 = 23;

		// Token: 0x0400119F RID: 4511
		public const int elementId_stickBaseButton7 = 24;

		// Token: 0x040011A0 RID: 4512
		public const int elementId_stickBaseButton8 = 25;

		// Token: 0x040011A1 RID: 4513
		public const int elementId_stickBaseButton9 = 26;

		// Token: 0x040011A2 RID: 4514
		public const int elementId_stickBaseButton10 = 27;

		// Token: 0x040011A3 RID: 4515
		public const int elementId_stickBaseButton11 = 161;

		// Token: 0x040011A4 RID: 4516
		public const int elementId_stickBaseButton12 = 162;

		// Token: 0x040011A5 RID: 4517
		public const int elementId_stickHat1Up = 28;

		// Token: 0x040011A6 RID: 4518
		public const int elementId_stickHat1UpRight = 29;

		// Token: 0x040011A7 RID: 4519
		public const int elementId_stickHat1Right = 30;

		// Token: 0x040011A8 RID: 4520
		public const int elementId_stickHat1DownRight = 31;

		// Token: 0x040011A9 RID: 4521
		public const int elementId_stickHat1Down = 32;

		// Token: 0x040011AA RID: 4522
		public const int elementId_stickHat1DownLeft = 33;

		// Token: 0x040011AB RID: 4523
		public const int elementId_stickHat1Left = 34;

		// Token: 0x040011AC RID: 4524
		public const int elementId_stickHat1Up_Left = 35;

		// Token: 0x040011AD RID: 4525
		public const int elementId_stickHat2Up = 36;

		// Token: 0x040011AE RID: 4526
		public const int elementId_stickHat2Up_right = 37;

		// Token: 0x040011AF RID: 4527
		public const int elementId_stickHat2Right = 38;

		// Token: 0x040011B0 RID: 4528
		public const int elementId_stickHat2Down_Right = 39;

		// Token: 0x040011B1 RID: 4529
		public const int elementId_stickHat2Down = 40;

		// Token: 0x040011B2 RID: 4530
		public const int elementId_stickHat2Down_Left = 41;

		// Token: 0x040011B3 RID: 4531
		public const int elementId_stickHat2Left = 42;

		// Token: 0x040011B4 RID: 4532
		public const int elementId_stickHat2Up_Left = 43;

		// Token: 0x040011B5 RID: 4533
		public const int elementId_stickHat3Up = 84;

		// Token: 0x040011B6 RID: 4534
		public const int elementId_stickHat3Up_Right = 85;

		// Token: 0x040011B7 RID: 4535
		public const int elementId_stickHat3Right = 86;

		// Token: 0x040011B8 RID: 4536
		public const int elementId_stickHat3Down_Right = 87;

		// Token: 0x040011B9 RID: 4537
		public const int elementId_stickHat3Down = 88;

		// Token: 0x040011BA RID: 4538
		public const int elementId_stickHat3Down_Left = 89;

		// Token: 0x040011BB RID: 4539
		public const int elementId_stickHat3Left = 90;

		// Token: 0x040011BC RID: 4540
		public const int elementId_stickHat3Up_Left = 91;

		// Token: 0x040011BD RID: 4541
		public const int elementId_stickHat4Up = 92;

		// Token: 0x040011BE RID: 4542
		public const int elementId_stickHat4Up_Right = 93;

		// Token: 0x040011BF RID: 4543
		public const int elementId_stickHat4Right = 94;

		// Token: 0x040011C0 RID: 4544
		public const int elementId_stickHat4Down_Right = 95;

		// Token: 0x040011C1 RID: 4545
		public const int elementId_stickHat4Down = 96;

		// Token: 0x040011C2 RID: 4546
		public const int elementId_stickHat4Down_Left = 97;

		// Token: 0x040011C3 RID: 4547
		public const int elementId_stickHat4Left = 98;

		// Token: 0x040011C4 RID: 4548
		public const int elementId_stickHat4Up_Left = 99;

		// Token: 0x040011C5 RID: 4549
		public const int elementId_mode1 = 44;

		// Token: 0x040011C6 RID: 4550
		public const int elementId_mode2 = 45;

		// Token: 0x040011C7 RID: 4551
		public const int elementId_mode3 = 46;

		// Token: 0x040011C8 RID: 4552
		public const int elementId_throttle1Axis = 49;

		// Token: 0x040011C9 RID: 4553
		public const int elementId_throttle2Axis = 155;

		// Token: 0x040011CA RID: 4554
		public const int elementId_throttle1MinDetent = 166;

		// Token: 0x040011CB RID: 4555
		public const int elementId_throttle2MinDetent = 167;

		// Token: 0x040011CC RID: 4556
		public const int elementId_throttleButton1 = 50;

		// Token: 0x040011CD RID: 4557
		public const int elementId_throttleButton2 = 51;

		// Token: 0x040011CE RID: 4558
		public const int elementId_throttleButton3 = 52;

		// Token: 0x040011CF RID: 4559
		public const int elementId_throttleButton4 = 53;

		// Token: 0x040011D0 RID: 4560
		public const int elementId_throttleButton5 = 54;

		// Token: 0x040011D1 RID: 4561
		public const int elementId_throttleButton6 = 55;

		// Token: 0x040011D2 RID: 4562
		public const int elementId_throttleButton7 = 56;

		// Token: 0x040011D3 RID: 4563
		public const int elementId_throttleButton8 = 57;

		// Token: 0x040011D4 RID: 4564
		public const int elementId_throttleButton9 = 58;

		// Token: 0x040011D5 RID: 4565
		public const int elementId_throttleButton10 = 59;

		// Token: 0x040011D6 RID: 4566
		public const int elementId_throttleBaseButton1 = 60;

		// Token: 0x040011D7 RID: 4567
		public const int elementId_throttleBaseButton2 = 61;

		// Token: 0x040011D8 RID: 4568
		public const int elementId_throttleBaseButton3 = 62;

		// Token: 0x040011D9 RID: 4569
		public const int elementId_throttleBaseButton4 = 63;

		// Token: 0x040011DA RID: 4570
		public const int elementId_throttleBaseButton5 = 64;

		// Token: 0x040011DB RID: 4571
		public const int elementId_throttleBaseButton6 = 65;

		// Token: 0x040011DC RID: 4572
		public const int elementId_throttleBaseButton7 = 66;

		// Token: 0x040011DD RID: 4573
		public const int elementId_throttleBaseButton8 = 67;

		// Token: 0x040011DE RID: 4574
		public const int elementId_throttleBaseButton9 = 68;

		// Token: 0x040011DF RID: 4575
		public const int elementId_throttleBaseButton10 = 69;

		// Token: 0x040011E0 RID: 4576
		public const int elementId_throttleBaseButton11 = 132;

		// Token: 0x040011E1 RID: 4577
		public const int elementId_throttleBaseButton12 = 133;

		// Token: 0x040011E2 RID: 4578
		public const int elementId_throttleBaseButton13 = 134;

		// Token: 0x040011E3 RID: 4579
		public const int elementId_throttleBaseButton14 = 135;

		// Token: 0x040011E4 RID: 4580
		public const int elementId_throttleBaseButton15 = 136;

		// Token: 0x040011E5 RID: 4581
		public const int elementId_throttleSlider1 = 70;

		// Token: 0x040011E6 RID: 4582
		public const int elementId_throttleSlider2 = 71;

		// Token: 0x040011E7 RID: 4583
		public const int elementId_throttleSlider3 = 72;

		// Token: 0x040011E8 RID: 4584
		public const int elementId_throttleSlider4 = 73;

		// Token: 0x040011E9 RID: 4585
		public const int elementId_throttleDial1 = 74;

		// Token: 0x040011EA RID: 4586
		public const int elementId_throttleDial2 = 142;

		// Token: 0x040011EB RID: 4587
		public const int elementId_throttleDial3 = 143;

		// Token: 0x040011EC RID: 4588
		public const int elementId_throttleDial4 = 144;

		// Token: 0x040011ED RID: 4589
		public const int elementId_throttleMiniStickX = 75;

		// Token: 0x040011EE RID: 4590
		public const int elementId_throttleMiniStickY = 76;

		// Token: 0x040011EF RID: 4591
		public const int elementId_throttleMiniStickPress = 77;

		// Token: 0x040011F0 RID: 4592
		public const int elementId_throttleWheel1Forward = 145;

		// Token: 0x040011F1 RID: 4593
		public const int elementId_throttleWheel1Back = 146;

		// Token: 0x040011F2 RID: 4594
		public const int elementId_throttleWheel1Press = 147;

		// Token: 0x040011F3 RID: 4595
		public const int elementId_throttleWheel2Forward = 148;

		// Token: 0x040011F4 RID: 4596
		public const int elementId_throttleWheel2Back = 149;

		// Token: 0x040011F5 RID: 4597
		public const int elementId_throttleWheel2Press = 150;

		// Token: 0x040011F6 RID: 4598
		public const int elementId_throttleWheel3Forward = 151;

		// Token: 0x040011F7 RID: 4599
		public const int elementId_throttleWheel3Back = 152;

		// Token: 0x040011F8 RID: 4600
		public const int elementId_throttleWheel3Press = 153;

		// Token: 0x040011F9 RID: 4601
		public const int elementId_throttleHat1Up = 100;

		// Token: 0x040011FA RID: 4602
		public const int elementId_throttleHat1Up_Right = 101;

		// Token: 0x040011FB RID: 4603
		public const int elementId_throttleHat1Right = 102;

		// Token: 0x040011FC RID: 4604
		public const int elementId_throttleHat1Down_Right = 103;

		// Token: 0x040011FD RID: 4605
		public const int elementId_throttleHat1Down = 104;

		// Token: 0x040011FE RID: 4606
		public const int elementId_throttleHat1Down_Left = 105;

		// Token: 0x040011FF RID: 4607
		public const int elementId_throttleHat1Left = 106;

		// Token: 0x04001200 RID: 4608
		public const int elementId_throttleHat1Up_Left = 107;

		// Token: 0x04001201 RID: 4609
		public const int elementId_throttleHat2Up = 108;

		// Token: 0x04001202 RID: 4610
		public const int elementId_throttleHat2Up_Right = 109;

		// Token: 0x04001203 RID: 4611
		public const int elementId_throttleHat2Right = 110;

		// Token: 0x04001204 RID: 4612
		public const int elementId_throttleHat2Down_Right = 111;

		// Token: 0x04001205 RID: 4613
		public const int elementId_throttleHat2Down = 112;

		// Token: 0x04001206 RID: 4614
		public const int elementId_throttleHat2Down_Left = 113;

		// Token: 0x04001207 RID: 4615
		public const int elementId_throttleHat2Left = 114;

		// Token: 0x04001208 RID: 4616
		public const int elementId_throttleHat2Up_Left = 115;

		// Token: 0x04001209 RID: 4617
		public const int elementId_throttleHat3Up = 116;

		// Token: 0x0400120A RID: 4618
		public const int elementId_throttleHat3Up_Right = 117;

		// Token: 0x0400120B RID: 4619
		public const int elementId_throttleHat3Right = 118;

		// Token: 0x0400120C RID: 4620
		public const int elementId_throttleHat3Down_Right = 119;

		// Token: 0x0400120D RID: 4621
		public const int elementId_throttleHat3Down = 120;

		// Token: 0x0400120E RID: 4622
		public const int elementId_throttleHat3Down_Left = 121;

		// Token: 0x0400120F RID: 4623
		public const int elementId_throttleHat3Left = 122;

		// Token: 0x04001210 RID: 4624
		public const int elementId_throttleHat3Up_Left = 123;

		// Token: 0x04001211 RID: 4625
		public const int elementId_throttleHat4Up = 124;

		// Token: 0x04001212 RID: 4626
		public const int elementId_throttleHat4Up_Right = 125;

		// Token: 0x04001213 RID: 4627
		public const int elementId_throttleHat4Right = 126;

		// Token: 0x04001214 RID: 4628
		public const int elementId_throttleHat4Down_Right = 127;

		// Token: 0x04001215 RID: 4629
		public const int elementId_throttleHat4Down = 128;

		// Token: 0x04001216 RID: 4630
		public const int elementId_throttleHat4Down_Left = 129;

		// Token: 0x04001217 RID: 4631
		public const int elementId_throttleHat4Left = 130;

		// Token: 0x04001218 RID: 4632
		public const int elementId_throttleHat4Up_Left = 131;

		// Token: 0x04001219 RID: 4633
		public const int elementId_leftPedal = 168;

		// Token: 0x0400121A RID: 4634
		public const int elementId_rightPedal = 169;

		// Token: 0x0400121B RID: 4635
		public const int elementId_slidePedals = 170;

		// Token: 0x0400121C RID: 4636
		public const int elementId_stick = 171;

		// Token: 0x0400121D RID: 4637
		public const int elementId_stickMiniStick1 = 172;

		// Token: 0x0400121E RID: 4638
		public const int elementId_stickMiniStick2 = 173;

		// Token: 0x0400121F RID: 4639
		public const int elementId_stickHat1 = 174;

		// Token: 0x04001220 RID: 4640
		public const int elementId_stickHat2 = 175;

		// Token: 0x04001221 RID: 4641
		public const int elementId_stickHat3 = 176;

		// Token: 0x04001222 RID: 4642
		public const int elementId_stickHat4 = 177;

		// Token: 0x04001223 RID: 4643
		public const int elementId_throttle1 = 178;

		// Token: 0x04001224 RID: 4644
		public const int elementId_throttle2 = 179;

		// Token: 0x04001225 RID: 4645
		public const int elementId_throttleMiniStick = 180;

		// Token: 0x04001226 RID: 4646
		public const int elementId_throttleHat1 = 181;

		// Token: 0x04001227 RID: 4647
		public const int elementId_throttleHat2 = 182;

		// Token: 0x04001228 RID: 4648
		public const int elementId_throttleHat3 = 183;

		// Token: 0x04001229 RID: 4649
		public const int elementId_throttleHat4 = 184;
	}
}
