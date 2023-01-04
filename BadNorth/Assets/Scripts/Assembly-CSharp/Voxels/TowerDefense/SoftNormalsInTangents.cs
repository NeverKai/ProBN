using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x02000675 RID: 1653
	public class SoftNormalsInTangents : MonoBehaviour
	{
		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06002A19 RID: 10777 RVA: 0x0009654F File Offset: 0x0009494F
		// (set) Token: 0x06002A1A RID: 10778 RVA: 0x00096557 File Offset: 0x00094957
		public MeshRenderer meshRenderer { get; private set; }

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x06002A1B RID: 10779 RVA: 0x00096560 File Offset: 0x00094960
		// (set) Token: 0x06002A1C RID: 10780 RVA: 0x00096568 File Offset: 0x00094968
		public MeshFilter meshFilter { get; private set; }

		// Token: 0x06002A1D RID: 10781 RVA: 0x00096574 File Offset: 0x00094974
		private static Mesh GetSoftMesh(Mesh srcMesh)
		{
			Mesh mesh;
			if (!SoftNormalsInTangents.outlineMeshDict.TryGetValue(srcMesh, out mesh))
			{
				mesh = UnityEngine.Object.Instantiate<Mesh>(srcMesh);
				mesh.name = srcMesh.name + " Soft";
				mesh.SoftNormalsInTangents();
				SoftNormalsInTangents.outlineMeshDict.Add(srcMesh, mesh);
				SoftNormalsInTangents.outlineMeshDict.Add(mesh, mesh);
			}
			return mesh;
		}

		// Token: 0x06002A1E RID: 10782 RVA: 0x000965CF File Offset: 0x000949CF
		private void Start()
		{
			if (this.initialized)
			{
				return;
			}
			this.initialized = true;
			if (!this.manualInitialize)
			{
				this.Initialize();
			}
		}

		// Token: 0x06002A1F RID: 10783 RVA: 0x000965F8 File Offset: 0x000949F8
		private void Initialize()
		{
			this.manualInitialize = true;
			MeshFilter disabledComponentInParent = this.GetDisabledComponentInParent<MeshFilter>();
			MeshRenderer disabledComponentInParent2 = this.GetDisabledComponentInParent<MeshRenderer>();
			Material sharedMaterial = disabledComponentInParent2.sharedMaterial;
			disabledComponentInParent.sharedMesh = SoftNormalsInTangents.GetSoftMesh(disabledComponentInParent.sharedMesh);
		}

		// Token: 0x06002A20 RID: 10784 RVA: 0x00096634 File Offset: 0x00094A34
		public static void InitializeAll(Transform t)
		{
			SoftNormalsInTangents[] componentsInChildren = t.GetComponentsInChildren<SoftNormalsInTangents>(true);
			foreach (SoftNormalsInTangents softNormalsInTangents in componentsInChildren)
			{
				softNormalsInTangents.Initialize();
			}
		}

		// Token: 0x04001B76 RID: 7030
		private static Dictionary<Mesh, Mesh> outlineMeshDict = new Dictionary<Mesh, Mesh>();

		// Token: 0x04001B79 RID: 7033
		[SerializeField]
		private bool initialized;

		// Token: 0x04001B7A RID: 7034
		private bool manualInitialize;
	}
}
