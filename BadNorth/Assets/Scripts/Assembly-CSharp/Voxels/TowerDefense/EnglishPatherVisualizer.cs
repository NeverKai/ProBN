using System.Collections.Generic;
using RTM.Utilities;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020007A8 RID: 1960
	public class EnglishPatherVisualizer : SquadComponent, NavSpot.IHighlight, ISquadSetup
	{
		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x060032C0 RID: 12992 RVA: 0x000D7A8A File Offset: 0x000D5E8A
		Color NavSpot.IHighlight.highlightColor
		{
			get
			{
				return this.highlightColor;
			}
		}

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x060032C1 RID: 12993 RVA: 0x000D7A92 File Offset: 0x000D5E92
		private EnglishSquad enSquad
		{
			get
			{
				return this._enSquad;
			}
		}

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x060032C2 RID: 12994 RVA: 0x000D7A9F File Offset: 0x000D5E9F
		// (set) Token: 0x060032C3 RID: 12995 RVA: 0x000D7AA8 File Offset: 0x000D5EA8
		public NavSpot navSpot
		{
			get
			{
				return this._navSpot;
			}
			set
			{
				if (this._navSpot == value)
				{
					return;
				}
				if (this._navSpot)
				{
					this._navSpot.RemoveHighlight(this);
				}
				this._navSpot = value;
				if (this._navSpot)
				{
					this._navSpot.AddHighlight(this);
				}
			}
		}

		// Token: 0x060032C4 RID: 12996 RVA: 0x000D7B08 File Offset: 0x000D5F08
		private void LateUpdate()
		{
			float num;
			if (this.enSquad.agents.Count == 0)
			{
				num = 0f;
			}
			else
			{
				num = float.MaxValue;
				for (int i = 0; i < this.enSquad.agents.Count; i++)
				{
					num = Mathf.Min(num, this.enSquad.agents[i].orderDist);
				}
				if (this.enSquad.standard)
				{
					num = (this.enSquad.standard.agent.orderDist + num) / 2f;
				}
			}
			if (num < this.navDistance)
			{
				this.navDistance = Mathf.Lerp(this.navDistance, num, Time.unscaledDeltaTime * 10f);
			}
			if (this.enSquad.isSelected)
			{
				this.stateAlpha = new Vector2(1f, 0.8f);
			}
			else if (EnglishSquad.selected == null)
			{
				this.stateAlpha = Vector2.zero;
			}
			else
			{
				this.stateAlpha = new Vector2(1f, 0f);
			}
			this.distanceAlpha = new Vector3(1f, 0.2f) * ExtraMath.RemapValue(this.navDistance, 0.5f, 1f);
			float b = (float)((!this.enSquad.isSelected) ? 0 : 1);
			this.alpha = Vector2.Lerp(this.alpha, ExtraMath.Screen(this.stateAlpha, this.distanceAlpha), Time.unscaledDeltaTime * 10f);
			this.radius = Mathf.Lerp(this.radius, b, Time.unscaledDeltaTime * 10f);
			this.highlightColor.a = this.alpha.y;
			this.pathEnd.gameObject.SetActive(this.alpha != Vector2.zero);
			this.path.gameObject.SetActive(this.navDistance > 0.01f);
			if (this.pathEnd.gameObject.activeSelf)
			{
				this.pathEndBlock.SetFloat(this.outlineId, this.alpha.x);
				this.pathEndBlock.SetFloat(this.fillId, this.alpha.y);
				this.pathEndBlock.SetFloat(this.shadeId, this.alpha.y * 0.3f);
				this.pathEndMr.SetPropertyBlock(this.pathEndBlock);
			}
			if (this.path.gameObject.activeSelf)
			{
				this.pathBlock.SetFloat(this.navDistId, this.navDistance);
				this.pathBlock.SetFloat(this.fillId, this.alpha.x);
				this.pathBlock.SetFloat(this.shadeId, this.alpha.x * 0.3f);
				this.pathBlock.SetFloat(this.radiusId, this.radius);
				this.pathMr.SetPropertyBlock(this.pathBlock);
			}
		}

		// Token: 0x060032C5 RID: 12997 RVA: 0x000D7E54 File Offset: 0x000D6254
		private void Start()
		{
			this.highlightColor = this.enSquad.hero.color * 1.2f;
			this.highlightColor.a = 1f;
			this.pathEndBlock = new MaterialPropertyBlock();
			this.pathBlock = new MaterialPropertyBlock();
			this.pathEndBlock.SetColor(ShaderId.colorId, this.highlightColor);
			this.pathBlock.SetColor(ShaderId.colorId, this.highlightColor);
			this.pathEndMr.SetPropertyBlock(this.pathEndBlock);
			this.pathMr.SetPropertyBlock(this.pathEndBlock);
		}

		// Token: 0x060032C6 RID: 12998 RVA: 0x000D7F00 File Offset: 0x000D6300
		void ISquadSetup.SquadSetup(Squad squad)
		{
			this._enSquad.Target = (squad as EnglishSquad);
			EnglishPatherSquad pather = this.enSquad.pather;
			this.enSquad.onAllDead += delegate()
			{
				this.navSpot = null;
			};
			this.pathEndMf = this.pathEnd.GetComponentInChildren<MeshFilter>();
			this.pathEndMr = this.pathEnd.GetComponentInChildren<MeshRenderer>();
			this.pathMf = this.path.GetComponent<MeshFilter>();
			this.pathMr = this.path.GetComponent<MeshRenderer>();
			this.pathMf.sharedMesh = this.GetMesh();
		}

		// Token: 0x060032C7 RID: 12999 RVA: 0x000D7F96 File Offset: 0x000D6396
		private void SetPathEnd(IPathTarget target)
		{
			this.pathEnd.transform.position = target.endPointPosition + EnglishPatherVisualizer.offset;
			this.pathEndMf.sharedMesh = target.endPointMesh;
			this.navSpot = (target as NavSpot);
		}

		// Token: 0x060032C8 RID: 13000 RVA: 0x000D7FD8 File Offset: 0x000D63D8
		public void OnNewTarget(IPathTarget target, bool showPath)
		{
			this.SetPathEnd(target);
			List<Vector3> list = ListPool<Vector3>.GetList(1024);
			List<Vector2> list2 = ListPool<Vector2>.GetList(1024);
			List<Vector2> list3 = ListPool<Vector2>.GetList(1024);
			int num = 0;
			if (showPath)
			{
				NavPos navPos;
				if (this.enSquad.standard)
				{
					if (this.enSquad.standard.agent.navPos.valid)
					{
						navPos = this.enSquad.standard.agent.navPos;
					}
					else
					{
						navPos = new NavPos(this.enSquad.faction.island.navManager.navigationMesh, this.enSquad.standard.transform.position, true, 1f);
					}
				}
				else
				{
					navPos = this.enSquad.GetAverageNavPos();
				}
				this.navDistance = target.GetDistanceFrom(navPos);
				float num2 = 0f;
				for (int i = 0; i < 1024; i++)
				{
					Vector3 pos = navPos.pos;
					Vector3 normal = navPos.GetNormal();
					Vector3 zero = Vector3.zero;
					float num3 = 0f;
					target.SampleDistanceDir(navPos, ref zero, ref num3);
					navPos.pos += zero.normalized * 0.2f;
					if (target.endPointBounds.Contains(navPos.pos) || num3 < 0.2f)
					{
						break;
					}
					num2 += Vector3.Distance(pos, navPos.pos);
					Vector3 normal2 = navPos.GetNormal();
					while (num2 > 0.2f)
					{
						num++;
						num2 -= 0.2f;
						Vector3 vector = Vector3.MoveTowards(navPos.pos, pos, num2);
						vector += EnglishPatherVisualizer.offset;
						for (int j = 0; j < 4; j++)
						{
							list.Add(vector);
							list3.Add(new Vector2(num3 - num2, 0f));
						}
						list2.Add(new Vector2(-1f, 0f));
						list2.Add(new Vector2(0f, -1f));
						list2.Add(new Vector2(1f, 0f));
						list2.Add(new Vector2(0f, 1f));
					}
				}
			}
			for (int k = num; k < 256; k++)
			{
				for (int l = 0; l < 4; l++)
				{
					list.Add(Vector3.zero);
					list2.Add(new Vector2(0f, 0f));
					list3.Add(new Vector2(0f, 0f));
				}
			}
			this.pathMf.sharedMesh.SetVertices(list);
			this.pathMf.sharedMesh.SetUVs(0, list2);
			this.pathMf.sharedMesh.SetUVs(1, list3);
			ListPool<Vector3>.ReturnList(list);
			ListPool<Vector2>.ReturnList(list2);
			ListPool<Vector2>.ReturnList(list3);
		}

		// Token: 0x060032C9 RID: 13001 RVA: 0x000D82EC File Offset: 0x000D66EC
		private Mesh GetMesh()
		{
			if (EnglishPatherVisualizer.meshStack.Count > 0)
			{
				return EnglishPatherVisualizer.meshStack.Pop();
			}
			Mesh mesh = new Mesh();
			EnglishPatherVisualizer.meshIndex++;
			List<Vector3> list = ListPool<Vector3>.GetList(1024);
			List<Vector2> list2 = ListPool<Vector2>.GetList(1024);
			List<Vector2> list3 = ListPool<Vector2>.GetList(1024);
			List<int> list4 = ListPool<int>.GetList(1536);
			for (int i = 0; i < 256; i++)
			{
				list.Add(Vector3.zero);
				list.Add(Vector3.zero);
				list.Add(Vector3.zero);
				list.Add(Vector3.zero);
				list3.Add(Vector2.zero);
				list3.Add(Vector2.zero);
				list3.Add(Vector2.zero);
				list3.Add(Vector2.zero);
				list2.Add(new Vector2(-1f, 0f));
				list2.Add(new Vector2(0f, -1f));
				list2.Add(new Vector2(1f, 0f));
				list2.Add(new Vector2(0f, 1f));
				int num = i * 4;
				list4.Add(num);
				list4.Add(num + 1);
				list4.Add(num + 2);
				list4.Add(num + 2);
				list4.Add(num + 3);
				list4.Add(num);
			}
			mesh.SetVertices(list);
			mesh.SetTriangles(list4, 0);
			mesh.SetUVs(0, list2);
			mesh.SetUVs(1, list3);
			mesh.bounds = new Bounds(new Vector3(0f, (float)EnglishPatherVisualizer.meshIndex, 0f), Vector3.one * 1000f);
			mesh.name = string.Format("PathMesh {0}", EnglishPatherVisualizer.meshIndex);
			ListPool<Vector3>.ReturnList(list);
			ListPool<Vector2>.ReturnList(list2);
			ListPool<Vector2>.ReturnList(list3);
			ListPool<int>.ReturnList(list4);
			return mesh;
		}

		// Token: 0x060032CA RID: 13002 RVA: 0x000D84E0 File Offset: 0x000D68E0
		private void OnDestroy()
		{
			this.navSpot = null;
			if (this.pathMf && this.pathMf.sharedMesh)
			{
				EnglishPatherVisualizer.meshStack.Push(this.pathMf.sharedMesh);
			}
		}

		// Token: 0x04002273 RID: 8819
		[SerializeField]
		private GameObject pathEnd;

		// Token: 0x04002274 RID: 8820
		[SerializeField]
		private GameObject path;

		// Token: 0x04002275 RID: 8821
		private MeshFilter pathEndMf;

		// Token: 0x04002276 RID: 8822
		private MeshFilter pathMf;

		// Token: 0x04002277 RID: 8823
		private MeshRenderer pathEndMr;

		// Token: 0x04002278 RID: 8824
		private MeshRenderer pathMr;

		// Token: 0x04002279 RID: 8825
		private MaterialPropertyBlock pathEndBlock;

		// Token: 0x0400227A RID: 8826
		private MaterialPropertyBlock pathBlock;

		// Token: 0x0400227B RID: 8827
		private ShaderId fillId = "_Fill";

		// Token: 0x0400227C RID: 8828
		private ShaderId outlineId = "_Outline";

		// Token: 0x0400227D RID: 8829
		private ShaderId shadeId = "_Shade";

		// Token: 0x0400227E RID: 8830
		private ShaderId navDistId = "_NavDist";

		// Token: 0x0400227F RID: 8831
		private ShaderId radiusId = "_Radius";

		// Token: 0x04002280 RID: 8832
		private Vector2 distanceAlpha;

		// Token: 0x04002281 RID: 8833
		private Vector2 stateAlpha;

		// Token: 0x04002282 RID: 8834
		private Color highlightColor;

		// Token: 0x04002283 RID: 8835
		private static Vector3 offset = new Vector3(0f, 0.05f, 0f);

		// Token: 0x04002284 RID: 8836
		private static Stack<Mesh> meshStack = new Stack<Mesh>();

		// Token: 0x04002285 RID: 8837
		private static int meshIndex = 0;

		// Token: 0x04002286 RID: 8838
		private const int maxCount = 256;

		// Token: 0x04002287 RID: 8839
		private WeakReference<EnglishSquad> _enSquad = new WeakReference<EnglishSquad>(null);

		// Token: 0x04002288 RID: 8840
		private NavSpot _navSpot;

		// Token: 0x04002289 RID: 8841
		private float navDistance;

		// Token: 0x0400228A RID: 8842
		private Vector2 alpha = Vector2.zero;

		// Token: 0x0400228B RID: 8843
		private float radius;

		// Token: 0x020007A9 RID: 1961
		private class PathNode
		{
			// Token: 0x060032CD RID: 13005 RVA: 0x000D8562 File Offset: 0x000D6962
			public PathNode(GameObject gameObject)
			{
				this.gameObject = gameObject;
			}

			// Token: 0x0400228C RID: 8844
			public NavPos navPos;

			// Token: 0x0400228D RID: 8845
			public GameObject gameObject;

			// Token: 0x0400228E RID: 8846
			public float distance;
		}
	}
}
