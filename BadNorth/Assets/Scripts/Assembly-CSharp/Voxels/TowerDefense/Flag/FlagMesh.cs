using System;
using System.Collections.Generic;
using UnityEngine;
using Voxels.TowerDefense.SpriteMagic;

namespace Voxels.TowerDefense.Flag
{
	// Token: 0x02000514 RID: 1300
	public class FlagMesh : AgentComponent
	{
		// Token: 0x060021CD RID: 8653 RVA: 0x0005EFB4 File Offset: 0x0005D3B4
		private void Awake()
		{
			this.mf = base.GetComponent<MeshFilter>();
			this.mr = base.GetComponent<MeshRenderer>();
			this.boxCollider = base.GetComponentInChildren<BoxCollider>();
			this.block = new MaterialPropertyBlock();
			this.flagPhysics = base.GetComponent<FlagPhysics>();
			if (!FlagMesh.mirrorMaterial)
			{
				for (int i = 0; i < 12; i++)
				{
					int num = i * 12;
					int num2 = i * 2;
					FlagMesh.tris[num] = num2;
					FlagMesh.tris[num + 1] = num2 + 1;
					FlagMesh.tris[num + 2] = num2 + 2;
					FlagMesh.tris[num + 3] = num2 + 2;
					FlagMesh.tris[num + 4] = num2 + 1;
					FlagMesh.tris[num + 5] = num2 + 3;
					FlagMesh.tris[num + 6] = num2;
					FlagMesh.tris[num + 7] = num2 + 2;
					FlagMesh.tris[num + 8] = num2 + 1;
					FlagMesh.tris[num + 9] = num2 + 2;
					FlagMesh.tris[num + 10] = num2 + 3;
					FlagMesh.tris[num + 11] = num2 + 1;
				}
				FlagMesh.mirrorMaterial = UnityEngine.Object.Instantiate<Material>(this.mr.sharedMaterial);
				FlagMesh.mirrorMaterial.EnableKeyword("_MIRROR_ON");
			}
			if (FlagMesh.meshes.Count == 0)
			{
				this.mesh = new Mesh();
				this.mesh.MarkDynamic();
				this.mesh.vertices = FlagMesh.verts;
				this.mesh.triangles = FlagMesh.tris;
				this.mesh.bounds = new Bounds(Vector3.zero, Vector3.one * 10000f);
			}
			else
			{
				this.mesh = FlagMesh.meshes.Pop();
			}
			this.mf.sharedMesh = this.mesh;
			this.mirrorMr = this.mr.gameObject.AddEmptyChild("Mirror").AddComponent<MeshRenderer>();
			this.mirrorMr.sharedMaterial = FlagMesh.mirrorMaterial;
			this.mirrorMr.gameObject.AddComponent<MeshFilter>().sharedMesh = this.mesh;
		}

		// Token: 0x060021CE RID: 8654 RVA: 0x0005F1B4 File Offset: 0x0005D5B4
		public void SetHero(HeroDefinition hero)
		{
			using (new ScopedProfiler("FlagMesh.SetHero", null))
			{
				Sprite flag = hero.graphics.flag;
				if (!(this.sprite == flag))
				{
					for (int i = 0; i < FlagMesh.colors.Length; i++)
					{
						FlagMesh.colors[i] = hero.color;
					}
					this.block.SetTexture(ShaderId.mainTexId, flag.texture);
					this.mr.SetPropertyBlock(this.block);
					this.mirrorMr.SetPropertyBlock(this.block);
					this.sprite = flag;
					this.spriteBounds = new SpriteBounds(flag);
					Vector3 vector = new Vector3(0f, -this.spriteBounds.v.size.y, this.spriteBounds.v.size.x * 0.7f);
					this.boxCollider.center = vector / 2f;
					this.boxCollider.size = ExtraMath.Abs(vector) + Vector3.right * 0.1f;
					this.mesh.uv = FlagMesh.uvs;
					this.mesh.colors32 = FlagMesh.colors;
				}
			}
		}

		// Token: 0x060021CF RID: 8655 RVA: 0x0005F344 File Offset: 0x0005D744
		private void OnDestroy()
		{
			if (this.mesh)
			{
				FlagMesh.meshes.Push(this.mesh);
				this.mesh = null;
			}
		}

