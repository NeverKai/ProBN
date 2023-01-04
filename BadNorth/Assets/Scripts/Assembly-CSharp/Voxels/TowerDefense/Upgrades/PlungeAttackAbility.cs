using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.TowerDefense.Upgrades
{
	// Token: 0x0200085A RID: 2138
	public class PlungeAttackAbility : NavSpotTargetableAbility
	{
		// Token: 0x06003801 RID: 14337 RVA: 0x000F1E1C File Offset: 0x000F021C
		protected override void DoSquadSpawnAction_Implementation()
		{
			base.DoSquadSpawnAction_Implementation();
			this.projectileSolver = base.GetComponent<IProjectileSolver>();
			PlungeAttackAbility.Settings settings = this.levelSettings[base.upgradeLevel];
			base.GetComponent<CliffDropTargeter>().maxDropHeight = settings.maxHeight;
			this.explosionDef = UnityEngine.Object.Instantiate<ExplosionDef>(settings.explosion);
			Agent minionPrefab = base.squad.hero.monoHero.minionPrefab;
			Swordsman component = minionPrefab.GetComponent<Swordsman>();
			this.explosionDef.damage += 2f * component.damageLevels[base.squad.level];
			this.explosionDef.maxStun += 2f * component.stunLevels[base.squad.level];
		}

		// Token: 0x06003802 RID: 14338 RVA: 0x000F1EDC File Offset: 0x000F02DC
		protected override void DoTargetedAction(NavSpot heroNavSpot, NavSpot target)
		{
			Vector3 position = heroNavSpot.transform.position;
			Vector3 position2 = target.transform.position;
			Vector3 normalized = (position2 - position).GetZeroY().normalized;
			float d = heroNavSpot.transform.position.y - target.transform.position.y;
			float num = 3f;
			List<Agent> agents = base.squad.agents;
			float num2 = float.MaxValue;
			float num3 = float.MinValue;
			for (int i = 0; i < agents.Count; i++)
			{
				Agent agent = agents[i];
				float sqrMagnitude = (agent.wPos - position2).GetZeroY().sqrMagnitude;
				if (sqrMagnitude <= num * num)
				{
					num2 = Mathf.Min(num2, sqrMagnitude);
					num3 = Mathf.Max(num3, sqrMagnitude);
				}
			}
			num2 = Mathf.Sqrt(num2);
			num3 = Mathf.Sqrt(num3);
			int j = 0;
			int count = agents.Count;
			while (j < count)
			{
				Agent agent2 = agents[j];
				float magnitude = (agent2.wPos - position2).GetZeroY().magnitude;
				if (magnitude <= num)
				{
					float timer = ExtraMath.RemapValue(magnitude, num2, num3, 0f, 0.6f);
					Vector3 zeroY = base.squad.GetRelativePositionInBounds(agent2).GetZeroY();
					Vector3 vector = Vector3.ProjectOnPlane(zeroY, normalized);
					vector += position + normalized;
					Debug.DrawLine(agent2.transform.position, vector, Color.yellow, 5f);
					PlungeAttackComponent component = agent2.GetComponent<PlungeAttackComponent>();
					Vector3 vector2 = target.navPos.pos;
					vector2 += zeroY * 0.5f;
					vector2 += UnityEngine.Random.insideUnitSphere * this.landPositionError * d;
					vector2 = Vector3.MoveTowards(vector2, agent2.transform.position, 0.1f);
					NavPos navPos = target.navPos;
					navPos.pos = vector2;
					bool flag = base.squad.heroAgent == agent2;
					HeroVoice heroVoice = (!flag) ? null : base.squad.hero.voice;
					FabricEventReference jumpSound = (!heroVoice) ? PlungeAttackComponent.fabricLaunchID : heroVoice.plungeJumpSounds[base.upgradeLevel];
					component.DoPlungeAttack(vector, navPos, zeroY, this.projectileSolver, this.explosionDef, jumpSound, timer);
					component.OnLanded -= this.OnAgentLanded;
					component.OnLanded += this.OnAgentLanded;
				}
				j++;
			}
			SquadMover.MoveToDirect(base.squad, target, false);
			Debug.DrawRay(target.transform.position, Vector3.up, Color.red, 1f);
			this.Invoke(new Action(this.OnAgentLanded), this.timeout);
		}

		// Token: 0x06003803 RID: 14339 RVA: 0x000F21E9 File Offset: 0x000F05E9
		private void OnAgentLanded()
		{
			if (base.isActive)
			{
				base.OnEnded();
			}
		}

		// Token: 0x06003804 RID: 14340 RVA: 0x000F21FC File Offset: 0x000F05FC
		private void OnDestroy()
		{
			UnityEngine.Object.Destroy(this.explosionDef);
		}

		// Token: 0x04002632 RID: 9778
		[Header("Plunge Attack")]
		[SerializeField]
		private float landPositionError = 0.3f;

		// Token: 0x04002633 RID: 9779
		[SerializeField]
		private float timeout = 4f;

		// Token: 0x04002634 RID: 9780
		[SerializeField]
		private PlungeAttackAbility.Settings[] levelSettings = new PlungeAttackAbility.Settings[3];

		// Token: 0x04002635 RID: 9781
		private IProjectileSolver projectileSolver;

		// Token: 0x04002636 RID: 9782
		private ExplosionDef explosionDef;

		// Token: 0x0200085B RID: 2139
		[Serializable]
		private class Settings
		{
			// Token: 0x04002637 RID: 9783
			public float maxHeight;

			// Token: 0x04002638 RID: 9784
			public ExplosionDef explosion;
		}
	}
}
