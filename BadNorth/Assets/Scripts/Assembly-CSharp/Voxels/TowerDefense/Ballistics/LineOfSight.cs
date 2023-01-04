using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.TowerDefense.Ballistics
{
	// Token: 0x0200079C RID: 1948
	public class LineOfSight : AgentComponent
	{
		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x06003233 RID: 12851 RVA: 0x000D4BA4 File Offset: 0x000D2FA4
		public LineOfSight.Sight target
		{
			get
			{
				for (int i = 0; i < this.enemies.Count; i++)
				{
					if (this.enemies[i].agent)
					{
						return this.enemies[i];
					}
				}
				return default(LineOfSight.Sight);
			}
		}

		// Token: 0x06003234 RID: 12852 RVA: 0x000D4C04 File Offset: 0x000D3004
		public void SetupLineOfSight(TrajectoryUtility trajectory, AgentState agentState, IThreat threat)
		{
			this.trajectory = trajectory;
			this.threat = threat;
			this.enemies = new List<LineOfSight.Sight>(16);
			agentState.OnUpdate += this.OnUpdate;
			agentState.OnDeactivate = (Action)Delegate.Combine(agentState.OnDeactivate, new Action(delegate()
			{
				this.enemies.Clear();
			}));
			this.radius = trajectory.maxValidRadius;
			this.sqRadius = this.radius * this.radius;
			Func<Squad, bool> squadFunc = (Squad s1) => (s1.bounds.ClosestPoint(base.agent.wPos).GetXZ() - base.agent.wPos.GetXZ()).sqrMagnitude < this.sqRadius;
			Func<Agent, bool> agentFunc = (Agent a1) => (a1.wPos.GetXZ() - base.agent.wPos.GetXZ()).sqrMagnitude < this.sqRadius && a1.groundedState.active && a1.gameObject.activeInHierarchy;
			this.enemyEnumerator = AgentEnumerators.MultiFrameFunc(base.agent.faction.enemy, squadFunc, agentFunc);
			LineOfSight.updateFrameIterator = (LineOfSight.updateFrameIterator + 1) % 3;
			this.updateFrame = LineOfSight.updateFrameIterator;
		}

		// Token: 0x06003235 RID: 12853 RVA: 0x000D4CCF File Offset: 0x000D30CF
		public LineOfSight.Sight GetClosestEnemy()
		{
			return this.target;
		}

		// Token: 0x06003236 RID: 12854 RVA: 0x000D4CD8 File Offset: 0x000D30D8
		public LineOfSight.Sight InSight(Agent targetAgent)
		{
			LineOfSight.Sight sight = this.GetSight(targetAgent);
			if (sight.agent)
			{
				for (int i = 0; i < this.enemies.Count; i++)
				{
					if (this.enemies[i].agent == targetAgent)
					{
						this.enemies[i] = sight;
						return sight;
					}
				}
				this.enemies.Add(sight);
				return sight;
			}
			if (targetAgent)
			{
				for (int j = this.enemies.Count - 1; j >= 0; j--)
				{
					if (this.enemies[j].agent == targetAgent)
					{
						this.enemies.RemoveAt(j);
					}
				}
			}
			return sight;
		}

		// Token: 0x06003237 RID: 12855 RVA: 0x000D4DAC File Offset: 0x000D31AC
		private bool Raycast(Vector3 start, Vector3 end, LayerMask mask)
		{
			Vector3 direction = end - start;
			bool flag = Physics.Raycast(new Ray(start, direction), direction.magnitude, mask);
			Debug.DrawLine(start, end, (!flag) ? Color.green : Color.red);
			return flag;
		}

		// Token: 0x06003238 RID: 12856 RVA: 0x000D4DF8 File Offset: 0x000D31F8
		private LineOfSight.Sight GetSight(Agent targetAgent)
		{
			LineOfSight.Sight result = default(LineOfSight.Sight);
			if (!targetAgent)
			{
				return result;
			}
			if (!targetAgent.gameObject.activeInHierarchy)
			{
				return result;
			}
			Vector3 chestPos = targetAgent.chestPos;
			Vector3 chestPos2 = base.agent.chestPos;
			Vector3 vector = chestPos - chestPos2;
			if (Vector3.SqrMagnitude(vector) > this.radius * this.radius)
			{
				return result;
			}
			TrajectorySample trajectorySample = this.trajectory.Sample(vector);
			if (!trajectorySample.valid)
			{
				return result;
			}
			Vector3 vector2 = chestPos2 + trajectorySample.highPoint;
			LayerMask arrowLow = LayerMaster.arrowLow;
			LayerMask arrowHigh = LayerMaster.arrowHigh;
			if (vector.GetXZ().sqrMagnitude < (vector2 - chestPos2).GetXZ().sqrMagnitude)
			{
				if (Physics.CheckSphere(chestPos2, 0f, arrowLow) || Physics.CheckSphere(chestPos, 0f, arrowLow))
				{
					if (this.Raycast(chestPos2, chestPos, arrowHigh))
					{
						return result;
					}
					result.mask0 = arrowHigh;
				}
				else
				{
					if (this.Raycast(chestPos2, chestPos, arrowLow))
					{
						return result;
					}
					result.mask0 = arrowLow;
				}
				result.mask1 = arrowLow;
			}
			else
			{
				if (Physics.CheckSphere(chestPos2, 0f, arrowLow))
				{
					if (this.Raycast(chestPos2, vector2, arrowHigh))
					{
						return result;
					}
					result.mask0 = arrowHigh;
				}
				else
				{
					if (this.Raycast(chestPos2, vector2, arrowLow))
					{
						return result;
					}
					result.mask0 = arrowLow;
				}
				if (Physics.CheckSphere(chestPos, 0f, arrowLow))
				{
					if (this.Raycast(chestPos, vector2, arrowHigh))
					{
						return result;
					}
					result.mask1 = arrowHigh;
				}
				else
				{
					if (this.Raycast(chestPos, vector2, arrowLow))
					{
						return result;
					}
					result.mask1 = arrowLow;
				}
			}
			result.agent = targetAgent;
			return result;
		}

		// Token: 0x06003239 RID: 12857 RVA: 0x000D4FE8 File Offset: 0x000D33E8
		public bool CheckSquad(Squad squad)
		{
			return squad.faction != base.agent.squad.faction && Vector3.SqrMagnitude(squad.bounds.ClosestPoint(base.agent.chestPos) - base.agent.chestPos) < this.radius * this.radius;
		}

		// Token: 0x0600323A RID: 12858 RVA: 0x000D5054 File Offset: 0x000D3454
		private float GetScore(LineOfSight.Sight sight)
		{
			Agent agent = sight.agent;
			float num = -Vector3.SqrMagnitude(base.agent.chestPos - sight.agent.chestPos);
			if (agent.stun.fall.active)
			{
				num += this.stunBias;
			}
			else if (agent.shield)
			{
				num += this.shieldBias;
			}
			if (agent.enemyDist < 1f)
			{
				num += 1f;
			}
			return num;
		}

		// Token: 0x0600323B RID: 12859 RVA: 0x000D50DC File Offset: 0x000D34DC
		private void OnUpdate()
		{
			this.updateFrame = (this.updateFrame + 1) % 3;
			if (this.updateFrame == 0)
			{
				this.enemyEnumerator.MoveNext();
				Agent targetAgent = this.enemyEnumerator.Current;
				this.InSight(targetAgent);
				for (int i = this.enemies.Count - 1; i >= 0; i--)
				{
					LineOfSight.Sight sight = this.enemies[i];
					Agent agent = sight.agent;
					if (agent == null || !agent.aliveAndGrounded.active)
					{
						this.enemies.RemoveAt(i);
					}
					else
					{
						agent.rangeWorry.PoseThreat(this.threat);
						sight.score = this.GetScore(sight);
						this.enemies[i] = sight;
					}
				}
				this.enemies.Sort();
				if (this.enemies.Count > 15)
				{
					this.enemies.RemoveRange(15, this.enemies.Count - 15);
				}
			}
		}

		// Token: 0x0600323C RID: 12860 RVA: 0x000D51E8 File Offset: 0x000D35E8
		private void OnDrawGizmosSelected()
		{
			if (Application.isPlaying)
			{
				for (int i = 0; i < this.enemies.Count; i++)
				{
					Agent agent = this.enemies[i].agent;
					if (agent)
					{
						Gizmos.DrawLine(base.agent.transform.position, agent.transform.position);
					}
				}
			}
		}

		// Token: 0x0600323D RID: 12861 RVA: 0x000D5260 File Offset: 0x000D3660
		public bool GetTreatValid(Agent victim)
		{
			for (int i = 0; i < this.enemies.Count; i++)
			{
				if (this.enemies[i].agent == victim)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600323E RID: 12862 RVA: 0x000D52AB File Offset: 0x000D36AB
		private void OnDestroy()
		{
			this.trajectory = null;
			this.enemies = null;
			this.threat = null;
			this.enemyEnumerator = null;
		}

		// Token: 0x04002217 RID: 8727
		private TrajectoryUtility trajectory;

		// Token: 0x04002218 RID: 8728
		private float radius;

		// Token: 0x04002219 RID: 8729
		private float sqRadius;

		// Token: 0x0400221A RID: 8730
		[SerializeField]
		private float shieldBias = -2f;

		// Token: 0x0400221B RID: 8731
		[SerializeField]
		private float stunBias = 2.5f;

		// Token: 0x0400221C RID: 8732
		public List<LineOfSight.Sight> enemies;

		// Token: 0x0400221D RID: 8733
		private IThreat threat;

		// Token: 0x0400221E RID: 8734
		private IEnumerator<Agent> enemyEnumerator;

		// Token: 0x0400221F RID: 8735
		private static int updateFrameIterator;

		// Token: 0x04002220 RID: 8736
		private const int updateFrameInterval = 3;

		// Token: 0x04002221 RID: 8737
		private int updateFrame;

		// Token: 0x04002222 RID: 8738
		private const int maxTargets = 15;

		// Token: 0x0200079D RID: 1949
		[Serializable]
		public struct Sight : IComparable<LineOfSight.Sight>
		{
			// Token: 0x06003242 RID: 12866 RVA: 0x000D5386 File Offset: 0x000D3786
			int IComparable<LineOfSight.Sight>.CompareTo(LineOfSight.Sight other)
			{
				return other.score.CompareTo(this.score);
			}

			// Token: 0x04002223 RID: 8739
			public Agent agent;

			// Token: 0x04002224 RID: 8740
			public LayerMask mask0;

			// Token: 0x04002225 RID: 8741
			public LayerMask mask1;

			// Token: 0x04002226 RID: 8742
			public float score;
		}
	}
}
