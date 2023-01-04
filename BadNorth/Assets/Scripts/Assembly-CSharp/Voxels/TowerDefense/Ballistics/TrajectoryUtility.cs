using System;
using System.Linq;
using InspectorExpressions;
using UnityEngine;

namespace Voxels.TowerDefense.Ballistics
{
	// Token: 0x020007C4 RID: 1988
	public class TrajectoryUtility : MonoBehaviour
	{
		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x0600338F RID: 13199 RVA: 0x000DD6E4 File Offset: 0x000DBAE4
		public Vector2 size
		{
			get
			{
				return new Vector3((float)this.MaxZ, (float)(this.MaxY - this.MinY));
			}
		}

		// Token: 0x06003390 RID: 13200 RVA: 0x000DD708 File Offset: 0x000DBB08
		private int CoordinateToIndex(Vector2 coordinate)
		{
			int num = (int)coordinate.x;
			int num2 = (int)coordinate.y;
			return num + num2 * this.MaxZ;
		}

		// Token: 0x06003391 RID: 13201 RVA: 0x000DD734 File Offset: 0x000DBB34
		private Vector4 SamplePoint(Vector2 coordinate, TrajectoryUtility.Attribute attribute)
		{
			if (coordinate.x >= this.size.x || coordinate.x < 0f)
			{
				return Vector4.zero;
			}
			if (coordinate.y >= this.size.y || coordinate.y < 0f)
			{
				return Vector4.zero;
			}
			TrajectoryUtility.Trajectory trajectory = this.trajectories[(int)coordinate.x + (int)coordinate.y * (int)this.size.x];
			Vector2 vector;
			switch (attribute)
			{
			case TrajectoryUtility.Attribute.StartVelocity:
				vector = trajectory.startVelocity;
				break;
			case TrajectoryUtility.Attribute.HighPoint:
				vector = trajectory.attributes.highPoint;
				break;
			case TrajectoryUtility.Attribute.ImpactVelocity:
				vector = trajectory.impactVelocity;
				break;
			case TrajectoryUtility.Attribute.ImpactTime:
				vector = new Vector2(trajectory.impactTime, 0f);
				break;
			case TrajectoryUtility.Attribute.MidPoint:
				vector = trajectory.attributes.midPoint;
				break;
			default:
				vector = Vector2.zero;
				break;
			}
			return new Vector4(vector.x, vector.y, 1f, (float)((!trajectory.valid) ? 0 : 1));
		}

		// Token: 0x06003392 RID: 13202 RVA: 0x000DD878 File Offset: 0x000DBC78
		private Attributes SamplePoint(Vector2 coordinate)
		{
			return this.trajectories[(int)coordinate.x + (int)coordinate.y * (int)this.size.x].attributes;
		}

		// Token: 0x06003393 RID: 13203 RVA: 0x000DD8B4 File Offset: 0x000DBCB4
		private Vector4 SampleLinearGrid(Vector2 diff, TrajectoryUtility.Attribute attribute)
		{
			Vector2 vector = ExtraMath.Floor(diff);
			Vector2 b = diff - vector;
			Vector2 vector2 = Vector2.one - b;
			Vector4 vector3 = Vector4.zero;
			vector3 += this.SamplePoint(vector + new Vector2(0f, 0f), attribute) * vector2.x * vector2.y;
			vector3 += this.SamplePoint(vector + new Vector2(1f, 0f), attribute) * b.x * vector2.y;
			vector3 += this.SamplePoint(vector + new Vector2(0f, 1f), attribute) * vector2.x * b.y;
			vector3 += this.SamplePoint(vector + new Vector2(1f, 1f), attribute) * b.x * b.y;
			if (vector3.w < 0.5f)
			{
				return Vector3.zero;
			}
			vector3 /= vector3.z;
			vector3.z = 0f;
			return vector3;
		}

