using System;
using System.Collections.Generic;
using System.Diagnostics;
using CS.Lights;
using Fabric;
using ReflexCLI.Attributes;
using UnityEngine;
using Voxels.TowerDefense.ScriptAnimations;

namespace Voxels.TowerDefense
{
	// Token: 0x02000779 RID: 1913
	public class House : MonoBehaviour
	{
		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x06003198 RID: 12696 RVA: 0x000CEFC1 File Offset: 0x000CD3C1
		public bool intact
		{
			get
			{
				return this.intactState.active && !this.destroying.active;
			}
		}

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x06003199 RID: 12697 RVA: 0x000CEFE4 File Offset: 0x000CD3E4
		public bool destroyed
		{
			get
			{
				return this.pillaged.active || this.destroying.active;
			}
		}

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x0600319A RID: 12698 RVA: 0x000CF004 File Offset: 0x000CD404
		// (set) Token: 0x0600319B RID: 12699 RVA: 0x000CF00C File Offset: 0x000CD40C
		public BoxCollider boxCollider { get; private set; }

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x0600319C RID: 12700 RVA: 0x000CF015 File Offset: 0x000CD415
		// (set) Token: 0x0600319D RID: 12701 RVA: 0x000CF01D File Offset: 0x000CD41D
		public Bounds bounds { get; private set; }

		// Token: 0x140000A1 RID: 161
		// (add) Token: 0x0600319E RID: 12702 RVA: 0x000CF028 File Offset: 0x000CD428
		// (remove) Token: 0x0600319F RID: 12703 RVA: 0x000CF060 File Offset: 0x000CD460
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action onTorched = delegate()
		{
		};

		// Token: 0x140000A2 RID: 162
		// (add) Token: 0x060031A0 RID: 12704 RVA: 0x000CF098 File Offset: 0x000CD498
		// (remove) Token: 0x060031A1 RID: 12705 RVA: 0x000CF0D0 File Offset: 0x000CD4D0
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action onDestroyed = delegate()
		{
		};

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x060031A2 RID: 12706 RVA: 0x000CF106 File Offset: 0x000CD506
		// (set) Token: 0x060031A3 RID: 12707 RVA: 0x000CF10E File Offset: 0x000CD50E
		public float fire { get; private set; }

