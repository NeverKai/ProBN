using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x0200060C RID: 1548
	public class ExtraRenderers : MonoBehaviour
	{
		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x060027D8 RID: 10200 RVA: 0x000816D2 File Offset: 0x0007FAD2
		// (set) Token: 0x060027D9 RID: 10201 RVA: 0x000816DA File Offset: 0x0007FADA
		public MeshRenderer meshRenderer { get; private set; }

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x060027DA RID: 10202 RVA: 0x000816E3 File Offset: 0x0007FAE3
		// (set) Token: 0x060027DB RID: 10203 RVA: 0x000816EB File Offset: 0x0007FAEB
		public MeshFilter meshFilter { get; private set; }

		// Token: 0x060027DC RID: 10204 RVA: 0x000816F4 File Offset: 0x0007FAF4
		private static Mesh GetOutlineMesh(Mesh srcMesh)
		{
			Mesh mesh;
			if (!ExtraRenderers.outlineMeshDict.TryGetValue(srcMesh, out mesh))
			{
				mesh = UnityEngine.Object.Instantiate<Mesh>(srcMesh);
				mesh.name = srcMesh.name + " Inverted";
				mesh.SoftNormalsInTangents();
				mesh.Invert();
				ExtraRenderers.outlineMeshDict.Add(srcMesh, mesh);
				ExtraRenderers.outlineMeshDict.Add(mesh, mesh);
			}
			return mesh;
		}

		// Token: 0x060027DD RID: 10205 RVA: 0x00081758 File Offset: 0x0007FB58
		private static Material GetMaterial(Material srcMaterial, string keyword)
		{
			int key = srcMaterial.GetHashCode() + keyword.GetHashCode();
			Material material;
			if (!ExtraRenderers.materialDict.TryGetValue(key, out material))
			{
				material = UnityEngine.Object.Instantiate<Material>(srcMaterial);
				material.name = srcMaterial.name + keyword;
				foreach (string text in keyword.Split(" ".ToCharArray()))
				{
					material.EnableKeyword(text);
				}
				ExtraRenderers.materialDict.Add(key, material);
				int key2 = material.GetHashCode() + keyword.GetHashCode();
				ExtraRenderers.materialDict.Add(key2, material);
			}
			return material;
		}

		// Token: 0x060027DE RID: 10206 RVA: 0x000817FC File Offset: 0x0007FBFC
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

		// Token: 0x060027DF RID: 10207 RVA: 0x00081824 File Offset: 0x0007FC24
		private void Initialize()
		{
			this.manualInitialize = true;
			MeshFilter disabledComponentInParent = this.GetDisabledComponentInParent<MeshFilter>();
			MeshRenderer component = disabledComponentInParent.gameObject.GetComponent<MeshRenderer>();
			Material sharedMaterial = component.sharedMaterial;
			GameObject gameObject = (!(component.gameObject == base.gameObject)) ? base.gameObject : base.gameObject.AddEmptyChild(this.keyword);
			this.meshFilter = gameObject.GetOrAddComponent<MeshFilter>();
			this.meshRenderer = gameObject.GetOrAddComponent<MeshRenderer>();
			this.meshFilter.sharedMesh = ((!this.flippedMesh) ? disabledComponentInParent.sharedMesh : ExtraRenderers.GetOutlineMesh(disabledComponentInParent.sharedMesh));
			this.meshRenderer.sharedMaterial = ExtraRenderers.GetMaterial(sharedMaterial, this.keyword);
		}

		// Token: 0x060027E0 RID: 10208 RVA: 0x000818E0 File Offset: 0x0007FCE0
		public static void InitializeAll(Transform t)
		{
			ExtraRenderers[] componentsInChildren = t.GetComponentsInChildren<ExtraRenderers>(true);
			foreach (ExtraRenderers extraRenderers in componentsInChildren)
			{
				extraRenderers.Initialize();
			}
		}

		// Token: 0x04001981 RID: 6529
		private static Dictionary<int, Material> materialDict = new Dictionary<int, Material>();

		// Token: 0x04001982 RID: 6530
		private static Dictionary<Mesh, Mesh> outlineMeshDict = new Dictionary<Mesh, Mesh>();

		// Token: 0x04001983 RID: 6531
		public string keyword;

		// Token: 0x04001984 RID: 6532
		public bool flippedMesh = true;

		// Token: 0x04001985 RID: 6533
		[SerializeField]
		private bool initialized;

		// Token: 0x04001988 RID: 6536
		private bool manualInitialize;
	}
}
