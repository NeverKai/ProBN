using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x02000875 RID: 2165
	public class WindParticle : MonoBehaviour
	{
		// Token: 0x060038C0 RID: 14528 RVA: 0x000F5700 File Offset: 0x000F3B00
		private void Start()
		{
			this.particleSystems = base.GetComponentsInChildren<ParticleSystem>(true);
			int num = 0;
			foreach (ParticleSystem particleSystem in this.particleSystems)
			{
				num = Mathf.Max(num, particleSystem.main.maxParticles);
			}
			this.particles = new ParticleSystem.Particle[num];
			this.wind = base.GetComponentInParent<Island>().wind;
			base.enabled = this.wind;
		}

		// Token: 0x060038C1 RID: 14529 RVA: 0x000F5780 File Offset: 0x000F3B80
		private void LateUpdate()
		{
			for (int i = 0; i < this.particleSystems.Length; i++)
			{
				ParticleSystem particleSystem = this.particleSystems[i];
				int particleCount = particleSystem.particleCount;
				if (particleCount != 0)
				{
					particleSystem.GetParticles(this.particles);
					if (this.mode == WindParticle.SampleMode.Linear)
					{
						for (int j = 0; j < particleCount; j++)
						{
							ParticleSystem.Particle[] array = this.particles;
							int num = j;
							array[num].position = array[num].position + this.wind.GetWindLinear(this.particles[j].position) * Time.deltaTime * this.multiplier;
						}
					}
					else
					{
						for (int k = 0; k < particleCount; k++)
						{
							ParticleSystem.Particle[] array2 = this.particles;
							int num2 = k;
							array2[num2].position = array2[num2].position + this.wind.GetWindPoint(this.particles[k].position) * Time.deltaTime * this.multiplier;
						}
					}
					particleSystem.SetParticles(this.particles, particleCount);
				}
			}
		}

		// Token: 0x0400269E RID: 9886
		private Wind wind;

		// Token: 0x0400269F RID: 9887
		private ParticleSystem[] particleSystems;

		// Token: 0x040026A0 RID: 9888
		private ParticleSystem.Particle[] particles;

		// Token: 0x040026A1 RID: 9889
		public WindParticle.SampleMode mode;

		// Token: 0x040026A2 RID: 9890
		public float multiplier = 1f;

		// Token: 0x02000876 RID: 2166
		public enum SampleMode
		{
			// Token: 0x040026A4 RID: 9892
			Linear,
			// Token: 0x040026A5 RID: 9893
			Point
		}
	}
}
