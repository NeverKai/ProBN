using System;
using RTM.Utilities;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020008A3 RID: 2211
	public class TargetNavSpotHighlight : MonoBehaviour, NavSpot.IHighlight, TargetNavSpot.INavSpotComponent, TargetMesh.IMeshComponent
	{
		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x060039CC RID: 14796 RVA: 0x000FCC3C File Offset: 0x000FB03C
		Color NavSpot.IHighlight.highlightColor
		{
			get
			{
				if (!base.gameObject.activeInHierarchy)
				{
					return Color.clear;
				}
				Color color = this.colorRenderer.sharedMaterial.GetColor(TargetNavSpotHighlight.colorPropertyId);
				color.a = this.baseAlpha;
				color.a *= this.owner.visibilityFraction * Mathf.Lerp(1f, this.hoverTargetAlpha, this.owner.hoverFraction);
				color.a *= this.colorRenderer.sharedMaterial.GetFloat(TargetNavSpotHighlight.fillId);
				return color;
			}
		}

		// Token: 0x060039CD RID: 14797 RVA: 0x000FCCDC File Offset: 0x000FB0DC
		void TargetMesh.IMeshComponent.Init(TargetMesh owner)
		{
			this.owner = owner;
			this.baseAlpha = this.colorRenderer.sharedMaterial.GetColor(TargetNavSpotHighlight.colorPropertyId).a;
			owner.onVisibleChange = (Action<bool>)Delegate.Combine(owner.onVisibleChange, new Action<bool>(this.OnVisibleChange));
		}

		// Token: 0x060039CE RID: 14798 RVA: 0x000FCD35 File Offset: 0x000FB135
		void TargetMesh.IMeshComponent.SetMesh(Mesh mesh)
		{
		}

		// Token: 0x060039CF RID: 14799 RVA: 0x000FCD37 File Offset: 0x000FB137
		void TargetNavSpot.INavSpotComponent.SetNavSpot(NavSpot newNavSpot)
		{
			if (this.navSpot)
			{
				this.navSpot.Target.RemoveHighlight(this);
			}
			this.navSpot.Target = newNavSpot;
		}

		// Token: 0x060039D0 RID: 14800 RVA: 0x000FCD67 File Offset: 0x000FB167
		private void OnVisibleChange(bool visible)
		{
			if (this.navSpot)
			{
				if (visible)
				{
					this.navSpot.Target.AddHighlight(this);
				}
				else
				{
					this.navSpot.Target.RemoveHighlight(this);
				}
			}
		}

		// Token: 0x060039D1 RID: 14801 RVA: 0x000FCDA7 File Offset: 0x000FB1A7
		private void Reset()
		{
			this.colorRenderer = base.GetComponent<MeshRenderer>();
		}

		// Token: 0x040027D9 RID: 10201
		[SerializeField]
		private MeshRenderer colorRenderer;

		// Token: 0x040027DA RID: 10202
		private WeakReference<NavSpot> navSpot = new WeakReference<NavSpot>(null);

		// Token: 0x040027DB RID: 10203
		[SerializeField]
		private float hoverTargetAlpha = 4f;

		// Token: 0x040027DC RID: 10204
		private TargetMesh owner;

		// Token: 0x040027DD RID: 10205
		private static int fillId = Shader.PropertyToID("_Fill");

		// Token: 0x040027DE RID: 10206
		private static int colorPropertyId = Shader.PropertyToID("_Color");

		// Token: 0x040027DF RID: 10207
		private float baseAlpha = 1f;
	}
}
