using System;
using System.Collections.Generic;
using CS.Lights;
using ReflexCLI.Attributes;
using RTM.Utilities;
using UnityEngine;

namespace Voxels.TowerDefense.WorldEnvironment
{
	// Token: 0x02000879 RID: 2169
	[ConsoleCommandClassCustomizer("WorldWeather")]
	public class WorldWeather : Singleton<WorldWeather>, IGameSetup, EnvironmentManager.IStartLevel, EnvironmentManager.IEndLevel
	{
		// Token: 0x060038CC RID: 14540 RVA: 0x000F647E File Offset: 0x000F487E
		protected override void Awake()
		{
			base.Awake();
			this.weatherLight = base.GetComponent<WeatherLight>();
		}

		// Token: 0x060038CD RID: 14541 RVA: 0x000F6494 File Offset: 0x000F4894
		private void Update()
		{
			this.SetDay(Singleton<EnvironmentManager>.instance.year, Singleton<EnvironmentManager>.instance.day);
			this.Apply();
			if (this.weatherSystem.snowfall >= 0.1f)
			{
				this.hasSnowedOnIsland = true;
			}
			if (Singleton<EnvironmentManager>.instance.day.dayPhase == DayPhase.Night)
			{
				this.hasBeenNightOnIsland = true;
			}
			if (this.alternate)
			{
				this.rainParticles.Redraw(Time.deltaTime * 2f);
				this.rainParticles.SetInterpolate(0f);
				this.snowParticles.SetInterpolate(0.5f);
			}
			else
			{
				this.snowParticles.Redraw(Time.deltaTime * 2f);
				this.snowParticles.SetInterpolate(0f);
				this.rainParticles.SetInterpolate(0.5f);
			}
			this.alternate = !this.alternate;
			this.weatherLight.rainIntensity = this.weatherSystem.rainfall;
			this.weatherLight.snowIntensity = this.weatherSystem.snowfall;
		}

		// Token: 0x060038CE RID: 14542 RVA: 0x000F65B0 File Offset: 0x000F49B0
		public void SetDay(Year year, Day day)
		{
			this.weatherSystem.time = day;
			Month month = year.month;
			Shader.SetGlobalFloat(this.yearId, year);
			while (this.weatherSystem.key1.time < this.weatherSystem.time)
			{
				WorldWeather.WeatherKey key = this.weatherSystem.key0;
				WorldWeather.WeatherKey key2 = this.weatherSystem.key1;
				bool flag = key2.snowfall > 0f || key.snowfall > 0f;
				bool flag2 = key2.rainfall > 0f || key.rainfall > 0f;
				key = key2;
				float num = UnityEngine.Random.Range(this.timeBetweenChanges.x, this.timeBetweenChanges.y);
				key2.time += num;
				if (UnityEngine.Random.value < this.changeProbability)
				{
					key2.wind = Mathf.Clamp01(UnityEngine.Random.value + this.windProbability[month] - 1f);
				}
				if (UnityEngine.Random.value < this.changeProbability)
				{
					key2.cloudCoverage = (float)((UnityEngine.Random.value >= this.cloudsProbability[month]) ? 0 : 1);
				}
				float num2 = key.precipitation;
				if (key2.cloudCoverage == 1f)
				{
					if (UnityEngine.Random.value < this.changeProbability || key.cloudCoverage < 0.5f)
					{
						num2 = Mathf.Clamp01(UnityEngine.Random.value + this.precipitationProbability[month] * 2f - 1f);
					}
				}
				else
				{
					num2 = 0f;
				}
				key2.temperature = this.temperature[year.month];
				key2.temperature += -year.winterSummerWave * (1f - key2.cloudCoverage) * 2f;
				bool flag3 = key2.temperature < 0f;
				key2.snowfall = ((!flag3) ? 0f : num2);
				key2.rainfall = ((!flag3) ? num2 : 0f);
				if (key2.rainfall > 0.2f)
				{
					key2.thunder = (float)((UnityEngine.Random.value >= this.thunderProbability[month]) ? 0 : 1);
				}
				else
				{
					key2.thunder = 0f;
				}
				key2.snowCoverage = Mathf.MoveTowards(key2.snowCoverage, 1f, num * (key2.snowfall + key.snowfall) * 0.2f);
				key2.snowCoverage = Mathf.MoveTowards(key2.snowCoverage, 0f, num * key2.rainfall * 2f);
				key2.snowCoverage = Mathf.MoveTowards(key2.snowCoverage, 0f, num * Mathf.Max(0f, key2.temperature) * 0.5f);
				flag2 = (flag2 || key2.rainfall > 0f);
				flag = (flag || key2.snowfall > 0f);
				if (this.rainParticles)
				{
					this.rainParticles.gameObject.SetActive(flag2);
				}
				if (this.snowParticles)
				{
					this.snowParticles.gameObject.SetActive(flag);
				}
				this.weatherSystem.key0 = key;
				this.weatherSystem.key1 = key2;
			}
		}

