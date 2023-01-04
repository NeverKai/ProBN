using System;
using UnityEngine;
using UnityEngine.UI;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x0200081C RID: 2076
	public class GraphicColorParent : MonoBehaviour
	{
		// Token: 0x06003641 RID: 13889 RVA: 0x000E9E1C File Offset: 0x000E821C
		private void OnValidate()
		{
			foreach (Graphic graphic in base.GetComponentsInChildren<Graphic>())
			{
				graphic.SetVerticesDirty();
			}
		}

		// Token: 0x06003642 RID: 13890 RVA: 0x000E9E50 File Offset: 0x000E8250
		public static Color GetColor(GraphicColorParent.Type type, GraphicColorParent parent)
		{
			Color color = (!parent) ? ScriptableObjectSingleton<PrefabManager>.instance.defaultGraphicParentColor : parent.color;
			switch (type)
			{
			case GraphicColorParent.Type.Button:
				return color;
			case GraphicColorParent.Type.Background:
			{
				float b = color.r * 0.2f + color.g * 0.7f + color.b * 0.1f;
				return (color * 0.5f + Color.white * b * 0.3f).SetA(1f);
			}
			case GraphicColorParent.Type.Decor:
				return Color.black.SetA(0.4f);
			case GraphicColorParent.Type.Dim:
				if (parent)
				{
					return (color * 0.2f).SetA(0.7f);
				}
				return new Color(0.2f, 0.2f, 0.2f, 0.7f);
			case GraphicColorParent.Type.Header:
				return Color.black.SetA(0.8f);
			default:
				return color;
			}
		}

		// Token: 0x040024D0 RID: 9424
		[SerializeField]
		public Color color = Color.white;

		// Token: 0x0200081D RID: 2077
		public enum Type
		{
			// Token: 0x040024D2 RID: 9426
			Button,
			// Token: 0x040024D3 RID: 9427
			Background,
			// Token: 0x040024D4 RID: 9428
			Decor,
			// Token: 0x040024D5 RID: 9429
			Dim,
			// Token: 0x040024D6 RID: 9430
			Header,
			// Token: 0x040024D7 RID: 9431
			None
		}
	}
}
