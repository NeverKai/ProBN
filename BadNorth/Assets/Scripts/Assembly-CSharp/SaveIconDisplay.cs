using System;
using CS.Platform;
using ReflexCLI.Attributes;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020004EE RID: 1262
public class SaveIconDisplay : MonoBehaviour
{
	// Token: 0x17000443 RID: 1091
	// (get) Token: 0x06002050 RID: 8272 RVA: 0x00056F22 File Offset: 0x00055322
	private float earliestCloseTime
	{
		get
		{
			return this.openTime + this.minDisplayTime;
		}
	}

	// Token: 0x06002051 RID: 8273 RVA: 0x00056F34 File Offset: 0x00055334
	private void Start()
	{
		PlatformEvents.OnSaveLocalStartedEvent += this.ShowSaveIcon;
		PlatformEvents.OnSaveLocalCompleteEvent += this.HideSaveIcon;
		PlatformEvents.OnSaveLoadFailEvent += delegate(bool b)
		{
			this.HideSaveIcon();
		};
		this.hideTimer = 0f;
		if (BasePlatformManager.Instance != null && BasePlatformManager.Instance.IsLocalSaving)
		{
			this.ShowSaveIcon();
		}
		else
		{
			this.HideSaveIcon(true);
		}
	}

	// Token: 0x06002052 RID: 8274 RVA: 0x00056FB0 File Offset: 0x000553B0
	private void Update()
	{
		if (Time.unscaledTime < this.earliestCloseTime)
		{
			float num = Time.unscaledTime - this.openTime;
			this.SetAlpha(Mathf.Lerp(this.openAlpha, 1f, num * 2f / this.fadeTime));
		}
		else if (this.hideTimer > 0f)
		{
			this.hideTimer -= Time.unscaledDeltaTime;
			this.SetAlpha(Mathf.Lerp(0f, 1f, this.hideTimer / this.fadeTime));
			if (this.hideTimer <= 0f)
			{
				this.saveIcon.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x06002053 RID: 8275 RVA: 0x00057068 File Offset: 0x00055468
	private void OnDestroy()
	{
		PlatformEvents.OnSaveLocalStartedEvent -= this.ShowSaveIcon;
		PlatformEvents.OnSaveLocalCompleteEvent -= this.HideSaveIcon;
		PlatformEvents.OnSaveLoadFailEvent -= this.HideSaveIcon;
	}

	// Token: 0x06002054 RID: 8276 RVA: 0x000570A0 File Offset: 0x000554A0
	[ConsoleCommand("")]
	private void ShowSaveIcon()
	{
		this.hideTimer = 0f;
		this.openTime = Time.unscaledTime;
		this.openAlpha = this.saveIcon.color.a;
		this.saveIcon.gameObject.SetActive(true);
	}

	// Token: 0x06002055 RID: 8277 RVA: 0x000570ED File Offset: 0x000554ED
	private void HideSaveIcon()
	{
		this.HideSaveIcon(false);
	}

	// Token: 0x06002056 RID: 8278 RVA: 0x000570F8 File Offset: 0x000554F8
	[ConsoleCommand("")]
	private void HideSaveIcon(bool instant = false)
	{
		if (instant)
		{
			this.openTime = 0f;
			this.hideTimer = 0f;
			this.SetAlpha(0f);
			this.saveIcon.gameObject.SetActive(false);
		}
		else
		{
			this.hideTimer = this.fadeTime;
		}
	}

	// Token: 0x06002057 RID: 8279 RVA: 0x0005714E File Offset: 0x0005554E
	[ConsoleCommand("")]
	private void ShowAndHideAfter(float seconds)
	{
		this.ShowSaveIcon();
		base.Invoke("HideSaveIcon", seconds);
	}

	// Token: 0x06002058 RID: 8280 RVA: 0x00057164 File Offset: 0x00055564
	private void SetAlpha(float a)
	{
		Color color = this.saveIcon.color;
		color.a = a;
		this.saveIcon.color = color;
	}

	// Token: 0x0400140E RID: 5134
	[SerializeField]
	private Image saveIcon;

	// Token: 0x0400140F RID: 5135
	[SerializeField]
	private float showTime = 0.25f;

	// Token: 0x04001410 RID: 5136
	[SerializeField]
	private float fadeTime = 0.5f;

	// Token: 0x04001411 RID: 5137
	[SerializeField]
	private float minDisplayTime = 1.5f;

	// Token: 0x04001412 RID: 5138
	private float openTime;

	// Token: 0x04001413 RID: 5139
	private float openAlpha;

	// Token: 0x04001414 RID: 5140
	private float hideTimer;
}
