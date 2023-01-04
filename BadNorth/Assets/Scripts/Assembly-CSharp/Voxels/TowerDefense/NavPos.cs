using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x0200065F RID: 1631
	public struct NavPos
	{
		// Token: 0x06002978 RID: 10616 RVA: 0x000918E8 File Offset: 0x0008FCE8
		public NavPos(NavigationMesh navMesh, Vector3 worldPos, bool world = true, float yScale = 1f)
		{
			this._tri = null;
			this._y = 0f;
			this._posXZ = Vector2.zero;
			this._bary = Vector3.zero;
			this._hasBary = false;
			this._hasY = false;
			float num = float.PositiveInfinity;
			if (world)
			{
				worldPos = navMesh.transform.InverseTransformPoint(worldPos);
			}
			for (int i = 0; i < navMesh.tris.Length; i++)
			{
				Tri tri = navMesh.tris[i];
				Vector3 vector = tri.pos - worldPos;
				vector.y *= yScale;
				float num2 = Vector3.SqrMagnitude(vector);
				if (num2 < num)
				{
					num = num2;
					this._tri = tri;
					this._posXZ = this._tri.pos.GetXZ();
				}
			}
			this.MoveTo(worldPos);
		}

		// Token: 0x06002979 RID: 10617 RVA: 0x000919BC File Offset: 0x0008FDBC
		public NavPos(Vert vert)
		{
			this._tri = vert.tris[0];
			this._posXZ = vert.pos.GetXZ();
			this._bary = Vector3.zero;
			this._y = vert.pos.y;
			this._hasBary = false;
			this._hasY = true;
		}

		// Token: 0x0600297A RID: 10618 RVA: 0x00091A18 File Offset: 0x0008FE18
		public NavPos(Vert vert, Vector3 pos)
		{
			this._tri = vert.tris[0];
			this._posXZ = vert.pos.GetXZ();
			this._bary = Vector3.zero;
			this._y = 0f;
			this._hasBary = false;
			this._hasY = false;
			this.MoveTo(pos);
		}

		// Token: 0x0600297B RID: 10619 RVA: 0x00091A74 File Offset: 0x0008FE74
		public NavPos(Edge edge)
		{
			this._tri = edge.tris[0];
			this._posXZ = edge.pos.GetXZ();
			this._bary = Vector3.zero;
			this._y = 0f;
			this._hasBary = false;
			this._hasY = false;
		}

		// Token: 0x0600297C RID: 10620 RVA: 0x00091AC8 File Offset: 0x0008FEC8
		public NavPos(Tri tri)
		{
			this._tri = tri;
			this._posXZ = tri.pos.GetXZ();
			this._y = 0f;
			this._bary = Vector3.zero;
			this._hasBary = false;
			this._hasY = false;
		}

		// Token: 0x0600297D RID: 10621 RVA: 0x00091B08 File Offset: 0x0008FF08
		public NavPos(Tri tri, Vector3 worldPos)
		{
			this._tri = tri;
			this._posXZ = tri.pos.GetXZ();
			this._y = 0f;
			this._bary = Vector3.zero;
			this._hasBary = false;
			this._hasY = false;
			this.MoveTo(worldPos);
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x0600297E RID: 10622 RVA: 0x00091B59 File Offset: 0x0008FF59
		public Tri tri
		{
			get
			{
				return this._tri;
			}
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x0600297F RID: 10623 RVA: 0x00091B61 File Offset: 0x0008FF61
		public Vector2 posXZ
		{
			get
			{
				return this._posXZ;
			}
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06002980 RID: 10624 RVA: 0x00091B69 File Offset: 0x0008FF69
		public Vector3 bary
		{
			get
			{
				if (!this._hasBary)
				{
					this._bary = this.tri.WorldToBary(this._posXZ);
					this._hasBary = true;
				}
				return this._bary;
			}
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06002981 RID: 10625 RVA: 0x00091B9C File Offset: 0x0008FF9C
		// (set) Token: 0x06002982 RID: 10626 RVA: 0x00091C0D File Offset: 0x0009000D
		public Vector3 pos
		{
			get
			{
				if (this._tri == null)
				{
					Debug.LogError("NavPos pos is null");
				}
				else if (!this._hasY)
				{
					this._y = this.tri.GetY(this._posXZ);
					this._hasY = true;
				}
				return new Vector3(this._posXZ.x, this._y, this._posXZ.y);
			}
			set
			{
				this.MoveTo(value);
			}
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06002983 RID: 10627 RVA: 0x00091C17 File Offset: 0x00090017
		public Vector3 velocity
		{
			get
			{
				return (this.tri != null) ? this.tri.navigationMesh.velocity : Vector3.zero;
			}
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x06002984 RID: 10628 RVA: 0x00091C3E File Offset: 0x0009003E
		public NavigationMesh navigationMesh
		{
			get
			{
				return (this.tri != null) ? this.tri.navigationMesh : null;
			}
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x06002985 RID: 10629 RVA: 0x00091C5C File Offset: 0x0009005C
		public Island island
		{
			get
			{
				return (!(this.navigationMesh == null)) ? this.navigationMesh.island : null;
			}
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x06002986 RID: 10630 RVA: 0x00091C80 File Offset: 0x00090080
		public Transform transform
		{
			get
			{
				return (this.tri != null) ? this.navigationMesh.transform : null;
			}
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x06002987 RID: 10631 RVA: 0x00091C9E File Offset: 0x0009009E
		// (set) Token: 0x06002988 RID: 10632 RVA: 0x00091CB1 File Offset: 0x000900B1
		public Vector3 wPos
		{
			get
			{
				return this.transform.TransformPoint(this.pos);
			}
			set
			{
				this.pos = this.transform.InverseTransformPoint(value);
			}
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x06002989 RID: 10633 RVA: 0x00091CC5 File Offset: 0x000900C5
		public bool onMain
		{
			get
			{
				return this.island;
			}
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x0600298A RID: 10634 RVA: 0x00091CD2 File Offset: 0x000900D2
		public bool valid
		{
			get
			{
				return this._tri != null && !float.IsNaN(this._posXZ.x + this._posXZ.y);
			}
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x0600298B RID: 10635 RVA: 0x00091D01 File Offset: 0x00090101
		public bool isNull
		{
			get
			{
				return this._tri == null;
			}
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x0600298C RID: 10636 RVA: 0x00091D0C File Offset: 0x0009010C
		public bool isNaN
		{
			get
			{
				return float.IsNaN(this._posXZ.x + this._posXZ.y);
			}
		}

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x0600298D RID: 10637 RVA: 0x00091D2A File Offset: 0x0009012A
		public bool isInfinity
		{
			get
			{
				return float.IsInfinity(this._posXZ.x + this._posXZ.y);
			}
		}

		// Token: 0x0600298E RID: 10638 RVA: 0x00091D48 File Offset: 0x00090148
		public void SetNull()
		{
			this._tri = null;
		}

		// Token: 0x0600298F RID: 10639 RVA: 0x00091D51 File Offset: 0x00090151
		public float GetGrass()
		{
			return this.tri.navigationMesh.grass;
		}

		// Token: 0x06002990 RID: 10640 RVA: 0x00091D64 File Offset: 0x00090164
		public float GetWallDistance()
		{
			return this.bary.x * this.tri.verts[0].distanceToWall + this.bary.y * this.tri.verts[1].distanceToWall + this.bary.z * this.tri.verts[2].distanceToWall;
		}

		// Token: 0x06002991 RID: 10641 RVA: 0x00091DE4 File Offset: 0x000901E4
		public float GetCliffDistance()
		{
			return this.bary.x * this.tri.verts[0].distanceToCliff + this.bary.y * this.tri.verts[1].distanceToCliff + this.bary.z * this.tri.verts[2].distanceToCliff;
		}

		// Token: 0x06002992 RID: 10642 RVA: 0x00091E64 File Offset: 0x00090264
		public float GetBorderDistance()
		{
			return this.bary.x * this.tri.verts[0].distanceToBorder + this.bary.y * this.tri.verts[1].distanceToBorder + this.bary.z * this.tri.verts[2].distanceToBorder;
		}

		// Token: 0x06002993 RID: 10643 RVA: 0x00091EE4 File Offset: 0x000902E4
		public float GetBorderDistancePrecise()
		{
			float a = Mathf.Min(this.navigationMesh.bounds.extents.x, this.navigationMesh.bounds.extents.z);
			if (this.tri.edges[0].border)
			{
				a = Mathf.Min(a, this.tri.acrossEdgeLength[0] * this.bary.x);
			}
			if (this.tri.edges[1].border)
			{
				a = Mathf.Min(a, this.tri.acrossEdgeLength[1] * this.bary.y);
			}
			if (this.tri.edges[2].border)
			{
				a = Mathf.Min(a, this.tri.acrossEdgeLength[2] * this.bary.z);
			}
			a = Mathf.Min(a, this.tri.verts[0].distanceToBorder + this.tri.acrossEdgeLength[0] * (1f - this.bary.x));
			a = Mathf.Min(a, this.tri.verts[1].distanceToBorder + this.tri.acrossEdgeLength[1] * (1f - this.bary.y));
			return Mathf.Min(a, this.tri.verts[2].distanceToBorder + this.tri.acrossEdgeLength[2] * (1f - this.bary.z));
		}

		// Token: 0x06002994 RID: 10644 RVA: 0x000920C0 File Offset: 0x000904C0
		public Vector3 GetBorderVector()
		{
			return this.bary.x * this.tri.verts[0].borderVector + this.bary.y * this.tri.verts[1].borderVector + this.bary.z * this.tri.verts[2].borderVector;
		}

		// Token: 0x06002995 RID: 10645 RVA: 0x00092154 File Offset: 0x00090554
		public Vector3 GetWallVector()
		{
			return this.bary.x * this.tri.verts[0].wallVector + this.bary.y * this.tri.verts[1].wallVector + this.bary.z * this.tri.verts[2].wallVector;
		}

		// Token: 0x06002996 RID: 10646 RVA: 0x000921E8 File Offset: 0x000905E8
		public Vector3 GetCliffVector()
		{
			return this.bary.x * this.tri.verts[0].cliffVector + this.bary.y * this.tri.verts[1].cliffVector + this.bary.z * this.tri.verts[2].cliffVector;
		}

		// Token: 0x06002997 RID: 10647 RVA: 0x0009227C File Offset: 0x0009067C
		public float GetCliffyness()
		{
			return this.bary.x * this.tri.verts[0].cliffyness + this.bary.y * this.tri.verts[1].cliffyness + this.bary.z * this.tri.verts[2].cliffyness;
		}

		// Token: 0x06002998 RID: 10648 RVA: 0x000922FC File Offset: 0x000906FC
		public float GetWalliness()
		{
			return this.bary.x * this.tri.verts[0].walliness + this.bary.y * this.tri.verts[1].walliness + this.bary.z * this.tri.verts[2].walliness;
		}

		// Token: 0x06002999 RID: 10649 RVA: 0x0009237C File Offset: 0x0009077C
		public float GetBorderness()
		{
			float num = 0f;
			if (this.tri.verts[0].border)
			{
				num += this.bary.x;
			}
			if (this.tri.verts[1].border)
			{
				num += this.bary.y;
			}
			if (this.tri.verts[2].border)
			{
				num += this.bary.z;
			}
			return num;
		}

		// Token: 0x0600299A RID: 10650 RVA: 0x00092414 File Offset: 0x00090814
		public Vector3 GetSlope()
		{
			Vector3 a = Vector3.zero;
			a += this.tri.verts[0].slope * this.bary.x;
			a += this.tri.verts[1].slope * this.bary.y;
			return a + this.tri.verts[2].slope * this.bary.z;
		}

		// Token: 0x0600299B RID: 10651 RVA: 0x000924B8 File Offset: 0x000908B8
		public Vector3 GetNormal()
		{
			Vector3 a = Vector3.zero;
			a += this.tri.verts[0].normal * this.bary.x;
			a += this.tri.verts[1].normal * this.bary.y;
			return a + this.tri.verts[2].normal * this.bary.z;
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x0600299C RID: 10652 RVA: 0x0009255C File Offset: 0x0009095C
		public static NavPos empty
		{
			get
			{
				return default(NavPos);
			}
		}

		// Token: 0x0600299D RID: 10653 RVA: 0x00092574 File Offset: 0x00090974
		public Vert GetClosestVert()
		{
			return this.tri.verts[(this.bary.x <= this.bary.y) ? ((this.bary.y <= this.bary.z) ? 2 : 1) : ((this.bary.x <= this.bary.z) ? 2 : 0)];
		}

		// Token: 0x0600299E RID: 10654 RVA: 0x00092609 File Offset: 0x00090A09
		public static bool operator ==(NavPos a, NavPos b)
		{
			return a._posXZ == b._posXZ && a.tri == b.tri;
		}

		// Token: 0x0600299F RID: 10655 RVA: 0x00092636 File Offset: 0x00090A36
		public static NavPos operator +(NavPos a, Vector3 b)
		{
			a.pos += b;
			return a;
		}

		// Token: 0x060029A0 RID: 10656 RVA: 0x0009264C File Offset: 0x00090A4C
		public static NavPos operator -(NavPos a, Vector3 b)
		{
			a.pos -= b;
			return a;
		}

		// Token: 0x060029A1 RID: 10657 RVA: 0x00092662 File Offset: 0x00090A62
		public static bool operator !=(NavPos a, NavPos b)
		{
			return !(a == b);
		}

		// Token: 0x060029A2 RID: 10658 RVA: 0x0009266E File Offset: 0x00090A6E
		public override bool Equals(object obj)
		{
			return obj is NavPos && this == (NavPos)obj;
		}

		// Token: 0x060029A3 RID: 10659 RVA: 0x00092690 File Offset: 0x00090A90
		public override int GetHashCode()
		{
			return this.tri.GetHashCode() + this.pos.GetHashCode();
		}

		// Token: 0x060029A4 RID: 10660 RVA: 0x000926C0 File Offset: 0x00090AC0
		public Vert GetSingleVert()
		{
			for (int i = 0; i < 3; i++)
			{
				if (this.bary.GetComponent(i) > 0.99f)
				{
					return this.tri.verts[i];
				}
			}
			return null;
		}

		// Token: 0x060029A5 RID: 10661 RVA: 0x00092708 File Offset: 0x00090B08
		public void ConstrainTo(IEnumerable<Tri> tris)
		{
			foreach (Tri tri in tris)
			{
				if (tri == this._tri)
				{
					return;
				}
			}
			Tri tri2 = this._tri;
			Vector2 posXZ = this._posXZ;
			float num = float.MaxValue;
			foreach (Tri tri3 in tris)
			{
				Vector2 vector = tri3.ClampXZ(this._posXZ);
				float sqrMagnitude = (this._posXZ - vector).sqrMagnitude;
				if (sqrMagnitude < num)
				{
					num = sqrMagnitude;
					posXZ = vector;
					tri2 = tri3;
				}
			}
			if (this._tri != tri2)
			{
				this._tri = tri2;
				this._posXZ = posXZ;
				this._hasBary = false;
				this._hasY = false;
			}
		}

		// Token: 0x060029A6 RID: 10662 RVA: 0x00092820 File Offset: 0x00090C20
		public bool TriCast(NavPos navPos)
		{
			return this._tri != null && (navPos.tri == this._tri || Tri.TriCastXZ(this._tri, this._posXZ, navPos._posXZ, navPos._tri));
		}

		// Token: 0x060029A7 RID: 10663 RVA: 0x0009286D File Offset: 0x00090C6D
		public bool Move(Vector3 offset)
		{
			return this.MoveTo(this.pos + offset);
		}

		// Token: 0x060029A8 RID: 10664 RVA: 0x00092884 File Offset: 0x00090C84
		public bool MoveTo(Vector3 target)
		{
			bool result;
			using (new ScopedProfiler("NavPos.MoveTo", null))
			{
				if (target.x == this._posXZ.x && target.z == this._posXZ.x)
				{
					result = true;
				}
				else
				{
					Edge edge;
					result = this.MoveTo(target, out edge);
				}
			}
			return result;
		}

		// Token: 0x060029A9 RID: 10665 RVA: 0x00092904 File Offset: 0x00090D04
		public bool MoveTo(Vector3 target, out Edge lastEdge)
		{
			lastEdge = null;
			Vector2 xz = target.GetXZ();
			if (xz == this._posXZ)
			{
				return true;
			}
			float f = xz.x + xz.y;
			if (float.IsNaN(f))
			{
				Debug.LogError("Trying to set NaN navPos");
				return false;
			}
			if (float.IsInfinity(f))
			{
				Debug.LogError("Trying to set infinity navPos");
				return false;
			}
			this._hasBary = false;
			this._hasY = false;
			Tri tri = null;
			Vector2 zero = Vector2.zero;
			float maxValue = float.MaxValue;
			this.MoveInternal(xz, this._tri, ref tri, ref maxValue, ref zero, ref lastEdge);
			if (tri == null)
			{
				Debug.LogError("Trying to set null tri");
				return false;
			}
			this._tri = tri;
			this._posXZ = zero;
			return maxValue == 0f;
		}

		// Token: 0x060029AA RID: 10666 RVA: 0x000929C8 File Offset: 0x00090DC8
		private void MoveInternal(Vector2 targetXZ, Tri currentTri, ref Tri closestTri, ref float closestDistance, ref Vector2 closestXZ, ref Edge closestEdge)
		{
			if (closestDistance == 0f)
			{
				return;
			}
			int index;
			Edge edge;
			Vert vert;
			Vector2 vector = currentTri.ClampXZ(targetXZ, out index, out edge, out vert);
			if (edge == null)
			{
				closestDistance = 0f;
				closestTri = currentTri;
				closestXZ = vector;
				closestEdge = null;
				return;
			}
			float num = Vector3.SqrMagnitude(vector - targetXZ);
			if (num < closestDistance)
			{
				closestDistance = num;
				closestTri = currentTri;
				closestXZ = vector;
				closestEdge = edge;
				if (!edge.border)
				{
					this.MoveInternal(targetXZ, currentTri.tris[index], ref closestTri, ref closestDistance, ref closestXZ, ref closestEdge);
				}
				else
				{
					Vector2 lhs = targetXZ - this._posXZ;
					float num2 = Vector2.Dot(lhs, edge.xzDir);
					if (num2 != 0f)
					{
						vert = edge.verts[(num2 <= 0f) ? 0 : 1];
					}
				}
				if (vert != null)
				{
					int num3 = 0;
					while (num3 < vert.tris.Count && closestDistance > 0f)
					{
						Tri tri = vert.tris[num3];
						if (tri != currentTri)
						{
							this.MoveInternal(targetXZ, tri, ref closestTri, ref closestDistance, ref closestXZ, ref closestEdge);
						}
						num3++;
					}
				}
			}
		}

		// Token: 0x04001B0F RID: 6927
		private Tri _tri;

		// Token: 0x04001B10 RID: 6928
		private Vector2 _posXZ;

		// Token: 0x04001B11 RID: 6929
		private bool _hasBary;

		// Token: 0x04001B12 RID: 6930
		private Vector3 _bary;

		// Token: 0x04001B13 RID: 6931
		private bool _hasY;

		// Token: 0x04001B14 RID: 6932
		private float _y;
	}
}