		// Token: 0x06003394 RID: 13204 RVA: 0x000DDA14 File Offset: 0x000DBE14
		public TrajectorySample Sample(Vector3 diff)
		{
			Vector2 vector = new Vector2(0f, diff.y - (float)this.MinY);
			if (vector.y < 0f || vector.y > this.size.y - 1f)
			{
				return default(TrajectorySample);
			}
			Vector2 xz = diff.GetXZ();
			vector.x = xz.magnitude;
			if (vector.x > this.size.x - 1f)
			{
				return default(TrajectorySample);
			}
			Vector2 dirXZ = xz / vector.x;
			vector.x = Mathf.Min(vector.x, this.size.x - 1f);
			vector.y = Mathf.Clamp(vector.y, 0f, this.size.y - 1f);
			Vector2 b = ExtraMath.Floor(vector);
			Vector2 vector2 = ExtraMath.Ceil(vector);
			Vector2 b2 = vector - b;
			Vector2 vector3 = Vector2.one - b2;
			Attributes attributes = default(Attributes) + this.SamplePoint(new Vector2(b.x, b.y)) * (vector3.x * vector3.y);
			attributes += this.SamplePoint(new Vector2(vector2.x, b.y)) * (b2.x * vector3.y);
			attributes += this.SamplePoint(new Vector2(b.x, vector2.y)) * (vector3.x * b2.y);
			attributes += this.SamplePoint(new Vector2(vector2.x, vector2.y)) * (b2.x * b2.y);
			return new TrajectorySample(dirXZ, attributes);
		}

		// Token: 0x06003395 RID: 13205 RVA: 0x000DDC30 File Offset: 0x000DC030
		public Vector4 Sample(Vector3 diff, TrajectoryUtility.Attribute attribute)
		{
			Vector3 vector = diff;
			vector.y = 0f;
			Vector2 diff2 = new Vector2(vector.magnitude, diff.y - (float)this.MinY);
			Vector4 result = this.SampleLinearGrid(diff2, attribute);
			vector.Normalize();
			result.z = vector.z * result.x;
			result.x = vector.x * result.x;
			return result;
		}

		// Token: 0x06003396 RID: 13206 RVA: 0x000DDCA8 File Offset: 0x000DC0A8
		public float GetShootTime(Vector3 diff)
		{
			return this.Sample(diff).impactTime;
		}

		// Token: 0x06003397 RID: 13207 RVA: 0x000DDCC4 File Offset: 0x000DC0C4
		public Vector4 GetShootDirection(Vector3 diff, TrajectoryUtility.Attribute attribute)
		{
			Vector3 vector = diff;
			vector.y = 0f;
			Vector2 diff2 = new Vector2(vector.magnitude, diff.y - (float)this.MinY);
			Vector4 result = this.SampleLinearGrid(diff2, attribute);
			vector.Normalize();
			result.z = vector.z * result.x;
			result.x = vector.x * result.x;
			return result;
		}

		// Token: 0x06003398 RID: 13208 RVA: 0x000DDD3C File Offset: 0x000DC13C
		[ContextMenu("Setup")]
		private void Setup()
		{
			this.trajectories = new TrajectoryUtility.Trajectory[(int)this.size.x * (int)this.size.y];
			for (int i = 0; i < this.trajectories.Length; i++)
			{
				Vector2 v;
				v.x = (float)i % this.size.x;
				v.y = (float)(i / (int)this.size.x + this.MinY);
				this.trajectories[i] = new TrajectoryUtility.Trajectory(v, this.settings, this);
			}
		}

		// Token: 0x06003399 RID: 13209 RVA: 0x000DDDE0 File Offset: 0x000DC1E0
		[ContextMenu("Iterate100")]
		private void Iterate100()
		{
			for (int i = 0; i < 100; i++)
			{
				this.Iterate();
			}
		}

		// Token: 0x0600339A RID: 13210 RVA: 0x000DDE06 File Offset: 0x000DC206
		private void OnValidate()
		{
			this.Iterate();
		}

		// Token: 0x0600339B RID: 13211 RVA: 0x000DDE10 File Offset: 0x000DC210
		private void Iterate()
		{
			string text = this.costFunction.TryParseExpression();
			if (text != null)
			{
				return;
			}
			string text2 = this.validFunction.TryParseExpression();
			if (text2 != null)
			{
				return;
			}
			for (int i = 0; i < this.trajectories.Length; i++)
			{
				this.trajectories[i].Iterate(this.settings, this);
			}
			this.maxValidRadius = this.trajectories.Max((TrajectoryUtility.Trajectory x) => (!x.valid) ? 0f : x.end.x) + 0.5f;
		}

