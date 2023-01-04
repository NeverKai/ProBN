using System;
using UnityEngine;
using UnityEngine.Profiling;

// Token: 0x020005F5 RID: 1525
public struct ScopedProfiler : IDisposable
{
	// Token: 0x06002759 RID: 10073 RVA: 0x0007F9DA File Offset: 0x0007DDDA
	public ScopedProfiler(string name, UnityEngine.Object targetObject = null)
	{
		this.sampler = null;
		if (targetObject)
		{
		}
	}

	// Token: 0x0600275A RID: 10074 RVA: 0x0007F9F3 File Offset: 0x0007DDF3
	private ScopedProfiler(CustomSampler sampler)
	{
		this.sampler = sampler;
	}

	// Token: 0x0600275B RID: 10075 RVA: 0x0007F9FC File Offset: 0x0007DDFC
	void IDisposable.Dispose()
	{
		if (this.sampler != null)
		{
		}
	}

	// Token: 0x0600275C RID: 10076 RVA: 0x0007FA0E File Offset: 0x0007DE0E
	public static implicit operator ScopedProfiler(string name)
	{
		return new ScopedProfiler(name, null);
	}

	// Token: 0x0600275D RID: 10077 RVA: 0x0007FA17 File Offset: 0x0007DE17
	public static implicit operator ScopedProfiler(CustomSampler sampler)
	{
		return new ScopedProfiler(sampler);
	}

	// Token: 0x0400193D RID: 6461
	private CustomSampler sampler;
}
