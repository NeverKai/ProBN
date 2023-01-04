using System;
using System.Collections.Generic;
using ReflexCLI.Attributes;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x02000868 RID: 2152
	[ConsoleCommandClassCustomizer("Wallet")]
	public abstract class Wallet : MonoBehaviour
	{
		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x0600386A RID: 14442 RVA: 0x000F420E File Offset: 0x000F260E
		// (set) Token: 0x0600386B RID: 14443 RVA: 0x000F4218 File Offset: 0x000F2618
		[ConsoleCommand("")]
		public int balance
		{
			get
			{
				return this._balance;
			}
			set
			{
				if (value != this._balance)
				{
					int balance = this.balance;
					this._balance = value;
					this.OnChange();
					for (int i = 0; i < this.walletListeners.Count; i++)
					{
						this.walletListeners[i].OnWalletChange(balance, this.balance, this);
					}
				}
			}
		}

		// Token: 0x0600386C RID: 14444 RVA: 0x000F427A File Offset: 0x000F267A
		public virtual void Awake()
		{
			this.OnChange();
		}

		// Token: 0x0600386D RID: 14445 RVA: 0x000F4282 File Offset: 0x000F2682
		public virtual void OnChange()
		{
		}

		// Token: 0x0600386E RID: 14446 RVA: 0x000F4284 File Offset: 0x000F2684
		[ConsoleCommand("")]
		public void Add(int add)
		{
			this.balance = Mathf.Max(0, this.balance + add);
		}

		// Token: 0x0600386F RID: 14447 RVA: 0x000F429A File Offset: 0x000F269A
		public bool Withdraw(int amount)
		{
			if (this.balance >= amount)
			{
				this.balance -= amount;
				this.OnChange();
				return true;
			}
			return false;
		}

		// Token: 0x0400266C RID: 9836
		[SerializeField]
		private int _balance;

		// Token: 0x0400266D RID: 9837
		public List<IWalletListener> walletListeners = new List<IWalletListener>();
	}
}
