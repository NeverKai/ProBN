using System;
using RTM.Pools;
using UnityEngine;
using UnityEngine.EventSystems;
using Voxels.TowerDefense.ScriptAnimations;

namespace Voxels.TowerDefense.UI.UpgradeScreen
{
	// Token: 0x0200091C RID: 2332
	public class UpgradeCurve : UIBehaviour, IPoolable
	{
		// Token: 0x06003E88 RID: 16008 RVA: 0x00119FA4 File Offset: 0x001183A4
		public UpgradeCurve Setup(UpgradeToken token, SlotToken slot)
		{
			this.slot = slot;
			this.token = token;
			TargetAnimator<float> anim = slot.visibleState.anim;
			anim.setFunc = (Action<float>)Delegate.Combine(anim.setFunc, new Action<float>(this.SetAlpha));
			TargetAnimator<float> anim2 = token.visibleState.anim;
			anim2.setFunc = (Action<float>)Delegate.Combine(anim2.setFunc, new Action<float>(this.SetAlpha));
			TargetAnimator visAnim = token.scrollItem.visAnim;
			visAnim.setFunc = (Action<float>)Delegate.Combine(visAnim.setFunc, new Action<float>(this.SetAlpha));
			slot.onDirty = (Action)Delegate.Combine(slot.onDirty, new Action(this.SetDirty));
			token.onDirty = (Action)Delegate.Combine(token.onDirty, new Action(this.SetDirty));
			slot.onReturnToPool = (Action)Delegate.Combine(slot.onReturnToPool, new Action(this.OnTokensReturn));
			token.onReturnToPool = (Action)Delegate.Combine(token.onReturnToPool, new Action(this.OnTokensReturn));
			AnimatedState visibleState = token.visibleState;
			visibleState.onActivity = (Action<bool>)Delegate.Combine(visibleState.onActivity, new Action<bool>(this.OnTokenNotVisible));
			this.simpleCurve.SetAllDirty();
			this.SetAlpha(0f);
			slot.curves.Add(this);
			token.curves.Add(this);
			return this;
		}

		// Token: 0x06003E89 RID: 16009 RVA: 0x0011A121 File Offset: 0x00118521
		private void OnTokenNotVisible(bool x)
		{
			if (!x)
			{
				this.OnTokensReturn();
			}
		}

		// Token: 0x06003E8A RID: 16010 RVA: 0x0011A12F File Offset: 0x0011852F
		private void OnTokensReturn()
		{
			this.pool.ReturnToPool(this);
		}

		// Token: 0x06003E8B RID: 16011 RVA: 0x0011A140 File Offset: 0x00118540
		private void SetDirty()
		{
			Vector2 v = this.token.transform.position - this.slot.transform.position;
			v = base.transform.InverseTransformVector(v);
			float num = Mathf.Sign(v.x);
			Vector2 tangent = new Vector2(v.x * 0.3f, 0f);
			float num2 = v.y * 0.04f;
			this.simpleCurve.points[0] = new SimpleCurve.Point(new Vector2(0.5f, 0.5f), new Vector2(0f, num2), tangent, this.slot.transform as RectTransform);
			this.simpleCurve.points[1] = new SimpleCurve.Point(new Vector2(0.5f, 0.5f), new Vector2(v.x * 0.5f - num * Mathf.Abs(v.y) * 0.1f, num2), tangent, this.slot.transform as RectTransform);
			this.simpleCurve.points[2] = new SimpleCurve.Point(new Vector2(0.5f - num * 0.3f, 0.5f), new Vector2(0f, -num2), tangent, this.token.transform as RectTransform);
			this.simpleCurve.SetVerticesDirty();
		}

		// Token: 0x06003E8C RID: 16012 RVA: 0x0011A2B8 File Offset: 0x001186B8
		private void SetAlpha(float x = 0f)
		{
			this.simpleCurve.color = this.simpleCurve.color.SetA(this.token.scrollItem.visAnim.current * this.token.visibleState.anim.current * this.slot.visibleState.value);
			this.simpleCurve.SetMaterialDirty();
		}

		// Token: 0x06003E8D RID: 16013 RVA: 0x0011A327 File Offset: 0x00118727
		void IPoolable.SetPool<T>(LocalPool<T> pool)
		{
			this.pool = (pool as LocalPool<UpgradeCurve>);
			this.simpleCurve = base.GetComponent<SimpleCurve>();
		}

		// Token: 0x06003E8E RID: 16014 RVA: 0x0011A346 File Offset: 0x00118746
		void IPoolable.OnRemoved()
		{
			base.gameObject.SetActive(true);
		}

		// Token: 0x06003E8F RID: 16015 RVA: 0x0011A354 File Offset: 0x00118754
		void IPoolable.OnReturned()
		{
			TargetAnimator<float> anim = this.slot.visibleState.anim;
			anim.setFunc = (Action<float>)Delegate.Remove(anim.setFunc, new Action<float>(this.SetAlpha));
			TargetAnimator<float> anim2 = this.token.visibleState.anim;
			anim2.setFunc = (Action<float>)Delegate.Remove(anim2.setFunc, new Action<float>(this.SetAlpha));
			TargetAnimator visAnim = this.token.scrollItem.visAnim;
			visAnim.setFunc = (Action<float>)Delegate.Remove(visAnim.setFunc, new Action<float>(this.SetAlpha));
			SlotToken slotToken = this.slot;
			slotToken.onDirty = (Action)Delegate.Remove(slotToken.onDirty, new Action(this.SetDirty));
			UpgradeToken upgradeToken = this.token;
			upgradeToken.onDirty = (Action)Delegate.Remove(upgradeToken.onDirty, new Action(this.SetDirty));
			SlotToken slotToken2 = this.slot;
			slotToken2.onReturnToPool = (Action)Delegate.Remove(slotToken2.onReturnToPool, new Action(this.OnTokensReturn));
			UpgradeToken upgradeToken2 = this.token;
			upgradeToken2.onReturnToPool = (Action)Delegate.Remove(upgradeToken2.onReturnToPool, new Action(this.OnTokensReturn));
			AnimatedState visibleState = this.token.visibleState;
			visibleState.onActivity = (Action<bool>)Delegate.Remove(visibleState.onActivity, new Action<bool>(this.OnTokenNotVisible));
			this.slot.curves.Remove(this);
			this.token.curves.Remove(this);
			this.slot = null;
			this.token = null;
			base.gameObject.SetActive(false);
		}

		// Token: 0x06003E90 RID: 16016 RVA: 0x0011A4FC File Offset: 0x001188FC
		private void OnDrawGizmos()
		{
			if (this.slot && this.token)
			{
				Gizmos.DrawLine(this.slot.transform.position, this.token.transform.position);
			}
		}

		// Token: 0x06003E91 RID: 16017 RVA: 0x0011A550 File Offset: 0x00118950
		private void OnDrawGizmosSelected()
		{
			if (this.slot && this.token)
			{
				Gizmos.color = Color.yellow;
				Gizmos.DrawLine(this.slot.transform.position, this.token.transform.position);
			}
		}

		// Token: 0x04002BBE RID: 11198
		public SlotToken slot;

		// Token: 0x04002BBF RID: 11199
		public UpgradeToken token;

		// Token: 0x04002BC0 RID: 11200
		private SimpleCurve simpleCurve;

		// Token: 0x04002BC1 RID: 11201
		private LocalPool<UpgradeCurve> pool;
	}
}
