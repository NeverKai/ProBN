using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020005E1 RID: 1505
public class EventArray : MonoBehaviour
{
	// Token: 0x0600270A RID: 9994 RVA: 0x0007DB28 File Offset: 0x0007BF28
	public void ExecuteEvent(int index)
	{
		this.events[index].Invoke();
	}

	// Token: 0x04001904 RID: 6404
	public UnityEvent[] events;
}
