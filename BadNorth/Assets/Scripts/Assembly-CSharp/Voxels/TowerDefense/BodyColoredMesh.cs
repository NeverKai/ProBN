using System;
using System.Collections.Generic;
using UnityEngine;
using Voxels.TowerDefense.CampaignGeneration;
using Voxels.TowerDefense.SpriteMagic;

namespace Voxels.TowerDefense
{
	// Token: 0x02000690 RID: 1680
	public class BodyColoredMesh : AgentComponent, Agent.IUpdateVisuals, Campaign.ICampaignDestroy
	{
		// Token: 0x06002B07 RID: 11015 RVA: 0x0009ACAC File Offset: 0x000990AC
		void Agent.IUpdateVisuals.UpdateVisuals()
		{
			MeshFilter component = base.GetComponent<MeshFilter>();
			if (this.mesh)
			{
				UnityEngine.Object.Destroy(this.mesh);
			}
			this.mesh = UnityEngine.Object.Instantiate<Mesh>(component.sharedMesh);
			Sprite sprite = base.agent.GetComponentInChildren<SpriteAnimator>(true).sprite2;
			this.color = sprite.texture.GetPixel((int)sprite.textureRect.xMin + 32, (int)sprite.textureRect.yMin + 24);
			List<Color32> list = ListPool<Color32>.GetList(this.mesh.vertexCount);
			for (int i = 0; i < this.mesh.vertexCount; i++)
			{
				list.Add(this.color);
			}
			this.mesh.SetColors(list);
			ListPool<Color32>.ReturnList(list);
			component.sharedMesh = this.mesh;
		}

		// Token: 0x06002B08 RID: 11016 RVA: 0x0009AD97 File Offset: 0x00099197
		private void OnDestroy()
		{
			if (this.mesh)
			{
				UnityEngine.Object.Destroy(this.mesh);
			}
		}

		// Token: 0x06002B09 RID: 11017 RVA: 0x0009ADB4 File Offset: 0x000991B4
		void Campaign.ICampaignDestroy.OnCampaignDestroy(Campaign campaign)
		{
			if (this.mesh)
			{
				UnityEngine.Object.Destroy(this.mesh);
			}
		}

		// Token: 0x04001C07 RID: 7175
		private Mesh mesh;

		// Token: 0x04001C08 RID: 7176
		public Color color;
	}
}
