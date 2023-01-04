using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020007BD RID: 1981
	public class SimpleProjectile
	{
		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x06003355 RID: 13141 RVA: 0x000DC557 File Offset: 0x000DA957
		// (set) Token: 0x06003356 RID: 13142 RVA: 0x000DC55F File Offset: 0x000DA95F
		public Vector3 launchVelocity { get; private set; }

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x06003357 RID: 13143 RVA: 0x000DC568 File Offset: 0x000DA968
		public float launchTime
		{
			get
			{
				return this.startTime;
			}
		}

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x06003358 RID: 13144 RVA: 0x000DC570 File Offset: 0x000DA970
		public float flightTime
		{
			get
			{
				return this.duration;
			}
		}

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x06003359 RID: 13145 RVA: 0x000DC578 File Offset: 0x000DA978
		public float currentTime
		{
			get
			{
				return Time.time - this.startTime;
			}
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x0600335A RID: 13146 RVA: 0x000DC586 File Offset: 0x000DA986
		public float timeRemaining
		{
			get
			{
				return Mathf.Max(this.duration - this.currentTime, 0f);
			}
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x0600335B RID: 13147 RVA: 0x000DC59F File Offset: 0x000DA99F
		public Vector3 position
		{
			get
			{
				return this.currentPos;
			}
		}

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x0600335C RID: 13148 RVA: 0x000DC5A7 File Offset: 0x000DA9A7
		public bool hasArrived
		{
			get
			{
				return this.currentTime >= this.duration;
			}
		}

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x0600335D RID: 13149 RVA: 0x000DC5BA File Offset: 0x000DA9BA
		public Vector3 velocity
		{
			get
			{
				return this.ComputeVeolcity();
			}
		}

		// Token: 0x0600335E RID: 13150 RVA: 0x000DC5C4 File Offset: 0x000DA9C4
		public void Launch(Vector3 startPos, Vector3 endPos, float gravity, IProjectileSolver solver)
		{
			Vector3 vector = endPos - startPos;
			Vector2 targetDisplacement = ProjectileMath.Get2DVec(vector);
			Vector2 vector2;
			float num;
			if (solver.SolveProjectile(targetDisplacement, gravity, out vector2, out num))
			{
				this.Launch(startPos, ProjectileMath.Get3DVec(vector2, vector), gravity, num);
			}
		}

		// Token: 0x0600335F RID: 13151 RVA: 0x000DC604 File Offset: 0x000DAA04
		public void Launch(Vector3 startPos, Vector3 launchVelocity, float gravity, float duration)
		{
			this.startPos = startPos;
			this.currentPos = startPos;
			this.gravity = gravity;
			this.duration = duration;
			this.startTime = Time.time;
			this.launchVelocity = launchVelocity;
		}

		// Token: 0x06003360 RID: 13152 RVA: 0x000DC644 File Offset: 0x000DAA44
		public void Update()
		{
			float time = Time.time - this.startTime;
			this.currentPos = this.startPos + ProjectileMath.GetDisplacementAtTime(this.launchVelocity, this.gravity, time);
		}

		// Token: 0x06003361 RID: 13153 RVA: 0x000DC684 File Offset: 0x000DAA84
		public Vector3 ComputeVeolcity()
		{
			Vector3 launchVelocity = this.launchVelocity;
			float currentTime = this.currentTime;
			launchVelocity.y += this.gravity * currentTime * currentTime;
			return launchVelocity;
		}

		// Token: 0x040022EB RID: 8939
		private Vector3 startPos;

		// Token: 0x040022EC RID: 8940
		private Vector3 currentPos;

		// Token: 0x040022EE RID: 8942
		private float duration;

		// Token: 0x040022EF RID: 8943
		private float startTime;

		// Token: 0x040022F0 RID: 8944
		private float gravity;
	}
}
