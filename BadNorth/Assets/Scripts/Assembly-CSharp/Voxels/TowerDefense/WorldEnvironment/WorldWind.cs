using System;
using UnityEngine;

namespace Voxels.TowerDefense.WorldEnvironment
{
	// Token: 0x0200087C RID: 2172
	public class WorldWind : Singleton<WorldWind>, IGameSetup
	{
		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x060038EA RID: 14570 RVA: 0x000F7273 File Offset: 0x000F5673
		private float windInterpolator
		{
			get
			{
				return Singleton<WorldWeather>.instance.weatherSystem.wind;
			}
		}

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x060038EB RID: 14571 RVA: 0x000F7284 File Offset: 0x000F5684
		public float windSpeed
		{
			get
			{
				return Mathf.Lerp(1f, 4f, this.windInterpolator);
			}
		}

		// Token: 0x060038EC RID: 14572 RVA: 0x000F729B File Offset: 0x000F569B
		public void Reset()
		{
			this.windTime = 0f;
			this.sqrtWindTime = 0f;
			this.maxWindTime = 0f;
		}

		// Token: 0x060038ED RID: 14573 RVA: 0x000F72C0 File Offset: 0x000F56C0
		private void Update()
		{
			this.maxWindTime = (this.maxWindTime + Time.deltaTime * 4f * this.windInterpolator) % 1000000f;
			this.windTime = (this.windTime + Time.deltaTime * this.windSpeed) % 1000000f;
			this.sqrtWindTime = (this.sqrtWindTime + Time.deltaTime * Mathf.Sqrt(this.windSpeed)) % 1000000f;
			Shader.SetGlobalFloat(this.maxWindTimeId, this.maxWindTime);
			Shader.SetGlobalFloat(this.windTimeId, this.windTime);
			Shader.SetGlobalFloat(this.sqrtWindTimeId, this.sqrtWindTime);
			Shader.SetGlobalFloat(this.windInterpolatorId, this.windInterpolator);
			Shader.SetGlobalFloat(this.maxWindSpeedId, 4f);
			Shader.SetGlobalFloat(this.minWindSpeedId, 1f);
			Shader.SetGlobalFloat(this.windSpeedId, this.windSpeed);
		}

		// Token: 0x060038EE RID: 14574 RVA: 0x000F73CD File Offset: 0x000F57CD
		void IGameSetup.OnGameAwake()
		{
			Singleton<WorldWind>._instance = this;
		}

		// Token: 0x040026D8 RID: 9944
		private const float minWindSpeed = 1f;

		// Token: 0x040026D9 RID: 9945
		private const float maxWindSpeed = 4f;

		// Token: 0x040026DA RID: 9946
		private float maxWindTime;

		// Token: 0x040026DB RID: 9947
		public float windTime;

		// Token: 0x040026DC RID: 9948
		public float sqrtWindTime;

		// Token: 0x040026DD RID: 9949
		private ShaderId windInterpolatorId = "_WindInterpolator";

		// Token: 0x040026DE RID: 9950
		private ShaderId maxWindSpeedId = "_MaxWindSpeed";

		// Token: 0x040026DF RID: 9951
		private ShaderId minWindSpeedId = "_MinWindSpeed";

		// Token: 0x040026E0 RID: 9952
		private ShaderId windSpeedId = "_WindSpeed";

		// Token: 0x040026E1 RID: 9953
		private ShaderId maxWindTimeId = "_MaxWindTime";

		// Token: 0x040026E2 RID: 9954
		private ShaderId windTimeId = "_WindTime";

		// Token: 0x040026E3 RID: 9955
		private ShaderId sqrtWindTimeId = "_SqrtWindTime";
	}
}
