using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Voxels.TowerDefense.SpriteMagic;

namespace Voxels.TowerDefense.RaidGeneration
{
	// Token: 0x02000792 RID: 1938
	public class Landing : MonoBehaviour
	{
		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06003200 RID: 12800 RVA: 0x000D3BCA File Offset: 0x000D1FCA
		public float radius => this.shipPrefab.radius * 0.9f;

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x06003201 RID: 12801 RVA: 0x000D3BDD File Offset: 0x000D1FDD
		private float length
		{
			get
			{
				return this.shipPrefab.length * 0.8f;
			}
		}

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x06003202 RID: 12802 RVA: 0x000D3BF0 File Offset: 0x000D1FF0
		private float speed
		{
			get
			{
				return this.shipPrefab.speed;
			}
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x06003203 RID: 12803 RVA: 0x000D3BFD File Offset: 0x000D1FFD
		public ShipLoad randomLoad => this.shipLoads[UnityEngine.Random.Range(0, this.shipLoads.Count)];

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x06003204 RID: 12804 RVA: 0x000D3C1B File Offset: 0x000D201B
		public int bounty
		{
			get
			{
				return this.shipLoads.Sum((ShipLoad x) => x.bounty);
			}
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06003205 RID: 12805 RVA: 0x000D3C45 File Offset: 0x000D2045
		public int agentCount
		{
			get
			{
				return this.shipLoads.Sum((ShipLoad x) => x.count);
			}
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x06003206 RID: 12806 RVA: 0x000D3C6F File Offset: 0x000D206F
		public float agentArea
		{
			get
			{
				return this.shipLoads.Sum((ShipLoad x) => x.area);
			}
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x06003207 RID: 12807 RVA: 0x000D3C99 File Offset: 0x000D2099
		public float startTime
		{
			get
			{
				return this.shipGroup.startTime + this.timeOffset * this.shipGroup.wave.timeSpreadShip;
			}
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x06003208 RID: 12808 RVA: 0x000D3CBE File Offset: 0x000D20BE
		public Vector3 pos
		{
			get
			{
				return base.transform.position;
			}
		}

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x06003209 RID: 12809 RVA: 0x000D3CCB File Offset: 0x000D20CB
		// (set) Token: 0x0600320A RID: 12810 RVA: 0x000D3CD3 File Offset: 0x000D20D3
		public Longship spawnedShip { get; private set; }

		// Token: 0x0600320B RID: 12811 RVA: 0x000D3CDC File Offset: 0x000D20DC
		public void Reset()
		{
			if (this.spawnedShip)
			{
				UnityEngine.Object.Destroy(this.spawnedShip.gameObject);
			}
			this.spawnedShip = null;
		}

		// Token: 0x0600320C RID: 12812 RVA: 0x000D3D05 File Offset: 0x000D2105
		public Landing Duplicate()
		{
			return this.shipGroup.AddLanding(Instantiate(base.gameObject, base.transform.parent).GetComponent<Landing>());
		}

		// Token: 0x0600320D RID: 12813 RVA: 0x000D3D2D File Offset: 0x000D212D
		public void Init(Island island)
		{
			this.island = island;
			this.timeOffset = UnityEngine.Random.value;
			base.gameObject.layer = LayerMaster.landings.id;
		}

		// Token: 0x0600320E RID: 12814 RVA: 0x000D3D58 File Offset: 0x000D2158
		public void AddShipLoad(ShipLoad shipLoad)
		{
			if (shipLoad.landing)
			{
				shipLoad.landing.shipLoads.Remove(shipLoad);
			}
			this.shipLoads.Add(shipLoad);
			shipLoad.landing = this;
			shipLoad.transform.SetParent(base.transform);
		}

		// Token: 0x0600320F RID: 12815 RVA: 0x000D3DAC File Offset: 0x000D21AC
		public bool TryPlace(NavPos navPos, Vector3 dir, float speedMultiplier, List<Landing> existingLandings)
		{
			dir = dir.GetZeroY().normalized;
			Vector3 zeroY = navPos.pos.GetZeroY();
			if (!this.island)
			{
				Debug.Log("No island");
			}
			Landing.ShipTravel shipTravel = new Landing.ShipTravel(zeroY, -dir, this.speed * speedMultiplier, this.speed, this.island.fog.capsuleCollider);
			Vector3 startPos = shipTravel.startPos.SetY(this.startTime * 0.1f);
			Vector3 endPos = shipTravel.endPos.SetY((this.startTime + shipTravel.duration) * 0.1f);
			this.moveCube = new Landing.ColCube(startPos, endPos, this.radius, this.length);
			foreach (Landing landing in existingLandings)
			{
				if (this.moveCube.CheckBox(landing.moveCube))
				{
					return false;
				}
				if (this.moveCube.CheckBox(landing.standCube))
				{
					return false;
				}
			}
			this.standCube = new Landing.ColCube(dir, zeroY, this.length, this.radius, endPos.y, 40f);
			foreach (Landing landing2 in existingLandings)
			{
				if (this.standCube.CheckBox(landing2.moveCube))
				{
					return false;
				}
				if (this.standCube.CheckBox(landing2.standCube))
				{
					return false;
				}
			}
			Ray ray = new Ray(zeroY + dir * shipTravel.distance, -dir);
			if (Physics.SphereCast(ray, this.radius, shipTravel.distance - this.radius * 2f, LayerMaster.moduleMask))
			{
				return false;
			}
			this.shipTravel = shipTravel;
			this.navPos = navPos;
			base.transform.position = zeroY;
			base.transform.rotation = Quaternion.LookRotation(dir);
			this.dir = dir;
			existingLandings.Add(this);
			this.placed = true;
			return true;
		}

		// Token: 0x06003210 RID: 12816 RVA: 0x000D4034 File Offset: 0x000D2434
		public Longship Spawn()
		{
			base.gameObject.SetActive(false);
			Longship component = UnityEngine.Object.Instantiate<GameObject>(this.shipPrefab.gameObject, this.pos, Quaternion.LookRotation(this.dir), base.transform).GetComponent<Longship>();
			component.transform.Rotate(0f, 180f, 0f);
			component.Setup(this);
			Squad squad = this.shipGroup.squad;
			Bounds bounds = component.navigationMesh.bounds;
			int num = 0;
			foreach (ShipLoad shipLoad in this.shipLoads)
			{
				num += shipLoad.count;
			}
			int num2 = 0;
			foreach (ShipLoad shipLoad2 in this.shipLoads)
			{
				for (int i = 0; i < shipLoad2.count; i++)
				{
					num2++;
					Vector3 zero = Vector3.zero;
					zero.z = ExtraMath.RemapValue((float)num2 + 0.5f, 0f, (float)num, -bounds.extents.z, bounds.extents.z);
					zero.x = UnityEngine.Random.Range(-bounds.extents.x, bounds.extents.x);
					NavPos navPos = component.navPos;
					navPos.pos = zero;
					Agent agent = squad.CreateAgent(shipLoad2.vikingRef.agent, navPos);
					agent.GetOrAddComponent<Pirate>().AddToLongship(component);
					agent.GetOrAddComponent<VikingAgent>().vikingReference = shipLoad2.vikingRef;
				}
			}
			component.GetComponentInChildren<CorpseManager>().corpseCount = component.agents.Count;
			component.GetComponentInChildren<CorpseManager>().Precache();
			this.spawnedShip = component;
			ExtraRenderers.InitializeAll(component.transform);
			BatchedSprite[] componentsInChildren = base.GetComponentsInChildren<BatchedSprite>(true);
			foreach (BatchedSprite batchedSprite in componentsInChildren)
			{
				batchedSprite.Awake();
			}
			return component;
		}

		// Token: 0x06003211 RID: 12817 RVA: 0x000D4294 File Offset: 0x000D2694
		public Longship Launch()
		{
			base.gameObject.SetActive(true);
			return this.spawnedShip;
		}

		// Token: 0x06003212 RID: 12818 RVA: 0x000D42A8 File Offset: 0x000D26A8
		public void OnDrawGizmos()
		{
			if (!this.shipPrefab)
			{
				return;
			}
			Gizmos.color = this.color;
			Gizmos.matrix = base.transform.localToWorldMatrix;
			Gizmos.DrawSphere(Vector3.zero, 0.1f);
			Gizmos.DrawLine(new Vector3(this.radius, 0f, 0f), new Vector3(0f, 0f, -this.radius));
			Gizmos.DrawLine(new Vector3(this.radius, 0f, 0f), new Vector3(this.radius, 0f, this.length));
			Gizmos.DrawLine(new Vector3(this.radius, 0f, this.length), new Vector3(0f, 0f, this.length + this.radius));
			Gizmos.DrawLine(new Vector3(-this.radius, 0f, 0f), new Vector3(0f, 0f, -this.radius));
			Gizmos.DrawLine(new Vector3(-this.radius, 0f, 0f), new Vector3(-this.radius, 0f, this.length));
			Gizmos.DrawLine(new Vector3(-this.radius, 0f, this.length), new Vector3(0f, 0f, this.length + this.radius));
			Gizmos.matrix = this.standCube.mat;
			Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
			Gizmos.matrix = this.moveCube.mat;
			Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
		}

		// Token: 0x040021D8 RID: 8664
		public Longship shipPrefab;

		// Token: 0x040021D9 RID: 8665
		public ShipGroup shipGroup;

		// Token: 0x040021DA RID: 8666
		public List<ShipLoad> shipLoads = new List<ShipLoad>();

		// Token: 0x040021DB RID: 8667
		public float timeOffset;

		// Token: 0x040021DC RID: 8668
		private const float timeScale = 0.1f;

		// Token: 0x040021DD RID: 8669
		public NavPos navPos;

		// Token: 0x040021DE RID: 8670
		public Vector3 dir;

		// Token: 0x040021DF RID: 8671
		public bool placed;

		// Token: 0x040021E0 RID: 8672
		public Color color = Color.yellow;

		// Token: 0x040021E1 RID: 8673
		public ShipTravel shipTravel;

		// Token: 0x040021E3 RID: 8675
		[SerializeField]
		[HideInInspector]
		private Island island;

		// Token: 0x040021E4 RID: 8676
		private Landing.ColCube moveCube;

		// Token: 0x040021E5 RID: 8677
		private Landing.ColCube standCube;

		// Token: 0x02000793 RID: 1939
		public struct ShipTravel
		{
			// Token: 0x06003216 RID: 12822 RVA: 0x000D4478 File Offset: 0x000D2878
			public ShipTravel(Vector3 pos, Vector3 direction, float speed, float fleeSpeed, CapsuleCollider col)
			{
				float num = 50f;
				this.endPos = pos;
				this.startPos = this.endPos - direction * num;
				Ray ray = new Ray(this.startPos, direction);
				RaycastHit raycastHit;
				if (col.Raycast(ray, out raycastHit, num))
				{
					this.startPos = raycastHit.point;
				}
				else
				{
					this.startPos = this.endPos - direction * 6f;
				}
				this.distance = Vector3.Distance(this.startPos, this.endPos);
				this.duration = this.distance / speed;
				this.fleeDuration = this.distance / fleeSpeed;
			}

			// Token: 0x040021E9 RID: 8681
			public readonly Vector3 startPos;

			// Token: 0x040021EA RID: 8682
			public readonly Vector3 endPos;

			// Token: 0x040021EB RID: 8683
			public readonly float duration;

			// Token: 0x040021EC RID: 8684
			public readonly float fleeDuration;

			// Token: 0x040021ED RID: 8685
			public readonly float distance;
		}

		// Token: 0x02000794 RID: 1940
		private struct ColCube
		{
			// Token: 0x06003217 RID: 12823 RVA: 0x000D452C File Offset: 0x000D292C
			public ColCube(Vector3 dir, Vector3 pos, float length, float radius, float minY, float maxY)
			{
				Quaternion q = Quaternion.LookRotation(dir);
				this.mat = Matrix4x4.TRS(pos, q, Vector3.one);
				pos = new Vector3(0f, 0f, length / 2f);
				pos = this.mat.MultiplyPoint(pos);
				pos.y = (minY + maxY) / 2f;
				Vector3 a = new Vector3(radius, (maxY - minY) / 2f, length / 2f);
				this.mat = Matrix4x4.TRS(pos, q, a * 2f);
			}

			// Token: 0x06003218 RID: 12824 RVA: 0x000D45C0 File Offset: 0x000D29C0
			public ColCube(Vector3 startPos, Vector3 endPos, float radius, float length)
			{
				Quaternion quaternion = Quaternion.LookRotation(endPos - startPos);
				float num = Vector3.Distance(startPos, endPos);
				float y = (quaternion * Vector3.forward * length).y;
				Vector3 vector = (startPos + endPos) / 2f;
				vector += quaternion * Vector3.up * y / 2f;
				Vector3 a = new Vector3(radius, y / 2f, num / 2f);
				this.mat = Matrix4x4.TRS(vector, quaternion, a * 2f);
			}

			// Token: 0x06003219 RID: 12825 RVA: 0x000D4664 File Offset: 0x000D2A64
			public bool CheckBox(Landing.ColCube colCube)
			{
				return Intersections.OrientedBoxBox(this.mat, colCube.mat);
			}

			// Token: 0x040021EE RID: 8686
			public Matrix4x4 mat;
		}
	}
}
