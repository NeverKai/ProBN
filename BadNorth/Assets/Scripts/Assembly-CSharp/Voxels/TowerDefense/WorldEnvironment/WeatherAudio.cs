using System;
using Fabric;
using UnityEngine;

namespace Voxels.TowerDefense.WorldEnvironment
{
	// Token: 0x02000873 RID: 2163
	public class WeatherAudio : ChildComponent<WorldWeather>
	{
		// Token: 0x060038B7 RID: 14519 RVA: 0x000F539C File Offset: 0x000F379C
		private void Awake()
		{
			this.wind.gameObject = base.gameObject;
			this.rain.gameObject = base.gameObject;
			this.snow.gameObject = base.gameObject;
			this.thunder.gameObject = base.gameObject;
		}

		// Token: 0x060038B8 RID: 14520 RVA: 0x000F53F0 File Offset: 0x000F37F0
		private void Update()
		{
			WorldWeather.WeatherKey current = base.manager.weatherSystem.current;
			using (new ScopedProfiler("wind", null))
			{
				this.wind.intensity = current.wind;
			}
			using (new ScopedProfiler("rain", null))
			{
				this.rain.intensity = current.rainfall;
			}
			using (new ScopedProfiler("thunder", null))
			{
				this.thunder.intensity = current.thunder;
			}
			float num = Mathf.Max(current.snowfall, Mathf.InverseLerp(this.minSnowCoverage, 1f, current.snowCoverage));
			using (new ScopedProfiler("snow", null))
			{
				this.snow.intensity = num;
			}
			using (new ScopedProfiler("night", null))
			{
				bool flag = Singleton<EnvironmentManager>.instance.day.dayPhase == DayPhase.Night && num < 0.05f;
				this.night.intensity = ((!flag) ? 0f : 1f);
			}
		}

		// Token: 0x060038B9 RID: 14521 RVA: 0x000F559C File Offset: 0x000F399C
		private void OnDisable()
		{
			this.wind.intensity = 0f;
			this.rain.intensity = 0f;
			this.snow.intensity = 0f;
			this.thunder.intensity = 0f;
		}

		// Token: 0x04002693 RID: 9875
		[SerializeField]
		private WeatherAudio.WeatherLoop wind;

		// Token: 0x04002694 RID: 9876
		[SerializeField]
		private WeatherAudio.WeatherLoop rain;

		// Token: 0x04002695 RID: 9877
		[SerializeField]
		private WeatherAudio.WeatherLoop snow;

		// Token: 0x04002696 RID: 9878
		[SerializeField]
		private WeatherAudio.WeatherLoop thunder = new WeatherAudio.WeatherLoop("Amb/Thunder", "Intensity");

		// Token: 0x04002697 RID: 9879
		[SerializeField]
		private WeatherAudio.WeatherLoop night = new WeatherAudio.WeatherLoop("Amb/Night", "Intensity");

		// Token: 0x04002698 RID: 9880
		[SerializeField]
		private float minSnowCoverage = 0.3f;

		// Token: 0x02000874 RID: 2164
		[Serializable]
		private class WeatherLoop
		{
			// Token: 0x060038BA RID: 14522 RVA: 0x000F55E9 File Offset: 0x000F39E9
			public WeatherLoop(string loopString, string intensityString)
			{
				this.loopString = loopString;
				this.intensityString = intensityString;
			}

			// Token: 0x17000820 RID: 2080
			// (get) Token: 0x060038BB RID: 14523 RVA: 0x000F55FF File Offset: 0x000F39FF
			// (set) Token: 0x060038BC RID: 14524 RVA: 0x000F5608 File Offset: 0x000F3A08
			public bool playing
			{
				get
				{
					return this._playing;
				}
				private set
				{
					if (this._playing == value)
					{
						return;
					}
					this._playing = value;
					if (value)
					{
						FabricWrapper.PostEvent(this.loopString, this.gameObject);
					}
					else
					{
						FabricWrapper.PostEvent(this.loopString, EventAction.StopSound, null, this.gameObject);
					}
				}
			}

			// Token: 0x17000821 RID: 2081
			// (get) Token: 0x060038BD RID: 14525 RVA: 0x000F565A File Offset: 0x000F3A5A
			// (set) Token: 0x060038BE RID: 14526 RVA: 0x000F5664 File Offset: 0x000F3A64
			public float intensity
			{
				get
				{
					return this._intensity;
				}
				set
				{
					if (this._intensity == value)
					{
						return;
					}
					this._intensity = value;
					this.playing = (value > 0f);
					if (this.playing)
					{
						using (new ScopedProfiler("Fabric - probably editor-only garbage", null))
						{
							EventManager.Instance.SetParameter(this.loopString, this.intensityString, value, this.gameObject);
						}
					}
				}
			}

			// Token: 0x04002699 RID: 9881
			[SerializeField]
			private string loopString;

			// Token: 0x0400269A RID: 9882
			[SerializeField]
			private string intensityString;

			// Token: 0x0400269B RID: 9883
			[NonSerialized]
			public GameObject gameObject;

			// Token: 0x0400269C RID: 9884
			private bool _playing;

			// Token: 0x0400269D RID: 9885
			private float _intensity;
		}
	}
}
