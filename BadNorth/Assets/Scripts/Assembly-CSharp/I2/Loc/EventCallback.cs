using System;
using UnityEngine;

namespace I2.Loc
{
	// Token: 0x020003CD RID: 973
	[Serializable]
	public class EventCallback
	{
		// Token: 0x060015C4 RID: 5572 RVA: 0x0002D7C0 File Offset: 0x0002BBC0
		public void Execute(UnityEngine.Object Sender = null)
		{
			if (this.HasCallback() && Application.isPlaying)
			{
				this.Target.gameObject.SendMessage(this.MethodName, Sender, SendMessageOptions.DontRequireReceiver);
			}
		}

		// Token: 0x060015C5 RID: 5573 RVA: 0x0002D7EF File Offset: 0x0002BBEF
		public bool HasCallback()
		{
			return this.Target != null && !string.IsNullOrEmpty(this.MethodName);
		}

		// Token: 0x04000DB5 RID: 3509
		public MonoBehaviour Target;

		// Token: 0x04000DB6 RID: 3510
		public string MethodName = string.Empty;
	}
}