		// Token: 0x060021D0 RID: 8656 RVA: 0x0005F370 File Offset: 0x0005D770
		private void UpdateArrays()
		{
			float num = this.sprite.border.x * this.sprite.texture.texelSize.x / this.spriteBounds.uv.width * this.spriteBounds.v.width;
			float d = this.sprite.border.w * this.sprite.texture.texelSize.y / this.spriteBounds.uv.height * this.spriteBounds.v.height;
			float num2 = this.spriteBounds.v.width - num;
			float num3 = num2 / 12f * 1.5f;
			List<FlagPhysics.Node> nodes = this.flagPhysics.nodes;
			Vector3 a = base.transform.up;
			Vector3 vector = base.transform.position;
			Vector3 vector2 = vector;
			float num4 = num;
			int i = 0;
			if (nodes.Count > 0)
			{
				Vector3 b = (nodes[0].pos - base.transform.position) / 6f;
				for (int j = 0; j < nodes.Count; j++)
				{
					Vector3 vector3 = (nodes[j].pos - vector) / 3f;
					Vector3 vector4 = (vector + nodes[j].pos) / 2f;
					Vector3 vector5 = (a + nodes[j].up) / 2f;
					Vector3 a2 = vector;
					Vector3 a3 = vector + b;
					Vector3 a4 = vector4 - vector3;
					Vector3 vector6 = vector4;
					float num5 = Vector3.Distance(a2, vector6);
					float num6 = num3 / num5;
					for (float num7 = 0f; num7 < 1f; num7 += num6)
					{
						float num8 = num7;
						float d2 = 1f - num7;
						Vector3 vector7 = Vector3.zero;
						Vector3 a5 = Vector3.Lerp(a, vector5, num8);
						vector7 += a2 * d2 * d2 * d2;
						vector7 += a3 * d2 * d2 * num8 * 3f;
						vector7 += a4 * d2 * num8 * num8 * 3f;
						vector7 += vector6 * num8 * num8 * num8;
						float num9 = Vector3.Distance(vector2, vector7);
						num4 += num9;
						Vector3 b2 = a5 * d;
						if (num4 > this.spriteBounds.v.size.x)
						{
							float maxDistanceDelta = num4 - this.spriteBounds.v.size.x;
							vector7 = Vector3.MoveTowards(vector7, vector2, maxDistanceDelta);
							while (i < 13)
							{
								FlagMesh.verts[i * 2] = vector7 - a5 * this.spriteBounds.v.size.y + b2;
								FlagMesh.verts[i * 2 + 1] = vector7 + b2;
								FlagMesh.uvs[i * 2] = new Vector2(this.spriteBounds.uv.max.x, this.spriteBounds.uv.min.y);
								FlagMesh.uvs[i * 2 + 1] = new Vector2(this.spriteBounds.uv.max.x, this.spriteBounds.uv.max.y);
								i++;
							}
							return;
						}
						FlagMesh.verts[i * 2] = vector7 - a5 * this.spriteBounds.v.size.y + b2;
						FlagMesh.verts[i * 2 + 1] = vector7 + b2;
						float x = this.spriteBounds.uv.min.x + num4 / this.spriteBounds.v.size.x * this.spriteBounds.uv.size.x;
						FlagMesh.uvs[i * 2] = new Vector2(x, this.spriteBounds.uv.min.y);
						FlagMesh.uvs[i * 2 + 1] = new Vector2(x, this.spriteBounds.uv.max.y);
						i++;
						if (i == 13)
						{
							return;
						}
						vector2 = vector7;
					}
					vector = vector4;
					b = vector3;
					a = vector5;
				}
			}
			while (i < 13)
			{
				Vector3 b3 = a * d;
				FlagMesh.verts[i * 2] = vector - a * this.spriteBounds.v.size.y + b3;
				FlagMesh.verts[i * 2 + 1] = vector + b3;
				FlagMesh.uvs[i * 2] = new Vector2(this.spriteBounds.uv.max.x, this.spriteBounds.uv.min.y);
				FlagMesh.uvs[i * 2 + 1] = new Vector2(this.spriteBounds.uv.max.x, this.spriteBounds.uv.max.y);
				i++;
			}
		}

