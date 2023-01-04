using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x02000757 RID: 1879
	public class Fog : IslandComponent, IIslandAwake, IIslandEnter
	{
		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x0600310C RID: 12556 RVA: 0x000CA379 File Offset: 0x000C8779
		// (set) Token: 0x0600310D RID: 12557 RVA: 0x000CA381 File Offset: 0x000C8781
		public float minRad
		{
			get
			{
				return this._minRad;
			}
			set
			{
				this._minRad = value;
			}
		}

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x0600310E RID: 12558 RVA: 0x000CA38A File Offset: 0x000C878A
		// (set) Token: 0x0600310F RID: 12559 RVA: 0x000CA397 File Offset: 0x000C8797
		public float maxRad
		{
			get
			{
				return this.capsuleCollider.radius;
			}
			set
			{
				this.capsuleCollider.radius = value;
			}
		}

		// Token: 0x06003110 RID: 12560 RVA: 0x000CA3A8 File Offset: 0x000C87A8
		public void UpdateSize(Vector3 multiwaveSize)
		{
			float magnitude = (multiwaveSize.GetXZ() / 2f).magnitude;
			this.maxRad = magnitude * this.maxRadMultiplier + this.maxRadAdd;
			this.minRad = magnitude * this.minRadMultiplier + this.minRadAdd;
			float num = Mathf.Lerp(this.minRad, this.maxRad, 0f);
			this.fogParticles.transform.localScale = new Vector3(num, num, 1f);
		}

		// Token: 0x06003111 RID: 12561 RVA: 0x000CA42B File Offset: 0x000C882B
		private void OnDrawGizmos()
		{
			if (this.capsuleCollider)
			{
				Gizmos.color = Color.white;
				Gizmos.matrix = Matrix4x4.identity;
				ExtraGizmos.DrawCircle(Vector3.zero, this.maxRad, 32);
			}
		}

		// Token: 0x06003112 RID: 12562 RVA: 0x000CA463 File Offset: 0x000C8863
		public void OnIslandAwake(Island island)
		{
			this.UpdateSize(island.size);
		}

		// Token: 0x06003113 RID: 12563 RVA: 0x000CA474 File Offset: 0x000C8874
		public IEnumerator<GenInfo> OnIslandEnter(Island island)
		{
			Shader.SetGlobalFloat("_FogMinRad", this.minRad);
			Shader.SetGlobalFloat("_FogMaxRad", this.maxRad);
			yield return new GenInfo("Fog", GenInfo.Mode.interruptable);
			yield break;
		}

		// Token: 0x040020CA RID: 8394
		public CapsuleCollider capsuleCollider;

		// Token: 0x040020CB RID: 8395
		public ParticleSystem fogParticles;

		// Token: 0x040020CC RID: 8396
		private float _minRad;

		// Token: 0x040020CD RID: 8397
		public float minRadMultiplier = 0.5f;

		// Token: 0x040020CE RID: 8398
		public float maxRadMultiplier = 1.2f;

		// Token: 0x040020CF RID: 8399
		public float minRadAdd = 1f;

		// Token: 0x040020D0 RID: 8400
		public float maxRadAdd = 2f;
	}
}
