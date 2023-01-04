using System;
using UnityEngine;
using UnityEngine.Events;

namespace Voxels.TowerDefense
{
	// Token: 0x020007CA RID: 1994
	public abstract class ReusableEffect : MonoBehaviour
	{
		// Token: 0x060033B8 RID: 13240 RVA: 0x000DF0B5 File Offset: 0x000DD4B5
		public void PaintSoot(float radius)
		{
			Singleton<IslandGameplayManager>.instance.island.painter.Paint(new Bounds(base.transform.position, Vector3.one * radius), Painter.sootColor);
		}

		// Token: 0x060033B9 RID: 13241 RVA: 0x000DF0EB File Offset: 0x000DD4EB
		public ReusableEffect GetOrCreate(Transform parent = null)
		{
			if (!this.instance)
			{
				this.instance = UnityEngine.Object.Instantiate<ReusableEffect>(this, parent).Setup();
			}
			return this.instance;
		}

		// Token: 0x060033BA RID: 13242 RVA: 0x000DF118 File Offset: 0x000DD518
		private ReusableEffect Setup()
		{
			this.navPosEffects = base.GetComponentsInChildren<ReusableEffect.INavPosEffect>(true);
			this.effectComponents = base.GetComponentsInChildren<ReusableEffect.IEffectComponent>(true);
			ParticleSystem[] componentsInChildren = base.GetComponentsInChildren<ParticleSystem>();
			foreach (ParticleSystem particleSystem in componentsInChildren)
			{
				ParticleSystem.MainModule main = particleSystem.main;
				main.loop = false;
				main.playOnAwake = false;
			}
			return this;
		}

		// Token: 0x060033BB RID: 13243 RVA: 0x000DF179 File Offset: 0x000DD579
		public ReusableEffect PlayAt(Vector3 pos)
		{
			return this.PlayAt(pos, Vector3.forward);
		}

		// Token: 0x060033BC RID: 13244 RVA: 0x000DF187 File Offset: 0x000DD587
		public ReusableEffect PlayAt(Vector3 pos, Vector3 forward)
		{
			return this.PlayAt(pos, Quaternion.LookRotation(forward, UnityEngine.Random.insideUnitSphere), Vector3.one);
		}

		// Token: 0x060033BD RID: 13245 RVA: 0x000DF1A0 File Offset: 0x000DD5A0
		public ReusableEffect PlayAt(Vector3 pos, Quaternion rotation, Vector3 scale)
		{
			return this.GetOrCreate(null).PlayAtInternal(pos, rotation, scale);
		}

		// Token: 0x060033BE RID: 13246 RVA: 0x000DF1B1 File Offset: 0x000DD5B1
		public ReusableEffect PlayAt(NavPos navPos, Quaternion rotation, Vector3 scale)
		{
			return this.GetOrCreate(null).PlayAtInternal(navPos, rotation, scale);
		}

		// Token: 0x060033BF RID: 13247 RVA: 0x000DF1C4 File Offset: 0x000DD5C4
		private ReusableEffect PlayAtInternal(Vector3 pos, Quaternion rotation, Vector3 scale)
		{
			base.transform.position = pos;
			base.transform.rotation = rotation;
			base.transform.localScale = scale;
			this.onPlay.Invoke();
			foreach (ReusableEffect.IEffectComponent effectComponent in this.effectComponents)
			{
				effectComponent.PlayAt(pos, rotation, scale);
			}
			return this;
		}

		// Token: 0x060033C0 RID: 13248 RVA: 0x000DF22C File Offset: 0x000DD62C
		private ReusableEffect PlayAtInternal(NavPos navPos, Quaternion rotation, Vector3 scale)
		{
			this.PlayAtInternal(navPos.pos, rotation, scale);
			foreach (ReusableEffect.INavPosEffect navPosEffect in this.navPosEffects)
			{
				navPosEffect.PlayAt(navPos);
			}
			return this;
		}

		// Token: 0x04002338 RID: 9016
		private ReusableEffect.INavPosEffect[] navPosEffects;

		// Token: 0x04002339 RID: 9017
		private ReusableEffect.IEffectComponent[] effectComponents;

		// Token: 0x0400233A RID: 9018
		private ReusableEffect instance;

		// Token: 0x0400233B RID: 9019
		[SerializeField]
		private UnityEvent onPlay;

		// Token: 0x020007CB RID: 1995
		public interface INavPosEffect
		{
			// Token: 0x060033C1 RID: 13249
			void PlayAt(NavPos navPos);
		}

		// Token: 0x020007CC RID: 1996
		public interface IEffectComponent
		{
			// Token: 0x060033C2 RID: 13250
			void PlayAt(Vector3 pos, Quaternion rotation, Vector3 scale);
		}
	}
}
