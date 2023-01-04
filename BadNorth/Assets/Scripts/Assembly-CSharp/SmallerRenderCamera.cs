using System;
using UnityEngine;

// Token: 0x020005FE RID: 1534
public class SmallerRenderCamera : MonoBehaviour
{
	// Token: 0x06002780 RID: 10112 RVA: 0x0007FDDC File Offset: 0x0007E1DC
	private void Start()
	{
		if (!DeviceCaps.isLowendDevice || DeviceCaps.hasLowResolutionScreen)
		{
			UnityEngine.Object.Destroy(this);
			return;
		}
		SmallerRenderCamera.renderScaleInUse = this.renderScale;
		this.myCamera = base.GetComponent<Camera>();
		int width = Mathf.RoundToInt((float)Screen.width * this.renderScale);
		int height = Mathf.RoundToInt((float)Screen.height * this.renderScale);
		this.renderTexture = new RenderTexture(width, height, 24);
		this.myCamera = base.GetComponent<Camera>();
		this.newCamera = new GameObject("Main Camera Buffer Display").AddComponent<Camera>();
		this.newCamera.transform.SetParent(base.transform);
		this.newCamera.transform.localPosition = Vector3.zero;
		this.newCamera.transform.localRotation = Quaternion.identity;
		this.newCamera.transform.localScale = Vector3.one;
		this.newCamera.cullingMask = 0;
		this.newCamera.clearFlags = CameraClearFlags.Nothing;
		this.newCamera.depth = this.myCamera.depth + 50f;
		this.myCamera.targetTexture = this.renderTexture;
		this.newCamera.gameObject.AddComponent<CameraRenderImage>().SetTexture(this.renderTexture, false);
	}

	// Token: 0x06002781 RID: 10113 RVA: 0x0007FF27 File Offset: 0x0007E327
	private void OnDestroy()
	{
		if (this.renderTexture != null)
		{
			this.myCamera.targetTexture = null;
			UnityEngine.Object.Destroy(this.renderTexture);
		}
	}

	// Token: 0x0400195B RID: 6491
	public static float renderScaleInUse = 1f;

	// Token: 0x0400195C RID: 6492
	[SerializeField]
	private float renderScale = 0.78f;

	// Token: 0x0400195D RID: 6493
	private RenderTexture renderTexture;

	// Token: 0x0400195E RID: 6494
	private Camera myCamera;

	// Token: 0x0400195F RID: 6495
	private Camera newCamera;
}
