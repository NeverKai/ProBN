using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x02000884 RID: 2180
	public class WaterfallSplash : MonoBehaviour
	{
		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x06003902 RID: 14594 RVA: 0x000F7F88 File Offset: 0x000F6388
		// (set) Token: 0x06003903 RID: 14595 RVA: 0x000F7F90 File Offset: 0x000F6390
		public float volume { get; private set; }

		// Token: 0x06003904 RID: 14596 RVA: 0x000F7F9C File Offset: 0x000F639C
		public void UpdateVisibility(Camera camera)
		{
			Vector3 position = base.transform.position;
			Ray ray = camera.ViewportPointToRay(camera.WorldToViewportPoint(position));
			float magnitude = (position - ray.origin).magnitude;
			bool flag = Physics.Raycast(ray, magnitude, LayerMaster.waterfallVisibilityMask);
			if (!Physics.SphereCast(ray, this.radius, magnitude, LayerMaster.waterfallVisibilityMask))
			{
				this.visibility = WaterfallSplash.Visibility.Visible;
			}
			else if (!flag)
			{
				this.visibility = WaterfallSplash.Visibility.Partial;
			}
			else
			{
				this.visibility = WaterfallSplash.Visibility.Hidden;
			}
		}

		// Token: 0x06003905 RID: 14597 RVA: 0x000F8032 File Offset: 0x000F6432
		private void Update()
		{
			this.volume = Mathf.MoveTowards(this.volume, this.GetTargetVolume(), 2.5f * Time.deltaTime);
		}

		// Token: 0x06003906 RID: 14598 RVA: 0x000F8058 File Offset: 0x000F6458
		private float GetTargetVolume()
		{
			switch (this.visibility)
			{
			case WaterfallSplash.Visibility.Hidden:
				return 0.075f;
			case WaterfallSplash.Visibility.Partial:
				return 0.15f;
			case WaterfallSplash.Visibility.Visible:
				return 0.2f;
			default:
				throw new NotImplementedException(string.Format("Unknown enum {0}", this.visibility));
			}
		}

		// Token: 0x06003907 RID: 14599 RVA: 0x000F80B0 File Offset: 0x000F64B0
		private void OnDrawGizmos()
		{
			Color color = Gizmos.color;
			Gizmos.color = WaterfallSplash.GetGizmoColor(this.visibility);
			Gizmos.DrawSphere(base.transform.position, this.radius);
			Gizmos.color = color;
		}

		// Token: 0x06003908 RID: 14600 RVA: 0x000F80EF File Offset: 0x000F64EF
		public static Color GetGizmoColor(WaterfallSplash.Visibility vis)
		{
			switch (vis)
			{
			case WaterfallSplash.Visibility.Hidden:
				return Color.red;
			case WaterfallSplash.Visibility.Partial:
				return Color.yellow;
			case WaterfallSplash.Visibility.Visible:
				return Color.green;
			default:
				throw new NotImplementedException(string.Format("Unknown enum {0}", vis));
			}
		}

		// Token: 0x04002702 RID: 9986
		public float radius;

		// Token: 0x04002703 RID: 9987
		public WaterfallSplash.Visibility visibility;

		// Token: 0x02000885 RID: 2181
		public enum Visibility
		{
			// Token: 0x04002706 RID: 9990
			Hidden,
			// Token: 0x04002707 RID: 9991
			Partial,
			// Token: 0x04002708 RID: 9992
			Visible
		}
	}
}