		// Token: 0x060038CF RID: 14543 RVA: 0x000F695C File Offset: 0x000F4D5C
		private void Apply()
		{
			WorldWeather.WeatherKey current = this.weatherSystem.current;
			this.tmpCloudCoverage.x = current.cloudCoverage;
			this.tmpCloudCoverage.y = Mathf.Lerp(1.05f, 0.7f, current.cloudCoverage) * Shader.GetGlobalFloat("_Saturation");
			this.tmpCloudCoverage.z = Mathf.Lerp(1f, 0.4f, current.cloudCoverage);
			Shader.SetGlobalVector(this.cloudCoverageId, this.tmpCloudCoverage);
			Shader.SetGlobalFloat(this.snowCoverageId, current.snowCoverage);
			Shader.SetGlobalFloat(this.frostId, current.frost);
			if (current.frost > 0f && !this.frostOn)
			{
				Shader.EnableKeyword("_FROST_ON");
				this.frostOn = true;
			}
			else if (current.frost == 0f && this.frostOn)
			{
				Shader.DisableKeyword("_FROST_ON");
				this.frostOn = false;
			}
			if (current.snowCoverage > 0f && !this.snowOn)
			{
				Shader.EnableKeyword("_SNOW_ON");
				this.snowOn = true;
			}
			else if (current.snowCoverage == 0f && this.snowOn)
			{
				Shader.DisableKeyword("_SNOW_ON");
				this.snowOn = false;
			}
			if (this.snowParticles)
			{
				if (current.snowfall > 0f)
				{
					this.snowParticles.gameObject.SetActive(true);
				}
				this.snowParticles.amount = current.snowfall;
			}
			if (this.rainParticles)
			{
				if (current.rainfall > 0f)
				{
					this.rainParticles.gameObject.SetActive(true);
				}
				this.rainParticles.amount = current.rainfall;
			}
		}

		// Token: 0x060038D0 RID: 14544 RVA: 0x000F6B60 File Offset: 0x000F4F60
		void EnvironmentManager.IStartLevel.StartLevel(EnvironmentManager envManager, LevelNode level)
		{
			this.levelNode.Target = level;
			this.weatherSystem = Profile.campaign.weatherSystem;
			this.SetDay(envManager.year, envManager.day);
			if (this.weatherSystem.snowfall > 0f)
			{
				this.snowParticles.gameObject.SetActive(true);
				this.snowParticles.Warmup(200, 4f);
			}
			else
			{
				this.snowParticles.gameObject.SetActive(false);
			}
			if (this.weatherSystem.rainfall > 0f)
			{
				this.rainParticles.gameObject.SetActive(true);
				this.rainParticles.Warmup(200, 4f);
			}
			else
			{
				this.rainParticles.gameObject.SetActive(false);
			}
			List<IslandStyle2> list = ListPool<IslandStyle2>.GetList(2);
			level.levelState.GetReferencedObjects(list);
			foreach (IslandStyle2 islandStyle in list)
			{
				islandStyle.ApplyShaderConstants(envManager.year);
			}
			list.ReturnToListPool<IslandStyle2>();
			this.hasSnowedOnIsland = false;
			this.hasBeenNightOnIsland = false;
		}

