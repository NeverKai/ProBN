using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000518 RID: 1304
public class FrameRate : MonoBehaviour
{
	// Token: 0x060021E7 RID: 8679 RVA: 0x00060C8B File Offset: 0x0005F08B
	private void Start()
	{
	}

	// Token: 0x060021E8 RID: 8680 RVA: 0x00060C90 File Offset: 0x0005F090
	private void Update()
	{
		this.text.text = (1f / Time.smoothDeltaTime).ToString("f2");
	}

	// Token: 0x040014AE RID: 5294
	public Text text;
}
