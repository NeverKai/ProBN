using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels
{
	// Token: 0x0200066F RID: 1647
	[Serializable]
	public class SavedWave
	{
		// Token: 0x06002A03 RID: 10755 RVA: 0x00095EA4 File Offset: 0x000942A4
		public SavedWave(Vector3 size, int count)
		{
			this.size = size;
			this.moduleOffset = new Vector3(-(size.x - 1f) / 2f, 0f, -(size.z - 1f) / 2f);
			this.module2World = this.moduleOffset.GetMoveMatrix();
			this.world2Module = this.module2World.inverse;
			this.dominos = new List<SavedWave.SavedModule>(count);
			this.voxel2world = this.module2World * Matrix4x4.TRS(-Vector3.one, Quaternion.identity, Vector3.one);
			this.world2Voxel = this.voxel2world.inverse;
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06002A04 RID: 10756 RVA: 0x00095F5F File Offset: 0x0009435F
		public Bounds bounds
		{
			get
			{
				return new Bounds((this.size - Vector3.one) / 2f, this.size);
			}
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06002A05 RID: 10757 RVA: 0x00095F86 File Offset: 0x00094386
		public Bounds voxelBounds
		{
			get
			{
				return new Bounds(this.size / 2f, this.size + Vector3.one);
			}
		}

		// Token: 0x06002A06 RID: 10758 RVA: 0x00095FB0 File Offset: 0x000943B0
		public IEnumerator<GenInfo> Reconstruct(Transform container)
		{
			for (int i = 0; i < this.dominos.Count; i++)
			{
				SavedWave.SavedModule domino = this.dominos[i];
				GameObject go = domino.orientedModule.GetGameObject();
				if (go)
				{
					go.transform.localPosition += domino.offset + this.moduleOffset;
					go.transform.SetParent(container, false);
					yield return new GenInfo("Placing", GenInfo.Mode.interruptable);
				}
			}
			yield return new GenInfo("Placing", GenInfo.Mode.interruptable);
			List<BoxCollider> boxColliders = ListPool<BoxCollider>.GetList(32);
			container.GetComponentsInChildren<BoxCollider>(true, boxColliders);
			yield return new GenInfo("Placing", GenInfo.Mode.interruptable);
			foreach (BoxCollider boxCollider in boxColliders)
			{
				if (boxCollider.transform.lossyScale.x < 0f)
				{
					boxCollider.size = boxCollider.size.SetX(-boxCollider.size.x);
				}
				boxCollider.enabled = true;
			}
			boxColliders.ReturnToListPool<BoxCollider>();
			yield break;
		}

		// Token: 0x04001B64 RID: 7012
		public Vector3 size;

		// Token: 0x04001B65 RID: 7013
		public List<SavedWave.SavedModule> dominos;

		// Token: 0x04001B66 RID: 7014
		public Vector3 moduleOffset;

		// Token: 0x04001B67 RID: 7015
		public Matrix4x4 module2World;

		// Token: 0x04001B68 RID: 7016
		public Matrix4x4 world2Module;

		// Token: 0x04001B69 RID: 7017
		public Matrix4x4 world2Voxel;

		// Token: 0x04001B6A RID: 7018
		public Matrix4x4 voxel2world;

		// Token: 0x02000670 RID: 1648
		public struct SavedModule
		{
			// Token: 0x04001B6B RID: 7019
			public OrientedModule orientedModule;

			// Token: 0x04001B6C RID: 7020
			public Placement placement;

			// Token: 0x04001B6D RID: 7021
			public Vector3 offset;
		}
	}
}
