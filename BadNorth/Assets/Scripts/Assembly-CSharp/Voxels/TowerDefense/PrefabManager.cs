using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Voxels.TowerDefense.SpriteMagic;
using Voxels.TowerDefense.Upgrades;

namespace Voxels.TowerDefense
{
	// Token: 0x020005BF RID: 1471
	[AssetPath("Assets/Settings/Resources/")]
	[Serializable]
	public class PrefabManager : ScriptableObjectSingleton<PrefabManager>
	{
		// Token: 0x04001864 RID: 6244
		public ReusableParticle dustEffect;

		// Token: 0x04001865 RID: 6245
		public ReusableParticle hitEffect;

		// Token: 0x04001866 RID: 6246
		public ReusableParticle bloodStain;

		// Token: 0x04001867 RID: 6247
		public ReusableParticle bloodSplash;

		// Token: 0x04001868 RID: 6248
		public ReusableParticle bloodSplat;

		// Token: 0x04001869 RID: 6249
		public ReusableEffect bloodSlash;

		// Token: 0x0400186A RID: 6250
		public ReusableParticle splash;

		// Token: 0x0400186B RID: 6251
		public ReusableParticle swordClash;

		// Token: 0x0400186C RID: 6252
		public ReusableParticle plungeLandEffect;

		// Token: 0x0400186D RID: 6253
		public SpriteStamp bloodDecal;

		// Token: 0x0400186E RID: 6254
		public GameObject jumpTrail;

		// Token: 0x0400186F RID: 6255
		public Island island;

		// Token: 0x04001870 RID: 6256
		public Mesh[] corpseMeshes;

		// Token: 0x04001871 RID: 6257
		public Material corpseMaterial;

		// Token: 0x04001872 RID: 6258
		public Ragdoll ragdoll;

		// Token: 0x04001873 RID: 6259
		public Torch torch;

		// Token: 0x04001874 RID: 6260
		public EnglishSquad englishSquad;

		// Token: 0x04001875 RID: 6261
		public SquadTemplate militiaTemplate;

		// Token: 0x04001876 RID: 6262
		public Squad vikingSquad;

		// Token: 0x04001877 RID: 6263
		public AudioMixerSnapshot activeAbilityAudioMixerSnapshot;

		// Token: 0x04001878 RID: 6264
		public AudioMixerSnapshot normalAudioMixerSnapshot;

		// Token: 0x04001879 RID: 6265
		[Header("UI")]
		public Color defaultGraphicParentColor = Color.white;

		// Token: 0x0400187A RID: 6266
		public ButtonWidget continueButton;

		// Token: 0x0400187B RID: 6267
		[Header("Upgrades")]
		public List<UpgradeComponent> universalUpgrades;

		// Token: 0x0400187C RID: 6268
		public WorldSpaceNavSpotCursor navSpotCursor;

		// Token: 0x0400187D RID: 6269
		public FloatingIcon abilityFloatingIcon;

		// Token: 0x0400187E RID: 6270
		public TargetNavSpot targetNavspot;

		// Token: 0x0400187F RID: 6271
		public BoxCollider landingBoxCollider;

		// Token: 0x04001880 RID: 6272
		public HeightBasedProjectileSolver disembarkProjectileSolver;
	}
}
