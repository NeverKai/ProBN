using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace CS.Platform.Utils
{
	// Token: 0x02000070 RID: 112
	public class ThreadHandler
	{
		// Token: 0x060004F6 RID: 1270 RVA: 0x000146D4 File Offset: 0x00012AD4
		public ThreadHandler(string name)
		{
			this._threadName = name;
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00014768 File Offset: 0x00012B68
		~ThreadHandler()
		{
			this.Abort(false);
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00014798 File Offset: 0x00012B98
		public void Start()
		{
			object threadAbortLock = this._threadAbortLock;
			lock (threadAbortLock)
			{
				object threadRestartLock = this._threadRestartLock;
				lock (threadRestartLock)
				{
					object threadLock = this._threadLock;
					lock (threadLock)
					{
						object threadOnceLock = this._threadOnceLock;
						lock (threadOnceLock)
						{
							if (this._threadAbort && this._thread != null)
							{
								this._threadAbort = false;
							}
							else if (this._thread != null && this._threadOnce)
							{
								this.Restart();
							}
							else if (!this.Running)
							{
								this._threadAbort = false;
								this.PauseThread = false;
								this._thread = new Thread(new ThreadStart(this.ThreadLoop));
								this._thread.Name = this._threadName;
								this._thread.Priority = this._priority;
								this._threadRestart = true;
								this._thread.Start();
							}
						}
					}
				}
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x000148E4 File Offset: 0x00012CE4
		public bool Running
		{
			get
			{
				object threadLock = this._threadLock;
				bool result;
				lock (threadLock)
				{
					result = (this._thread != null);
				}
				return result;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x00014928 File Offset: 0x00012D28
		// (set) Token: 0x060004FB RID: 1275 RVA: 0x00014968 File Offset: 0x00012D68
		public bool PauseThread
		{
			get
			{
				object threadPauseLock = this._threadPauseLock;
				bool threadPause;
				lock (threadPauseLock)
				{
					threadPause = this._threadPause;
				}
				return threadPause;
			}
			set
			{
				object threadPauseLock = this._threadPauseLock;
				lock (threadPauseLock)
				{
					this._threadPause = value;
				}
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060004FC RID: 1276 RVA: 0x000149A8 File Offset: 0x00012DA8
		public bool ThreadPaused
		{
			get
			{
				object threadPausedLock = this._threadPausedLock;
				bool result;
				lock (threadPausedLock)
				{
					result = (this._threadPaused && this.PauseThread);
				}
				return result;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x000149F4 File Offset: 0x00012DF4
		// (set) Token: 0x060004FE RID: 1278 RVA: 0x00014A34 File Offset: 0x00012E34
		private bool AbortThread
		{
			get
			{
				object threadAbortLock = this._threadAbortLock;
				bool threadAbort;
				lock (threadAbortLock)
				{
					threadAbort = this._threadAbort;
				}
				return threadAbort;
			}
			set
			{
				object threadAbortLock = this._threadAbortLock;
				lock (threadAbortLock)
				{
					this._threadAbort = value;
				}
			}
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x00014A74 File Offset: 0x00012E74
		public void Abort(bool block)
		{
			this.AbortThread = true;
			if (block)
			{
				bool flag = true;
				while (flag)
				{
					object threadLock = this._threadLock;
					lock (threadLock)
					{
						flag = (this._thread != null);
					}
				}
			}
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x00014AD4 File Offset: 0x00012ED4
		public void Pause(bool block)
		{
			this.PauseThread = true;
			if (block)
			{
				bool flag = true;
				while (flag)
				{
					flag = !this.ThreadPaused;
				}
			}
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x00014B08 File Offset: 0x00012F08
		public void ForceAbort()
		{
			object threadAbortLock = this._threadAbortLock;
			lock (threadAbortLock)
			{
				object threadRestartLock = this._threadRestartLock;
				lock (threadRestartLock)
				{
					object threadLock = this._threadLock;
					lock (threadLock)
					{
						object threadOnceLock = this._threadOnceLock;
						lock (threadOnceLock)
						{
							if (this._thread != null)
							{
								this._thread.Abort();
							}
						}
					}
				}
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000502 RID: 1282 RVA: 0x00014BC4 File Offset: 0x00012FC4
		// (set) Token: 0x06000503 RID: 1283 RVA: 0x00014C04 File Offset: 0x00013004
		public bool RunThreadOnce
		{
			get
			{
				object threadOnceLock = this._threadOnceLock;
				bool threadOnce;
				lock (threadOnceLock)
				{
					threadOnce = this._threadOnce;
				}
				return threadOnce;
			}
			set
			{
				object threadOnceLock = this._threadOnceLock;
				lock (threadOnceLock)
				{
					this._threadOnce = value;
				}
			}
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x00014C44 File Offset: 0x00013044
		public void Complete()
		{
			this._threadComplete = true;
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000505 RID: 1285 RVA: 0x00014C4D File Offset: 0x0001304D
		// (set) Token: 0x06000506 RID: 1286 RVA: 0x00014C55 File Offset: 0x00013055
		public ThreadPriority Priority
		{
			get
			{
				return this._priority;
			}
			set
			{
				this._priority = value;
				if (this._thread != null)
				{
					this._thread.Priority = this._priority;
				}
			}
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00014C7C File Offset: 0x0001307C
		public void AddPart(Action part)
		{
			object threadCallLock = this._threadCallLock;
			lock (threadCallLock)
			{
				this._threadCalls.Add(part);
			}
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x00014CC0 File Offset: 0x000130C0
		public void RemovePart(Action part)
		{
			object threadCalls = this._threadCalls;
			lock (threadCalls)
			{
				this._threadCalls.Remove(part);
			}
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x00014D04 File Offset: 0x00013104
		public void ClearParts()
		{
			object threadCalls = this._threadCalls;
			lock (threadCalls)
			{
				this._threadCalls.Clear();
			}
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x00014D48 File Offset: 0x00013148
		public bool HasPart(Action part)
		{
			object threadCalls = this._threadCalls;
			bool result;
			lock (threadCalls)
			{
				result = this._threadCalls.Contains(part);
			}
			return result;
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x00014D8C File Offset: 0x0001318C
		public void Restart()
		{
			object threadRestartLock = this._threadRestartLock;
			lock (threadRestartLock)
			{
				this._threadRestart = true;
			}
		}

		// Token: 0x14000036 RID: 54
		// (add) Token: 0x0600050C RID: 1292 RVA: 0x00014DCC File Offset: 0x000131CC
		// (remove) Token: 0x0600050D RID: 1293 RVA: 0x00014E04 File Offset: 0x00013204
		
		public event Action OnCompletion;

		// Token: 0x0600050E RID: 1294 RVA: 0x00014E3A File Offset: 0x0001323A
		public void ClearComplete()
		{
			this.OnCompletion = null;
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x00014E44 File Offset: 0x00013244
		private void ThreadLoop()
		{
			bool flag = false;
			while (!flag)
			{
				if (!this.PauseThread)
				{
					object threadPausedLock = this._threadPausedLock;
					lock (threadPausedLock)
					{
						this._threadPaused = false;
					}
					object threadRestartLock = this._threadRestartLock;
					lock (threadRestartLock)
					{
						if (this._threadRestart)
						{
							this._numberCalling = 0;
							this._threadRestart = false;
						}
					}
					object threadCallLock = this._threadCallLock;
					lock (threadCallLock)
					{
						if (this._numberCalling < this._threadCalls.Count)
						{
							Action action2 = this._threadCalls[this._numberCalling];
							try
							{
								if (action2 != null)
								{
									action2();
								}
							}
							catch (Exception ex)
							{
								Debug.ThreadLogError("Failed Update | Call: {0} | Source: {1} | Message: {2}", new object[]
								{
									action2,
									ex.TargetSite,
									ex.Message
								});
							}
							this._numberCalling++;
						}
						if (this._threadComplete)
						{
							object threadAbortLock = this._threadAbortLock;
							lock (threadAbortLock)
							{
								object threadLock = this._threadLock;
								lock (threadLock)
								{
									flag = true;
									this._thread = null;
									Action action = this.OnCompletion;
									BasePlatformManager.Instance.AddToNextUpdate(delegate
									{
										if (action != null)
										{
											action();
										}
									});
									break;
								}
							}
						}
						object threadAbortLock2 = this._threadAbortLock;
						lock (threadAbortLock2)
						{
							object threadRestartLock2 = this._threadRestartLock;
							lock (threadRestartLock2)
							{
								object threadLock2 = this._threadLock;
								lock (threadLock2)
								{
									object threadOnceLock = this._threadOnceLock;
									lock (threadOnceLock)
									{
										if (this._numberCalling >= this._threadCalls.Count)
										{
											this._numberCalling = 0;
											if (this.RunThreadOnce && !this._threadRestart)
											{
												flag = true;
												this._thread = null;
												Action action = this.OnCompletion;
												BasePlatformManager.Instance.AddToNextUpdate(delegate
												{
													if (action != null)
													{
														action();
													}
												});
												break;
											}
										}
									}
								}
							}
						}
					}
				}
				else
				{
					object threadPausedLock2 = this._threadPausedLock;
					lock (threadPausedLock2)
					{
						this._threadPaused = true;
					}
				}
				object threadAbortLock3 = this._threadAbortLock;
				lock (threadAbortLock3)
				{
					object threadLock3 = this._threadLock;
					lock (threadLock3)
					{
						if (this.AbortThread)
						{
							flag = true;
							this._thread = null;
							break;
						}
					}
				}
			}
		}

		// Token: 0x04000211 RID: 529
		private string _threadName = string.Empty;

		// Token: 0x04000212 RID: 530
		private object _threadLock = new object();

		// Token: 0x04000213 RID: 531
		private Thread _thread;

		// Token: 0x04000214 RID: 532
		private object _threadPauseLock = new object();

		// Token: 0x04000215 RID: 533
		private bool _threadPause = true;

		// Token: 0x04000216 RID: 534
		private object _threadPausedLock = new object();

		// Token: 0x04000217 RID: 535
		private bool _threadPaused = true;

		// Token: 0x04000218 RID: 536
		private object _threadAbortLock = new object();

		// Token: 0x04000219 RID: 537
		private bool _threadAbort;

		// Token: 0x0400021A RID: 538
		private object _threadOnceLock = new object();

		// Token: 0x0400021B RID: 539
		private bool _threadOnce;

		// Token: 0x0400021C RID: 540
		private bool _threadComplete;

		// Token: 0x0400021D RID: 541
		private ThreadPriority _priority = ThreadPriority.BelowNormal;

		// Token: 0x0400021E RID: 542
		private object _threadCallLock = new object();

		// Token: 0x0400021F RID: 543
		private List<Action> _threadCalls = new List<Action>();

		// Token: 0x04000220 RID: 544
		private object _threadRestartLock = new object();

		// Token: 0x04000221 RID: 545
		private bool _threadRestart;

		// Token: 0x04000222 RID: 546
		private int _numberCalling;
	}
}
