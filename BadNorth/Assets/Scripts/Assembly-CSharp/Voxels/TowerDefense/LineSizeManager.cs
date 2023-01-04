using System;
using ReflexCLI.Attributes;
using RTM.OnScreenDebug;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Voxels.TowerDefense
{
	// Token: 0x0200079E RID: 1950
	public class LineSizeManager : UIBehaviour
	{
		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x06003244 RID: 12868 RVA: 0x000D53AD File Offset: 0x000D37AD
		// (set) Token: 0x06003245 RID: 12869 RVA: 0x000D53B4 File Offset: 0x000D37B4
		[ConsoleCommand("")]
		public static float lineWidth
		{
			get
			{
				return LineSizeManager._lineWidth;
			}
			set
			{
				LineSizeManager._lineWidth = value;
				Shader.SetGlobalFloat(LineSizeManager.lineWidthId, value);
			}
		}

		// Token: 0x06003246 RID: 12870 RVA: 0x000D53CC File Offset: 0x000D37CC
		protected override void OnRectTransformDimensionsChange()
		{
			base.OnRectTransformDimensionsChange();
			this.UpdateLineWidth();
		}

		// Token: 0x06003247 RID: 12871 RVA: 0x000D53DA File Offset: 0x000D37DA
		protected override void Awake()
		{
			base.Awake();
			this.UpdateLineWidth();
		}

		// Token: 0x06003248 RID: 12872 RVA: 0x000D53E8 File Offset: 0x000D37E8
		private void UpdateLineWidth()
		{
			float b = (float)Screen.currentResolution.height;
			float a = (float)Screen.height;
			float b2 = Mathf.Lerp(a, b, 0.25f);
			LineSizeManager._lineWidth = this.multiplier * Mathf.Max(1080f, b2) / 1080f;
			Shader.SetGlobalFloat(LineSizeManager.lineWidthId, LineSizeManager.lineWidth);
			LineSizeManager.onLineWidthChange();
		}

		// Token: 0x04002227 RID: 8743
		private static DebugChannelGroup dbgGroup = new DebugChannelGroup("LineWidth", EVerbosity.Quiet, 10);

		// Token: 0x04002228 RID: 8744
		private static float _lineWidth = 1f;

		// Token: 0x04002229 RID: 8745
		[SerializeField]
		private float multiplier = 1f;

		// Token: 0x0400222A RID: 8746
		private static ShaderId lineWidthId = "_LineWidth";

		// Token: 0x0400222B RID: 8747
		public static Action onLineWidthChange = delegate()
		{
		};
	}
}
