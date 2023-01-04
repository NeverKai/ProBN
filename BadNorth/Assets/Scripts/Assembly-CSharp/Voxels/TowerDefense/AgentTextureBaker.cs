using System;
using SpriteComposing;
using UnityEngine;
using UnityEngine.Rendering;
using Voxels.TowerDefense.SpriteMagic;

namespace Voxels.TowerDefense
{
	// Token: 0x020005C4 RID: 1476
	public class AgentTextureBaker : Singleton<AgentTextureBaker>
	{
		// Token: 0x06002689 RID: 9865 RVA: 0x0007A060 File Offset: 0x00078460
		[ContextMenu("Draw")]
		public void Draw(Agent agent, float bottom, float top, RenderTexture tex)
		{
			if (!agent)
			{
				return;
			}
			if (this.buffer == null)
			{
				this.buffer = new CommandBuffer();
				this.drawTex = new RenderTexture(tex.width * 2, tex.height * 2, 24, RenderTextureFormat.ARGB32);
			}
			this.buffer.Clear();
			float num = 0.5f * (top - bottom) * ((float)this.drawTex.width / (float)this.drawTex.height);
			Matrix4x4 projectionMatrix = Matrix4x4.Ortho(-num, num, bottom, top, 10f, -10f);
			Matrix4x4 matrix4x = Matrix4x4.Rotate(Quaternion.Euler(0f, 216f, 0f) * Quaternion.Euler(10f, 0f, 0f)).inverse;
			matrix4x *= Matrix4x4.TRS(agent.transform.position, agent.transform.rotation, Vector3.one).inverse;
			this.buffer.DisableShaderKeyword("_GAME_ON");
			this.buffer.SetProjectionMatrix(projectionMatrix);
			this.buffer.SetRenderTarget(this.drawTex);
			this.buffer.ClearRenderTarget(true, true, new Color(0f, 0f, 0f, 0f));
			this.RecursiveDraw(agent.transform, this.buffer, matrix4x);
			this.buffer.EnableShaderKeyword("_GAME_ON");
			Graphics.ExecuteCommandBuffer(this.buffer);
			this.buffer.Clear();
			Graphics.Blit(this.drawTex, tex, this.blitMaterial);
		}

		// Token: 0x0600268A RID: 9866 RVA: 0x0007A204 File Offset: 0x00078604
		private void RecursiveDraw(Transform t, CommandBuffer buffer, Matrix4x4 m)
		{
			SpriteRenderer component = t.GetComponent<SpriteRenderer>();
			if (component && component.sprite)
			{
				Mesh mesh = SpriteMeshDictionary.GetMesh(component.sprite);
				BatchedSprite component2 = component.GetComponent<BatchedSprite>();
				if (component2)
				{
					MaterialPropertyBlock materialPropertyBlock = component2.GetMaterialPropertyBlock();
					materialPropertyBlock.SetVector(AgentTextureBaker.uvId, component.color.SetB(0f) - Color.white);
					SpriteAnimator spriteAnimator = component2 as SpriteAnimator;
					if (spriteAnimator)
					{
						spriteAnimator.UpdateSprite2();
						materialPropertyBlock.SetVector(AgentTextureBaker.partTexTexelSizeId, spriteAnimator.sprite2.texture.texelSize);
					}
					materialPropertyBlock.SetVector(AgentTextureBaker.mainTexTexelSizeId, component.sprite.texture.texelSize);
					buffer.DrawMesh(mesh, m * component.transform.localToWorldMatrix, component.sharedMaterial, 0, 0, materialPropertyBlock);
				}
				else
				{
					buffer.DrawMesh(mesh, m * component.transform.localToWorldMatrix, component.sharedMaterial);
				}
			}
			MeshRenderer component3 = t.GetComponent<MeshRenderer>();
			if (component3)
			{
				buffer.DrawMesh(component3.GetComponent<MeshFilter>().sharedMesh, m * component3.transform.localToWorldMatrix, component3.sharedMaterial);
			}
			for (int i = 0; i < t.childCount; i++)
			{
				Transform child = t.GetChild(i);
				if (child.gameObject.activeSelf)
				{
					this.RecursiveDraw(child, buffer, m);
				}
			}
		}

		// Token: 0x04001889 RID: 6281
		private CommandBuffer buffer;

		// Token: 0x0400188A RID: 6282
		private static ShaderId uvId = "_UV";

		// Token: 0x0400188B RID: 6283
		private static ShaderId partTexTexelSizeId = "_PartTex_TexelSize";

		// Token: 0x0400188C RID: 6284
		private static ShaderId mainTexTexelSizeId = "_MainTex_TexelSize";

		// Token: 0x0400188D RID: 6285
		[SerializeField]
		private Transform transform0;

		// Token: 0x0400188E RID: 6286
		[SerializeField]
		private Transform transform1;

		// Token: 0x0400188F RID: 6287
		[SerializeField]
		private Material blitMaterial;

		// Token: 0x04001890 RID: 6288
		private const float rotationX = 10f;

		// Token: 0x04001891 RID: 6289
		private const float rotationY = 216f;

		// Token: 0x04001892 RID: 6290
		public RenderTexture drawTex;
	}
}
