using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020007B9 RID: 1977
	public interface IProjectileSolver
	{
		// Token: 0x06003332 RID: 13106
		bool SolveProjectile(Vector2 targetDisplacement, float gravity, out Vector2 launchVelocity, out float timeToTarget);
	}
}