		// Token: 0x060031A4 RID: 12708 RVA: 0x000CF118 File Offset: 0x000CD518
		public bool Check(NavigationMesh navMesh, Village village)
		{
			Bounds bounds = new Bounds(base.transform.TransformPoint(this.vertBounds.min), Vector3.zero);
			bounds.Encapsulate(base.transform.TransformPoint(this.vertBounds.max));
			for (int i = 0; i < navMesh.verts.Length; i++)
			{
				if (bounds.Contains(navMesh.verts[i].pos))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060031A5 RID: 12709 RVA: 0x000CF19C File Offset: 0x000CD59C
		public void Setup(NavigationMesh navMesh, Village village)
		{
			this.village = village;
			this.intactState = new AgentState("Intact", this.stateRoot.rootState, true, true);
			this.burning = new AgentState("Burning", this.intactState, false, false);
			this.destroying = new AgentState("Destroying", this.intactState, false, false);
			this.pillaged = new AgentState("Pillaged", this.stateRoot.rootState, false, true);
			this.fireAudioState = new AgentState("FireAudio", this.stateRoot.rootState, false, false);
			this.anySmoothFire = new AgentState("AnySmoothFire", this.stateRoot.rootState, false, false);
			this.invulnerable = new AgentState("Invulnerable", this.stateRoot.rootState, false, false);
			AgentState agentState = this.pillaged;
			agentState.OnChange = (Action<bool>)Delegate.Combine(agentState.OnChange, new Action<bool>(delegate(bool x)
			{
				this.animator.SetBool("Pillaged", x);
			}));
			this.windFire = base.GetComponentInChildren<WindSmoke>(true);
			this.windFire.gameObject.SetActive(false);
			this.houseLight = base.GetComponent<HouseLight>();
			AgentState agentState2 = this.intactState;
			agentState2.OnDeactivate = (Action)Delegate.Combine(agentState2.OnDeactivate, new Action(delegate()
			{
				this.smoothFireAnim.SetTarget(0f, null, null, null, 0f, null);
			}));
			this.smoothFireAnim = new TargetAnimator<float>(() => this.smoothFire, delegate(float x)
			{
				this.smoothFire = x;
				this.windFire.SetMaterialProperty(House.fireShaderId, x);
				this.windFire.gameObject.SetActive(x > 0f);
				this.anySmoothFire.SetActive(x > 0f);
				this.animator.SetFloat(House.fireId, x);
			}, this.stateRoot.rootState, new LerpTowards(1f, 1f));
			this.burning.OnUpdate += delegate()
			{
				if (this.arsonists.Count == 0)
				{
					this.fire -= Time.deltaTime / 6f;
					if (this.fire < 0f)
					{
						this.fire = 0f;
						this.burning.SetActive(false);
					}
				}
				this.houseLight.SetHouseBurning(this.fire > 0f);
				this.smoothFireAnim.SetTarget(this.fire, null, null, null, 0f, null);
			};
			AgentState agentState3 = this.burning;
			agentState3.OnDeactivate = (Action)Delegate.Combine(agentState3.OnDeactivate, new Action(delegate()
			{
				this.smoothFireAnim.SetTarget(0f, null, null, null, 0f, null);
			}));
			this.intactState.OnUpdate += delegate()
			{
				if (!this.destroying.active && this.timer > 0f)
				{
					this.timer -= Time.deltaTime * village.timerPerArsonist.Evaluate((float)this.arsonists.Count);
					this.timer = Mathf.Clamp01(this.timer);
				}
			};
			this.fireAudioState.OnUpdate += delegate()
			{
				EventManager.Instance.SetParameter(House.fireAudio, "Intensity", this.smoothFire, this.gameObject);
			};
			AgentState agentState4 = this.fireAudioState;
			agentState4.OnActivate = (Action)Delegate.Combine(agentState4.OnActivate, new Action(delegate()
			{
				FabricWrapper.PostEvent(House.fireAudio, this.gameObject);
				EventManager.Instance.SetParameter(House.fireAudio, "Intensity", this.smoothFire, this.gameObject);
			}));
			AgentState agentState5 = this.fireAudioState;
			agentState5.OnDeactivate = (Action)Delegate.Combine(agentState5.OnDeactivate, new Action(delegate()
			{
				EventManager.Instance.PostEvent(House.fireAudio, EventAction.StopSound, null, this.gameObject);
				EventManager.Instance.SetParameter(House.fireAudio, "Intensity", 0f, this.gameObject);
			}));
			AgentState agentState6 = this.anySmoothFire;
			agentState6.OnChange = (Action<bool>)Delegate.Combine(agentState6.OnChange, new Action<bool>(delegate(bool x)
			{
				this.fireAudioState.SetActive(x && !this.suppressAudio);
			}));
			this.worldPos = base.transform.TransformPoint(this.localPos);
			this.worldTargetPos = this.windFire.transform.position;
			Bounds bounds = new Bounds(base.transform.TransformPoint(this.vertBounds.min), Vector3.zero);
			bounds.Encapsulate(base.transform.TransformPoint(this.vertBounds.max));
			List<int> list = ListPool<int>.GetList(16);
			float num = float.MaxValue;
			for (int i = 0; i < navMesh.verts.Length; i++)
			{
				Vert vert = navMesh.verts[i];
				if (bounds.Contains(vert.pos))
				{
					list.Add(i);
					float sqrMagnitude = (base.transform.InverseTransformPoint(vert.pos) - this.doorPos).sqrMagnitude;
					if (sqrMagnitude < num)
					{
						this.doorVert = vert;
						num = sqrMagnitude;
					}
				}
			}
			this.distanceField = new DistanceField(navMesh, list, "house");
			this.navPos = new NavPos(navMesh.verts[list[0]]);
			list.ReturnToListPool<int>();
			this.boxCollider = base.GetComponentInChildren<BoxCollider>(true);
			if (base.transform.lossyScale.x * this.boxCollider.size.x < 0f)
			{
				this.boxCollider.size = this.boxCollider.size.SetX(-this.boxCollider.size.x);
			}
			this.boxCollider.enabled = true;
			Vector3 a = this.boxCollider.transform.TransformPoint(this.boxCollider.center - this.boxCollider.size / 2f);
			Vector3 b = this.boxCollider.transform.TransformPoint(this.boxCollider.center + this.boxCollider.size / 2f);
			this.bounds = new Bounds((a + b) / 2f, ExtraMath.Abs(a - b));
			village.island.voxelSpace.AddCoverage(this.bounds, 0.7f);
			this.suppressAudio = false;
			this.coinContainer = base.gameObject.AddEmptyChild("CoinContainer").transform;
			this.coinContainer.transform.position = this.worldTargetPos;
		}

		// Token: 0x060031A6 RID: 12710 RVA: 0x000CF6B8 File Offset: 0x000CDAB8
		private void OnDestroy()
		{
			this.distanceField = null;
			this.arsonists.Clear();
			this.village = null;
			this.stateRoot.OnDestroy();
			this.stateRoot = null;
			this.intactState = null;
			this.burning = null;
			this.destroying = null;
			this.pillaged = null;
			this.fireAudioState = null;
			this.anySmoothFire = null;
			this.invulnerable = null;
			this.smoothFireAnim = null;
			this.doorVert = null;
			this.windFire = null;
		}

		// Token: 0x060031A7 RID: 12711 RVA: 0x000CF736 File Offset: 0x000CDB36
		private void Update()
		{
			if (this.intactState != null)
			{
				this.stateRoot.Update();
			}
		}

		// Token: 0x060031A8 RID: 12712 RVA: 0x000CF74E File Offset: 0x000CDB4E
		public void SetInvulnerable()
		{
			this.invulnerable.SetActive(true);
		}

		// Token: 0x060031A9 RID: 12713 RVA: 0x000CF75D File Offset: 0x000CDB5D
		public void SetSuppressAudio(bool suppress)
		{
			this.suppressAudio = suppress;
			this.fireAudioState.SetActive(!suppress && this.anySmoothFire.active);
		}

		// Token: 0x060031AA RID: 12714 RVA: 0x000CF786 File Offset: 0x000CDB86
		[ConsoleCommand("")]
		[ContextMenu("Pillage")]
		private void DebugPillage()
		{
			this.TorchThrow(null);
			this.TorchLand(null);
		}

		// Token: 0x060031AB RID: 12715 RVA: 0x000CF798 File Offset: 0x000CDB98
		[ConsoleCommand("")]
		private static void PillageAll()
		{
			foreach (House house in Singleton<IslandGameplayManager>.instance.island.village.houses)
			{
				house.DebugPillage();
			}
		}

		// Token: 0x060031AC RID: 12716 RVA: 0x000CF7D8 File Offset: 0x000CDBD8
		public void PaintSoot()
		{
			Bounds bounds = this.bounds;
			bounds.extents += new Vector3(0.4f, 0.2f, 0.4f);
			this.village.island.painter.Paint(bounds, Painter.sootColor);
		}

		// Token: 0x060031AD RID: 12717 RVA: 0x000CF830 File Offset: 0x000CDC30
		public bool TryThrow(Arsonist arsonist)
		{
			if (this.timer <= 0f && this.intactState.active && !this.destroying.active && arsonist == this.arsonists[0])
			{
				this.arsonists.RemoveAt(0);
				this.arsonists.Add(arsonist);
				this.timer += 1f;
				return true;
			}
			return false;
		}

		// Token: 0x060031AE RID: 12718 RVA: 0x000CF8B1 File Offset: 0x000CDCB1
		public void TorchThrow(Torch torch)
		{
			if (this.fire > 1f)
			{
				this.destroying.SetActive(true);
				this.killer = torch;
			}
		}

		// Token: 0x060031AF RID: 12719 RVA: 0x000CF8D7 File Offset: 0x000CDCD7
		public void Done()
		{
			this.onDestroyed();
			this.village.HousePillaged(this);
			this.smoothFireAnim.SetTargetAndCurrent(0f);
		}

		// Token: 0x060031B0 RID: 12720 RVA: 0x000CF900 File Offset: 0x000CDD00
		public void TorchLand(Torch torch)
		{
			this.onTorched();
			if (torch != this.killer && !this.destroying.active)
			{
				this.burning.SetActive(true);
				this.fire += this.village.torchDamage;
			}
			else if (!this.invulnerable.active)
			{
				this.pillaged.SetActive(true);
				FabricEventReference eventId = (this.destroyAudioOverride == null) ? House.destroyAudio : this.destroyAudioOverride;
				FabricWrapper.PostEvent(eventId, base.gameObject);
				this.killer = null;
				this.PaintSoot();
			}
		}

		// Token: 0x060031B1 RID: 12721 RVA: 0x000CF9B8 File Offset: 0x000CDDB8
		private void OnDrawGizmosSelected()
		{
			Gizmos.matrix = base.transform.localToWorldMatrix;
			Gizmos.color = new Color(1f, 1f, 1f, 1f);
			Gizmos.DrawWireCube(this.vertBounds.center, this.vertBounds.size);
			Gizmos.color = new Color(1f, 1f, 0f, 1f);
			Gizmos.matrix = Matrix4x4.identity;
			Gizmos.color = Color.red;
			this.distanceField.DrawGizmos();
		}

		// Token: 0x060031B2 RID: 12722 RVA: 0x000CFA4C File Offset: 0x000CDE4C
		private void OnDrawGizmos()
		{
			if (this.coinBounds.extents.GetVolume() > 0f)
			{
				Gizmos.DrawWireCube(this.coinBounds.center, this.coinBounds.extents * 2f);
			}
			for (int i = 0; i < this.arsonists.Count; i++)
			{
				Gizmos.DrawLine(this.arsonists[i].agent.wPos, base.transform.position);
			}
			Gizmos.matrix = base.transform.localToWorldMatrix;
			Gizmos.DrawRay(this.doorPos, Vector3.up * 0.2f);
		}

		// Token: 0x060031B3 RID: 12723 RVA: 0x000CFB04 File Offset: 0x000CDF04
		public void Reset()
		{
			ParticleSystem[] componentsInChildren = base.GetComponentsInChildren<ParticleSystem>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].Clear(true);
			}
			base.StopAllCoroutines();
			this.smoothFireAnim.SetTargetAndCurrent(0f);
			this.fire = 0f;
			this.invulnerable.SetActive(false);
			this.intactState.SetActive(true);
			this.burning.SetActive(false);
			this.destroying.SetActive(false);
			this.anySmoothFire.SetActive(false);
			this.fireAudioState.SetActive(false);
			this.smoothFireAnim.SetCurrent(0f);
			this.killer = null;
			this.suppressAudio = false;
			this.coinContainer.transform.position = this.worldTargetPos;
		}

