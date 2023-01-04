using System;
using UnityEngine;

// Token: 0x0200057A RID: 1402
[Serializable]
public class PlatformSettingsMap<M> where M : new()
{
	// Token: 0x040016F8 RID: 5880
	[SerializeField]
	public EPlatform platform = EPlatform.Windows;

	// Token: 0x040016F9 RID: 5881
	[SerializeField]
	public M settings = Activator.CreateInstance<M>();
}
