using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020006F7 RID: 1783
	[Serializable]
	public class HeroVoice : ScriptableObject
	{
		// Token: 0x04001E99 RID: 7833
		[Header("General")]
		public FabricEventReference portraitSelectAudio = "Sfx/Characters/01Select";

		// Token: 0x04001E9A RID: 7834
		public FabricEventReference deathAudio = "UI/InGame/SquadLost";

		// Token: 0x04001E9B RID: 7835
		[Header("Spears")]
		public FabricEventReference spearsDownSound = "Sfx/English/Spear/SpearsDown";

		// Token: 0x04001E9C RID: 7836
		public FabricEventReference spearsUpSound = "Sfx/English/Spear/SpearsUp";

		// Token: 0x04001E9D RID: 7837
		[Header("Archers")]
		public FabricEventReference[] archeryFocusStartAudioId = new FabricEventReference[]
		{
			"Sfx/Ability/ArcheryFocus/Start01",
			"Sfx/Ability/ArcheryFocus/Start02",
			"Sfx/Ability/ArcheryFocus/Start03"
		};

		// Token: 0x04001E9E RID: 7838
		public FabricEventReference[] archeryFocusStopAudioId = new FabricEventReference[]
		{
			"Sfx/Ability/ArcheryFocus/Stop"
		};

		// Token: 0x04001E9F RID: 7839
		[Header("Infantry")]
		public FabricEventReference[] plungeJumpSounds = new FabricEventReference[]
		{
			"Sfx/Ability/Plunge/Plunge01",
			"Sfx/Ability/Plunge/Plunge02",
			"Sfx/Ability/Plunge/Plunge03"
		};

		// Token: 0x04001EA0 RID: 7840
		public FabricEventReference plungeLandSound = "Sfx/English/Land";

		// Token: 0x04001EA1 RID: 7841
		[Header("Warhammer")]
		public FabricEventReference[] warhammerJumpSounds = new FabricEventReference[]
		{
			"Sfx/Ability/GroundPound/Jump01",
			"Sfx/Ability/GroundPound/Jump02",
			"Sfx/Ability/GroundPound/Jump03"
		};

		// Token: 0x04001EA2 RID: 7842
		public FabricEventReference[] warhammerLandSounds = new FabricEventReference[]
		{
			"Sfx/Ability/GroundPound/Land01",
			"Sfx/Ability/GroundPound/Land02",
			"Sfx/Ability/GroundPound/Land03"
		};
	}
}