		// Token: 0x060031B4 RID: 12724 RVA: 0x000CFBD8 File Offset: 0x000CDFD8
		public void StartLevelPillaged()
		{
			this.pillaged.SetActive(true);
		}

		// Token: 0x04002158 RID: 8536
		public Transform coinSpawnPos;

		// Token: 0x04002159 RID: 8537
		public Vector3 worldTargetPos;

		// Token: 0x0400215A RID: 8538
		public Vector3 localPos;

		// Token: 0x0400215B RID: 8539
		public Vector3 worldPos;

		// Token: 0x0400215C RID: 8540
		public Vector3 doorPos;

		// Token: 0x0400215D RID: 8541
		public Bounds vertBounds;

		// Token: 0x0400215E RID: 8542
		public DistanceField distanceField;

		// Token: 0x04002160 RID: 8544
		private static FabricEventReference fireAudio = "Sfx/House/Fire";

		// Token: 0x04002161 RID: 8545
		private static FabricEventReference destroyAudio = "Sfx/House/Destroyed";

		// Token: 0x04002162 RID: 8546
		[NonSerialized]
		public FabricEventReference destroyAudioOverride;

		// Token: 0x04002163 RID: 8547
		private float pillageShake = 0.4f;

		// Token: 0x04002164 RID: 8548
		[SerializeField]
		public Animator animator;