		// Token: 0x060038D1 RID: 14545 RVA: 0x000F6CB8 File Offset: 0x000F50B8
		void EnvironmentManager.IEndLevel.EndLevel(EnvironmentManager envManager, LevelNode level)
		{
			this.levelNode.Target = null;
			Profile.campaign.weatherSystem = this.weatherSystem;
		}

		// Token: 0x060038D2 RID: 14546 RVA: 0x000F6CD6 File Offset: 0x000F50D6
		void IGameSetup.OnGameAwake()
		{
			Singleton<WorldWeather>._instance = this;
		}

		// Token: 0x040026B6 RID: 9910
		[Header("Probabilities")]
		[SerializeField]
		private MonthlyProbability cloudsProbability = new Vector4(0.7f, 0.2f, 0.1f, 0.7f);

		// Token: 0x040026B7 RID: 9911
		[SerializeField]
		private MonthlyProbability windProbability = new Vector4(0.3f, 0.1f, 0.1f, 0.5f);

		// Token: 0x040026B8 RID: 9912
		[SerializeField]
		private MonthlyProbability precipitationProbability = new Vector4(0.9f, 0.7f, 0.7f, 0.8f);

		// Token: 0x040026B9 RID: 9913
		[SerializeField]
		private MonthlyProbability thunderProbability = new Vector4(0f, 0f, 0.8f, 0.4f);

		// Token: 0x040026BA RID: 9914
		[SerializeField]
		private MonthlyProbability temperature = new Vector4(0f, 0f, 0.8f, 0.4f);

		// Token: 0x040026BB RID: 9915
		[Header("Particle references")]
		public WindParticleSystem snowParticles;

		// Token: 0x040026BC RID: 9916
		[Header("Particle references")]
		public WindParticleSystem rainParticles;

		// Token: 0x040026BD RID: 9917
		private bool alternate;

		// Token: 0x040026BE RID: 9918
		[Space]
		[SerializeField]
		public WorldWeather.WeatherSystem weatherSystem;

		// Token: 0x040026BF RID: 9919
		[Space]
		[SerializeField]
		private Vector2 timeBetweenChanges = new Vector2(1f, 4f);

		// Token: 0x040026C0 RID: 9920
		[SerializeField]
		private float changeProbability = 0.4f;

		// Token: 0x040026C1 RID: 9921
		private ShaderId cloudCoverageId = "_CloudCoverage";

		// Token: 0x040026C2 RID: 9922
		private ShaderId snowCoverageId = "_SnowAmount";

		// Token: 0x040026C3 RID: 9923
		private ShaderId yearId = "_Year";

		// Token: 0x040026C4 RID: 9924
		private ShaderId temperatureId = "_Temperature";

		// Token: 0x040026C5 RID: 9925
		private ShaderId frostId = "_Frost";

		// Token: 0x040026C6 RID: 9926
		public bool hasSnowedOnIsland;

		// Token: 0x040026C7 RID: 9927
		public bool hasBeenNightOnIsland;

		// Token: 0x040026C8 RID: 9928
		private WeakReference<LevelNode> levelNode = new WeakReference<LevelNode>(null);

		// Token: 0x040026C9 RID: 9929
		private WeatherLight weatherLight;

		// Token: 0x040026CA RID: 9930
		private bool snowOn;

		// Token: 0x040026CB RID: 9931
		private bool frostOn;

		// Token: 0x040026CC RID: 9932
		private Vector4 tmpCloudCoverage = Vector4.zero;

		// Token: 0x0200087A RID: 2170
		[Serializable]
		public struct WeatherKey
		{
			// Token: 0x17000822 RID: 2082
			// (get) Token: 0x060038D3 RID: 14547 RVA: 0x000F6CDE File Offset: 0x000F50DE
			public float precipitation
			{
				get
				{
					return this.rainfall + this.snowfall;
				}
			}

			// Token: 0x17000823 RID: 2083
			// (get) Token: 0x060038D4 RID: 14548 RVA: 0x000F6CED File Offset: 0x000F50ED
			public float frost
			{
				get
				{
					return Mathf.Clamp01(0.5f - this.temperature);
				}
			}

