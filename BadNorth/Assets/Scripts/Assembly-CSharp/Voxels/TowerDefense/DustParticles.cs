using System;
using System.Collections.Generic;
using UnityEngine;
using Voxels.TowerDefense.WorldEnvironment;

namespace Voxels.TowerDefense
{
	// Token: 0x0200086A RID: 2154
	public class DustParticles : Singleton<DustParticles>, IslandGameplayManager.IAwake, IslandGameplayManager.IWipeIsland
	{
		// Token: 0x06003872 RID: 14450 RVA: 0x000F42E7 File Offset: 0x000F26E7
		private void OnDestroy()
		{
			UnityEngine.Object.Destroy(this.mesh);
		}

		// Token: 0x06003873 RID: 14451 RVA: 0x000F42F4 File Offset: 0x000F26F4
		void IslandGameplayManager.IAwake.OnAwake(IslandGameplayManager manager)
		{
			MeshRenderer component = base.GetComponent<MeshRenderer>();
			MeshFilter component2 = base.GetComponent<MeshFilter>();
			this.mesh = new Mesh();
			component2.sharedMesh = this.mesh;
			List<Vector2> list = ListPool<Vector2>.GetList(1024);
			List<Vector2> list2 = ListPool<Vector2>.GetList(1024);
			List<int> list3 = ListPool<int>.GetList(1536);
			for (int i = 0; i < 256; i++)
			{
				int num = i * 4;
				Vector2 vector = Vector2.one;
				Vector2 item = new Vector2((UnityEngine.Random.value + (float)(i / 128)) * 0.5f, UnityEngine.Random.value);
				for (int j = num; j < num + 4; j++)
				{
					list.Add(vector);
					list2.Add(item);
					vector = ExtraMath.Rotate2D90(vector);
				}
				list3.Add(num);
				list3.Add(num + 1);
				list3.Add(num + 2);
				list3.Add(num + 2);
				list3.Add(num + 3);
				list3.Add(num);
			}
			this.mesh.vertices = this.verts;
			this.mesh.SetUVs(0, list);
			this.mesh.SetUVs(1, list2);
			this.mesh.SetTriangles(list3, 0);
			this.mesh.tangents = this.tangents;
			list.ReturnToListPool<Vector2>();
			list2.ReturnToListPool<Vector2>();
			list3.ReturnToListPool<int>();
			this.Clear();
		}

		// Token: 0x06003874 RID: 14452 RVA: 0x000F4464 File Offset: 0x000F2864
		[ContextMenu("Test")]
		private void Test()
		{
			for (int i = 0; i < 32; i++)
			{
				Debug.Log(this.GetVertIndexFromIndex(i));
			}
		}

		// Token: 0x06003875 RID: 14453 RVA: 0x000F4495 File Offset: 0x000F2895
		private int GetVertIndexFromIndex(int index)
		{
			if (index % 3 == 0)
			{
				return index / 3 % 128 + 128;
			}
			return index * 2 / 3 % 128;
		}

		// Token: 0x06003876 RID: 14454 RVA: 0x000F44BA File Offset: 0x000F28BA
		public void SpawnParticles(Vector3 pos)
		{
			this.SpawnParticles(pos, Vector3.zero);
		}

		// Token: 0x06003877 RID: 14455 RVA: 0x000F44C8 File Offset: 0x000F28C8
		public void SpawnParticles(Vector3 pos, Vector3 dir)
		{
			Vector4 vector = dir.SetW(Singleton<WorldWind>.instance.sqrtWindTime);
			int num = this.GetVertIndexFromIndex(this.index) * 4;
			this.tangents[num] = vector;
			this.tangents[num + 1] = vector;
			this.tangents[num + 2] = vector;
			this.tangents[num + 3] = vector;
			this.verts[num] = pos;
			this.verts[num + 1] = pos;
			this.verts[num + 2] = pos;
			this.verts[num + 3] = pos;
			this.index++;
			this.dirty = true;
		}

		// Token: 0x06003878 RID: 14456 RVA: 0x000F45A6 File Offset: 0x000F29A6
		private void LateUpdate()
		{
			if (this.dirty)
			{
				this.mesh.vertices = this.verts;
				this.mesh.tangents = this.tangents;
				this.dirty = false;
			}
		}

		// Token: 0x06003879 RID: 14457 RVA: 0x000F45DC File Offset: 0x000F29DC
		void IslandGameplayManager.IWipeIsland.OnWipe(Island island)
		{
			this.Clear();
		}

		// Token: 0x0600387A RID: 14458 RVA: 0x000F45E4 File Offset: 0x000F29E4
		private void Clear()
		{
			for (int i = 0; i < this.tangents.Length; i++)
			{
				this.tangents[i] = new Vector4(0f, 0f, 0f, -10f);
			}
			this.mesh.tangents = this.tangents;
			this.dirty = false;
			this.index = 0;
		}

		// Token: 0x0400266E RID: 9838
		private const int count = 256;

		// Token: 0x0400266F RID: 9839
		private const int halfCount = 128;

		// Token: 0x04002670 RID: 9840
		private const int vertCount = 1024;

		// Token: 0x04002671 RID: 9841
		private Vector4[] tangents = new Vector4[1024];

		// Token: 0x04002672 RID: 9842
		private Vector3[] verts = new Vector3[1024];

		// Token: 0x04002673 RID: 9843
		private Mesh mesh;

		// Token: 0x04002674 RID: 9844
		private int index;

		// Token: 0x04002675 RID: 9845
		private bool dirty;
	}
}
