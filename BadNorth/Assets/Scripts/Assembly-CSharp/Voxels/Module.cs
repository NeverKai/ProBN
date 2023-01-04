using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels
{
	// Token: 0x0200062B RID: 1579
	[SelectionBase]
	public class Module : MonoBehaviour
	{
		// Token: 0x06002884 RID: 10372 RVA: 0x000876EB File Offset: 0x00085AEB
		public GameObject GetOrGreateObjectToCopy()
		{
			if (!this.objectToCopy)
			{
				this.objectToCopy = base.gameObject.AddEmptyChild(base.name);
			}
			return this.objectToCopy;
		}

		// Token: 0x06002885 RID: 10373 RVA: 0x0008771C File Offset: 0x00085B1C
		public int GetSetKey()
		{
			if (this.dirtyKey)
			{
				string text = string.Empty;
				for (int i = 0; i < this.sets.Count; i++)
				{
					text += this.sets[i].name;
				}
				this.setKey = text.GetHashCode();
				this.dirtyKey = false;
			}
			return this.setKey;
		}

		// Token: 0x06002886 RID: 10374 RVA: 0x00087787 File Offset: 0x00085B87
		public void MarkDirty()
		{
			this.dirtyKey = true;
		}

		// Token: 0x06002887 RID: 10375 RVA: 0x00087790 File Offset: 0x00085B90
		public bool Contains(Vector3 pos)
		{
			pos = base.transform.worldToLocalMatrix.MultiplyPoint(pos);
			pos = ExtraMath.Round(pos);
			for (int i = 0; i < this.cells.Count; i++)
			{
				if (this.cells[i].pos == pos)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002888 RID: 10376 RVA: 0x000877F8 File Offset: 0x00085BF8
		public GameObject GetGameObject(TransformSettings settings)
		{
			if (this.objectToCopy)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.objectToCopy);
				settings.ApplyLocal(gameObject.transform);
				return gameObject;
			}
			return null;
		}

		// Token: 0x040019FF RID: 6655
		public bool isNull;

		// Token: 0x04001A00 RID: 6656
		public Bounds bounds;

		// Token: 0x04001A01 RID: 6657
		public List<Module.Cell> cells = new List<Module.Cell>();

		// Token: 0x04001A02 RID: 6658
		public float beachLength;

		// Token: 0x04001A03 RID: 6659
		public GameObject objectToCopy;

		// Token: 0x04001A04 RID: 6660
		public List<MeshFilter> meshFilters = new List<MeshFilter>();

		// Token: 0x04001A05 RID: 6661
		public List<OrientedModule> orientations = new List<OrientedModule>();

		// Token: 0x04001A06 RID: 6662
		public List<ModuleSet> sets = new List<ModuleSet>();

		// Token: 0x04001A07 RID: 6663
		public int placementToShow;

		// Token: 0x04001A08 RID: 6664
		public bool house;

		// Token: 0x04001A09 RID: 6665
		public int goldCount;

		// Token: 0x04001A0A RID: 6666
		public bool forcedNavigability;

		// Token: 0x04001A0B RID: 6667
		private bool dirtyKey = true;

		// Token: 0x04001A0C RID: 6668
		private int setKey;

		// Token: 0x0200062C RID: 1580
		[Serializable]
		public class Cell
		{
			// Token: 0x06002889 RID: 10377 RVA: 0x00087834 File Offset: 0x00085C34
			public Cell(Vector3 pos)
			{
				this.pos = pos;
				for (int i = 0; i < this.corners.Length; i++)
				{
					this.corners[i] = new Corner(Constants.corners[i]);
				}
			}

			// Token: 0x04001A0D RID: 6669
			public Vector3 pos;

			// Token: 0x04001A0E RID: 6670
			public List<Edge> edges = new List<Edge>();

			// Token: 0x04001A0F RID: 6671
			public Corner[] corners = new Corner[8];

			// Token: 0x04001A10 RID: 6672
			public bool[] internalSide = new bool[6];

			// Token: 0x04001A11 RID: 6673
			public bool[] navigable = new bool[6];
		}
	}
}
