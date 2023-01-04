using System;
using UnityEngine;

// Token: 0x020004F1 RID: 1265
[ExecuteInEditMode]
public class BackgroundImage : MonoBehaviour
{
	// Token: 0x0600205F RID: 8287 RVA: 0x000572C4 File Offset: 0x000556C4
	private void LateUpdate()
	{
		base.transform.position = Camera.main.transform.position + Camera.main.transform.forward * (Camera.main.farClipPlane - 1f);
		base.transform.rotation = Camera.main.transform.rotation;
		float num = Camera.main.orthographicSize * 2f;
		base.transform.localScale = new Vector3(num / (float)Screen.height * (float)Screen.width, num, 1f);
	}
}
