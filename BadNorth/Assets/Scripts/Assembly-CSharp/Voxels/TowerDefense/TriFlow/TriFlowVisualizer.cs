using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.TowerDefense.TriFlow
{
	// Token: 0x0200080C RID: 2060
	public class TriFlowVisualizer : MonoBehaviour, IIslandProcessor
	{
		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x060035E5 RID: 13797 RVA: 0x000E8CA8 File Offset: 0x000E70A8
		private MeshFilter mf
		{
			get
			{
				if (!this._mf)
				{
					this._mf = base.GetComponent<MeshFilter>();
				}
				return this._mf;
			}
		}

		// Token: 0x060035E6 RID: 13798 RVA: 0x000E8CCC File Offset: 0x000E70CC
		public void Awake()
		{
			this.mesh = new Mesh();
			this.mf.sharedMesh = this.mesh;
		}

		// Token: 0x060035E7 RID: 13799 RVA: 0x000E8CEC File Offset: 0x000E70EC
		IEnumerator<GenInfo> IIslandProcessor.OnIslandProcess(Island island, SavedWave savedWave)
		{
			this.navMesh = island.navMesh;
			this.navMesh.GetNewMesh(this.mesh);
			yield return new GenInfo("TriflowVis", GenInfo.Mode.interruptable);
			yield break;
		}

		// Token: 0x060035E8 RID: 13800 RVA: 0x000E8D0E File Offset: 0x000E710E
		public void OnDestroy()
		{
			if (this.mesh)
			{
				UnityEngine.Object.Destroy(this.mesh);
			}
		}

		// Token: 0x060035E9 RID: 13801 RVA: 0x000E8D2B File Offset: 0x000E712B
		private void LateUpdate()
		{
		}

		// Token: 0x040024A8 RID: 9384
		private MeshFilter _mf;

		// Token: 0x040024A9 RID: 9385
		private Mesh mesh;

		// Token: 0x040024AA RID: 9386
		private NavigationMesh navMesh;
	}
}
