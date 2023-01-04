using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Voxels.TowerDefense
{
	// Token: 0x0200002A RID: 42
	public class UserAudioMixer : MonoBehaviour
	{
		// Token: 0x060000E2 RID: 226 RVA: 0x0000613C File Offset: 0x0000453C
		private void Awake()
		{
			this.instance = this;
			UserSettings.onUpdated += this.Apply;
			this.Apply(Profile.userSettings);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00006161 File Offset: 0x00004561
		private void OnDestroy()
		{
			UserSettings.onUpdated -= this.Apply;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00006174 File Offset: 0x00004574
		private void Apply(UserSettings settings)
		{
			if (settings != null)
			{
				this.mixer.SetFloat(this.sfxSettings.parameterName, this.sfxSettings.GetVolume(settings.sfxVolume));
				this.mixer.SetFloat(this.musicSettings.parameterName, this.musicSettings.GetVolume(settings.musicVolume));
			}
		}

		// Token: 0x04000069 RID: 105
		[SerializeField]
		private AudioMixer mixer;

		// Token: 0x0400006A RID: 106
		[SerializeField]
		private UserAudioMixer.Settings sfxSettings = new UserAudioMixer.Settings();

		// Token: 0x0400006B RID: 107
		[SerializeField]
		private UserAudioMixer.Settings musicSettings = new UserAudioMixer.Settings();

		// Token: 0x0400006C RID: 108
		private UserAudioMixer instance;

		// Token: 0x0200002B RID: 43
		[Serializable]
		private class Settings
		{
			// Token: 0x060000E6 RID: 230 RVA: 0x00006200 File Offset: 0x00004600
			public float GetVolume(int vol)
			{
				if (vol <= 0)
				{
					return -80f;
				}
				float f = Mathf.Clamp((float)vol / (float)this.steps, 0.1f, 1f);
				return -this.minVolumeDb * Mathf.Log10(f);
			}

			// Token: 0x0400006D RID: 109
			public string parameterName = string.Empty;

			// Token: 0x0400006E RID: 110
			public float minVolumeDb = -80f;

			// Token: 0x0400006F RID: 111
			public float maxVolumeDb;

			// Token: 0x04000070 RID: 112
			public int steps = 10;
		}
	}
}
