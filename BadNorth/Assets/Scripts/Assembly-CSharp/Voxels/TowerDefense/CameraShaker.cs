using System;
using ReflexCLI.Attributes;
using RTM.OnScreenDebug;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020006BB RID: 1723
	[ConsoleCommandClassCustomizer("Camera")]
	public class CameraShaker : Singleton<CameraShaker>, IslandGameplayManager.ISetupIsland
	{
		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x06002C8B RID: 11403 RVA: 0x000A5FB3 File Offset: 0x000A43B3
		protected float deltaTime
		{
			get
			{
				return Mathf.Lerp(Time.unscaledDeltaTime, Time.deltaTime, this.timeScaleEffect);
			}
		}

		// Token: 0x06002C8C RID: 11404 RVA: 0x000A5FCA File Offset: 0x000A43CA
		void IslandGameplayManager.ISetupIsland.OnSetup(Island island)
		{
			this.time = 0f;
			this.shakeIntensity = 0f;
		}

		// Token: 0x06002C8D RID: 11405 RVA: 0x000A5FE4 File Offset: 0x000A43E4
		public void ShakeOnce(float intensity)
		{
			intensity /= 5f;
			float current = this.shakeIntensity;
			this.shakeIntensity = ExtraMath.ExpLerpTowards(current, 1f, 1E-05f, intensity);
		}

		// Token: 0x06002C8E RID: 11406 RVA: 0x000A6018 File Offset: 0x000A4418
		private void LateUpdate()
		{
			this.limitedShakeIntensity = ((this.limitedShakeIntensity >= this.shakeIntensity) ? this.shakeIntensity : Mathf.MoveTowards(this.limitedShakeIntensity, this.shakeIntensity, this.acceleration * this.deltaTime));
			float num = (CameraShaker.FixedShake <= 0f) ? this.limitedShakeIntensity : Mathf.Clamp(CameraShaker.FixedShake, 0f, 1f);
			this.time += this.deltaTime * Mathf.Lerp(this.timeScaleMin, this.timeScaleMax, num);
			Vector3 localPosition = num * new Vector3(this.ComputeNoise(this.time + this.seedOffsets.x, this.time + this.seedOffsets.x) * this.positionMultiplier.x, this.ComputeNoise(this.time + this.seedOffsets.y, this.time + this.seedOffsets.y) * this.positionMultiplier.y);
			this.shakeTransform.localPosition = localPosition;
			this.shakeIntensity = Mathf.MoveTowards(this.shakeIntensity, 0f, this.deltaTime / this.decayTime);
		}

		// Token: 0x06002C8F RID: 11407 RVA: 0x000A6161 File Offset: 0x000A4561
		private float ComputeNoise(float x, float y)
		{
			return Mathf.PerlinNoise(x, y) * 2f - 1f;
		}

		// Token: 0x06002C90 RID: 11408 RVA: 0x000A6176 File Offset: 0x000A4576
		public void ApplyTo(Transform inTransform)
		{
			inTransform.localPosition = this.shakeTransform.localPosition;
			inTransform.localRotation = this.shakeTransform.localRotation;
		}

		// Token: 0x06002C91 RID: 11409 RVA: 0x000A619C File Offset: 0x000A459C
		private void Update()
		{
			if (CameraShaker.NumPadShakeEnabled)
			{
				for (KeyCode keyCode = KeyCode.Keypad0; keyCode <= KeyCode.Keypad9; keyCode++)
				{
					if (Input.GetKeyDown(keyCode))
					{
						float intensity = (keyCode != KeyCode.Keypad0) ? ((float)(keyCode - KeyCode.Keypad0) * 0.1f) : 1f;
						this.ShakeOnce(intensity);
					}
				}
			}
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x06002C92 RID: 11410 RVA: 0x000A6203 File Offset: 0x000A4603
		// (set) Token: 0x06002C93 RID: 11411 RVA: 0x000A620F File Offset: 0x000A460F
		[ConsoleCommand("")]
		private static bool ShakeEnabled
		{
			get
			{
				return Singleton<CameraShaker>.instance.enabled;
			}
			set
			{
				Singleton<CameraShaker>.instance.enabled = value;
			}
		}

		// Token: 0x04001D34 RID: 7476
		private DebugChannelGroup dbgGroup = new DebugChannelGroup("CameraShaker", EVerbosity.Quiet, 100);

		// Token: 0x04001D35 RID: 7477
		[SerializeField]
		[Range(0f, 1f)]
		private float timeScaleEffect = 0.6f;

		// Token: 0x04001D36 RID: 7478
		[Header("Object References")]
		[SerializeField]
		private Transform shakeTransform;

		// Token: 0x04001D37 RID: 7479
		[Header("Motion")]
		[SerializeField]
		private Vector2 positionMultiplier = new Vector2(0.1f, 0.1f);

		// Token: 0x04001D38 RID: 7480
		[SerializeField]
		private float timeScaleMin = 1f;

		// Token: 0x04001D39 RID: 7481
		[SerializeField]
		private float timeScaleMax = 4f;

		// Token: 0x04001D3A RID: 7482
		[SerializeField]
		private float acceleration = 10f;

		// Token: 0x04001D3B RID: 7483
		[SerializeField]
		private float decayTime = 1f;

		// Token: 0x04001D3C RID: 7484
		[Header("Noise")]
		[SerializeField]
		private Vector2 seedOffsets = new Vector2(0f, 500f);

		// Token: 0x04001D3D RID: 7485
		private float shakeIntensity;

		// Token: 0x04001D3E RID: 7486
		private float limitedShakeIntensity;

		// Token: 0x04001D3F RID: 7487
		private float time;

		// Token: 0x04001D40 RID: 7488
		[ConsoleCommand("")]
		private static float FixedShake;

		// Token: 0x04001D41 RID: 7489
		[ConsoleCommand("")]
		private static bool NumPadShakeEnabled;
	}
}
