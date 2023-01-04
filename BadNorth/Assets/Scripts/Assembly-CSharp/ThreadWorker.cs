using System;
using System.Collections;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

// Token: 0x02000603 RID: 1539
public class ThreadWorker
{
	// Token: 0x06002793 RID: 10131 RVA: 0x00080276 File Offset: 0x0007E676
	public ThreadWorker(IEnumerator threadImplementation, string name) : this()
	{
		this.Start(threadImplementation, name);
	}

	// Token: 0x06002794 RID: 10132 RVA: 0x00080286 File Offset: 0x0007E686
	public ThreadWorker()
	{
	}

	// Token: 0x1700054E RID: 1358
	// (get) Token: 0x06002795 RID: 10133 RVA: 0x000802B3 File Offset: 0x0007E6B3
	// (set) Token: 0x06002796 RID: 10134 RVA: 0x000802BB File Offset: 0x0007E6BB
	public Exception exception { get; private set; }

	// Token: 0x06002797 RID: 10135 RVA: 0x000802C4 File Offset: 0x0007E6C4
	public void Start(IEnumerator threadImplementation, string name)
	{
		try
		{
			this.name = name;
			this.ChildThread = new Thread(new ParameterizedThreadStart(this.ThreadLoop));
			this.ChildThread.Start(threadImplementation);
		}
		catch (Exception ex)
		{
			this.name = string.Empty;
			if (this.ChildThread != null)
			{
				this.ChildThread.Abort();
			}
			this.ChildThread = null;
			throw ex;
		}
	}

	// Token: 0x06002798 RID: 10136 RVA: 0x0008033C File Offset: 0x0007E73C
	public void Resume()
	{
		this.SuspendHandle.Set();
	}

	// Token: 0x06002799 RID: 10137 RVA: 0x0008034A File Offset: 0x0007E74A
	public void Suspend()
	{
		if (!this.WantAbort)
		{
			this.SuspendHandle.Reset();
		}
	}

	// Token: 0x0600279A RID: 10138 RVA: 0x00080363 File Offset: 0x0007E763
	public void Abort(bool block = true)
	{
		this.WantAbort = true;
		this.Resume();
		if (block)
		{
			this.AbortHandle.WaitOne();
		}
	}

	// Token: 0x0600279B RID: 10139 RVA: 0x00080384 File Offset: 0x0007E784
	private void ThreadLoop(object threadImplementation)
	{
		Stopwatch stopwatch = Stopwatch.StartNew();
		try
		{
			IEnumerator enumerator = threadImplementation as IEnumerator;
			while (!this.WantAbort && enumerator.MoveNext())
			{
				if (this.WantAbort)
				{
					break;
				}
				stopwatch.Stop();
				this.SuspendHandle.WaitOne();
				stopwatch.Start();
			}
		}
		catch (Exception exception)
		{
			this.exception = exception;
			this.WantAbort = true;
			UnityEngine.Debug.LogException(exception);
			UnityEngine.Debug.LogErrorFormat("Error occured in thread - '{0}'", new object[]
			{
				this.name
			});
		}
		this.AbortHandle.Set();
		this.ChildThread = null;
	}

	// Token: 0x0600279C RID: 10140 RVA: 0x0008043C File Offset: 0x0007E83C
	public bool IsRunning()
	{
		return this.ChildThread != null;
	}

	// Token: 0x0600279D RID: 10141 RVA: 0x0008044A File Offset: 0x0007E84A
	public bool IsSuspended()
	{
		return this.IsRunning() && !this.SuspendHandle.WaitOne(0);
	}

	// Token: 0x0600279E RID: 10142 RVA: 0x00080469 File Offset: 0x0007E869
	public bool IsCompleted()
	{
		return this.ChildThread == null;
	}

	// Token: 0x0600279F RID: 10143 RVA: 0x00080474 File Offset: 0x0007E874
	public bool Exceptioned()
	{
		return this.exception != null;
	}

	// Token: 0x0400196B RID: 6507
	private Thread ChildThread;

	// Token: 0x0400196C RID: 6508
	private EventWaitHandle SuspendHandle = new EventWaitHandle(true, EventResetMode.ManualReset);

	// Token: 0x0400196D RID: 6509
	private EventWaitHandle AbortHandle = new EventWaitHandle(false, EventResetMode.ManualReset);

	// Token: 0x0400196E RID: 6510
	private bool WantAbort;

	// Token: 0x04001970 RID: 6512
	private string name = string.Empty;
}
