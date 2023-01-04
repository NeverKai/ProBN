using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000568 RID: 1384
public class MonoEvents : MonoBehaviour
{
	// Token: 0x06002400 RID: 9216 RVA: 0x00070D41 File Offset: 0x0006F141
	private void Awake()
	{
		this.onAwake.Invoke();
	}

	// Token: 0x06002401 RID: 9217 RVA: 0x00070D4E File Offset: 0x0006F14E
	private void Start()
	{
		this.onStart.Invoke();
	}

	// Token: 0x06002402 RID: 9218 RVA: 0x00070D5B File Offset: 0x0006F15B
	private void Update()
	{
		this.onUpdate.Invoke();
	}

	// Token: 0x040016A3 RID: 5795
	public UnityEvent onAwake = new UnityEvent();

	// Token: 0x040016A4 RID: 5796
	public UnityEvent onStart = new UnityEvent();

	// Token: 0x040016A5 RID: 5797
	public UnityEvent onUpdate = new UnityEvent();
}