		// Token: 0x0600339C RID: 13212 RVA: 0x000DDEA8 File Offset: 0x000DC2A8
		private void OnDrawGizmosSelected()
		{
			Gizmos.matrix = base.transform.localToWorldMatrix;
			if (this.trajectories != null)
			{
				for (int i = 0; i < this.trajectories.Length; i++)
				{
					TrajectoryUtility.Trajectory trajectory = this.trajectories[i];
					Gizmos.color = Color.white.SetA((!trajectory.valid) ? 0.2f : 1f);
					Gizmos.DrawWireCube(trajectory.end, Vector3.one * 0.02f);
					if (trajectory.valid)
					{
						Color cyan = Color.cyan;
						if (trajectory.valid)
						{
							Gizmos.DrawRay(trajectory.end, trajectory.startVelocity / this.settings.maxSpeed);
							Gizmos.color = cyan;
							Gizmos.DrawRay(trajectory.end, trajectory.impactVelocity / this.settings.maxSpeed);
						}
						Gizmos.color = cyan.SetA((!this.trajectories[i].valid) ? 0.1f : 0.3f);
						Vector3 center = Vector3.zero;
						float num = float.PositiveInfinity;
						Projectile projectile = trajectory.GetProjectile(this.settings);
						for (int j = 0; j < 100; j++)
						{
							Vector3 pos = projectile.pos;
							for (int k = 0; k < 10; k++)
							{
								projectile.Update();
							}
							Vector3 pos2 = projectile.pos;
							Vector3 a = ExtraMath.ClosestPointOnLineSegment(pos, pos2, trajectory.end);
							float num2 = Vector3.SqrMagnitude(a - trajectory.end);
							if (num2 < num)
							{
								center = pos2;
								num = num2;
							}
							else
							{
								if (projectile.pos.y < trajectory.end.y && projectile.velocity.y < 0f)
								{
									break;
								}
								if (projectile.pos.x > trajectory.end.x)
								{
									break;
								}
							}
							Gizmos.DrawLine(pos, pos2);
						}
						if (trajectory.valid)
						{
							Gizmos.color = new Color(0f, 1f, 1f, 1f);
							Gizmos.DrawWireCube(center, Vector3.one * 0.01f);
							Gizmos.color = new Color(1f, 1f, 1f, 0.3f);
						}
					}
				}
				Gizmos.color = new Color(1f, 1f, 0f, 1f);
				TrajectorySample trajectorySample = this.Sample(this.test);
				if (trajectorySample.valid)
				{
					Vector3 startVelocity = trajectorySample.startVelocity;
					if (startVelocity != Vector3.zero)
					{
						Projectile projectile2 = new Projectile(startVelocity, this.settings);
						for (int l = 0; l < 100; l++)
						{
							Vector3 pos3 = projectile2.pos;
							for (int m = 0; m < 10; m++)
							{
								projectile2.Update();
							}
							Vector3 pos4 = projectile2.pos;
							Gizmos.DrawLine(pos3, pos4);
						}
					}
					Gizmos.color = Color.blue;
					Gizmos.DrawLine(Vector3.zero, trajectorySample.midPoint);
					Gizmos.DrawLine(trajectorySample.midPoint, this.test);
				}
				Gizmos.color = new Color(1f, 1f, 0f, 1f);
				Gizmos.matrix = base.transform.localToWorldMatrix * Matrix4x4.TRS(this.test, Quaternion.identity, new Vector3(1f, 1f, 0f));
				Gizmos.DrawWireSphere(Vector3.zero, 0.2f);
			}
			this.Iterate();
			Gizmos.color = new Color(1f, 1f, 1f, 0.2f);
		}

		// Token: 0x04002319 RID: 8985
		[Header("Bounds")]
		[Delayed]
		public int MinY = -5;

		// Token: 0x0400231A RID: 8986
		[Delayed]
		public int MaxY = 2;

		// Token: 0x0400231B RID: 8987
		[Delayed]
		public int MaxZ = 5;

		// Token: 0x0400231C RID: 8988
		[Header("Settings")]
		[SerializeField]
		public TrajectoryUtility.CostFunction costFunction;

		// Token: 0x0400231D RID: 8989
		[SerializeField]
		public TrajectoryUtility.CostFunction validFunction;

		// Token: 0x0400231E RID: 8990
		public ProjectileSettings settings;

		// Token: 0x0400231F RID: 8991
		public float maxValidRadius;

		// Token: 0x04002320 RID: 8992
		[Header("Results")]
		public TrajectoryUtility.Trajectory[] trajectories;

		// Token: 0x04002321 RID: 8993
		public Vector2 test;

