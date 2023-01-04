using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x0200069D RID: 1693
	public class Godray : AgentComponent
	{
		// Token: 0x06002BAB RID: 11179 RVA: 0x000A0284 File Offset: 0x0009E684
		public override void Setup()
		{
			base.Setup();
			if (!Godray.mesh)
			{
				Godray.mesh = new Mesh();
				Godray.mesh.vertices = new Vector3[]
				{
					Vector3.zero,
					Vector3.up * 0.1f,
					Vector3.up * 0.1f,
					Vector3.up * 20f,
					Vector3.up * 20f
				};
				Godray.mesh.uv2 = new Vector2[]
				{
					new Vector2(0f, 0f),
					new Vector2(1f, -1f),
					new Vector2(-1f, -1f),
					new Vector2(1f, 0f),
					new Vector2(-1f, 0f)
				};
				Godray.mesh.triangles = new int[]
				{
					0,
					1,
					2,
					2,
					1,
					3,
					2,
					3,
					4
				};
				Vector3 right = Vector3.right;
				Godray.mesh.normals = new Vector3[]
				{
					right,
					right,
					right,
					right,
					right
				};
			}
			MeshFilter component = base.GetComponent<MeshFilter>();
			MeshRenderer component2 = base.GetComponent<MeshRenderer>();
			component.sharedMesh = Godray.mesh;
			this.material = UnityEngine.Object.Instantiate<Material>(component2.sharedMaterial);
			component2.sharedMaterial = this.material;
			this.material.color = base.enSquad.hero.color;
			base.agent.OnAgentSelected += this.OnSelected;
			base.gameObject.SetActive(false);
		}

		// Token: 0x06002BAC RID: 11180 RVA: 0x000A04BB File Offset: 0x0009E8BB
		private void OnSelected(Agent agent, bool selected, bool showGodray)
		{
			if (selected && showGodray)
			{
				this.radius = 0.1f;
				base.gameObject.SetActive(true);
			}
			this.speed = (float)((!selected) ? 20 : 4);
		}

		// Token: 0x06002BAD RID: 11181 RVA: 0x000A04F8 File Offset: 0x0009E8F8
		private void Update()
		{
			this.radius = Mathf.Lerp(this.radius, this.target, Time.unscaledDeltaTime * this.speed);
			if (this.radius < 0.001f)
			{
				base.gameObject.SetActive(false);
			}
			else
			{
				this.material.SetFloat(Godray.radiusID, this.radius);
			}
		}

		// Token: 0x06002BAE RID: 11182 RVA: 0x000A0564 File Offset: 0x0009E964
		private void OnDestroy()
		{
			UnityEngine.Object.Destroy(this.material);
		}

		// Token: 0x04001C7A RID: 7290
		private static Mesh mesh;

		// Token: 0x04001C7B RID: 7291
		private static ShaderId radiusID = "_Radius";

		// Token: 0x04001C7C RID: 7292
		private Material material;

		// Token: 0x04001C7D RID: 7293
		private float target;

		// Token: 0x04001C7E RID: 7294
		private float radius;

		// Token: 0x04001C7F RID: 7295
		private float speed = 4f;
	}
}
