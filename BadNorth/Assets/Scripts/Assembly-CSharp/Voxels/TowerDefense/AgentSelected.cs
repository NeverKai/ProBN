using System;
using System.Collections.Generic;
using UnityEngine;
using Voxels.TowerDefense.ScriptAnimations;
using Voxels.TowerDefense.SpriteMagic;

namespace Voxels.TowerDefense
{
	// Token: 0x0200068B RID: 1675
	public class AgentSelected : AgentComponent
	{
		// Token: 0x06002AC7 RID: 10951 RVA: 0x00098B28 File Offset: 0x00096F28
		public override void Setup()
		{
			this.selectedState = new AgentState("Selected", base.agent.rootState, false, false);
			base.GetComponentsInChildren<MeshRenderer>(true, this.mrs);
			if (AgentSelected.block == null)
			{
				AgentSelected.block = new MaterialPropertyBlock();
			}
			for (int i = this.mrs.Count - 1; i >= 0; i--)
			{
				MeshRenderer meshRenderer = this.mrs[i];
				BatchedSprite component = meshRenderer.transform.parent.GetComponent<BatchedSprite>();
				if (component)
				{
					this.bs.Add(component);
					this.mrs.Remove(meshRenderer);
				}
			}
			base.enSquad.selectedState.anim.Subscribe(new Action<float>(this.UpdateSelection));
			base.enSquad.hoverState.anim.Subscribe(new Action<float>(this.UpdateHover));
		}

		// Token: 0x06002AC8 RID: 10952 RVA: 0x00098C18 File Offset: 0x00097018
		private void OnDestroy()
		{
			if (base.enSquad)
			{
				TargetAnimator<float> anim = base.enSquad.selectedState.anim;
				anim.setFunc = (Action<float>)Delegate.Remove(anim.setFunc, new Action<float>(this.UpdateSelection));
				TargetAnimator<float> anim2 = base.enSquad.hoverState.anim;
				anim2.setFunc = (Action<float>)Delegate.Remove(anim2.setFunc, new Action<float>(this.UpdateHover));
			}
		}

		// Token: 0x06002AC9 RID: 10953 RVA: 0x00098C98 File Offset: 0x00097098
		private void UpdateSelection(float f)
		{
			Color value = (f != 0f) ? base.enSquad.hero.color.SetA(f) : Color.clear;
			foreach (MeshRenderer meshRenderer in this.mrs)
			{
				meshRenderer.GetPropertyBlock(AgentSelected.block);
				AgentSelected.block.SetColor(AgentSelected.selectionColorId, value);
				meshRenderer.SetPropertyBlock(AgentSelected.block);
			}
			foreach (BatchedSprite batchedSprite in this.bs)
			{
				batchedSprite.block.SetColor(AgentSelected.selectionColorId, value);
				batchedSprite.ComittBlock();
			}
		}

		// Token: 0x06002ACA RID: 10954 RVA: 0x00098DA8 File Offset: 0x000971A8
		private void UpdateHover(float h)
		{
			foreach (MeshRenderer meshRenderer in this.mrs)
			{
				meshRenderer.GetPropertyBlock(AgentSelected.block);
				AgentSelected.block.SetFloat(AgentSelected.hoverId, h);
				meshRenderer.SetPropertyBlock(AgentSelected.block);
			}
			foreach (BatchedSprite batchedSprite in this.bs)
			{
				batchedSprite.block.SetFloat(AgentSelected.hoverId, h);
				batchedSprite.ComittBlock();
			}
		}

		// Token: 0x04001BCC RID: 7116
		[SerializeField]
		private List<BatchedSprite> bs = new List<BatchedSprite>();

		// Token: 0x04001BCD RID: 7117
		private List<MeshRenderer> mrs = new List<MeshRenderer>();

		// Token: 0x04001BCE RID: 7118
		private AgentState selectedState;

		// Token: 0x04001BCF RID: 7119
		private static ShaderId selectionColorId = "_SelectionColor";

		// Token: 0x04001BD0 RID: 7120
		private static ShaderId hoverId = "_Hover";

		// Token: 0x04001BD1 RID: 7121
		private static MaterialPropertyBlock block;
	}
}