		// Token: 0x020007C5 RID: 1989
		[Serializable]
		public class Trajectory
		{
			// Token: 0x0600339E RID: 13214 RVA: 0x000DE2C4 File Offset: 0x000DC6C4
			public Trajectory(Vector3 end, ProjectileSettings settings, TrajectoryUtility utility)
			{
				this.end = end;
				this.attributes = new Attributes(end.normalized * (settings.maxSpeed + settings.minSpeed) / 2f);
				this.GetCost(ref this.attributes, settings, utility);
			}

			// Token: 0x1700076A RID: 1898
			// (get) Token: 0x0600339F RID: 13215 RVA: 0x000DE320 File Offset: 0x000DC720
			public Vector2 startVelocity
			{
				get
				{
					return this.attributes.startVelocity;
				}
			}

			// Token: 0x1700076B RID: 1899
			// (get) Token: 0x060033A0 RID: 13216 RVA: 0x000DE32D File Offset: 0x000DC72D
			public Vector2 impactVelocity
			{
				get
				{
					return this.attributes.impactVelocity;
				}
			}

			// Token: 0x1700076C RID: 1900
			// (get) Token: 0x060033A1 RID: 13217 RVA: 0x000DE33A File Offset: 0x000DC73A
			public bool valid
			{
				get
				{
					return this.attributes.valid;
				}
			}

			// Token: 0x1700076D RID: 1901
			// (get) Token: 0x060033A2 RID: 13218 RVA: 0x000DE347 File Offset: 0x000DC747
			public float impactTime
			{
				get
				{
					return this.attributes.impactTime;
				}
			}

			// Token: 0x060033A3 RID: 13219 RVA: 0x000DE354 File Offset: 0x000DC754
			public Projectile GetProjectile(ProjectileSettings settings)
			{
				return new Projectile(this.startVelocity, settings);
			}

			// Token: 0x060033A4 RID: 13220 RVA: 0x000DE368 File Offset: 0x000DC768
			public bool InBounds(Vector3 pos)
			{
				pos.y = 0f;
				Vector3 vector = this.end;
				vector.y = 0f;
				return pos.sqrMagnitude < vector.sqrMagnitude;
			}

			// Token: 0x060033A5 RID: 13221 RVA: 0x000DE3A4 File Offset: 0x000DC7A4
			public Vector3 ClampVelocity(Vector3 velocity, ProjectileSettings settings)
			{
				velocity = velocity.normalized * Mathf.Clamp(velocity.magnitude, settings.minSpeed, settings.maxSpeed);
				velocity.x = Mathf.Abs(velocity.x);
				return velocity;
			}

			// Token: 0x060033A6 RID: 13222 RVA: 0x000DE3E0 File Offset: 0x000DC7E0
			public float GetCost(ref Attributes a, ProjectileSettings settings, TrajectoryUtility utility)
			{
				Projectile projectile = new Projectile(a.startVelocity, settings);
				float num = float.MaxValue;
				float num2 = 0f;
				bool flag = false;
				for (int i = 0; i < 100; i++)
				{
					Vector3 pos = projectile.pos;
					projectile.Update();
					Vector3 pos2 = projectile.pos;
					if (projectile.pos.y > num2)
					{
						a.highPoint = projectile.pos;
						num2 = projectile.pos.y;
					}
					Vector3 a2 = ExtraMath.ClosestPointOnLineSegment(pos, pos2, this.end);
					float sqrMagnitude = (a2 - this.end).sqrMagnitude;
					bool @bool = utility.validFunction.GetBool(sqrMagnitude, projectile.velocity.magnitude, num2, (float)i * Time.fixedDeltaTime, this.end.x, this.end.y, projectile.velocity.x, projectile.velocity.y, a.startVelocity.x, a.startVelocity.y);
					if (@bool)
					{
						flag = true;
					}
					if ((!flag || @bool) && sqrMagnitude < num)
					{
						num = sqrMagnitude;
						a.impactVelocity = projectile.velocity;
						a.impactTime = (float)i * Time.fixedDeltaTime;
					}
					if (projectile.velocity.y < 0f && projectile.pos.y < this.end.y)
					{
						break;
					}
				}
				num = Mathf.Sqrt(num);
				a.valid = utility.validFunction.GetBool(num, a.impactVelocity.magnitude, a.highPoint.y, a.impactTime, this.end.x, this.end.y, a.impactVelocity.x, a.impactVelocity.y, a.startVelocity.x, a.startVelocity.y);
				return utility.costFunction.GetCost(num, a.impactVelocity.magnitude, a.highPoint.y, a.impactTime, this.end.x, this.end.y, a.impactVelocity.x, a.impactVelocity.y, a.startVelocity.x, a.startVelocity.y);
			}

