using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020005F2 RID: 1522
public class PrerenderedUIElement : MonoBehaviour
{
	// Token: 0x06002747 RID: 10055 RVA: 0x0007EEE7 File Offset: 0x0007D2E7
	private void Awake()
	{
		if (PrerenderedUIElement.alphaBlendPremultiplied == null)
		{
			PrerenderedUIElement.alphaBlendPremultiplied = Resources.Load<Material>("AlphaBlendPremultiplied");
		}
		this.rectTransform = base.GetComponent<RectTransform>();
		this.graphic = base.GetComponent<Graphic>();
	}

	// Token: 0x06002748 RID: 10056 RVA: 0x0007EF20 File Offset: 0x0007D320
	private void Update()
	{
		if (this.renderCount > 0)
		{
			this.Render();
			if (this.renderTex != null)
			{
				this.renderCount--;
			}
		}
	}

	// Token: 0x06002749 RID: 10057 RVA: 0x0007EF54 File Offset: 0x0007D354
	private void Remove()
	{
		if (this.renderTex != null)
		{
			RenderTexture.ReleaseTemporary(this.renderTex);
		}
		if (this.rawImage != null)
		{
			UnityEngine.Object.Destroy(this.rawImage.gameObject);
		}
		this.renderTex = null;
		this.rawImage = null;
	}

	// Token: 0x0600274A RID: 10058 RVA: 0x0007EFAC File Offset: 0x0007D3AC
	private void OnDestroy()
	{
		this.Remove();
	}

	// Token: 0x0600274B RID: 10059 RVA: 0x0007EFB4 File Offset: 0x0007D3B4
	private void ComputeRect()
	{
		this.screenRect = this.rectTransform.GetWorldSpaceRect();
	}

	// Token: 0x0600274C RID: 10060 RVA: 0x0007EFC8 File Offset: 0x0007D3C8
	private void SetupCamera()
	{
		if (PrerenderedUIElement.renderCamera == null)
		{
			int num = LayerMask.NameToLayer("UI");
			GameObject gameObject = new GameObject("PrenderedUIElementCamera");
			gameObject.transform.SetParent(null);
			UnityEngine.Object.DontDestroyOnLoad(gameObject);
			PrerenderedUIElement.renderCamera = gameObject.AddComponent<Camera>();
			PrerenderedUIElement.renderCamera.orthographic = true;
			PrerenderedUIElement.renderCamera.cullingMask = 1 << num;
			PrerenderedUIElement.renderCamera.backgroundColor = Color.clear;
			PrerenderedUIElement.renderCamera.clearFlags = CameraClearFlags.Color;
			PrerenderedUIElement.renderCamera.nearClipPlane = -500f;
			PrerenderedUIElement.renderCamera.farClipPlane = 500f;
			gameObject = new GameObject("TempCanvas");
			gameObject.layer = num;
			gameObject.transform.SetParent(PrerenderedUIElement.renderCamera.transform);
			PrerenderedUIElement.tempCanvas = gameObject.AddComponent<Canvas>();
			PrerenderedUIElement.tempCanvas.renderMode = RenderMode.WorldSpace;
			PrerenderedUIElement.tempCanvas.worldCamera = PrerenderedUIElement.renderCamera;
			PrerenderedUIElement.tempCanvas.transform.localPosition = Vector3.zero;
			PrerenderedUIElement.tempCanvas.transform.localScale = Vector3.one;
			PrerenderedUIElement.tempCanvas.additionalShaderChannels = (AdditionalCanvasShaderChannels.TexCoord1 | AdditionalCanvasShaderChannels.TexCoord2 | AdditionalCanvasShaderChannels.Normal | AdditionalCanvasShaderChannels.Tangent);
			PrerenderedUIElement.renderCamera.enabled = false;
		}
		PrerenderedUIElement.renderCamera.targetTexture = this.renderTex;
	}

	// Token: 0x0600274D RID: 10061 RVA: 0x0007F108 File Offset: 0x0007D508
	private void Resize()
	{
		Canvas.ForceUpdateCanvases();
		this.ComputeRect();
		if ((int)this.screenRect.width <= 0 || (int)this.screenRect.height <= 0)
		{
			return;
		}
		if (this.renderTex != null && this.renderTex.width == (int)this.screenRect.width && this.renderTex.height == (int)this.screenRect.height)
		{
			return;
		}
		if (this.renderTex != null)
		{
			UnityEngine.Object.Destroy(this.renderTex);
		}
		this.renderTex = RenderTexture.GetTemporary((int)this.screenRect.width, (int)this.screenRect.height, 0, RenderTextureFormat.ARGB32);
		this.SetupCamera();
		PrerenderedUIElement.renderCamera.orthographicSize = this.screenRect.height * 0.5f;
		PrerenderedUIElement.tempCanvas.GetComponent<RectTransform>().sizeDelta = new Vector2(this.screenRect.width, this.screenRect.height);
		Canvas.ForceUpdateCanvases();
	}

