using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020007D0 RID: 2000
	public class ShaderConstantsOverride : MonoBehaviour
	{
		// Token: 0x060033D0 RID: 13264 RVA: 0x000DF5AC File Offset: 0x000DD9AC
		public void Update()
		{
			foreach (ShaderConstants.Modifier modifier in this.modifiers)
			{
				modifier.alpha = this.alpha;
			}
		}

		// Token: 0x060033D1 RID: 13265 RVA: 0x000DF5E4 File Offset: 0x000DD9E4
		public void OnEnable()
		{
			Singleton<ShaderConstants>.instance.modifiers.AddRange(this.modifiers);
		}

		// Token: 0x060033D2 RID: 13266 RVA: 0x000DF5FC File Offset: 0x000DD9FC
		public void OnDisable()
		{
			if (Singleton<ShaderConstants>.instance)
			{
				foreach (ShaderConstants.Modifier item in this.modifiers)
				{
					Singleton<ShaderConstants>.instance.modifiers.Remove(item);
				}
			}
		}

		// Token: 0x060033D3 RID: 13267 RVA: 0x000DF648 File Offset: 0x000DDA48
		public void OnDestroyed()
		{
			if (Singleton<ShaderConstants>.instance)
			{
				foreach (ShaderConstants.Modifier item in this.modifiers)
				{
					Singleton<ShaderConstants>.instance.modifiers.Remove(item);
				}
			}
		}

		// Token: 0x04002349 RID: 9033
		public float alpha = 1f;

		// Token: 0x0400234A RID: 9034
		[SerializeField]
		private ShaderConstants.Modifier[] modifiers;
	}
}
