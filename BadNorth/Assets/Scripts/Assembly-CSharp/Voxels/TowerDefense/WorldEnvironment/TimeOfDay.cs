using System;
using UnityEngine;

namespace Voxels.TowerDefense.WorldEnvironment
{
	// Token: 0x020005D3 RID: 1491
	public class TimeOfDay : ChildComponent<EnvironmentManager>, IGameSetup
	{
		// Token: 0x060026CC RID: 9932 RVA: 0x0007BFC1 File Offset: 0x0007A3C1
		void IGameSetup.OnGameAwake()
		{
			Singleton<ShaderConstants>.instance.modifiers.Add(this.saturationModifier);
		}

		// Token: 0x060026CD RID: 9933 RVA: 0x0007BFD8 File Offset: 0x0007A3D8
		public void Apply(float fraction)
		{
			float time = (fraction >= 0.5f) ? (2f - fraction * 2f) : (fraction * 2f);
			Shader.SetGlobalVector(this.sideSunColorId, this.sideSunColor.Evaluate(time) * this.sunIntensity);
			Shader.SetGlobalVector(this.upSunColorId, this.upSunColor.Evaluate(time) * this.sunIntensity);
			Shader.SetGlobalVector(this.minAmbientColorId, this._minAmbientColor.Evaluate(time) * this.ambientIntensity);
			Shader.SetGlobalVector(this.maxAmbientColorId, this._maxAmbientColor.Evaluate(time) * this.ambientIntensity);
			Shader.SetGlobalVector(this.fogColorId, this._fogColor.Evaluate(time));
			Vector4 vector = this._mirrorColor.Evaluate(time);
			Shader.SetGlobalVector(this.mirrorColorId, vector);
			Vector4 vector2 = this.cloudColor.Evaluate(time);
			Shader.SetGlobalVector(this.cloudColorId, vector2);
			Shader.SetGlobalVector(this.lutLerpId, this._lutLerp.Evaluate(time) - new Vector4(0.5f, 0.5f, 0.5f, 0f));
			Shader.SetGlobalVector(this.sunDirId, Quaternion.Euler(0f, 0f, 360f * fraction) * Vector3.down);
			this.saturationModifier.add = this.saturation.Evaluate(time);
			this.mirrorColor2 = Vector4.Lerp(vector, vector2, Shader.GetGlobalVector(this.cloudCoverageId).x);
			Shader.SetGlobalVector(this.mirrorColor2Id, this.mirrorColor2);
		}

		// Token: 0x060026CE RID: 9934 RVA: 0x0007C1E7 File Offset: 0x0007A5E7
		private void Update()
		{
			this.Apply(base.manager.day.dayFraction);
		}

		// Token: 0x040018C0 RID: 6336
		[SerializeField]
		private Gradient sideSunColor;

		// Token: 0x040018C1 RID: 6337
		private ShaderId sideSunColorId = "_SideSunColor";

		// Token: 0x040018C2 RID: 6338
		[SerializeField]
		private Gradient upSunColor;

		// Token: 0x040018C3 RID: 6339
		private ShaderId upSunColorId = "_UpSunColor";

		// Token: 0x040018C4 RID: 6340
		[SerializeField]
		private Gradient _minAmbientColor;

		// Token: 0x040018C5 RID: 6341
		private ShaderId minAmbientColorId = "_MinAmbientColor";

		// Token: 0x040018C6 RID: 6342
		[SerializeField]
		private Gradient _maxAmbientColor;

		// Token: 0x040018C7 RID: 6343
		private ShaderId maxAmbientColorId = "_MaxAmbientColor";

		// Token: 0x040018C8 RID: 6344
		[SerializeField]
		private Gradient _mirrorColor;

		// Token: 0x040018C9 RID: 6345
		private ShaderId mirrorColorId = "_MirrorColor";

		// Token: 0x040018CA RID: 6346
		private ShaderId mirrorColor2Id = "_MirrorColor2";

		// Token: 0x040018CB RID: 6347
		[SerializeField]
		private Gradient cloudColor;

		// Token: 0x040018CC RID: 6348
		private ShaderId cloudColorId = "_CloudColor";

		// Token: 0x040018CD RID: 6349
		[SerializeField]
		private Gradient _fogColor;

		// Token: 0x040018CE RID: 6350
		private ShaderId fogColorId = "_FogColor";

		// Token: 0x040018CF RID: 6351
		[SerializeField]
		private Gradient _lutLerp;

		// Token: 0x040018D0 RID: 6352
		private ShaderId lutLerpId = "_LutLerp";

		// Token: 0x040018D1 RID: 6353
		[SerializeField]
		private AnimationCurve saturation;

		// Token: 0x040018D2 RID: 6354
		private ShaderId sunDirId = "_SunDir";

		// Token: 0x040018D3 RID: 6355
		private ShaderId cloudCoverageId = "_CloudCoverage";

		// Token: 0x040018D4 RID: 6356
		private ShaderId lutOffset2Id = "_LutOffset2";

		// Token: 0x040018D5 RID: 6357
		[SerializeField]
		private float sunIntensity = 10f;

		// Token: 0x040018D6 RID: 6358
		[SerializeField]
		private float ambientIntensity = 1f;

		// Token: 0x040018D7 RID: 6359
		private ShaderConstants.Modifier saturationModifier = new ShaderConstants.Modifier("_Saturation", 0f, 1f, 1f, 0f);

		// Token: 0x040018D8 RID: 6360
		private Vector4 mirrorColor2;
	}
}
