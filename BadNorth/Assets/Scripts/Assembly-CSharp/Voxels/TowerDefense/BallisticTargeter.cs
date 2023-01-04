using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x02000897 RID: 2199
	public class BallisticTargeter : MonoBehaviour, ITargeter
	{
		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x06003989 RID: 14729 RVA: 0x000FBF6E File Offset: 0x000FA36E
		private float angleRadians
		{
			get
			{
				return this.referenceAngle * 0.017453292f;
			}
		}

		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x0600398A RID: 14730 RVA: 0x000FBF7C File Offset: 0x000FA37C
		public float projectileGravity
		{
			get
			{
				return Physics.gravity.y * this.projectileGravityScale;
			}
		}

		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x0600398B RID: 14731 RVA: 0x000FBF9D File Offset: 0x000FA39D
		public IProjectileSolver solver
		{
			get
			{
				return this._solver;
			}
		}

		// Token: 0x0600398C RID: 14732 RVA: 0x000FBFA5 File Offset: 0x000FA3A5
		private void Awake()
		{
			this._solver = base.GetComponent<IProjectileSolver>();
		}

		// Token: 0x0600398D RID: 14733 RVA: 0x000FBFB4 File Offset: 0x000FA3B4
		private bool Raycast(Vector3 start, Vector3 end)
		{
			RaycastHit raycastHit;
			return Physics.Raycast(new Ray(start, end - start), out raycastHit, Vector3.Distance(start, end), this.layers);
		}

		// Token: 0x0600398E RID: 14734 RVA: 0x000FBFEC File Offset: 0x000FA3EC
		public bool IsTargetable(NavSpot origin, NavSpot target, ref int currErrorId)
		{
			if (origin == target)
			{
				return false;
			}
			Vector3 vector = target.transform.position;
			Vector3 position = origin.transform.position;
			bool result;
			using (new ScopedProfiler("IsTargetable", null))
			{
				Vector3 vector2 = vector - position;
				if (ProjectileMath.IsUnderParabola(vector2, ProjectileMath.GetLaunchVelocity(this.referenceLaunchSpeed, this.angleRadians), Physics.gravity.y))
				{
					float projectileGravity = this.projectileGravity;
					Vector2 targetDisplacement = ProjectileMath.Get2DVec(vector2);
					Vector2 vector3;
					float b;
					if (this._solver.SolveProjectile(targetDisplacement, projectileGravity, out vector3, out b))
					{
						Vector3 launchVelocity = ProjectileMath.Get3DVec(vector3, vector2);
						float peakTime = ProjectileMath.GetPeakTime(launchVelocity.y, projectileGravity);
						float time = Mathf.LerpUnclamped(peakTime, b, 0.5f);
						Vector3 vector4 = position + ProjectileMath.GetDisplacementAtTime(launchVelocity, projectileGravity, peakTime);
						Vector3 vector5 = position + ProjectileMath.GetDisplacementAtTime(launchVelocity, projectileGravity, time);
						vector += Vector3.up * this.testVerticalOffset;
						bool flag = this.Raycast(position, vector4) || this.Raycast(vector4, vector5) || this.Raycast(vector5, vector);
						return !flag;
					}
				}
				result = false;
			}
			return result;
		}

		// Token: 0x0600398F RID: 14735 RVA: 0x000FC158 File Offset: 0x000FA558
		string ITargeter.GetErrorTerm(int errorId)
		{
			return "UPGRADES/COMMON/TOOLTIPS/NO_TARGETS";
		}

		// Token: 0x040027A7 RID: 10151
		[Header("Parabola Test")]
		[SerializeField]
		private float referenceLaunchSpeed = 5.2f;

		// Token: 0x040027A8 RID: 10152
		[SerializeField]
		private float referenceAngle = 65f;

		// Token: 0x040027A9 RID: 10153
		[Header("Projectile Motion")]
		[SerializeField]
		private float projectileGravityScale = 0.75f;

		// Token: 0x040027AA RID: 10154
		[Header("Projectile Collision Test")]
		[SerializeField]
		private LayerMask layers = 0;

		// Token: 0x040027AB RID: 10155
		[SerializeField]
		private float testVerticalOffset = 0.15f;

		// Token: 0x040027AC RID: 10156
		private IProjectileSolver _solver;
	}
}
