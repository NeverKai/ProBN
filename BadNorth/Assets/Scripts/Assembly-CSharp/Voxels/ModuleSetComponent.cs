using System;
using UnityEngine;

namespace Voxels
{
	// Token: 0x02000632 RID: 1586
	[RequireComponent(typeof(ModuleSet))]
	public abstract class ModuleSetComponent : MonoBehaviour
	{
		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x0600289A RID: 10394 RVA: 0x00088501 File Offset: 0x00086901
		public ModuleSet moduleSet
		{
			get
			{
				if (this._moduleSet == null)
				{
					this._moduleSet = base.GetComponent<ModuleSet>();
				}
				return this._moduleSet;
			}
		}

		// Token: 0x04001A25 RID: 6693
		private ModuleSet _moduleSet;
	}
}
