using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020007CE RID: 1998
	public class ShaderConstants : Singleton<ShaderConstants>, IGameSetup
	{
		// Token: 0x060033C5 RID: 13253 RVA: 0x000DF30C File Offset: 0x000DD70C
		private void SetColors()
		{
			Shader.SetGlobalVector("_BloodColor", this.bloodColor);
			Shader.SetGlobalVector("_SnowColor", this.snowColor);
			Shader.SetGlobalVector("_FoamColor", this.foamColor);
			Shader.SetGlobalVector("_LongshipColor", this.longshipColor);
			Shader.SetGlobalFloat("_ColorOffset", this.colorOffset);
			Shader.SetGlobalFloat("_WaterLevel", -0.105f);
		}

		// Token: 0x060033C6 RID: 13254 RVA: 0x000DF38C File Offset: 0x000DD78C
		private void Update()
		{
			Shader.SetGlobalFloat("_UnscaledTime", Time.unscaledTime);
		}

		// Token: 0x060033C7 RID: 13255 RVA: 0x000DF3A0 File Offset: 0x000DD7A0
		private void LateUpdate()
		{
			this.dict.Clear();
			this.modifiers.Sort((ShaderConstants.Modifier a, ShaderConstants.Modifier b) => a.sortOrder.CompareTo(b.sortOrder));
			foreach (ShaderConstants.Modifier modifier in this.modifiers)
			{
				int key = modifier.shaderId;
				float num;
				if (!this.dict.TryGetValue(key, out num))
				{
					num = 0f;
					this.dict.Add(key, num);
				}
				float num2 = num * modifier.multiply + modifier.add;
				num2 = Mathf.Lerp(num, num2, modifier.alpha);
				this.dict[key] = num2;
			}
			foreach (KeyValuePair<int, float> keyValuePair in this.dict)
			{
				Shader.SetGlobalFloat(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x060033C8 RID: 13256 RVA: 0x000DF4E4 File Offset: 0x000DD8E4
		private void OnValidate()
		{
			this.SetColors();
		}

		// Token: 0x060033C9 RID: 13257 RVA: 0x000DF4EC File Offset: 0x000DD8EC
		private void OnDestroy()
		{
			Shader.DisableKeyword("_GAME_ON");
		}

		// Token: 0x060033CA RID: 13258 RVA: 0x000DF4F8 File Offset: 0x000DD8F8
		protected override void Awake()
		{
			base.Awake();
			this.SetColors();
		}

		// Token: 0x060033CB RID: 13259 RVA: 0x000DF506 File Offset: 0x000DD906
		public void OnGameAwake()
		{
			Shader.EnableKeyword("_GAME_ON");
			this.SetColors();
		}

		// Token: 0x060033CC RID: 13260 RVA: 0x000DF518 File Offset: 0x000DD918
		private void OnDrawGizmos()
		{
			if (!Application.isPlaying)
			{
				Shader.DisableKeyword("_GAME_ON");
			}
		}

		// Token: 0x0400233C RID: 9020
		public Color bloodColor = Color.red;

		// Token: 0x0400233D RID: 9021
		public Color snowColor = new Color(0.7f, 0.7f, 0.7f, 1f);

		// Token: 0x0400233E RID: 9022
		public Color foamColor = new Color(0.7f, 0.7f, 0.7f, 1f);

		// Token: 0x0400233F RID: 9023
		public Color longshipColor = new Color(0.3f, 0.3f, 0.3f, 1f);

		// Token: 0x04002340 RID: 9024
		public float colorOffset;

		// Token: 0x04002341 RID: 9025
		[NonSerialized]
		public List<ShaderConstants.Modifier> modifiers = new List<ShaderConstants.Modifier>();

		// Token: 0x04002342 RID: 9026
		private Dictionary<int, float> dict = new Dictionary<int, float>();

		// Token: 0x020007CF RID: 1999
		[Serializable]
		public class Modifier
		{
			// Token: 0x060033CE RID: 13262 RVA: 0x000DF544 File Offset: 0x000DD944
			public Modifier(string name, float add = 0f, float multiply = 1f, float alpha = 1f, float sortOrder = 0f)
			{
				this.shaderId = name;
				this.add = add;
				this.multiply = multiply;
				this.alpha = alpha;
				this.sortOrder = sortOrder;
			}

			// Token: 0x04002344 RID: 9028
			[SerializeField]
			public ShaderId shaderId;

			// Token: 0x04002345 RID: 9029
			[SerializeField]
			public float add;

			// Token: 0x04002346 RID: 9030
			[SerializeField]
			public float multiply = 1f;

			// Token: 0x04002347 RID: 9031
			[SerializeField]
			public float alpha = 1f;

			// Token: 0x04002348 RID: 9032
			[SerializeField]
			public float sortOrder;
		}
	}
}
