using System;
using UnityEngine;

// Token: 0x020005BD RID: 1469
public class Screenshotter : MonoBehaviour
{
	// Token: 0x0600267C RID: 9852 RVA: 0x00079ED5 File Offset: 0x000782D5
	private void Awake()
	{
		this.count = 0;
	}

	// Token: 0x0600267D RID: 9853 RVA: 0x00079EDE File Offset: 0x000782DE
	public void Reset()
	{
		this.count = 0;
	}

	// Token: 0x0600267E RID: 9854 RVA: 0x00079EE8 File Offset: 0x000782E8
	public void Screenshot(string text)
	{
		string filename = string.Format("Screenshots/Screenshot_{0}_{1}.png", this.count.ToString("D4"), text);
		ScreenCapture.CaptureScreenshot(filename);
		this.count++;
	}

	// Token: 0x04001861 RID: 6241
	private int count;
}