			// Token: 0x060033A7 RID: 13223 RVA: 0x000DE660 File Offset: 0x000DCA60
			public void CalculateMidPoint(ref Attributes a, ProjectileSettings pSettings)
			{
				Projectile projectile = new Projectile(a.startVelocity, pSettings);
				for (float num = 0f; num < a.impactTime / 2f; num += Time.fixedDeltaTime)
				{
					projectile.Update();
				}
				float num2 = a.startVelocity.y / a.startVelocity.x;
				float num3 = a.impactVelocity.y / a.impactVelocity.x;
				float num4 = this.end.y - num3 * this.end.x;
				Vector2 b;
				b.x = num4 / (num2 - num3);
				b.y = b.x * num2;
				a.midPoint = Vector2.Lerp(projectile.pos, b, 0.3f);
			}

			// Token: 0x060033A8 RID: 13224 RVA: 0x000DE734 File Offset: 0x000DCB34
			public float Iterate(ProjectileSettings settings, TrajectoryUtility utility)
			{
				this.attributes.startVelocity = this.ClampVelocity(this.startVelocity, settings);
				float num = this.GetCost(ref this.attributes, settings, utility);
				for (float num2 = 0.1f; num2 <= 1f; num2 += ((!this.attributes.valid) ? 0.2f : 0.5f))
				{
					Vector2 vector = this.startVelocity;
					vector.x += UnityEngine.Random.Range(-settings.maxSpeed * 2f, settings.maxSpeed * 2f) * num2;
					vector.y += UnityEngine.Random.Range(-settings.maxSpeed * 2f, settings.maxSpeed * 2f) * num2;
					vector = this.ClampVelocity(vector, settings);
					Vector2 zero = Vector2.zero;
					Attributes attributes = new Attributes(vector);
					float cost = this.GetCost(ref attributes, settings, utility);
					if (cost < num && (!this.attributes.valid || attributes.valid))
					{
						this.attributes = attributes;
						num = cost;
					}
				}
				this.CalculateMidPoint(ref this.attributes, settings);
				return num;
			}

			// Token: 0x04002323 RID: 8995
			public Attributes attributes;

			// Token: 0x04002324 RID: 8996
			public Vector3 end;
		}

		// Token: 0x020007C6 RID: 1990
		[Serializable]
		public class CostFunction : ExpressionSerialized<TrajectoryUtility.CostFunction>
		{
			// Token: 0x060033AA RID: 13226 RVA: 0x000DE888 File Offset: 0x000DCC88
			[ExpressionEvaluator]
			public float GetCost(float minDist, float impactSpeed, float height, float time, float targetX, float targetY, float impactX, float impactY, float velocityX, float velocityY)
			{
				return base.EvaluateFloat(new double[]
				{
					(double)minDist,
					(double)impactSpeed,
					(double)height,
					(double)time,
					(double)targetX,
					(double)targetY,
					(double)impactX,
					(double)impactY,
					(double)velocityX,
					(double)velocityY
				});
			}

			// Token: 0x060033AB RID: 13227 RVA: 0x000DE8DC File Offset: 0x000DCCDC
			public bool GetBool(float minDist, float impactSpeed, float height, float time, float targetX, float targetY, float impactX, float impactY, float velocityX, float velocityY)
			{
				return base.EvaluateBool(new double[]
				{
					(double)minDist,
					(double)impactSpeed,
					(double)height,
					(double)time,
					(double)targetX,
					(double)targetY,
					(double)impactX,
					(double)impactY,
					(double)velocityX,
					(double)velocityY
				});
			}
		}

		// Token: 0x020007C7 RID: 1991
		public enum Attribute
		{
			// Token: 0x04002326 RID: 8998
			StartVelocity,
			// Token: 0x04002327 RID: 8999
			HighPoint,
			// Token: 0x04002328 RID: 9000
			ImpactVelocity,
			// Token: 0x04002329 RID: 9001
			ImpactTime,
			// Token: 0x0400232A RID: 9002
			MidPoint
		}
	}
}