			// Token: 0x060038D5 RID: 14549 RVA: 0x000F6D00 File Offset: 0x000F5100
			private new string ToString()
			{
				return string.Concat(new string[]
				{
					"\ntime ",
					this.time.ToString("F2"),
					"\ncloudCoverage ",
					this.cloudCoverage.ToString("F2"),
					"\nrainFall ",
					this.rainfall.ToString("F2"),
					"\nsnowfall ",
					this.snowfall.ToString("F2"),
					"\nsnowCoverage ",
					this.snowCoverage.ToString("F2"),
					"\nwind ",
					this.wind.ToString("F2"),
					"\nthunder ",
					this.thunder.ToString("F2")
				});
			}

			// Token: 0x060038D6 RID: 14550 RVA: 0x000F6DDC File Offset: 0x000F51DC
			public static WorldWeather.WeatherKey Lerp(WorldWeather.WeatherKey w0, WorldWeather.WeatherKey w1, float t)
			{
				return new WorldWeather.WeatherKey
				{
					time = Mathf.Lerp(w0.time, w1.time, t),
					cloudCoverage = Mathf.Lerp(w0.cloudCoverage, w1.cloudCoverage, t),
					rainfall = Mathf.Lerp(w0.rainfall, w1.rainfall, t),
					snowfall = Mathf.Lerp(w0.snowfall, w1.snowfall, t),
					snowCoverage = Mathf.Lerp(w0.snowCoverage, w1.snowCoverage, t),
					wind = Mathf.Lerp(w0.wind, w1.wind, t),
					thunder = Mathf.Lerp(w0.thunder, w1.thunder, t),
					temperature = Mathf.Lerp(w0.temperature, w1.temperature, t)
				};
			}

			// Token: 0x040026CD RID: 9933
			public float time;

			// Token: 0x040026CE RID: 9934
			[Range(0f, 1f)]
			public float cloudCoverage;

			// Token: 0x040026CF RID: 9935
			[Range(0f, 1f)]
			public float rainfall;

			// Token: 0x040026D0 RID: 9936
			[Range(0f, 1f)]
			public float snowfall;

			// Token: 0x040026D1 RID: 9937
			[Range(0f, 1f)]
			public float snowCoverage;

			// Token: 0x040026D2 RID: 9938
			[Range(0f, 1f)]
			public float wind;

			// Token: 0x040026D3 RID: 9939
			[Range(0f, 1f)]
			public float thunder;

			// Token: 0x040026D4 RID: 9940
			[Range(0f, 1f)]
			public float temperature;
		}

		// Token: 0x0200087B RID: 2171
		[Serializable]
		public struct WeatherSystem
		{
			// Token: 0x060038D7 RID: 14551 RVA: 0x000F6ECA File Offset: 0x000F52CA
			public WeatherSystem(float time, WorldWeather.WeatherKey key0, WorldWeather.WeatherKey key1)
			{
				this.time = time;
				this.key0 = key0;
				this.key1 = key1;
			}

			// Token: 0x17000824 RID: 2084
			// (get) Token: 0x060038D8 RID: 14552 RVA: 0x000F6EE1 File Offset: 0x000F52E1
			public float t
			{
				get
				{
					return ExtraMath.RemapValue(this.time, this.key0.time, this.key1.time, 0f, 1f);
				}
			}

			// Token: 0x17000825 RID: 2085
			// (get) Token: 0x060038D9 RID: 14553 RVA: 0x000F6F0E File Offset: 0x000F530E
			// (set) Token: 0x060038DA RID: 14554 RVA: 0x000F6F27 File Offset: 0x000F5327
			public WorldWeather.WeatherKey current
			{
				get
				{
					return WorldWeather.WeatherKey.Lerp(this.key0, this.key1, this.t);
				}
				set
				{
					this.key0 = value;
					this.key1 = value;
					this.key1.time = this.key1.time + 1f;
					this.time = this.key0.time;
				}
			}

			// Token: 0x17000826 RID: 2086
			// (get) Token: 0x060038DB RID: 14555 RVA: 0x000F6F60 File Offset: 0x000F5360
			// (set) Token: 0x060038DC RID: 14556 RVA: 0x000F6F7C File Offset: 0x000F537C
			public float cloudCoverage
			{
				get
				{
					return this.current.cloudCoverage;
				}
				set
				{
					if (this.cloudCoverage == value)
					{
						return;
					}
					WorldWeather.WeatherKey current = this.current;
					current.cloudCoverage = value;
					current.snowfall = Mathf.Min(current.snowfall, value);
					current.rainfall = Mathf.Min(current.rainfall, value);
					current.thunder = Mathf.Min(current.thunder, value);
					this.current = current;
				}
			}