		// Token: 0x060021D1 RID: 8657 RVA: 0x0005F9FC File Offset: 0x0005DDFC
		private void UpdateCollider()
		{
			Matrix4x4 worldToLocalMatrix = base.transform.worldToLocalMatrix;
			Vector3 vector = Vector3.zero;
			for (int i = 0; i < FlagMesh.verts.Length; i += 2)
			{
				vector += FlagMesh.verts[i] - FlagMesh.verts[3];
			}
			float num = vector.magnitude / (float)FlagMesh.verts.Length * 2f;
			num = Mathf.Lerp(this.boxCollider.size.z, num, 0.4f);
			Vector3 vector2 = new Vector3(0f, -this.spriteBounds.v.size.y, num);
			this.boxCollider.center = vector2 / 2f;
			this.boxCollider.size = ExtraMath.Abs(vector2) + Vector3.right * 0.1f;
			vector = vector.GetZeroY();
			if (vector == Vector3.zero)
			{
				return;
			}
			this.boxCollider.transform.rotation = Quaternion.LookRotation(vector);
		}

		// Token: 0x060021D2 RID: 8658 RVA: 0x0005FB28 File Offset: 0x0005DF28
		private void LateUpdate()
		{
			this.UpdateArrays();
			this.UpdateCollider();
			using (new ScopedProfiler("Apply arrays to mesh", null))
			{
				for (int i = 0; i < FlagMesh.verts.Length; i++)
				{
					Vector3 vector = FlagMesh.verts[i];
					FlagMesh.uv2s[i] = new Vector2(FlagMesh.verts[i].x, FlagMesh.verts[i].y);
					FlagMesh.uv3s[i] = new Vector2(FlagMesh.verts[i].z, 1f);
				}
				this.mesh.uv = FlagMesh.uvs;
				this.mesh.uv2 = FlagMesh.uv2s;
				this.mesh.uv3 = FlagMesh.uv3s;
			}
		}

		// Token: 0x060021D3 RID: 8659 RVA: 0x0005FC2C File Offset: 0x0005E02C
		private void OnDrawGizmos()
		{
			Gizmos.DrawWireMesh(this.mesh);
		}

		// Token: 0x04001487 RID: 5255
		private MeshFilter mf;

		// Token: 0x04001488 RID: 5256
		private MeshRenderer mr;

		// Token: 0x04001489 RID: 5257
		private MeshRenderer mirrorMr;

		// Token: 0x0400148A RID: 5258
		private FlagPhysics flagPhysics;

		// Token: 0x0400148B RID: 5259
		private static int meshCount = 0;

		// Token: 0x0400148C RID: 5260
		private static Stack<Mesh> meshes = new Stack<Mesh>();

		// Token: 0x0400148D RID: 5261
		private Mesh mesh;

		// Token: 0x0400148E RID: 5262
		private const int segmentCount = 12;

		// Token: 0x0400148F RID: 5263
		private const int vertCount = 26;

		// Token: 0x04001490 RID: 5264
		private static Vector3[] verts = new Vector3[26];

		// Token: 0x04001491 RID: 5265
		private static Vector2[] uvs = new Vector2[26];

		// Token: 0x04001492 RID: 5266
		private static Vector2[] uv2s = new Vector2[26];

		// Token: 0x04001493 RID: 5267
		private static Vector2[] uv3s = new Vector2[26];

		// Token: 0x04001494 RID: 5268
		private static Color32[] colors = new Color32[26];

		// Token: 0x04001495 RID: 5269
		private static int[] tris = new int[144];

		// Token: 0x04001496 RID: 5270
		public Sprite sprite;

		// Token: 0x04001497 RID: 5271
		public static Material mirrorMaterial;

		// Token: 0x04001498 RID: 5272
		private SpriteBounds spriteBounds;

		// Token: 0x04001499 RID: 5273
		private BoxCollider boxCollider;

		// Token: 0x0400149A RID: 5274
		private MaterialPropertyBlock block;
	}
}
