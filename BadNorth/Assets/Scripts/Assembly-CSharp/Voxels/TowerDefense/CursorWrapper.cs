using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020004B5 RID: 1205
	[AssetPath("Assets/Settings/Resources/")]
	[Serializable]
	public class CursorWrapper : ScriptableObjectSingleton<CursorWrapper>
	{
		// Token: 0x06001E85 RID: 7813 RVA: 0x00051810 File Offset: 0x0004FC10
		[RuntimeInitializeOnLoadMethod]
		private static void _Init()
		{
			ScriptableObjectSingleton<CursorWrapper>.instance.Init();
		}

		// Token: 0x06001E86 RID: 7814 RVA: 0x0005181C File Offset: 0x0004FC1C
		private void Init()
		{
			CursorWrapper.SetCursor(this.defaultCursor, this.defaultHotspot, CursorMode.Auto);
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06001E87 RID: 7815 RVA: 0x00051830 File Offset: 0x0004FC30
		// (set) Token: 0x06001E88 RID: 7816 RVA: 0x00051837 File Offset: 0x0004FC37
		public static CursorLockMode lockState
		{
			get
			{
				return Cursor.lockState;
			}
			set
			{
				Cursor.lockState = value;
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06001E89 RID: 7817 RVA: 0x0005183F File Offset: 0x0004FC3F
		public static bool visible
		{
			get
			{
				return Cursor.visible;
			}
		}

		// Token: 0x06001E8A RID: 7818 RVA: 0x00051846 File Offset: 0x0004FC46
		public static void BlockVisibility(object requester)
		{
			if (!CursorWrapper.visibilityBlockers.Contains(requester))
			{
				CursorWrapper.visibilityBlockers.Add(requester);
				CursorWrapper.UpdateVisibility();
			}
		}

		// Token: 0x06001E8B RID: 7819 RVA: 0x00051868 File Offset: 0x0004FC68
		public static void UnblockVisibility(object requester)
		{
			if (CursorWrapper.visibilityBlockers.Remove(requester))
			{
				CursorWrapper.UpdateVisibility();
			}
		}

		// Token: 0x06001E8C RID: 7820 RVA: 0x00051880 File Offset: 0x0004FC80
		private static void UpdateVisibility()
		{
			int count = CursorWrapper.visibilityBlockers.Count;
			if (count > 0)
			{
				for (int i = CursorWrapper.visibilityBlockers.Count - 1; i <= 0; i++)
				{
					if (CursorWrapper.visibilityBlockers[i] == null)
					{
						CursorWrapper.visibilityBlockers.RemoveAt(i);
					}
				}
			}
			Cursor.visible = (CursorWrapper.visibilityBlockers.Count == 0);
		}

		// Token: 0x06001E8D RID: 7821 RVA: 0x000518E9 File Offset: 0x0004FCE9
		public static void SetCursor(Texture2D texture)
		{
			CursorWrapper.SetCursor(texture, Vector2.zero, CursorMode.Auto);
		}

		// Token: 0x06001E8E RID: 7822 RVA: 0x000518F8 File Offset: 0x0004FCF8
		public static void SetCursor(Texture2D texture, Vector2 hotspot, CursorMode cursorMode = CursorMode.Auto)
		{
			using ("SetCursor")
			{
				if (texture == null)
				{
					texture = ScriptableObjectSingleton<CursorWrapper>.instance.defaultCursor;
					hotspot = ScriptableObjectSingleton<CursorWrapper>.instance.defaultHotspot;
				}
				if (CursorWrapper.textureCache != texture)
				{
					CursorWrapper.textureCache = texture;
					Cursor.SetCursor(texture, hotspot, cursorMode);
				}
			}
		}

		// Token: 0x040012FA RID: 4858
		[SerializeField]
		private Texture2D defaultCursor;

		// Token: 0x040012FB RID: 4859
		[SerializeField]
		private Vector2 defaultHotspot = Vector2.zero;

		// Token: 0x040012FC RID: 4860
		private static List<object> visibilityBlockers = new List<object>(8);

		// Token: 0x040012FD RID: 4861
		private static Texture2D textureCache = null;
	}
}