		// Token: 0x04002165 RID: 8549
		[SerializeField]
		public Animator uiAnimator;

		// Token: 0x04002166 RID: 8550
		public NavPos navPos;

		// Token: 0x04002168 RID: 8552
		[NonSerialized]
		public Bounds coinBounds = default(Bounds);

		// Token: 0x04002169 RID: 8553
		public List<Arsonist> arsonists = new List<Arsonist>();

		// Token: 0x0400216A RID: 8554
		private Village village;

		// Token: 0x0400216D RID: 8557
		private static int fireId = Animator.StringToHash("Fire");

		// Token: 0x0400216E RID: 8558
		[SerializeField]
		private AgentStateRoot stateRoot = new AgentStateRoot(6);

		// Token: 0x0400216F RID: 8559
		public AgentState intactState;

		// Token: 0x04002170 RID: 8560
		public AgentState burning;

		// Token: 0x04002171 RID: 8561
		public AgentState destroying;

		// Token: 0x04002172 RID: 8562
		public AgentState pillaged;

		// Token: 0x04002173 RID: 8563
		public AgentState fireAudioState;

		// Token: 0x04002174 RID: 8564
		public AgentState anySmoothFire;

		// Token: 0x04002175 RID: 8565
		public AgentState invulnerable;

		// Token: 0x04002176 RID: 8566
		private float smoothFire;

		// Token: 0x04002177 RID: 8567
		private TargetAnimator<float> smoothFireAnim;

		// Token: 0x04002179 RID: 8569
		public int coinCount = 1;

		// Token: 0x0400217A RID: 8570
		[NonSerialized]
		public Transform coinContainer;

		// Token: 0x0400217B RID: 8571
		private bool suppressAudio;

		// Token: 0x0400217C RID: 8572
		private HouseLight houseLight;

		// Token: 0x0400217D RID: 8573
		public Vert doorVert;

		// Token: 0x0400217E RID: 8574
		private WindSmoke windFire;

		// Token: 0x0400217F RID: 8575
		private static ShaderId fireShaderId = "_Fire";

		// Token: 0x04002180 RID: 8576
		private float timer;

		// Token: 0x04002181 RID: 8577
		private Torch killer;
	}
}
