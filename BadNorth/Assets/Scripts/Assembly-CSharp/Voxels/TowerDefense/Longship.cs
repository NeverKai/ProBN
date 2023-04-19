using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using CS.Platform;
using Fabric;
using ReflexCLI.Attributes;
using RTM.OnScreenDebug;
using UnityEngine;
using Voxels.TowerDefense.RaidGeneration;
using Voxels.TowerDefense.TriFlow;

namespace Voxels.TowerDefense
{
	[SelectionBase]
	public class Longship : MonoBehaviour, ITriFlowObject
	{
		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x0600324C RID: 12876 RVA: 0x000D55FE File Offset: 0x000D39FE
		public float radius => this.col.radius;

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x0600324D RID: 12877 RVA: 0x000D560B File Offset: 0x000D3A0B
		public float length => this.col.height;

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x0600324E RID: 12878 RVA: 0x000D5618 File Offset: 0x000D3A18
		public CapsuleCollider col
		{
			get
			{
				if (!this._col)
				{
					this._col = base.GetComponent<CapsuleCollider>();
				}
				return this._col;
			}
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x0600324F RID: 12879 RVA: 0x000D563C File Offset: 0x000D3A3C
		public float area
		{
			get
			{
				Vector3 extents = this.navMeshFilter.sharedMesh.bounds.extents;
				return (extents.x + 0.1f) * (extents.z + 0.1f) * 3.2f;
			}
		}

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x06003250 RID: 12880 RVA: 0x000D5683 File Offset: 0x000D3A83
		public Landing landing => this._landing;

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x06003251 RID: 12881 RVA: 0x000D5690 File Offset: 0x000D3A90
		public Squad squad => (this.agents.Count <= 0) ? null : this.agents[0].squad;

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x06003252 RID: 12882 RVA: 0x000D56BA File Offset: 0x000D3ABA
		public Landing.ShipTravel shipTravel => this.landing.shipTravel;

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x06003253 RID: 12883 RVA: 0x000D56C8 File Offset: 0x000D3AC8
		private Vector3 startPos => this.shipTravel.startPos;

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x06003254 RID: 12884 RVA: 0x000D56E4 File Offset: 0x000D3AE4
		private Vector3 endPos => this.shipTravel.endPos;

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x06003255 RID: 12885 RVA: 0x000D56FF File Offset: 0x000D3AFF
		// (set) Token: 0x06003256 RID: 12886 RVA: 0x000D5707 File Offset: 0x000D3B07
		public float interpolator { get; private set; }

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x06003257 RID: 12887 RVA: 0x000D5710 File Offset: 0x000D3B10
		public float totalDistance => this.shipTravel.distance;

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x06003258 RID: 12888 RVA: 0x000D572B File Offset: 0x000D3B2B
		public float distanceTravelled => this.interpolator * this.totalDistance;

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x06003259 RID: 12889 RVA: 0x000D573A File Offset: 0x000D3B3A
		public float distanceRemaining => (1f - this.interpolator) * this.totalDistance;

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x0600325A RID: 12890 RVA: 0x000D5750 File Offset: 0x000D3B50
		public float timeRemaining => (1f - this.interpolator) * this.shipTravel.duration;

		// Token: 0x140000A4 RID: 164
		// (add) Token: 0x0600325B RID: 12891 RVA: 0x000D5778 File Offset: 0x000D3B78
		// (remove) Token: 0x0600325C RID: 12892 RVA: 0x000D57B0 File Offset: 0x000D3BB0
		
		public event Action onShipArrival = delegate()
		{
		};

		// Token: 0x140000A5 RID: 165
		// (add) Token: 0x0600325D RID: 12893 RVA: 0x000D57E8 File Offset: 0x000D3BE8
		// (remove) Token: 0x0600325E RID: 12894 RVA: 0x000D5820 File Offset: 0x000D3C20
		
		public event Action onShipLaunch = delegate()
		{
		};

		// Token: 0x140000A6 RID: 166
		// (add) Token: 0x0600325F RID: 12895 RVA: 0x000D5858 File Offset: 0x000D3C58
		// (remove) Token: 0x06003260 RID: 12896 RVA: 0x000D5890 File Offset: 0x000D3C90
		
		public event Action<float> shipLeavingUpdate = delegate(float A_0)
		{
		};

		// Token: 0x140000A7 RID: 167
		// (add) Token: 0x06003261 RID: 12897 RVA: 0x000D58C8 File Offset: 0x000D3CC8
		// (remove) Token: 0x06003262 RID: 12898 RVA: 0x000D5900 File Offset: 0x000D3D00
		
		public event Action onShipExit = delegate()
		{
		};

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x06003263 RID: 12899 RVA: 0x000D5938 File Offset: 0x000D3D38
		public Data data => new Data(this, this.landing.navPos, false, false, this.distanceRemaining, this.landing.dir * this.distanceRemaining, ExtraMath.RemapValue(this.distanceRemaining, 4f, 1f));

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x06003264 RID: 12900 RVA: 0x000D5989 File Offset: 0x000D3D89
		public int numVikings => this.agents.Count;

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x06003265 RID: 12901 RVA: 0x000D5996 File Offset: 0x000D3D96
		public bool hasAnyVikings => this.numVikings > 0;

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x06003266 RID: 12902 RVA: 0x000D59A1 File Offset: 0x000D3DA1
		public Vector3 lookDir => -this.landing.dir;

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x06003267 RID: 12903 RVA: 0x000D59B3 File Offset: 0x000D3DB3
		public bool haveAllSpawned => this.spawnRoutine == null;

		// Token: 0x06003268 RID: 12904 RVA: 0x000D59C0 File Offset: 0x000D3DC0
		public void Explode()
		{
			float num = this.radius + 0.7f;
			Vector3 pos = this.landing.navPos.pos;
			List<Agent> staticListRadius = AgentEnumerators.GetStaticListRadius(pos, num, this.landing.navPos.island.english);
			bool flag = false;
			float num2 = (this.agents.Count <= 0) ? 0.35f : 1f;
			foreach (Agent agent in staticListRadius)
			{
				flag = true;
				Vector3 a = agent.wPos - pos;
				float magnitude = a.magnitude;
				float num3 = ExtraMath.RemapValue(magnitude, this.radius, num, this.landForce, 0f);
				float num4 = num3 * 10f * num2;
				float stun = num3 * 8f * num2;
				Vector3 normalized = (a - this.landing.dir).normalized;
				UnityEngine.Debug.DrawLine(agent.wPos, normalized * num4, Color.red, 2f);
				agent.DealDamage(new Attack(0f, num4, 0f, normalized, pos, this, this.squad, "Longship", null)
				{
					stun = stun
				});
				if (num4 > 1f)
				{
					IslandGameplayManager.RequestCombatAudio(this.knockbackSound, agent.gameObject);
				}
			}
			if (flag)
			{
				Singleton<CameraShaker>.instance.ShakeOnce(this.landForce * 0.2f);
			}
		}

		// Token: 0x06003269 RID: 12905 RVA: 0x000D5B6C File Offset: 0x000D3F6C
		private void Start()
		{
			FabricWrapper.PostEvent(this.shipAmbiance, base.gameObject);
		}

		// Token: 0x0600326A RID: 12906 RVA: 0x000D5B80 File Offset: 0x000D3F80
		private void Update()
		{
			if (this.outgoing)
			{
				this.UpdateOutGoing();
			}
			else
			{
				this.UpdateIncoming();
			}
		}

		// Token: 0x0600326B RID: 12907 RVA: 0x000D5BA0 File Offset: 0x000D3FA0
		private void UpdateIncoming()
		{
			if (this.interpolator < 1f)
			{
				if (this.spawnRoutine != null && !this.spawnRoutine.MoveNext())
				{
					this.spawnRoutine = null;
				}
				float duration = this.shipTravel.duration;
				float interpolator = this.interpolator;
				float num = (1f - this.interpolator) * duration;
				this.interpolator += Time.deltaTime / duration;
				float num2 = (1f - this.interpolator) * duration;
				if (interpolator < 0.5f && this.interpolator >= 0.5f)
				{
					foreach (Agent agent in this.agents)
					{
						if (agent)
						{
							agent.body.footstepSoundsEnabled = true;
						}
					}
				}
				if (num > 2f && num2 <= 2f)
				{
					this.onShipArrival();
				}
				this.UpdatePosition();
				if (this.interpolator >= 1f)
				{
					this.animator.SetTrigger(Longship.landId);
					FabricWrapper.PostEvent(this.landingSound, base.gameObject);
					FabricWrapper.PostEvent(this.shipAmbiance, EventAction.StopSound, base.gameObject);
				}
			}
			if (this.agents.Count > 0)
			{
				this.landing.navPos.island.english.presence.SampleFullData(this.landing.navPos, ref this.targetEnemyDist, ref this.targetEnemyDir, ref this.targetEnemyAgent);
				Data data = this.data;
				if (data.amount > 0f)
				{
					this.landing.navPos.island.vikings.presenceObj.AddPending(data);
				}
			}
		}

		// Token: 0x0600326C RID: 12908 RVA: 0x000D5D9C File Offset: 0x000D419C
		private void UpdateOutGoing()
		{
			if (this.interpolator > 0f)
			{
				this.interpolator -= Time.deltaTime / this.shipTravel.fleeDuration;
				this.UpdatePosition();
				this.shipLeavingUpdate(1f - this.interpolator);
			}
			else
			{
				this.onShipExit();
				base.gameObject.SetActive(false);
				FabricWrapper.PostEvent(this.shipAmbiance, EventAction.StopSound, base.gameObject);
			}
		}

		// Token: 0x0600326D RID: 12909 RVA: 0x000D5E26 File Offset: 0x000D4226
		public void RemoveAgent(Agent agent)
		{
			if (this.agents.Remove(agent) && this.agents.Count == 0 && this.landed)
			{
				base.enabled = false;
			}
		}

		// Token: 0x0600326E RID: 12910 RVA: 0x000D5E5B File Offset: 0x000D425B
		public void AddAgent(Agent agent)
		{
			this.agents.Add(agent);
		}

		// Token: 0x0600326F RID: 12911 RVA: 0x000D5E69 File Offset: 0x000D4269
		private void UpdatePosition()
		{
			this.UpdatePosition(Vector3.Lerp(this.startPos, this.endPos, this.interpolator));
		}

		// Token: 0x06003270 RID: 12912 RVA: 0x000D5E88 File Offset: 0x000D4288
		private void UpdatePosition(Vector3 pos)
		{
			Vector3 b = Vector3.zero;
			b = base.transform.position;
			base.transform.position = pos;
			if (Time.deltaTime > 0f)
			{
				this.navigationMesh.velocity = (base.transform.position - b) / Time.deltaTime;
			}
		}

		// Token: 0x06003271 RID: 12913 RVA: 0x000D5EE8 File Offset: 0x000D42E8
		[ConsoleCommand("")]
		public void Launch()
		{
			base.CancelInvoke();
			this.landed = false;
			this.outgoing = true;
			this.animator.SetTrigger(launchId);
			this.animator.enabled = true;
			FabricWrapper.PostEvent(this.launchSound, base.gameObject);
			FabricWrapper.PostEvent(this.shipAmbiance, base.gameObject);
			this.onShipLaunch();
		}

		// Token: 0x06003272 RID: 12914 RVA: 0x000D5F54 File Offset: 0x000D4354
		public void LaunchAnimComplete()
		{
			base.enabled = true;
		}

		// Token: 0x06003273 RID: 12915 RVA: 0x000D5F60 File Offset: 0x000D4360
		public void LandAnimComplete()
		{
			this.landed = true;
			if (this.agents.Count == 0)
			{
				base.enabled = false;
			}
			this.Invoke(delegate
			{
				this.animator.enabled = false;
			}, 1f);
			if (!this.hasAnyVikings)
			{
				BasePlatformManager.Instance.UnlockAchievement("ACHIEVEMENT_TURN_BACK_THE_TIDE");
			}
		}

		// Token: 0x06003274 RID: 12916 RVA: 0x000D5FBC File Offset: 0x000D43BC
		public void Setup(Landing landing)
		{
			this._landing = landing;
			base.transform.position = this.startPos;
			this.navMeshFilter.sharedMesh.GetVertices(Longship.verts);
			this.navMeshFilter.sharedMesh.GetTriangles(Longship.tris, 0);
			IEnumerator<GenInfo> enumerator = this.navigationMesh.Setup(Longship.verts, Longship.tris, null);
			while (enumerator.MoveNext())
			{
			}
			Longship.verts.Clear();
			Longship.tris.Clear();
			this.navigationMesh.grass = 0f;
			this.navMeshFilter.GetComponent<MeshRenderer>().enabled = false;
			this.animator = base.GetComponent<Animator>();
			this.bounds = default(Bounds);
			this.bounds.center = this.navigationMesh.verts[0].pos;
			for (int i = 0; i < this.navigationMesh.verts.Length; i++)
			{
				this.bounds.Encapsulate(this.navigationMesh.verts[i].pos);
			}
			this.navPos = new NavPos(this.navigationMesh, this.bounds.center, false, 1f);
			this.exitNavPos = new NavPos[this.exitPositions.Length];
			for (int j = 0; j < this.exitNavPos.Length; j++)
			{
				this.exitNavPos[j] = new NavPos(this.navPos.tri, this.exitPositions[j]);
			}
			this.spawnRoutine = this.SpawnRoutine();
			SoftNormalsInTangents.InitializeAll(base.transform);
		}

		// Token: 0x06003275 RID: 12917 RVA: 0x000D6178 File Offset: 0x000D4578
		private IEnumerator SpawnRoutine()
		{
			yield return null;
			int agentsPerFrame = Mathf.CeilToInt((float)this.agents.Count / 10f);
			int i = 0;
			int num = this.agents.Count;
			while (i < num)
			{
				Agent a = this.agents[i];
				a.body.footstepSoundsEnabled = false;
				a.Spawn();
				if (i % agentsPerFrame == agentsPerFrame - 1)
				{
					yield return null;
				}
				i++;
			}
			yield break;
		}

		// Token: 0x06003276 RID: 12918 RVA: 0x000D6194 File Offset: 0x000D4594
		public void OnProximity(Agent otherAgent)
		{
			if (this.distanceRemaining > 1f)
			{
				return;
			}
			float sqrMagnitude = (otherAgent.navPos.pos - this.landing.navPos.pos).sqrMagnitude;
			if (sqrMagnitude > 1f)
			{
				return;
			}
			float num = 1f - this.distanceRemaining;
			num *= 1f - Mathf.Sqrt(sqrMagnitude);
			num *= otherAgent.navPos.GetCliffyness();
			otherAgent.Intimidate(num, num * 0.4f);
		}

		// Token: 0x06003277 RID: 12919 RVA: 0x000D6220 File Offset: 0x000D4620
		private void OnDrawGizmos()
		{
			if (Application.isPlaying)
			{
				Gizmos.DrawLine(this.startPos, this.endPos);
				Gizmos.DrawRay(this.shipTravel.endPos, this.lookDir);
				Gizmos.DrawRay(this.shipTravel.endPos, this.targetEnemyDir);
			}
		}

		// Token: 0x06003278 RID: 12920 RVA: 0x000D627C File Offset: 0x000D467C
		private void OnDrawGizmosSelected()
		{
			Gizmos.matrix = this.navigationMesh.transform.localToWorldMatrix;
			Gizmos.color = Color.yellow;
			if (Application.isPlaying)
			{
				Gizmos.DrawSphere(this.navPos.wPos, 0.1f);
				for (int i = 0; i < this.exitNavPos.Length; i++)
				{
					Gizmos.DrawRay(this.exitNavPos[i].pos, Vector3.up * 0.1f);
				}
			}
			else
			{
				for (int j = 0; j < this.exitPositions.Length; j++)
				{
					Gizmos.DrawRay(this.exitPositions[j], Vector3.up * 0.3f);
				}
			}
		}

		// Token: 0x06003279 RID: 12921 RVA: 0x000D6348 File Offset: 0x000D4748
		private void OnDestroy()
		{
			FabricWrapper.PostEvent(this.shipAmbiance, EventAction.StopSound, base.gameObject);
			this.navigationMesh = null;
			this.navMeshFilter = null;
			this.evacuationLocation = null;
			this.agents.Clear();
		}

		private DebugChannelGroup dbgGroup = new DebugChannelGroup("Longship", EVerbosity.Minimal, 100);

		// Token: 0x0400222D RID: 8749
		public NavigationMesh navigationMesh;

		// Token: 0x0400222E RID: 8750
		public MeshFilter navMeshFilter;

		// Token: 0x0400222F RID: 8751
		public SquadEvacuationLocation evacuationLocation;

		// Token: 0x04002230 RID: 8752
		public float landForce = 0.5f;

		// Token: 0x04002231 RID: 8753
		public Vector3[] exitPositions = new Vector3[]
		{
			new Vector3(1f, 0f, 1f),
			new Vector3(-1f, 0f, 1f)
		};

		// Token: 0x04002232 RID: 8754
		public NavPos[] exitNavPos;

		// Token: 0x04002233 RID: 8755
		private CapsuleCollider _col;

		// Token: 0x04002234 RID: 8756
		public NavPos navPos;

		// Token: 0x04002235 RID: 8757
		private Bounds bounds;

		// Token: 0x04002236 RID: 8758
		private RTM.Utilities.WeakReference<Landing> _landing;

		// Token: 0x04002237 RID: 8759
		public bool landed;

		// Token: 0x04002238 RID: 8760
		private bool outgoing;

		// Token: 0x04002239 RID: 8761
		private Animator animator;

		// Token: 0x0400223A RID: 8762
		public static int launchId = Animator.StringToHash("Launch");

		// Token: 0x0400223B RID: 8763
		public static int landId = Animator.StringToHash("Land");

		// Token: 0x0400223C RID: 8764
		public List<Agent> agents;

		// Token: 0x0400223D RID: 8765
		public float speed = 0.3f;

		// Token: 0x0400223F RID: 8767
		[Header("Sound")]
		public FabricEventReference landingSound = "Sfx/Boat/LandBig";

		// Token: 0x04002240 RID: 8768
		public FabricEventReference launchSound = "Sfx/Boat/Depart";

		// Token: 0x04002241 RID: 8769
		public FabricEventReference knockbackSound = "Sfx/Fall/Fall";

		// Token: 0x04002242 RID: 8770
		public float minKnockbackForAudio = 2f;

		// Token: 0x04002243 RID: 8771
		private const float arrivalSoundTime = 2f;

		// Token: 0x04002248 RID: 8776
		private FabricEventReference shipAmbiance = "Amb/Boat";

		// Token: 0x04002249 RID: 8777
		public float targetEnemyDist = float.MaxValue;

		// Token: 0x0400224A RID: 8778
		public Vector3 targetEnemyDir;

		// Token: 0x0400224B RID: 8779
		public Agent targetEnemyAgent;

		// Token: 0x0400224C RID: 8780
		private IEnumerator spawnRoutine;

		// Token: 0x0400224D RID: 8781
		private static List<Vector3> verts = new List<Vector3>();

		// Token: 0x0400224E RID: 8782
		private static List<int> tris = new List<int>();
	}
}
