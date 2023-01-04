using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200062A RID: 1578
public class MeshPool : MonoBehaviour
{
	// Token: 0x06002880 RID: 10368 RVA: 0x000875C8 File Offset: 0x000859C8
	public Mesh GetMesh()
	{
		if (this.pool.Count > 0)
		{
			Mesh result = this.pool[this.pool.Count - 1];
			this.pool.RemoveAt(this.pool.Count - 1);
			return result;
		}
		Mesh mesh = new Mesh();
		mesh.MarkDynamic();
		return mesh;
	}

	// Token: 0x06002881 RID: 10369 RVA: 0x00087626 File Offset: 0x00085A26
	public void ReturnMesh(ref Mesh mesh)
	{
		if (mesh != null)
		{
			mesh.Clear();
			this.pool.Add(mesh);
			mesh = null;
		}
	}

	// Token: 0x06002882 RID: 10370 RVA: 0x0008764C File Offset: 0x00085A4C
	public void ClearPool()
	{
		Debug.Log("MeshPool.ClearPool " + this.pool.Count);
		for (int i = 0; i < this.pool.Count; i++)
		{
			UnityEngine.Object.Destroy(this.pool[i]);
		}
		this.pool.Clear();
	}

	// Token: 0x040019FE RID: 6654
	[SerializeField]
	private List<Mesh> pool = new List<Mesh>();
}
