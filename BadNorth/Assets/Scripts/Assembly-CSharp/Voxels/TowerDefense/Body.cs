using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Voxels.TowerDefense
{
	// Token: 0x0200068F RID: 1679
	public class Body : AgentComponent, IPassedClick, ILevelComponent
	{
		// Token: 0x06002AED RID: 10989 RVA: 0x00099E3B File Offset: 0x0009823B
		private Quaternion ComputeShadow(NavPos navPos)
		{
			return Quaternion.LookRotation(navPos.GetNormal() + Vector3.up);
		}

		// Token: 0x06002AEE RID: 10990 RVA: 0x00099E54 File Offset: 0x00098254
		void ILevelComponent.OnSetLevel(Agent agent, int level)
		{
			int num = level + 1;
			if (this.moveSoundLevelOverlay.IsValidIndex(num) && !string.IsNullOrEmpty(this.moveSoundLevelOverlay[num]))
			{
				this.layeredMoveSoundRef = this.moveSoundLevelOverlay[num];
			}
			else
			{
				this.layeredMoveSoundRef = null;
			}
		}

		// Token: 0x06002AEF RID: 10991 RVA: 0x00099EA8 File Offset: 0x000982A8
		public override void Setup()
		{
			base.Setup();
			this.hopping = new AgentState("Hop", base.agent.groundedState, false, true);
			this.sliding = new AgentState("Slide", base.agent.groundedState, false, true);
			this.standing = new AgentState("Standing", this.hopping, false, true);
			this.stepping = new AgentState("Stepping", this.hopping, false, true);
			AgentState agentState = this.sliding;
			agentState.OnActivate = (Action)Delegate.Combine(agentState.OnActivate, new Action(delegate()
			{
				base.transform.SetParent(base.agent.navPos.transform);
				base.agent.navPos.pos = base.transform.localPosition;
			}));
			this.sliding.OnUpdate += delegate()
			{
				base.transform.position = base.agent.navPos.wPos;
				this.UpdateRotation();
				if (base.agent.aliveState.active && base.agent.velocity.sqrMagnitude < 0.0001f)
				{
					this.sliding.SetActive(false);
				}
				if (base.agent.moveAnimate)
				{
					base.agent.animator.Play(Body.idleID);
				}
			};
			AgentState agentState2 = this.hopping;
			agentState2.OnActivate = (Action)Delegate.Combine(agentState2.OnActivate, new Action(delegate()
			{
				this.anchor = base.transform.localPosition;
			}));
			AgentState moveAnimateState = base.agent.moveAnimateState;
			moveAnimateState.OnActivate = (Action)Delegate.Combine(moveAnimateState.OnActivate, new Action(delegate()
			{
				if (this.standing.active)
				{
					base.agent.PlayAnimation(Body.idleID);
				}
			}));
			AgentState agentState3 = this.standing;
			agentState3.OnActivate = (Action)Delegate.Combine(agentState3.OnActivate, new Action(delegate()
			{
				if (base.agent.moveAnimateState.active)
				{
					base.agent.PlayAnimation(Body.idleID);
				}
				this.anchor = base.transform.localPosition;
				this.UpdateRotation();
			}));
			this.standing.OnUpdate += delegate()
			{
				Vector3 vector = base.agent.navPos.pos - this.anchor;
				float sqrMagnitude = vector.sqrMagnitude;
				Vector3 b = vector;
				float num = base.agent.radius * 0.3f;
				if (sqrMagnitude >= base.agent.radius * base.agent.radius || (sqrMagnitude > num * num && (vector - b).magnitude / Time.deltaTime < 0.1f))
				{
					this.stepping.SetActive(true);
				}
				else
				{
					if (base.agent.velocity != Vector3.zero)
					{
						Vector3 vector2 = base.transform.localPosition;
						vector2 += base.agent.navPos.transform.InverseTransformVector(base.agent.velocity) * Time.deltaTime;
						vector2 = base.agent.navPos.navigationMesh.bounds.ClosestPoint(vector2);
						base.transform.localPosition = vector2;
						this.UpdateRotation();
					}
					this.anchor = base.transform.localPosition;
				}
			};
			AgentState agentState4 = this.stepping;
			agentState4.OnActivate = (Action)Delegate.Combine(agentState4.OnActivate, new Action(delegate()
			{
				this.step = 1f;
				this.stepTime = 0.4f / Mathf.Lerp(base.agent.maxSpeed, 2f, 0.8f);
			}));
			this.stepping.OnUpdate += this.SteppingUpdate;
			this.shadowScale = base.agent.batchedShadow.transform.localScale.x;
			this.currentShadow = (this.startShadow = (this.endShadow = this.ComputeShadow(base.agent.navPos)));
			this.ApplyShadow(this.currentShadow);
			this.startGrass = (this.endGrass = base.agent.navPos.navigationMesh.grass);
			this.SetGrass();
			AgentState groundedState = base.agent.groundedState;
			groundedState.OnEmpty = (Action)Delegate.Combine(groundedState.OnEmpty, new Action(this.hopping.SetActiveTrue));
			base.agent.groundedState.OnUpdate += this.Upright;
			AgentState groundedState2 = base.agent.groundedState;
			groundedState2.OnActivate = (Action)Delegate.Combine(groundedState2.OnActivate, new Action(this.SetGrass));
			AgentState agentState5 = this.hopping;
			agentState5.OnEmpty = (Action)Delegate.Combine(agentState5.OnEmpty, new Action(this.stepping.SetActiveTrue));
			this.baseMoveSoundRef.ForceInit();
			if (this.layeredMoveSoundRef != null)
			{
				this.layeredMoveSoundRef.ForceInit();
			}
		}

		// Token: 0x06002AF0 RID: 10992 RVA: 0x0009A17C File Offset: 0x0009857C
		public void StopStep()
		{
			if (this.stepping.active)
			{
				this.step = 1f;
				base.agent.navPos.pos = base.transform.position;
			}
		}

		// Token: 0x06002AF1 RID: 10993 RVA: 0x0009A1B4 File Offset: 0x000985B4
		private void SteppingUpdate()
		{
			this.step += Time.deltaTime / this.stepTime;
			if (this.step >= 1f)
			{
				this.step -= 1f;
				this.step *= this.stepTime;
				Island island = this.endPos.island;
				this.endPos = base.agent.navPos;
				Transform parent = base.transform.parent;
				Transform transform = this.endPos.navigationMesh.transform;
				this.local = (parent == transform);
				if (this.local)
				{
					this.startPos = base.transform.localPosition;
					this.SetGrass(this.endPos);
				}
				else
				{
					this.startGrass = this.endGrass;
					this.endGrass = this.endPos.GetGrass();
					this.startPos = base.transform.position;
					base.transform.SetParent(transform);
				}
				this.startShadow = this.endShadow;
				this.endShadow = this.ComputeShadow(this.endPos);
				Vector3 a = (!this.local) ? (this.endPos.wPos - this.startPos) : (this.endPos.pos - this.startPos);
				float num = a.sqrMagnitude;
				if (num < 0.0001f)
				{
					this.standing.SetActive(true);
					base.transform.localPosition = base.agent.navPos.pos;
					this.walkDelta = Vector3.zero;
					this.currentShadow = (this.startShadow = this.endShadow);
					this.ApplyShadow(this.currentShadow);
					return;
				}
				num = Mathf.Sqrt(num);
				base.agent.walkedDistance += num;
				this.stepTime = num * 0.08f + 0.4f;
				this.stepTime /= Mathf.Lerp(base.agent.maxSpeed, 2f, 0.8f);
				this.walkDelta = a / this.stepTime;
				float value = num / this.stepTime;
				this.step /= this.stepTime;
				base.agent.animator.SetFloat(Body.speedID, value);
				this.alternating = !this.alternating;
				if (this.footstepSoundsEnabled)
				{
					IslandGameplayManager.RequestFootstepsAudio(this.baseMoveSoundRef, base.gameObject);
					IslandGameplayManager.RequestFootstepsAudio(this.layeredMoveSoundRef, base.gameObject);
				}
				if (num > 0.2f && this.endPos.island && island)
				{
					Singleton<DustParticles>.instance.SpawnParticles(base.transform.position, this.walkDelta);
				}
			}
			if (base.agent.velocity != Vector3.zero)
			{
				Vector3 vector = base.agent.velocity * Time.deltaTime;
				Vector3 b = this.endPos.transform.InverseTransformVector(vector);
				if (this.local)
				{
					this.startPos += b;
					this.startPos = this.endPos.navigationMesh.bounds.ClosestPoint(this.startPos);
				}
				else
				{
					this.startPos += vector;
				}
				this.endPos.pos = this.endPos.pos + b;
				this.endShadow = this.ComputeShadow(this.endPos);
			}
			if (this.local)
			{
				base.transform.localPosition = Vector3.Lerp(this.startPos, this.endPos.pos, this.step);
			}
			else
			{
				base.transform.position = Vector3.Lerp(this.startPos, this.endPos.wPos, this.step);
				this.SetGrass(Mathf.Lerp(this.startGrass, this.endGrass, this.step));
			}
			this.currentShadow = Quaternion.Lerp(this.startShadow, this.endShadow, this.step);
			this.ApplyShadow(this.currentShadow);
			if (base.agent.moveAnimateState.active)
			{
				base.agent.animator.Play(Body.walkID, -1, this.step * 0.5f + ((!this.alternating) ? 0f : 0.5f));
			}
		}

		// Token: 0x06002AF2 RID: 10994 RVA: 0x0009A66C File Offset: 0x00098A6C
		private void UpdateRotation()
		{
			NavPos navPos = base.agent.navPos;
			navPos.pos = base.transform.localPosition;
			this.currentShadow = this.ComputeShadow(navPos);
			this.ApplyShadow(this.currentShadow);
		}

		// Token: 0x06002AF3 RID: 10995 RVA: 0x0009A6B0 File Offset: 0x00098AB0
		private void ApplyShadow(Quaternion shadowRotation)
		{
			base.agent.shadow.transform.rotation = base.transform.parent.rotation * shadowRotation;
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06002AF4 RID: 10996 RVA: 0x0009A6DD File Offset: 0x00098ADD
		// (set) Token: 0x06002AF5 RID: 10997 RVA: 0x0009A6E5 File Offset: 0x00098AE5
		public Vector3 walkDelta
		{
			get
			{
				return this._walkDelta;
			}
			set
			{
				if (this._walkDelta == value)
				{
					return;
				}
				this._walkDelta = value;
				this.moving = (value != Vector3.zero);
			}
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06002AF6 RID: 10998 RVA: 0x0009A711 File Offset: 0x00098B11
		// (set) Token: 0x06002AF7 RID: 10999 RVA: 0x0009A719 File Offset: 0x00098B19
		public bool moving
		{
			get
			{
				return this._moving;
			}
			set
			{
				if (value == this._moving)
				{
					return;
				}
				base.agent.animator.SetBool(Body.movingID, value);
				this._moving = value;
			}
		}

		// Token: 0x06002AF8 RID: 11000 RVA: 0x0009A74C File Offset: 0x00098B4C
		private void Upright()
		{
			if (base.agent.navPos.island)
			{
				base.agent.shadow.transform.rotation = this.currentShadow;
			}
			else
			{
				base.transform.rotation = Quaternion.LookRotation(new Vector3(base.transform.forward.x, 0f, base.transform.forward.z));
				base.agent.shadow.transform.rotation = base.transform.parent.rotation * this.currentShadow;
			}
		}

		// Token: 0x06002AF9 RID: 11001 RVA: 0x0009A803 File Offset: 0x00098C03
		private void SetGrass()
		{
			this.SetGrass(base.agent.navPos);
		}

		// Token: 0x06002AFA RID: 11002 RVA: 0x0009A816 File Offset: 0x00098C16
		public void SetGrass(NavPos navPos)
		{
			this.SetGrass(navPos.navigationMesh.grass);
		}

		// Token: 0x06002AFB RID: 11003 RVA: 0x0009A82C File Offset: 0x00098C2C
		public void SetGrass(float grass)
		{
			if (grass == base.agent.batchedShadow.color.a)
			{
				return;
			}
			base.agent.batchedShadow.color = base.agent.batchedShadow.color.SetA(grass);
			base.agent.spriteAnimator.color = base.agent.spriteAnimator.color.SetA(grass);
		}

		// Token: 0x06002AFC RID: 11004 RVA: 0x0009A8A4 File Offset: 0x00098CA4
		private void OnDrawGizmos()
		{
			if (Application.isPlaying && this.endPos.valid)
			{
				Gizmos.color = Color.red;
				if (this.local)
				{
					Gizmos.matrix = this.endPos.transform.worldToLocalMatrix;
					Gizmos.DrawLine(this.endPos.pos, this.startPos);
				}
				else
				{
					Gizmos.DrawLine(this.endPos.wPos, this.startPos);
				}
			}
		}

		// Token: 0x06002AFD RID: 11005 RVA: 0x0009A928 File Offset: 0x00098D28
		public void OnPassedClick(ClickPasser clickPasser, PointerEventData eventData, RaycastHit raycastHit)
		{
			if (base.agent && base.agent.squad && base.agent.squad.passedClicker != null)
			{
				base.agent.squad.passedClicker.OnPassedClick(clickPasser, eventData, raycastHit);
			}
		}

		// Token: 0x04001BEA RID: 7146
		private NavPos endPos;

		// Token: 0x04001BEB RID: 7147
		private Vector3 anchor;

		// Token: 0x04001BEC RID: 7148
		private Vector3 startPos;

		// Token: 0x04001BED RID: 7149
		private Quaternion startShadow;

		// Token: 0x04001BEE RID: 7150
		private Quaternion endShadow;

		// Token: 0x04001BEF RID: 7151
		private Quaternion currentShadow;

		// Token: 0x04001BF0 RID: 7152
		private float shadowScale;

		// Token: 0x04001BF1 RID: 7153
		private float startGrass;

		// Token: 0x04001BF2 RID: 7154
		private float endGrass;

		// Token: 0x04001BF3 RID: 7155
		private static AnimId walkID = "Walk";

		// Token: 0x04001BF4 RID: 7156
		private static AnimId idleID = "Idle";

		// Token: 0x04001BF5 RID: 7157
		private static AnimId speedID = "Speed";

		// Token: 0x04001BF6 RID: 7158
		private static AnimId stepId = "Step";

		// Token: 0x04001BF7 RID: 7159
		private static AnimId movingID = "Moving";

		// Token: 0x04001BF8 RID: 7160
		public AgentState hopping;

		// Token: 0x04001BF9 RID: 7161
		public AgentState sliding;

		// Token: 0x04001BFA RID: 7162
		public AgentState stepping;

		// Token: 0x04001BFB RID: 7163
		public AgentState standing;

		// Token: 0x04001BFC RID: 7164
		[NonSerialized]
		public bool footstepSoundsEnabled = true;

		// Token: 0x04001BFD RID: 7165
		private float step = 1f;

		// Token: 0x04001BFE RID: 7166
		private float stepTime = 1f;

		// Token: 0x04001BFF RID: 7167
		private bool alternating;

		// Token: 0x04001C00 RID: 7168
		private float lastShadowScale = -1f;

		// Token: 0x04001C01 RID: 7169
		private bool local;

		// Token: 0x04001C02 RID: 7170
		public Vector3 _walkDelta;

		// Token: 0x04001C03 RID: 7171
		public bool _moving;

		// Token: 0x04001C04 RID: 7172
		[Header("Move Sounds")]
		[SerializeField]
		public FabricEventReference baseMoveSoundRef = "Sfx/Unit01/Move";

		// Token: 0x04001C05 RID: 7173
		[SerializeField]
		[FormerlySerializedAs("moveSound")]
		private string[] moveSoundLevelOverlay = new string[0];

		// Token: 0x04001C06 RID: 7174
		[SerializeField]
		[HideInInspector]
		private FabricEventReference layeredMoveSoundRef;
	}
}
