using System;
using ReflexCLI.Attributes;
using UnityEngine;
using Voxels.TowerDefense.Upgrades;

namespace Voxels.TowerDefense
{
	// Token: 0x0200088E RID: 2190
	[ConsoleCommandClassCustomizer("Explosion")]
	public static class ExplosionHelpers
	{
		// Token: 0x0600395E RID: 14686 RVA: 0x000FB090 File Offset: 0x000F9490
		public static void CreateExplosion(Transform transform, ExplosionDef definition, Agent instigator = null)
		{
			if (transform)
			{
				ExplosionHelpers.CreateExplosionInternal(transform.position, transform.up, transform.forward, NavPos.empty, definition, transform.gameObject, instigator);
			}
		}

		// Token: 0x0600395F RID: 14687 RVA: 0x000FB0C1 File Offset: 0x000F94C1
		public static void CreateExplosion(Vector3 position, Vector3 up, Vector3 forward, ExplosionDef definition, GameObject audioSource, Agent instigator = null)
		{
			ExplosionHelpers.CreateExplosionInternal(position, up, forward, NavPos.empty, definition, audioSource, instigator);
		}

		// Token: 0x06003960 RID: 14688 RVA: 0x000FB0D5 File Offset: 0x000F94D5
		public static void CreateExplosion(NavPos navPos, Vector3 forward, ExplosionDef definition, GameObject audioSource, Agent instigator = null)
		{
			ExplosionHelpers.CreateExplosionInternal(navPos.pos, navPos.GetNormal(), forward, navPos, definition, audioSource, instigator);
		}

		// Token: 0x06003961 RID: 14689 RVA: 0x000FB0F0 File Offset: 0x000F94F0
		private static void CreateExplosionInternal(Vector3 position, Vector3 normal, Vector3 forward, NavPos navPos, ExplosionDef definition, GameObject audioSource, Agent instigator)
		{
			if (!definition)
			{
				return;
			}
			forward.Normalize();
			Squad killerSquad = (!instigator) ? null : instigator.squad;
			float damage = definition.damage;
			int num = definition.maxDamagedAgents;
			float knockbackRadius = definition.knockbackRadius;
			AnimationCurve normalizedKnockbackFalloff = definition.normalizedKnockbackFalloff;
			float maxKnockback = definition.maxKnockback;
			AnimationCurve normalizedLaunchFalloff = definition.normalizedLaunchFalloff;
			float maxLaunch = definition.maxLaunch;
			AnimationCurve normalizedStunFalloff = definition.normalizedStunFalloff;
			float maxStun = definition.maxStun;
			Vector3 vector = position;
			float num2 = Mathf.Max(definition.damageRadius, knockbackRadius);
			Faction faction = (!instigator || definition.affectsFriendlyUnits) ? null : instigator.faction.enemy;
			bool flag = definition.angleOfEffect < 180f;
			if (flag && definition.minorRadius > 0f)
			{
				float num3 = definition.minorRadius / Mathf.Sin(0.017453292f * definition.angleOfEffect);
				Vector3 b = -num3 * forward;
				vector += b;
				num2 += num3;
			}
			foreach (Agent agent in AgentEnumerators.GetStaticListRadiusSorted(vector, num2, faction))
			{
				Vector3 a = agent.chestPos - position;
				float magnitude = a.magnitude;
				if (flag)
				{
					Vector3 from = agent.chestPos - vector;
					if (Vector3.Angle(from, forward) > definition.angleOfEffect && magnitude > definition.minorRadius)
					{
						continue;
					}
				}
				Vector3 a2 = (magnitude <= 0f) ? Vector3.up : (a / magnitude);
				Vector3 pos = agent.chestPos - a2 * agent.radius;
				float damage2 = 0f;
				if (num > 0 && ExplosionHelpers.CanAffectAgent(definition.damageRadius, magnitude, agent, instigator, definition.friendlyDamage))
				{
					damage2 = damage;
					num--;
				}
				float knockback = 0f;
				float launchImpulse = 0f;
				float stun = 1f;
				if (knockbackRadius > 0f && ExplosionHelpers.CanAffectAgent(knockbackRadius, magnitude, agent, instigator, definition.friendlyKnockback))
				{
					float time = magnitude / knockbackRadius;
					float num4 = 1f + 0.075f * (UnityEngine.Random.value - 0.5f);
					knockback = ((maxKnockback <= 0f) ? 0f : (maxKnockback * normalizedKnockbackFalloff.Evaluate(time) * num4));
					launchImpulse = ((maxLaunch <= 0f) ? 0f : (maxLaunch * normalizedLaunchFalloff.Evaluate(time) * num4));
					stun = ((maxStun <= 0f) ? 0f : (maxStun * normalizedStunFalloff.Evaluate(time)));
				}
				Vector3 direction = agent.chestPos - vector;
				agent.DealDamage(new Attack(damage2, knockback, launchImpulse, direction, pos, instigator, killerSquad, definition.fabricAudioAttackID, null)
				{
					stun = stun
				});
			}
			if (definition.explosionParticle)
			{
				if (navPos.valid)
				{
					definition.explosionParticle.PlayAt(navPos, Quaternion.LookRotation(navPos.GetNormal(), forward), Vector3.one);
				}
				else
				{
					definition.explosionParticle.PlayAt(position, Quaternion.LookRotation(forward, normal), Vector3.one);
				}
			}
			FabricWrapper.PostEvent(definition.fabricAudioID, audioSource);
			Singleton<CameraShaker>.instance.ShakeOnce(definition.cameraShake);
		}

		// Token: 0x06003962 RID: 14690 RVA: 0x000FB4C0 File Offset: 0x000F98C0
		private static void RagdollLaunch(Agent agent, float force, Vector3 dir)
		{
			if (force < 2f)
			{
				return;
			}
			EnglishSquad englishSquad = agent.squad as EnglishSquad;
			if (englishSquad)
			{
				InvulnerableAbility componentInChildren = englishSquad.GetComponentInChildren<InvulnerableAbility>();
				if (componentInChildren && componentInChildren.isActive)
				{
					return;
				}
			}
			Ragdoller component = agent.GetComponent<Ragdoller>();
			if (component)
			{
				component.Launch(force * dir);
			}
		}

		// Token: 0x06003963 RID: 14691 RVA: 0x000FB530 File Offset: 0x000F9930
		private static bool CanAffectAgent(float radius, float distance, Agent target, Agent instigator, ExplosionDef.FriendlyFire friendlyFire)
		{
			if (!instigator)
			{
				return true;
			}
			if (distance < radius)
			{
				if (target.faction != instigator.faction)
				{
					return true;
				}
				if (friendlyFire == ExplosionDef.FriendlyFire.FriendlySquads)
				{
					return instigator.squad != target.squad;
				}
				if (friendlyFire == ExplosionDef.FriendlyFire.OwnSquad)
				{
					return instigator != target;
				}
				if (friendlyFire == ExplosionDef.FriendlyFire.All)
				{
					return true;
				}
			}
			return false;
		}
	}
}
