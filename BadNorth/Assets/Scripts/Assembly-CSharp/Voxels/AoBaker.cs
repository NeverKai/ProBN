using System;
using System.Collections.Generic;
using UnityEngine;
using Voxels.TowerDefense;

namespace Voxels
{
	// Token: 0x02000606 RID: 1542
	public class AoBaker : IslandComponent, IIslandEnter, IIslandFirstEnter, IIslandDestroyEntered
	{
		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x060027B2 RID: 10162 RVA: 0x0008072A File Offset: 0x0007EB2A
		public Matrix4x4 world2voxel
		{
			get
			{
				return this.voxelSpace.world2Voxel;
			}
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x060027B3 RID: 10163 RVA: 0x00080737 File Offset: 0x0007EB37
		public VoxelSpace voxelSpace
		{
			get
			{
				return base.island.voxelSpace;
			}
		}

		// Token: 0x060027B4 RID: 10164 RVA: 0x00080744 File Offset: 0x0007EB44
		[ContextMenu("Insta bake")]
		private void InstaBake()
		{
			IEnumerator<GenInfo> enumerator = this.BakeAo(base.island);
			while (enumerator.MoveNext())
			{
			}
		}

		// Token: 0x060027B5 RID: 10165 RVA: 0x00080770 File Offset: 0x0007EB70
		public Vector2 DirToLatLong(Vector3 dir)
		{
			Vector2 result;
			result.y = (dir.y + 1f) / 2f;
			result.x = (Mathf.Atan2(dir.x, dir.z) * 57.29578f / 360f + 1f) % 1f;
			return result;
		}

		// Token: 0x060027B6 RID: 10166 RVA: 0x000807CC File Offset: 0x0007EBCC
		public Vector4 GetColor(Vector3 normal)
		{
			Vector3 lhs = normal;
			Vector3 vector = new Vector3(1f, 0.2f, 0f);
			Color color;
			color.r = Mathf.Clamp01(Vector3.Dot(lhs, vector.normalized));
			color.g = Mathf.Clamp01(normal.y);
			Vector3 lhs2 = normal;
			Vector3 vector2 = new Vector3(-1f, 0.2f, 0f);
			color.b = Mathf.Clamp01(Vector3.Dot(lhs2, vector2.normalized));
			color.a = 1f;
			color = color * color * color * color * color;
			color.a = (normal.y + 1f) / 2f;
			return color;
		}

		// Token: 0x060027B7 RID: 10167 RVA: 0x00080890 File Offset: 0x0007EC90
		private void PropagateColor(Marcher.Node node, float alpha = 1f)
		{
			node.hasColor = true;
			if (node.nodes.Count == 0)
			{
				node.color = this.GetColor(node.normal) * alpha;
			}
			else
			{
				node.color = Vector4.zero;
				alpha /= (float)node.nodes.Count;
				foreach (Marcher.Node node2 in node.nodes)
				{
					this.PropagateColor(node2, alpha);
					node.color += node2.color;
				}
			}
		}

		// Token: 0x060027B8 RID: 10168 RVA: 0x00080954 File Offset: 0x0007ED54
		private Vector4 GetAo(Marcher.Node node, Vector3 offset)
		{
			Vector3 vector = node.pos + offset;
			vector.y = Mathf.Abs(vector.y);
			if (!this.voxelSpace.voxelBounds.Contains(vector))
			{
				return node.color;
			}
			VoxelSpace.CornerVoxel cornerVoxel = this.voxelSpace.GetCornerVoxel(vector);
			if (cornerVoxel.inside)
			{
				return Vector4.zero;
			}
			if (node.nodes.Count == 0)
			{
				return node.color;
			}
			float d = 1f - cornerVoxel.coveredArea;
			Vector4 a = Vector4.zero;
			for (int i = 0; i < node.nodes.Count; i++)
			{
				Marcher.Node node2 = node.nodes[i];
				a += this.GetAo(node2, offset);
			}
			return a * d;
		}

		// Token: 0x060027B9 RID: 10169 RVA: 0x00080A2C File Offset: 0x0007EE2C
		IEnumerator<GenInfo> IIslandFirstEnter.OnIslandFirstEnter(Island island)
		{
			GenInfo genInfo = new GenInfo("AoBaker", GenInfo.Mode.interruptable);
			this.textureAO = new Fake3dTex(this.voxelSpace.size + Vector3.one, new Color(0f, 0f, 0f, 1f), false, island.texturePool);
			this.textureAO.Apply();
			yield return genInfo;
			this.textureNormal = new Fake3dTex(this.voxelSpace.size, new Color(0.5f, 0.5f, 0.5f, 0f), false, island.texturePool);
			this.textureNormal.Apply();
			yield return genInfo;
			IEnumerator<GenInfo> bakeBoth = this.BakeBoth(island);
			bool moving = true;
			while (moving)
			{
				using (new ScopedProfiler("Baking Both", null))
				{
					moving = bakeBoth.MoveNext();
				}
				yield return bakeBoth.Current;
			}
			yield return genInfo;
			yield break;
		}

		// Token: 0x060027BA RID: 10170 RVA: 0x00080A50 File Offset: 0x0007EE50
		public IEnumerator<GenInfo> BakeBoth(Island island)
		{
			IEnumerator<GenInfo> bakeAO = this.BakeAo(island);
			bool moving = true;
			while (moving)
			{
				using (new ScopedProfiler("Baking AO", null))
				{
					moving = bakeAO.MoveNext();
				}
				yield return bakeAO.Current;
			}
			yield return new GenInfo("Updating Normals", GenInfo.Mode.interruptable);
			this.voxelSpace.UpdateNormals();
			yield return new GenInfo("Updating Normals", GenInfo.Mode.interruptable);
			IEnumerator<GenInfo> normalRoutine = this.BakeNormal();
			bool moving2 = true;
			while (moving2)
			{
				using (new ScopedProfiler("Baking Normals", null))
				{
					moving2 = normalRoutine.MoveNext();
				}
				yield return normalRoutine.Current;
			}
			yield break;
		}

		// Token: 0x060027BB RID: 10171 RVA: 0x00080A74 File Offset: 0x0007EE74
		public IEnumerator<GenInfo> BakeAo(Island island)
		{
			Marcher marcher = (AoBaker.marcherStack.Count != 0) ? AoBaker.marcherStack.Pop() : new Marcher(5);
			GenInfo genInfo = new GenInfo("Baking light", GenInfo.Mode.interruptable);
			yield return genInfo;
			this.PropagateColor(marcher.center, 1f);
			for (int i = 0; i < this.voxelSpace.cornerVoxels.Length; i++)
			{
				using ("BakeAO For Loop " + i)
				{
					VoxelSpace.CornerVoxel cornerVoxel = this.voxelSpace.cornerVoxels[i];
					Vector4 ao = this.GetAo(marcher.center, cornerVoxel.pos);
					Color gamma = ao.gamma;
					this.textureAO.SetPixel(cornerVoxel.pos, gamma);
				}
				yield return genInfo;
			}
			this.textureAO.ApplyPixels();
			AoBaker.marcherStack.Push(marcher);
			yield return genInfo;
			yield break;
		}

		// Token: 0x060027BC RID: 10172 RVA: 0x00080A90 File Offset: 0x0007EE90
		private IEnumerator<GenInfo> BakeNormal()
		{
			for (int i = 0; i < this.voxelSpace.voxels.Length; i++)
			{
				VoxelSpace.Voxel voxel = this.voxelSpace.voxels[i];
				Vector3Int pos = voxel.pos;
				Vector3 normal = voxel.normal;
				normal = (normal + Vector3.one) / 2f;
				Color color = new Color(normal.x, normal.y, normal.z, voxel.openness);
				this.textureNormal.SetPixel(pos, color);
				yield return new GenInfo("Baking Normal", GenInfo.Mode.interruptable);
			}
			this.textureNormal.ApplyPixels();
			yield return new GenInfo("Baking Normal", GenInfo.Mode.interruptable);
			yield break;
		}

		// Token: 0x060027BD RID: 10173 RVA: 0x00080AAC File Offset: 0x0007EEAC
		public void ApplyShaderVariables()
		{
			this.textureAO.SetShaderVariables("_AoTex", "_AoTexVolume", "_AoTexSize");
			this.textureNormal.SetShaderVariables("_NormalTex", "_NormalTexVolume", "_NormalTexSize");
			Shader.SetGlobalMatrix("_World2Voxel", this.world2voxel);
			Shader.SetGlobalMatrix("_Voxel2World", base.island.voxelSpace.voxel2world);
		}

		// Token: 0x060027BE RID: 10174 RVA: 0x00080B18 File Offset: 0x0007EF18
		public IEnumerator<GenInfo> OnIslandEnter(Island island)
		{
			this.ApplyShaderVariables();
			yield return new GenInfo("AoBaker", GenInfo.Mode.interruptable);
			yield break;
		}

		// Token: 0x060027BF RID: 10175 RVA: 0x00080B33 File Offset: 0x0007EF33
		void IIslandDestroyEntered.OnIslandDestroyEntered(Island island)
		{
			this.textureAO.Destroy();
			this.textureNormal.Destroy();
		}

		// Token: 0x04001977 RID: 6519
		public int loops;

		// Token: 0x04001978 RID: 6520
		public int radius = 5;

		// Token: 0x04001979 RID: 6521
		public int bakerLoops = 10;

		// Token: 0x0400197A RID: 6522
		public Fake3dTex textureAO;

		// Token: 0x0400197B RID: 6523
		public Fake3dTex textureNormal;

		// Token: 0x0400197C RID: 6524
		private static Stack<Marcher> marcherStack = new Stack<Marcher>(2);
	}
}
