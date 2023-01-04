using System;
using System.Diagnostics;
using RTM.UISystem;
using UnityEngine;
using UnityEngine.Video;

namespace Voxels.TowerDefense
{
	// Token: 0x0200056A RID: 1386
	public class DemoAttractMode : MonoBehaviour, IScenePostprocessor
	{
		// Token: 0x1400007E RID: 126
		// (add) Token: 0x06002405 RID: 9221 RVA: 0x00070D78 File Offset: 0x0006F178
		// (remove) Token: 0x06002406 RID: 9222 RVA: 0x00070DAC File Offset: 0x0006F1AC
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action onAttractModeTriggered;

		// Token: 0x06002407 RID: 9223 RVA: 0x00070DE0 File Offset: 0x0006F1E0
		public static bool PlayVideo()
		{
			DemoAttractMode demoAttractMode = UnityEngine.Object.FindObjectOfType<DemoAttractMode>();
			if (demoAttractMode)
			{
				demoAttractMode.timeout.ExpireTimeout();
				return demoAttractMode.vidStarted;
			}
			return false;
		}

		// Token: 0x06002408 RID: 9224 RVA: 0x00070E14 File Offset: 0x0006F214
		private void Awake()
		{
			this.videoPath = Application.streamingAssetsPath + "/Demo/Video/DemoAttractVideo.mp4";
			this.timeout = base.GetComponent<Timeout>();
			this.timeout.OnTimedOut += this.OnTimedOut;
			this.timeout.OnTimerReset += this.OnTimerReset;
			this.videoPlayer = this.cameraReference.GetComponent<VideoPlayer>();
			if (this.videoPlayer)
			{
				this.videoPlayer.gameObject.SetActive(false);
			}
		}

		// Token: 0x06002409 RID: 9225 RVA: 0x00070EA4 File Offset: 0x0006F2A4
		private void Update()
		{
			if (!this.attractState.active)
			{
				base.enabled = false;
				return;
			}
			if (this.videoPlayer && this.vidStarted && !this.videoPlayer.isPlaying)
			{
				this.timeout.ResetTimer();
			}
		}

		// Token: 0x0600240A RID: 9226 RVA: 0x00070F00 File Offset: 0x0006F300
		private void OnTimedOut()
		{
			IslandGenerator.AddBlocker(this, this);
			DemoAttractMode.onAttractModeTriggered();
			this.attractState.SetActive(true);
			Singleton<UIManager>.instance.CloseAll();
			this.cameraReference.gameObject.SetActive(true);
			if (this.videoPlayer)
			{
				this.videoPlayer.url = this.videoPath;
				this.videoPlayer.Play();
				this.vidStarted = this.videoPlayer.isPlaying;
			}
			base.enabled = true;
		}

		// Token: 0x0600240B RID: 9227 RVA: 0x00070F8C File Offset: 0x0006F38C
		private void OnTimerReset()
		{
			if (this.attractState && this.attractState.active)
			{
				IslandGenerator.RemoveBlocker(this, this);
				this.resetState.SetActive(true);
				this.cameraReference.gameObject.SetActive(false);
				base.enabled = false;
				this.vidStarted = false;
			}
		}

		// Token: 0x0600240C RID: 9228 RVA: 0x00070FEB File Offset: 0x0006F3EB
		void IScenePostprocessor.OnPostprocessScene()
		{
		}

		// Token: 0x0600240D RID: 9229 RVA: 0x00070FED File Offset: 0x0006F3ED
		// Note: this type is marked as 'beforefieldinit'.
		static DemoAttractMode()
		{
			DemoAttractMode.onAttractModeTriggered = delegate()
			{
			};
		}

		// Token: 0x040016A6 RID: 5798
		private string videoPath;

		// Token: 0x040016A7 RID: 5799
		[Header("Scene References")]
		[SerializeField]
		private Camera cameraReference;

		// Token: 0x040016A8 RID: 5800
		[Header("Scene References")]
		[SerializeField]
		private SwitchVideoPlayback switchVideoPlayback;

		// Token: 0x040016A9 RID: 5801
		private VideoPlayer videoPlayer;

		// Token: 0x040016AA RID: 5802
		[Header("States")]
		[SerializeField]
		private State attractState;

		// Token: 0x040016AB RID: 5803
		[SerializeField]
		private State resetState;

		// Token: 0x040016AC RID: 5804
		private Timeout timeout;

		// Token: 0x040016AD RID: 5805
		private bool vidStarted;
	}
}
