using System;
using UnityEngine;

namespace RTM.UISystem.Internal
{
	// Token: 0x020004CF RID: 1231
	public static class Helpers
	{
		// Token: 0x06001F07 RID: 7943 RVA: 0x0005352D File Offset: 0x0005192D
		public static bool IsVertical(Vector2 input)
		{
			return Mathf.Abs(input.y) > Mathf.Abs(input.x);
		}

		// Token: 0x06001F08 RID: 7944 RVA: 0x00053549 File Offset: 0x00051949
		public static bool IsHorizontal(Vector2 input)
		{
			return !Helpers.IsVertical(input);
		}
	}
}
