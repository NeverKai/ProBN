using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005DA RID: 1498
public class CameraRenderImage : MonoBehaviour
{
	// Token: 0x060026E2 RID: 9954 RVA: 0x0007CB10 File Offset: 0x0007AF10
	private void Awake()
	{
		this.myCamera = base.GetComponent<Camera>();
		this.GenerateMesh();
		Material source = Resources.Load<Material>("FullScreenQuadMaterial");
		this.material = new Material(source);
	}

	// Token: 0x060026E3 RID: 9955 RVA: 0x0007CB46 File Offset: 0x0007AF46
	public void SetTexture(Texture t, bool drawFirst = true)
	{
		this.texture = t;
		this.material.mainTexture = t;
		this.drawFirst = drawFirst;
	}

	// Token: 0x060026E4 RID: 9956 RVA: 0x0007CB64 File Offset: 0x0007AF64
	private void OnPreRender()
	{
		if (this.myCamera.targetTexture != null)
		{
			Graphics.SetRenderTarget(this.myCamera.targetTexture);
		}
		else
		{
			Graphics.SetRenderTarget(null);
		}
		GL.Clear(true, true, Color.black);
		this.material.SetPass(0);
		Graphics.Blit(this.texture, this.myCamera.targetTexture);
	}

	// Token: 0x060026E5 RID: 9957 RVA: 0x0007CBD4 File Offset: 0x0007AFD4
	private void GenerateMesh()
	{
		List<Vector3> list = new List<Vector3>();
		List<int> list2 = new List<int>();
		list.Add(new Vector3(-1f, -1f, 0f));
		list.Add(new Vector3(-1f, 1f, 0f));
		list.Add(new Vector3(1f, 1f, 0f));
		list.Add(new Vector3(1f, -1f, 0f));
		list2.Add(0);
		list2.Add(1);
		list2.Add(2);
		list2.Add(2);
		list2.Add(3);
		list2.Add(0);
		this.mesh = new Mesh();
		this.mesh.SetVertices(list);
		this.mesh.RecalculateBounds();
		this.mesh.SetTriangles(list2, 0);
	}

	// Token: 0x040018E7 RID: 6375
	private Material material;

	// Token: 0x040018E8 RID: 6376
	public Texture texture;

	// Token: 0x040018E9 RID: 6377
	private Camera myCamera;

	// Token: 0x040018EA RID: 6378
	private bool drawFirst = true;

	// Token: 0x040018EB RID: 6379
	private Mesh mesh;
}
