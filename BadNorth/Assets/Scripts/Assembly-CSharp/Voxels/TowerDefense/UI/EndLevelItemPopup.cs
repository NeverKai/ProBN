using System;
using System.Collections.Generic;
using I2.Loc;
using UnityEngine;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x0200054A RID: 1354
	public class EndLevelItemPopup : MonoBehaviour, IGameSetup
	{
		// Token: 0x06002335 RID: 9013 RVA: 0x0006A73D File Offset: 0x00068B3D
		void IGameSetup.OnGameAwake()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x06002336 RID: 9014 RVA: 0x0006A74B File Offset: 0x00068B4B
		public void Done()
		{
			this.playing = false;
		}

		// Token: 0x06002337 RID: 9015 RVA: 0x0006A754 File Offset: 0x00068B54
		public IEnumerator<float> Display(HeroUpgradeDefinition upgradeDef)
		{
			this.greyscaleAlpha = 1f;
			base.gameObject.SetActive(true);
			this.playing = true;
			this.maskedSprite.Set(upgradeDef);
			this.nameLocalize.Term = upgradeDef.nameTerm;
			FabricWrapper.PostEvent(this.appearAudio);
			while (this.playing)
			{
				yield return this.greyscaleAlpha;
			}
			FabricWrapper.PostEvent(this.closeAudioId);
			base.gameObject.SetActive(false);
			yield return 0f;
			yield break;
		}

		// Token: 0x06002338 RID: 9016 RVA: 0x0006A778 File Offset: 0x00068B78
		public IEnumerator<float> Display(string locTerm)
		{
			this.greyscaleAlpha = 1f;
			base.gameObject.SetActive(true);
			this.playing = true;
			this.maskedSprite.Clear();
			this.nameLocalize.Term = locTerm;
			FabricWrapper.PostEvent(this.appearAudio);
			while (this.playing)
			{
				yield return this.greyscaleAlpha;
			}
			FabricWrapper.PostEvent(this.closeAudioId);
			base.gameObject.SetActive(false);
			yield return 0f;
			yield break;
		}

		// Token: 0x040015C4 RID: 5572
		[Header("References")]
		[SerializeField]
		private MaskedSprite maskedSprite;

		// Token: 0x040015C5 RID: 5573
		[SerializeField]
		private Localize nameLocalize;

		// Token: 0x040015C6 RID: 5574
		[SerializeField]
		private FabricEventReference appearAudio = "UI/InGame/FoundItem";

		// Token: 0x040015C7 RID: 5575
		private FabricEventReference openAudioId = "Mus/Thanks";

		// Token: 0x040015C8 RID: 5576
		private FabricEventReference closeAudioId = "UI/InGame/NotificationOff";

		// Token: 0x040015C9 RID: 5577
		[SerializeField]
		private float greyscaleAlpha;

		// Token: 0x040015CA RID: 5578
		private bool playing;
	}
}