	// Token: 0x0600274E RID: 10062 RVA: 0x0007F220 File Offset: 0x0007D620
	private void Render()
	{
		PrerenderedUIElement.isRendering++;
		if (this.rawImage == null)
		{
			GameObject gameObject = new GameObject("Prerendered Image - " + base.name);
			gameObject.transform.SetParent(base.transform);
			gameObject.transform.SetSiblingIndex(0);
			RectTransform rectTransform = gameObject.AddComponent<RectTransform>();
			rectTransform.localScale = Vector3.one;
			rectTransform.pivot = this.rectTransform.pivot;
			rectTransform.anchorMin = Vector2.zero;
			rectTransform.anchorMax = Vector2.one;
			rectTransform.offsetMin = Vector2.zero;
			rectTransform.offsetMax = Vector2.zero;
			rectTransform.sizeDelta = Vector2.zero;
			this.rawImage = gameObject.AddComponent<RawImage>();
			this.rawImage.raycastTarget = true;
			gameObject.layer = base.gameObject.layer;
			this.rawImage.material = PrerenderedUIElement.alphaBlendPremultiplied;
		}
		this.childrenActiveState.Clear();
		IEnumerator enumerator = base.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				this.childrenActiveState.Enqueue(transform.gameObject.activeSelf);
				transform.gameObject.SetActive(false);
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
		if (!this.alreadyMoved)
		{
			this.Resize();
		}
		if (PrerenderedUIElement.renderCamera == null)
		{
			return;
		}
		int siblingIndex = this.rectTransform.GetSiblingIndex();
		Transform parent = this.rectTransform.parent;
		Vector3 position = this.rectTransform.position;
		Vector2 anchorMin = this.rectTransform.anchorMin;
		Vector2 anchorMax = this.rectTransform.anchorMax;
		Vector2 offsetMin = this.rectTransform.offsetMin;
		Vector2 offsetMax = this.rectTransform.offsetMax;
		Vector2 pivot = this.rectTransform.pivot;
		Vector3 localScale = this.rectTransform.localScale;
		this.rectTransform.position = Vector3.right * 10000f;
		PrerenderedUIElement.renderCamera.transform.position = this.rectTransform.position;
		this.rectTransform.SetParent(PrerenderedUIElement.tempCanvas.transform);
		this.rectTransform.anchorMin = Vector2.zero;
		this.rectTransform.anchorMax = Vector2.one;
		this.rectTransform.offsetMin = Vector2.zero;
		this.rectTransform.offsetMax = Vector2.zero;
		this.rectTransform.localScale = Vector3.one;
		this.rectTransform.pivot = Vector2.zero;
		this.alreadyMoved = true;
		this.graphic.enabled = true;
		PrerenderedUIElement.renderCamera.Render();
		PrerenderedUIElement.renderCamera.targetTexture = null;
		this.rectTransform.SetParent(parent);
		this.rectTransform.SetSiblingIndex(siblingIndex);
		this.rectTransform.anchorMin = anchorMin;
		this.rectTransform.anchorMax = anchorMax;
		this.rectTransform.offsetMin = offsetMin;
		this.rectTransform.offsetMax = offsetMax;
		this.rectTransform.pivot = pivot;
		this.rectTransform.localScale = localScale;
		this.rectTransform.position = position;
		IEnumerator enumerator2 = base.transform.GetEnumerator();
		try
		{
			while (enumerator2.MoveNext())
			{
				object obj2 = enumerator2.Current;
				Transform transform2 = (Transform)obj2;
				transform2.gameObject.SetActive(this.childrenActiveState.Dequeue());
			}
		}
		finally
		{
			IDisposable disposable2;
			if ((disposable2 = (enumerator2 as IDisposable)) != null)
			{
				disposable2.Dispose();
			}
		}
		this.rawImage.texture = this.renderTex;
		this.graphic.enabled = false;
		PrerenderedUIElement.isRendering--;
	}

	// Token: 0x0600274F RID: 10063 RVA: 0x0007F604 File Offset: 0x0007DA04
	private void OnEnable()
	{
		this.alreadyMoved = false;
		this.ScheduleRender(1);
	}

	// Token: 0x06002750 RID: 10064 RVA: 0x0007F614 File Offset: 0x0007DA14
	private void OnDisable()
	{
		if (PrerenderedUIElement.isRendering == 0)
		{
			this.Remove();
		}
	}

	// Token: 0x06002751 RID: 10065 RVA: 0x0007F626 File Offset: 0x0007DA26
	public void ScheduleRender(int count = 1)
	{
		this.renderCount = count;
	}

	// Token: 0x04001926 RID: 6438
	private RenderTexture renderTex;

	// Token: 0x04001927 RID: 6439
	private RectTransform rectTransform;

	// Token: 0x04001928 RID: 6440
	private Graphic graphic;

	// Token: 0x04001929 RID: 6441
	private static Material alphaBlendPremultiplied;

	// Token: 0x0400192A RID: 6442
	private int renderCount;

	// Token: 0x0400192B RID: 6443
	private Vector3[] corners = new Vector3[4];

	// Token: 0x0400192C RID: 6444
	private Rect screenRect = default(Rect);

	// Token: 0x0400192D RID: 6445
	private static Camera renderCamera;

	// Token: 0x0400192E RID: 6446
	private static Canvas tempCanvas;

	// Token: 0x0400192F RID: 6447
	private bool alreadyMoved;

	// Token: 0x04001930 RID: 6448
	private RawImage rawImage;

	// Token: 0x04001931 RID: 6449
	private Queue<bool> childrenActiveState = new Queue<bool>(12);

	// Token: 0x04001932 RID: 6450
	private static int isRendering;
}
