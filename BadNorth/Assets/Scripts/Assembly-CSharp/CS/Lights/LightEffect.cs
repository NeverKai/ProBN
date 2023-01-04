using System;
using UnityEngine;

namespace CS.Lights
{
	// Token: 0x0200038D RID: 909
	public abstract class LightEffect : MonoBehaviour
	{
		// Token: 0x060014C3 RID: 5315 RVA: 0x0002AF97 File Offset: 0x00029397
		private void OnEnable()
		{
			LightEffectManager.RegisterLightEffecter(new Action<SetLightingEvent>(this.EffectLight));
		}

		// Token: 0x060014C4 RID: 5316 RVA: 0x0002AFAA File Offset: 0x000293AA
		private void OnDisable()
		{
			LightEffectManager.DeregisterLightEffecter(new Action<SetLightingEvent>(this.EffectLight));
		}

		// Token: 0x060014C5 RID: 5317 RVA: 0x0002AFBD File Offset: 0x000293BD
		private void EffectLight(SetLightingEvent effect)
		{
			if (this._active && !effect.Used)
			{
				this.EffectLogic(effect);
			}
		}

		// Token: 0x060014C6 RID: 5318
		protected abstract void EffectLogic(SetLightingEvent effect);

		// Token: 0x04000CE1 RID: 3297
		[SerializeField]
		protected int _layer;

		// Token: 0x04000CE2 RID: 3298
		[SerializeField]
		protected bool _active = true;
	}
}