			// Token: 0x17000827 RID: 2087
			// (get) Token: 0x060038DD RID: 14557 RVA: 0x000F6FE8 File Offset: 0x000F53E8
			// (set) Token: 0x060038DE RID: 14558 RVA: 0x000F7004 File Offset: 0x000F5404
			public float rainfall
			{
				get
				{
					return this.current.rainfall;
				}
				set
				{
					if (this.rainfall == value)
					{
						return;
					}
					WorldWeather.WeatherKey current = this.current;
					current.rainfall = value;
					this.current = current;
					this.cloudCoverage = Mathf.Max(this.cloudCoverage, value);
					this.snowfall = Mathf.Min(this.snowfall, 1f - value);
					this.thunder = Mathf.Min(this.thunder, value);
				}
			}

			// Token: 0x17000828 RID: 2088
			// (get) Token: 0x060038DF RID: 14559 RVA: 0x000F7070 File Offset: 0x000F5470
			// (set) Token: 0x060038E0 RID: 14560 RVA: 0x000F708C File Offset: 0x000F548C
			public float snowfall
			{
				get
				{
					return this.current.snowfall;
				}
				set
				{
					if (this.snowfall == value)
					{
						return;
					}
					WorldWeather.WeatherKey current = this.current;
					current.snowfall = value;
					this.current = current;
					this.rainfall = Mathf.Min(this.rainfall, 1f - value);
					this.cloudCoverage = Mathf.Max(this.cloudCoverage, value);
				}
			}

			// Token: 0x17000829 RID: 2089
			// (get) Token: 0x060038E1 RID: 14561 RVA: 0x000F70E8 File Offset: 0x000F54E8
			// (set) Token: 0x060038E2 RID: 14562 RVA: 0x000F7103 File Offset: 0x000F5503
			public float snowCoverage
			{
				get
				{
					return this.current.snowCoverage;
				}
				set
				{
					this.key0.snowCoverage = value;
					this.key1.snowCoverage = value;
				}
			}

			// Token: 0x1700082A RID: 2090
			// (get) Token: 0x060038E3 RID: 14563 RVA: 0x000F7120 File Offset: 0x000F5520
			// (set) Token: 0x060038E4 RID: 14564 RVA: 0x000F713B File Offset: 0x000F553B
			public float wind
			{
				get
				{
					return this.current.wind;
				}
				set
				{
					this.key0.wind = value;
					this.key1.wind = value;
				}
			}

			// Token: 0x1700082B RID: 2091
			// (get) Token: 0x060038E5 RID: 14565 RVA: 0x000F7158 File Offset: 0x000F5558
			// (set) Token: 0x060038E6 RID: 14566 RVA: 0x000F7174 File Offset: 0x000F5574
			public float thunder
			{
				get
				{
					return this.current.thunder;
				}
				set
				{
					if (this.thunder == value)
					{
						return;
					}
					WorldWeather.WeatherKey current = this.current;
					current.thunder = value;
					this.current = current;
					this.rainfall = Mathf.Max(this.rainfall, value);
				}
			}

			// Token: 0x1700082C RID: 2092
			// (get) Token: 0x060038E7 RID: 14567 RVA: 0x000F71B8 File Offset: 0x000F55B8
			// (set) Token: 0x060038E8 RID: 14568 RVA: 0x000F71D3 File Offset: 0x000F55D3
			public float temperature
			{
				get
				{
					return this.current.temperature;
				}
				set
				{
					this.key0.temperature = value;
					this.key1.temperature = value;
				}
			}

			// Token: 0x040026D5 RID: 9941
			public WorldWeather.WeatherKey key0;

			// Token: 0x040026D6 RID: 9942
			public WorldWeather.WeatherKey key1;

			// Token: 0x040026D7 RID: 9943
			public float time;
		}
	}
}
