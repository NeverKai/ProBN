using System;
using UnityEngine;

namespace Voxels
{
	// Token: 0x02000607 RID: 1543
	public class ChildAssigner : ModuleProcessor
	{
		// Token: 0x060027C2 RID: 10178 RVA: 0x00081404 File Offset: 0x0007F804
		public override void PreProcessModules(Module[] modules)
		{
			base.PreProcessModules(modules);
			for (int i = base.transform.childCount - 1; i >= 0; i--)
			{
				Transform child = base.transform.GetChild(i);
				for (int j = 0; j < modules.Length; j++)
				{
					if (modules[j].Contains(child.transform.position))
					{
						child.SetParent(modules[j].GetOrGreateObjectToCopy().transform, true);
					}
				}
			}
		}
	}
}
