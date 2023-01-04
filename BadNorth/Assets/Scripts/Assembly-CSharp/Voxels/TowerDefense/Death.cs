using System;
using UnityEngine;
using Voxels.TowerDefense.SpriteMagic;

namespace Voxels.TowerDefense
{
	// Token: 0x0200069C RID: 1692
	public class Death : AgentComponent
	{
		// Token: 0x06002BA4 RID: 11172 RVA: 0x0009FEE8 File Offset: 0x0009E2E8
		public override void Setup()
		{
			base.Setup();
			AgentState deadState = base.agent.deadState;
			deadState.OnActivate = (Action)Delegate.Combine(deadState.OnActivate, new Action(this.StartDie));
			base.agent.deadState.OnUpdate += this.OnUpdate;
			if (Death.bloodDecal == null)
			{
				Death.bloodDecal = UnityEngine.Object.Instantiate<SpriteStamp>(ScriptableObjectSingleton<PrefabManager>.instance.bloodDecal, base.agent.faction.island.runContainer);
			}
			CorpseManager component = base.agent.navPos.navigationMesh.GetComponent<CorpseManager>();
			if (component)
			{
				component.RegisterCorpse(base.agent.spriteAnimator);
			}
		}

		// Token: 0x06002BA5 RID: 11173 RVA: 0x0009FFB0 File Offset: 0x0009E3B0
		private void StartDie()
		{
			if (base.agent.groundedState.active)
			{
				FabricWrapper.PostEvent(this.deathSound, base.gameObject);
				FabricWrapper.PostEvent(this.deathSound2, base.gameObject);
				base.agent.PlayAnimation(Death.deathId);
				base.agent.moveAnimate = false;
				base.agent.body.sliding.SetActive(true);
			}
		}

		// Token: 0x06002BA6 RID: 11174 RVA: 0x000A002E File Offset: 0x0009E42E
		private void OnUpdate()
		{
			base.transform.position = base.agent.wPos;
		}

		// Token: 0x06002BA7 RID: 11175 RVA: 0x000A0048 File Offset: 0x0009E448
		public void Die()
		{
			NavPos navPos = base.agent.navPos;
			this.Die(navPos);
		}

		// Token: 0x06002BA8 RID: 11176 RVA: 0x000A0068 File Offset: 0x0009E468
		public void Die(NavPos navPos)
		{
			if (!navPos.valid)
			{
				NavPos navPos2 = new NavPos(base.agent.faction.island.navMesh, base.transform.position, true, 1f);
				if (Vector3.SqrMagnitude(navPos2.pos - base.transform.position) < 0.04f)
				{
					navPos = navPos2;
				}
			}
			if (navPos.valid)
			{
				navPos.pos = navPos.transform.InverseTransformPoint(base.transform.position);
				Vector3 wPos = navPos.wPos;
				Vector3 normal = navPos.GetNormal();
				CorpseManager component = navPos.navigationMesh.GetComponent<CorpseManager>();
				if (component)
				{
					Quaternion q = Quaternion.LookRotation(normal, -base.transform.forward + UnityEngine.Random.insideUnitSphere) * Quaternion.Euler(90f, 0f, 0f);
					Matrix4x4 matrix = Matrix4x4.TRS(wPos, q, base.transform.localScale);
					component.AddCorpse(matrix, base.agent.spriteAnimator, navPos);
				}
				ScriptableObjectSingleton<PrefabManager>.instance.bloodStain.PlayAt(wPos, normal);
				IslandGameplayManager.RequestFootstepsAudio(base.agent.faction.bloodStainSound, base.agent.gameObject);
				if (navPos.island)
				{
					Death.bloodDecal.Stamp(navPos, base.agent.radius * 10f);
					navPos.navigationMesh.island.painter.PaintBlood(wPos);
				}
				else
				{
					MeshPainter component2 = navPos.navigationMesh.GetComponent<MeshPainter>();
					if (component2)
					{
						component2.Paint(wPos);
					}
				}
			}
			else
			{
				base.agent.faction.island.painter.PaintBlood(base.transform.position);
			}
			base.agent.FinalDeath();
		}

		// Token: 0x04001C76 RID: 7286
		[Header("Sound")]
		public string deathSound = "Sfx/English/Militia/Die";

		// Token: 0x04001C77 RID: 7287
		public string deathSound2 = "Sfx/Juice/Impact";

		// Token: 0x04001C78 RID: 7288
		private static SpriteStamp bloodDecal;

		// Token: 0x04001C79 RID: 7289
		private static AnimId deathId = "Death";
	}
}
