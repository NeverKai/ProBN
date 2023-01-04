using System;
using UnityEngine;

// Token: 0x0200056B RID: 1387
public class DemoDependent : MonoBehaviour
{
	// Token: 0x040016AF RID: 5807
	[SerializeField]
	private DemoDependent.EAvailability availableInDemo;

	// Token: 0x040016B0 RID: 5808
	[SerializeField]
	private Behaviour specificComponent;

	// Token: 0x0200056C RID: 1388
	private enum EAvailability
	{
		// Token: 0x040016B2 RID: 5810
		NonDemoOnly,
		// Token: 0x040016B3 RID: 5811
		DemoOnly,
		// Token: 0x040016B4 RID: 5812
		AlwaysAvailable
	}
}
