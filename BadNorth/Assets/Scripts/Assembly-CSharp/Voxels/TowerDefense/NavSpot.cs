using System.Collections;
using System.Collections.Generic;
using MeshModifer;
using RTM.Utilities;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x02000664 RID: 1636
	[SelectionBase]
	public class NavSpot : MonoBehaviour, IPathTarget
	{
		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x060029B9 RID: 10681 RVA: 0x00093539 File Offset: 0x00091939
		// (set) Token: 0x060029BA RID: 10682 RVA: 0x00093541 File Offset: 0x00091941
		public NavSpotter navSpotter { get; private set; }

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x060029BB RID: 10683 RVA: 0x0009354A File Offset: 0x0009194A
		// (set) Token: 0x060029BC RID: 10684 RVA: 0x00093557 File Offset: 0x00091957
		public NavSpotController occupant
		{
			get
			{
				return this._occupant.Target;
			}
			set
			{
				this._occupant.Target = value;
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x060029BD RID: 10685 RVA: 0x00093565 File Offset: 0x00091965
		public bool isOccupied
		{
			get
			{
				return this.occupant != null;
			}
		}

		// Token: 0x060029BE RID: 10686 RVA: 0x00093573 File Offset: 0x00091973
		public void ClearHighlights()
		{
			this.highlights.Clear();
		}

		// Token: 0x060029BF RID: 10687 RVA: 0x00093580 File Offset: 0x00091980
		public void AddHighlight(NavSpot.IHighlight highlight)
		{
			if (!this.highlights.Contains(highlight))
			{
				this.highlights.Add(highlight);
			}
		}

		// Token: 0x060029C0 RID: 10688 RVA: 0x0009359F File Offset: 0x0009199F
		public bool RemoveHighlight(NavSpot.IHighlight highlight)
		{
			return this.highlights.Remove(highlight);
		}

		// Token: 0x060029C1 RID: 10689 RVA: 0x000935B0 File Offset: 0x000919B0
		public Color GetHighlightColor()
		{
			Color color = Color.clear;
			foreach (NavSpot.IHighlight highlight in this.highlights)
			{
				Color highlightColor = highlight.highlightColor;
				color = Color.Lerp(color, highlightColor.SetA(1f), highlightColor.a);
			}
			return color;
		}

		// Token: 0x060029C2 RID: 10690 RVA: 0x0009362C File Offset: 0x00091A2C
		public void Setup(Vert vert, Mesh meshPrefab, NavSpotter navSpotter, int index)
		{
			this.navSpotter = navSpotter;
			this.vert = vert;
			this.index = index;
			this.navPos = new NavPos(vert);
			this.distanceField = new DistanceField(this.navPos.navigationMesh, (int)vert.index, "NavSpot");
			base.transform.position = this.navPos.pos;
			this.normalizedPos = this.navPos.pos.normalized;
			Island island = vert.tris[0].navigationMesh.island;
			this.lookDir = vert.normal.GetZeroY();
			this.lookDir += this.navPos.pos / island.size.x;
			this.lookDir += island.voxelSpace.GetNormalLinear(vert.pos);
			this.lookDir = this.lookDir.GetZeroY().normalized;
			if (this.lookDir == Vector3.zero)
			{
				this.lookDir = Vector3.forward;
			}
			base.enabled = false;
			this.coroutine = this.MeshEnumerator();
			while (this.coroutine.MoveNext())
			{
			}
		}

		
		// Token: 0x060029C3 RID: 10691 RVA: 0x00093788 File Offset: 0x00091B88
		private IEnumerator MeshEnumerator()
		{
			this.bounds = new Bounds(base.transform.position, Vector3.one * 0.9f);
			this.mesh = this.navPos.navigationMesh.island.levelNode.campaign.meshPool.GetMesh();
			for (int j = 0; j < this.meshFilters.Length; j++)
			{
				this.meshFilters[j].sharedMesh = this.mesh;
			}
			int count = 16;
			Vector3[] directions = new Vector3[count];
			NavPos[] navs = new NavPos[count];
			NavPos[] centerNavs = new NavPos[count / 2];
			for (int k = 0; k < count; k++)
			{
				navs[k] = this.navPos;
				directions[k] = Quaternion.Euler(0f, ((float)k + 0.5f) * 360f / (float)count, 0f) * Vector3.forward * 0.7f;
			}
			yield return null;
			for (int i = 0; i < 30; i++)
			{
				for (int l = 0; l < count; l++)
				{
					int num = l;
					int num2 = (l + 1) % count;
					int num3 = (l + 2) % count;
					NavPos navPos = navs[num];
					NavPos navPos2 = navs[num2];
					NavPos navPos3 = navs[num3];
					Vector3 vector = navPos2.pos;
					vector = Vector3.MoveTowards(vector, this.navPos.pos + directions[l], 0.04f);
					vector = Vector3.Lerp(vector, (navPos.pos + navPos3.pos) / 2f, 0.4f);
					if (!this.bounds.Contains(vector))
					{
						vector = this.bounds.ClosestPoint(vector);
					}
					navs[num2].pos = vector;
				}
				yield return null;
			}
			for (int m = 0; m < count; m += 2)
			{
				centerNavs[m / 2] = this.navPos;
				centerNavs[m / 2].pos = (navs[m + 1].pos + this.navPos.pos) / 2f;
			}
			MeshBuilder meshBuilder = new MeshBuilder(base.transform.position, false);
			for (int n = 0; n < navs.Length; n++)
			{
				this.AddTriangle(meshBuilder, 1f, 1f, 0.5f, navs[n], navs[(n + 1) % count], centerNavs[n / 2]);
			}
			for (int num4 = 0; num4 < centerNavs.Length; num4++)
			{
				this.AddTriangle(meshBuilder, 0.5f, 0.5f, 1f, centerNavs[(num4 + 1) % centerNavs.Length], centerNavs[num4], navs[(num4 * 2 + 2) % count]);
			}
			for (int num5 = 0; num5 < centerNavs.Length; num5++)
			{
				this.AddTriangle(meshBuilder, 0.5f, 0.5f, 0f, centerNavs[num5], centerNavs[(num5 + 1) % centerNavs.Length], this.navPos);
			}
			meshBuilder.ApplyToMesh(this.mesh);
			Vector3[] mVerts = this.mesh.vertices;
			Vector2[] mUvs = this.mesh.uv;
			for (int num6 = 0; num6 < mVerts.Length; num6++)
			{
				mUvs[num6] = new Vector2(mVerts[num6].x, mVerts[num6].z) + Vector2.one / 2f;
			}
			this.mesh.uv2 = mUvs;
			BoxCollider collider = base.gameObject.AddComponent<BoxCollider>();
			collider.center = this.mesh.bounds.center;
			collider.size = this.mesh.bounds.size + Vector3.one * 0.1f;
			this.meshBounds = this.mesh.bounds;
			this.meshBounds.center = this.meshBounds.center + base.transform.position;
			yield return null;
			yield break;
		}

		// Token: 0x060029C4 RID: 10692 RVA: 0x000937A4 File Offset: 0x00091BA4
		private void AddTriangle(MeshBuilder mb, float a0, float a1, float a2, NavPos n0, NavPos n1, NavPos n2)
		{
			float[] array = new float[]
			{
				a0,
				a1,
				a2
			};
			NavPos[] array2 = new NavPos[]
			{
				n0,
				n1,
				n2
			};
			Triangle triangle = new Triangle();
			for (int i = 0; i < 3; i++)
			{
				NavPos navPos = array2[i];
				Vertex vertex = new Vertex(navPos.pos, navPos.GetNormal(), new Vector2(0f, array[i]), Color.white);
				triangle.verts[i] = vertex;
			}
			mb.AddTriangle(triangle);
		}

		// Token: 0x060029C5 RID: 10693 RVA: 0x00093864 File Offset: 0x00091C64
		public void Destroy()
		{
			this.navSpotter.island.meshPool.ReturnMesh(ref this.mesh);
			this.mesh = null;
			this.vert = null;
			this.navSpotter = null;
			this.meshFilters = null;
			this.animator = null;
			this.distanceField = null;
			this.coroutine = null;
			this.highlights.Clear();
			for (int i = 0; i < this.neighbours.Length; i++)
			{
				this.neighbours[i] = null;
			}
			this.neighbours = null;
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x060029C6 RID: 10694 RVA: 0x000938FC File Offset: 0x00091CFC
		private void OnDrawGizmosSelected()
		{
			Gizmos.matrix = ExtraGizmos.CloserToCameraMatrix();
			Gizmos.color = Color.yellow;
			this.distanceField.DrawGizmos();
			Gizmos.color = Color.red;
			for (int i = 0; i < this.neighbours.Length; i++)
			{
				NavSpot navSpot = this;
				while (navSpot)
				{
					NavSpot navSpot2 = navSpot.neighbours[i];
					if (!navSpot2)
					{
						break;
					}
					Gizmos.DrawLine(navSpot.vert.pos, navSpot2.vert.pos);
					navSpot = navSpot2;
				}
			}
		}

		// Token: 0x060029C7 RID: 10695 RVA: 0x00093993 File Offset: 0x00091D93
		private static int GetNeighbourIndex(Vector3 diff)
		{
			return (Mathf.RoundToInt(Mathf.Atan2(diff.x, diff.z) * 4f / 3.1415927f) + 8) % 8;
		}

		// Token: 0x060029C8 RID: 10696 RVA: 0x000939C0 File Offset: 0x00091DC0
		public static void MaybeSetupNeighboirs(NavSpot navSpot0, NavSpot navSpot1)
		{
			Vector3 vector = navSpot0.vert.pos - navSpot1.vert.pos;
			Vector3 vector2 = ExtraMath.Abs(vector);
			if (vector2.x > 1.2f || vector2.z > 1.2f || vector2.y > 0.7f)
			{
				return;
			}
			int neighbourIndex = NavSpot.GetNeighbourIndex(vector);
			if (navSpot0.navPos.TriCast(navSpot1.navPos))
			{
				navSpot0.neighbours[neighbourIndex] = navSpot1;
				navSpot1.neighbours[(neighbourIndex + 4) % 8] = navSpot0;
			}
		}

		// Token: 0x060029C9 RID: 10697 RVA: 0x00093A58 File Offset: 0x00091E58
		public static bool DiagonalSpotCast(NavSpot navSpot0, NavSpot navSpot1)
		{
			int neighbourIndex = NavSpot.GetNeighbourIndex(navSpot0.vert.pos - navSpot1.vert.pos);
			while (navSpot0)
			{
				if (navSpot0 == navSpot1)
				{
					return true;
				}
				navSpot0 = navSpot0.neighbours[neighbourIndex];
			}
			return false;
		}

		// Token: 0x060029CA RID: 10698 RVA: 0x00093AAF File Offset: 0x00091EAF
		public bool DiagonalSpotCast(NavSpot target)
		{
			return NavSpot.DiagonalSpotCast(this, target);
		}

		// Token: 0x060029CB RID: 10699 RVA: 0x00093AB8 File Offset: 0x00091EB8
		public static NavSpot GetNavSpot(Vector3 pos, bool includeOccupied)
		{
			return Singleton<CampaignManager>.instance.campaign.currentLevel.island.navSpotter.GetNavSpot(pos, includeOccupied);
		}

		// Token: 0x060029CC RID: 10700 RVA: 0x00093ADA File Offset: 0x00091EDA
		public static NavSpot NavSpotCast(Vector2 screenPos)
		{
			return Singleton<CampaignManager>.instance.campaign.currentLevel.island.navSpotter.NavSpotCast(screenPos);
		}

		// Token: 0x060029CD RID: 10701 RVA: 0x00093AFB File Offset: 0x00091EFB
		public static NavSpot NavSpotCast(Vector2 screenPos, out RaycastHit hit)
		{
			return Singleton<CampaignManager>.instance.campaign.currentLevel.island.navSpotter.NavSpotCast(screenPos, out hit);
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x060029CE RID: 10702 RVA: 0x00093B1D File Offset: 0x00091F1D
		NavPos IPathTarget.navPos
		{
			get
			{
				return this.navPos;
			}
		}

		// Token: 0x060029CF RID: 10703 RVA: 0x00093B25 File Offset: 0x00091F25
		float IPathTarget.GetDistanceFrom(NavPos navPos)
		{
			return this.distanceField.SampleDistance(navPos);
		}

		// Token: 0x060029D0 RID: 10704 RVA: 0x00093B33 File Offset: 0x00091F33
		void IPathTarget.SampleDistanceDir(NavPos navPos, ref Vector3 dir, ref float dist)
		{
			this.distanceField.Sample(navPos, ref dir, ref dist);
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x060029D1 RID: 10705 RVA: 0x00093B43 File Offset: 0x00091F43
		Bounds IPathTarget.endPointBounds
		{
			get
			{
				return this.meshBounds;
			}
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x060029D2 RID: 10706 RVA: 0x00093B4B File Offset: 0x00091F4B
		Vector3 IPathTarget.endPointPosition
		{
			get
			{
				return base.transform.position;
			}
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x060029D3 RID: 10707 RVA: 0x00093B58 File Offset: 0x00091F58
		Mesh IPathTarget.endPointMesh
		{
			get
			{
				return this.mesh;
			}
		}

		// Token: 0x04001B23 RID: 6947
		public Mesh mesh;

		// Token: 0x04001B24 RID: 6948
		public Vert vert;

		// Token: 0x04001B26 RID: 6950
		public NavPos navPos;

		// Token: 0x04001B27 RID: 6951
		public Vector3 lookDir;

		// Token: 0x04001B28 RID: 6952
		public Vector3 normal;

		// Token: 0x04001B29 RID: 6953
		public Vector3 normalizedPos;

		// Token: 0x04001B2A RID: 6954
		public int index;

		// Token: 0x04001B2B RID: 6955
		public int pixelIndex;

		// Token: 0x04001B2C RID: 6956
		public NavSpot[] neighbours = new NavSpot[8];

		// Token: 0x04001B2D RID: 6957
		private WeakReference<NavSpotController> _occupant = new WeakReference<NavSpotController>(null);

		// Token: 0x04001B2E RID: 6958
		public MeshFilter[] meshFilters;

		// Token: 0x04001B2F RID: 6959
		public Animator animator;

		// Token: 0x04001B30 RID: 6960
		public Bounds bounds;

		// Token: 0x04001B31 RID: 6961
		public Bounds meshBounds;

		// Token: 0x04001B32 RID: 6962
		public DistanceField distanceField;

		// Token: 0x04001B33 RID: 6963
		public static Vector3[] corners = new Vector3[]
		{
			new Vector3(-1f, 0f, -1f) / 2f,
			new Vector3(1f, 0f, -1f) / 2f,
			new Vector3(-1f, 0f, 1f) / 2f,
			new Vector3(1f, 0f, 1f) / 2f
		};

		// Token: 0x04001B34 RID: 6964
		private IEnumerator coroutine;

		// Token: 0x04001B35 RID: 6965
		private List<NavSpot.IHighlight> highlights = new List<NavSpot.IHighlight>();

		// Token: 0x02000665 RID: 1637
		public interface IHighlight
		{
			// Token: 0x170005AC RID: 1452
			// (get) Token: 0x060029D5 RID: 10709
			Color highlightColor { get; }
		}
	}
}
