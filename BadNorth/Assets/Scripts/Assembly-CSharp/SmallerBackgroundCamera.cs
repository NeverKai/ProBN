using System;
using UnityEngine;

// Token: 0x020005FD RID: 1533
[RequireComponent(typeof(Camera))]
public class SmallerBackgroundCamera : MonoBehaviour
{
	// Token: 0x04001959 RID: 6489
	[SerializeField]
	private float scaling = 0.3f;

	// Token: 0x0400195A RID: 6490
	[SerializeField]
	private int numFramesToRender = -1;
}
