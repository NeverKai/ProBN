using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

// Token: 0x0200057B RID: 1403
[Serializable]
public class PlatformVariantSettings<T, M> where T : class, new() where M : PlatformSettingsMap<T>, new()
{
	// Token: 0x14000084 RID: 132
	// (add) Token: 0x06002457 RID: 9303 RVA: 0x00072170 File Offset: 0x00070570
	// (remove) Token: 0x06002458 RID: 9304 RVA: 0x000721A8 File Offset: 0x000705A8
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event PlatformVariantSettings<T, M>.Del onUpdated = delegate()
	{
	};

	// Token: 0x06002459 RID: 9305 RVA: 0x000721DE File Offset: 0x000705DE
	public T Get()
	{
		return (this.current == null) ? this.DetermineAndGetCurrent() : this.current;
	}

	// Token: 0x0600245A RID: 9306 RVA: 0x00072204 File Offset: 0x00070604
	private T DetermineAndGetCurrent()
	{
		Platform.onPlatformUpdated += this.OnPlatformUpdated;
		this.current = (T)((object)null);
		int i = 0;
		int count = this.platformVariants.Count;
		while (i < count)
		{
			if (this.platformVariants[i] != null && Platform.Is(this.platformVariants[i].platform))
			{
				this.current = this.platformVariants[i].settings;
				return this.current;
			}
			i++;
		}
		this.current = this.defaultValues;
		return this.current;
	}

	// Token: 0x0600245B RID: 9307 RVA: 0x000722B8 File Offset: 0x000706B8
	private void OnPlatformUpdated()
	{
		T t = this.current;
		this.DetermineAndGetCurrent();
		if (t != null && t != this.current)
		{
			this.onUpdated();
		}
	}

	// Token: 0x0600245C RID: 9308 RVA: 0x00072300 File Offset: 0x00070700
	~PlatformVariantSettings()
	{
		Platform.onPlatformUpdated -= this.OnPlatformUpdated;
	}

	// Token: 0x040016FA RID: 5882
	[SerializeField]
	[Tooltip("Applied to any platforms not specified")]
	private T defaultValues = Activator.CreateInstance<T>();

	// Token: 0x040016FB RID: 5883
	[SerializeField]
	private List<M> platformVariants = new List<M>();

	// Token: 0x040016FC RID: 5884
	private T current;

	// Token: 0x0200057C RID: 1404
	// (Invoke) Token: 0x0600245F RID: 9311
	public delegate void Del();
}
