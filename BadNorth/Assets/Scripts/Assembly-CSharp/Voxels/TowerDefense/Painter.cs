using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020007B3 RID: 1971
	public class Painter : IslandComponent, IIslandProcessor, IIslandEnter, IIslandReset
	{
		// Token: 0x06003308 RID: 13064 RVA: 0x000DA528 File Offset: 0x000D8928
		public void PaintBlood(Vector3 pos)
		{
			this.Paint(new Bounds(pos + Vector3.up / 4f, Vector3.one / 2f), new Color(0.5f, 0f, 0f, 0f));
		}

		// Token: 0x06003309 RID: 13065 RVA: 0x000DA580 File Offset: 0x000D8980
		public void Paint(Bounds coverBounds, Color color)
		{
			Fake3dTex tex3d = base.island.levelNode.campaign.paintAtlas.tex3d;
			coverBounds.center += this.centerOffset;
			for (int i = Mathf.FloorToInt(coverBounds.min.x); i < Mathf.CeilToInt(coverBounds.max.x); i++)
			{
				for (int j = Mathf.FloorToInt(coverBounds.min.y); j < Mathf.CeilToInt(coverBounds.max.y); j++)
				{
					for (int k = Mathf.FloorToInt(coverBounds.min.z); k < Mathf.CeilToInt(coverBounds.max.z); k++)
					{
						Vector3Int vector3Int = new Vector3Int(i, j, k);
						if (this.bounds.Contains(vector3Int))
						{
							Vector3 b = Vector3.Max(vector3Int, coverBounds.min);
							Vector3 a = Vector3.Min(vector3Int + Vector3.one, coverBounds.max);
							Vector3 vector = a - b;
							float b2 = (vector.x + vector.y + vector.z) / 3f;
							tex3d.AddPixel(vector3Int + this.spriteOffset, color * b2);
						}
					}
				}
			}
			tex3d.ApplyPixels();
		}

		// Token: 0x0600330A RID: 13066 RVA: 0x000DA718 File Offset: 0x000D8B18
		IEnumerator<GenInfo> IIslandProcessor.OnIslandProcess(Island island, SavedWave savedWave)
		{
			this.islandSize = ExtraMath.RoundToInt(island.size);
			Vector3Int size = this.islandSize;
			size.y = island.levelNode.campaign.paintAtlas.tex3d.size.y;
			this.bounds = new Bounds(size / (int) 2f, size + Vector3.one);
			this.centerOffset.x = (float)(size.x + 1) / 2f;
			this.centerOffset.y = 1f;
			this.centerOffset.z = (float)(size.z + 1) / 2f;
			this.spriteOffset.x = island.levelNode.levelState.rectMinX;
			this.spriteOffset.y = 0;
			this.spriteOffset.z = island.levelNode.levelState.rectMinY;
			this.Paint(new Bounds(Vector3.zero, size * 2), new Color(0f, 0f, 0.2f, 0f));
			yield return new GenInfo("Painter", GenInfo.Mode.interruptable);
			yield break;
		}

		// Token: 0x0600330B RID: 13067 RVA: 0x000DA73A File Offset: 0x000D8B3A
		private void OnDrawGizmos()
		{
			Gizmos.DrawWireCube(this.bounds.center - this.centerOffset, this.bounds.size);
		}

		// Token: 0x0600330C RID: 13068 RVA: 0x000DA764 File Offset: 0x000D8B64
		IEnumerator<GenInfo> IIslandEnter.OnIslandEnter(Island island)
		{
			island.levelNode.campaign.paintAtlas.SavePixels();
			Shader.SetGlobalVector("_PaintTexOffset", this.centerOffset + this.spriteOffset);
			yield return new GenInfo("Painter", GenInfo.Mode.interruptable);
			yield break;
		}

		// Token: 0x0600330D RID: 13069 RVA: 0x000DA788 File Offset: 0x000D8B88
		IEnumerator<GenInfo> IIslandReset.OnIslandReset(Island island)
		{
			island.levelNode.campaign.paintAtlas.LoadPixels();
			yield return default(GenInfo);
			yield break;
		}

		// Token: 0x0600330E RID: 13070 RVA: 0x000DA7A4 File Offset: 0x000D8BA4
		public void DebugPaint()
		{
			Vert[] verts = base.island.navMesh.verts;
			UnityEngine.Random.State state = UnityEngine.Random.state;
			int i = 0;
			int num = (int)((float)verts.Length * 0.9f);
			while (i < num)
			{
				if (UnityEngine.Random.value > (float)i / (float)num)
				{
					this.PaintBlood(verts[i].pos);
				}
				i++;
			}
			UnityEngine.Random.state = state;
		}

		// Token: 0x040022A9 RID: 8873
		private Color defaultColor = new Color(0f, 0f, 0f, 0f);

		// Token: 0x040022AA RID: 8874
		public static Color bloodColor = new Color(1f, 0f, 0f, 0f);

		// Token: 0x040022AB RID: 8875
		public static Color sootColor = new Color(0f, 1f, 0f, 0f);

		// Token: 0x040022AC RID: 8876
		public Bounds bounds;

		// Token: 0x040022AD RID: 8877
		public Vector3 centerOffset;

		// Token: 0x040022AE RID: 8878
		public Vector3Int spriteOffset;

		// Token: 0x040022AF RID: 8879
		public Vector3Int islandSize;

		// Token: 0x020007B4 RID: 1972
		private enum Colors
		{
			// Token: 0x040022B1 RID: 8881
			Blood,
			// Token: 0x040022B2 RID: 8882
			Soot
		}
	}
}
