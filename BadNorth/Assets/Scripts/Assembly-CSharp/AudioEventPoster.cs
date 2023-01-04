using System;
using UnityEngine;

// Token: 0x020006AE RID: 1710
public class AudioEventPoster : MonoBehaviour
{
	// Token: 0x06002C33 RID: 11315 RVA: 0x000A4062 File Offset: 0x000A2462
	public void PostEvent(string eventName)
	{
		FabricWrapper.PostEvent(eventName);
	}

	// Token: 0x06002C34 RID: 11316 RVA: 0x000A406B File Offset: 0x000A246B
	public void PostEventGameObject(string eventName)
	{
		FabricWrapper.PostEvent(eventName, base.gameObject);
	}
}
