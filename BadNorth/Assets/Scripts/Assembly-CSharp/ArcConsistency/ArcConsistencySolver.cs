using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArcConsistency
{
	// Token: 0x02000003 RID: 3
	public class ArcConsistencySolver : MonoBehaviour
	{
		// Token: 0x06000005 RID: 5 RVA: 0x000020FE File Offset: 0x000004FE
		[ContextMenu("Run Fast")]
		private void RunFast()
		{
			if (!Application.isPlaying)
			{
				return;
			}
			this.StartCoroutineTimed(16f, new IEnumerator[]
			{
				this.Resolve()
			});
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002125 File Offset: 0x00000525
		[ContextMenu("Run Slow")]
		private void RunSlow()
		{
			if (!Application.isPlaying)
			{
				return;
			}
			base.StartCoroutine(this.Resolve());
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002140 File Offset: 0x00000540
		public IEnumerable Setup()
		{
			yield return null;
			foreach (ArcRule arcRule in base.GetComponentsInChildren<ArcRule>())
			{
				if (arcRule.enabled)
				{
					arcRule.Setup();
				}
			}
			yield return null;
			this.arcs = base.GetComponentsInChildren<Arc>();
			foreach (Arc arc in this.arcs)
			{
				arc.Setup();
			}
			this.domains = base.GetComponentsInChildren<Domain>();
			this.guessables = base.GetComponentsInChildren<IGuessable>();
			this.setup = true;
			yield break;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002164 File Offset: 0x00000564
		public IEnumerator Resolve()
		{
			if (!this.setup)
			{
				IEnumerator enumerator = this.Setup().GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						object s = enumerator.Current;
						yield return s;
					}
				}
				finally
				{
					IDisposable disposable;
					if ((disposable = (enumerator as IDisposable)) != null)
					{
						disposable.Dispose();
					}
				}
			}
			this.guesses.Clear();
			foreach (IGuessable guessable in this.guessables)
			{
				this.guesses.AddRange(guessable.GetGuesses());
			}
			this.guesses.Sort((Guess a, Guess b) => a.priority.CompareTo(b.priority));
			bool keepGoing = true;
			while (keepGoing)
			{
				keepGoing = false;
				foreach (bool broken in this.ConidtionalResolve())
				{
					yield return null;
					if (!broken)
					{
						keepGoing = true;
						break;
					}
				}
			}
			yield return null;
			yield break;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002180 File Offset: 0x00000580
		private IEnumerable<bool> ConidtionalResolve()
		{
			yield return true;
			foreach (Domain domain in this.domains)
			{
				domain.PrepareDomain();
			}
			yield return true;
			foreach (Arc arc in this.arcs)
			{
				this.PushToWorklist(arc);
			}
			yield return true;
			foreach (bool r in this.ReduceArc())
			{
				yield return r;
			}
			yield return true;
			foreach (bool r2 in this.ReduceByGuessing())
			{
				yield return r2;
			}
			this.MaybeDebugLog("DONE", this);
			yield return true;
			yield break;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021A3 File Offset: 0x000005A3
		private bool PushToWorklist(Arc arc)
		{
			if (!arc.inWorklist)
			{
				this.worklist.Push(arc);
				arc.inWorklist = true;
				return true;
			}
			return false;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021C6 File Offset: 0x000005C6
		private void MaybeDebugLog(string message, UnityEngine.Object target)
		{
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021C8 File Offset: 0x000005C8
		private void OnDomainReduced(Domain domain)
		{
			this.MaybeDebugLog(domain.ToString(), domain);
			foreach (Arc arc in domain.arcs)
			{
				this.PushToWorklist(arc);
				foreach (Arc arc2 in arc.rule.arcs)
				{
					this.PushToWorklist(arc2);
				}
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002284 File Offset: 0x00000684
		private IEnumerable<bool> ReduceByGuessing()
		{
			this.MaybeDebugLog("GEUSSING", this);
			for (int i = this.guesses.Count - 1; i >= 0; i--)
			{
				Guess guess = this.guesses[i];
				if (guess.TakeGuess())
				{
					this.MaybeDebugLog("GUESSED " + guess.domain.ToString(), guess.domain);
					this.OnDomainReduced(guess.domain);
					foreach (bool r in this.ReduceArc())
					{
						if (!r)
						{
							this.guesses.RemoveAt(i);
							this.guesses.Insert(0, guess);
						}
						yield return r;
					}
				}
			}
			this.guesses.Clear();
			yield return true;
			yield break;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000022A8 File Offset: 0x000006A8
		private IEnumerable<bool> ReduceArc()
		{
			bool broken = false;
			while (this.worklist.Count > 0)
			{
				Arc arc = this.worklist.Pop();
				bool change = false;
				for (int i = arc.domain.values.Count - 1; i >= 0; i--)
				{
					float value = arc.domain.values[i];
					if (!arc.rule.Valid(arc.domain, value))
					{
						arc.domain.values.RemoveAt(i);
						change = true;
						this.MaybeDebugLog(arc.domain.name + " reduced when applying " + arc.rule.name, arc);
						if (arc.domain.values.Count == 0)
						{
							broken = true;
							break;
						}
					}
				}
				if (!broken && change)
				{
					this.OnDomainReduced(arc.domain);
				}
				arc.inWorklist = false;
				yield return !broken;
			}
			yield break;
		}

		// Token: 0x04000004 RID: 4
		private bool setup;

		// Token: 0x04000005 RID: 5
		private Arc[] arcs;

		// Token: 0x04000006 RID: 6
		private ArcRule[] arcRules;

		// Token: 0x04000007 RID: 7
		private Domain[] domains;

		// Token: 0x04000008 RID: 8
		private IGuessable[] guessables;

		// Token: 0x04000009 RID: 9
		private Stack<Arc> worklist = new Stack<Arc>();

		// Token: 0x0400000A RID: 10
		private List<Guess> guesses = new List<Guess>();
	}
}
