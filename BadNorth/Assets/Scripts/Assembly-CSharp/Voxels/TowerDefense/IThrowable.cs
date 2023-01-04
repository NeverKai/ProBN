using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x0200088C RID: 2188
	public interface IThrowable
	{
		// Token: 0x06003948 RID: 14664
		void AttachTo(Transform newParent);

		// Token: 0x06003949 RID: 14665
		void SetVisible(bool visibility);

		// Token: 0x0600394A RID: 14666
		void ThrowAt(Vector3 worldSpaceTarget);

		// Token: 0x0600394B RID: 14667
		void Drop();
	}
}
