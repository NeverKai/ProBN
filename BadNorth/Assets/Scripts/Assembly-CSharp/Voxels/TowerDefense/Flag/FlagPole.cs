using System;
using System.Collections;
using RTM.OnScreenDebug;
using UnityEngine;

namespace Voxels.TowerDefense.Flag
{
	// Token: 0x02000517 RID: 1303
	public class FlagPole : AgentComponent
	{
		// Token: 0x060021DF RID: 8671 RVA: 0x0006059C File Offset: 0x0005E99C
		public override void Setup()
		{
			base.Setup();
			this.parentTransform = base.transform.parent;
			this.localPosition = base.transform.localPosition;
			this.worldPos = base.transform.position;
			this.tipPos = base.transform.position + this.idealUp * 0.4f;
			this.heroName = base.enSquad.hero.dbgName;
		}

		// Token: 0x060021E0 RID: 8672 RVA: 0x00060620 File Offset: 0x0005EA20
		private void Update()
		{
			if (Time.deltaTime <= 0f)
			{
				return;
			}
			float num = Time.deltaTime;
			int num2 = 0;
			do
			{
				if (this.held && base.agent.body.moving)
				{
					Vector4 normalLinear = base.agent.faction.island.voxelSpace.GetNormalLinear(this.tipPos);
					Vector3 a = normalLinear;
					this.idealUp = (a * (1f - normalLinear.w) + Vector3.up).normalized;
				}
				else
				{
					this.idealUp = Vector3.up;
				}
				float num3 = (num >= 0.009333334f) ? 0.008333334f : num;
				num -= num3;
				float t = 1f - num / Time.deltaTime;
				Vector3 a2 = Vector3.Lerp(this.worldPos, base.transform.position, t);
				Vector3 a3 = a2 + this.idealUp * 0.4f;
				Vector3 a4 = a3 - this.tipPos;
				this.tipVelocity += a4 * num3 * 130f;
				this.tipPos += this.tipVelocity * num3;
				this.tipVelocity -= this.tipVelocity * num3 * 12f;
				if (this.held)
				{
					this.tipVelocity += base.agent.body.walkDelta * num3 * 4f;
				}
				num2++;
			}
			while (num > 0.001f);
			base.transform.rotation = Quaternion.LookRotation(this.tipPos - base.transform.position) * Quaternion.Euler(90f, 0f, 0f);
			this.worldPos = base.transform.position;
		}

		// Token: 0x060021E1 RID: 8673 RVA: 0x0006083D File Offset: 0x0005EC3D
		public void Plant(Transform target)
		{
			this.Plant(target.position, target.rotation);
		}

		// Token: 0x060021E2 RID: 8674 RVA: 0x00060851 File Offset: 0x0005EC51
		public void Plant(Vector3 position, Quaternion rotation)
		{
			base.StopAllCoroutines();
			base.StartCoroutine(this.PlantRoutine(position, rotation));
		}

		// Token: 0x060021E3 RID: 8675 RVA: 0x00060868 File Offset: 0x0005EC68
		public void Collect()
		{
			base.StopAllCoroutines();
			base.StartCoroutine(this.CollectRoutine());
		}

		// Token: 0x060021E4 RID: 8676 RVA: 0x00060880 File Offset: 0x0005EC80
		private IEnumerator PlantRoutine(Vector3 position, Quaternion rotation)
		{
			base.transform.parent = base.agent.faction.island.runContainer;
			this.held = false;
			Vector3 targetUp = rotation * Vector3.up;
			float maxDist = float.MaxValue;
			while (maxDist > 0.01f)
			{
				Vector3 oldPos = base.transform.position;
				base.transform.position = Vector3.Lerp(base.transform.position, position, Time.deltaTime * 15f);
				this.idealUp = Vector3.Lerp(this.idealUp, targetUp, Time.deltaTime * 10f).normalized;
				this.tipPos += (base.transform.position - oldPos) * 0.5f;
				maxDist = Mathf.Max(Vector3.Distance(this.idealUp, targetUp), Vector3.Distance(base.transform.position, position));
				yield return null;
			}
			base.transform.position = position;
			this.idealUp = targetUp;
			yield break;
		}

		// Token: 0x060021E5 RID: 8677 RVA: 0x000608AC File Offset: 0x0005ECAC
		private IEnumerator CollectRoutine()
		{
			base.transform.parent = this.parentTransform;
			this.held = true;
			Vector3 targetUp = Vector3.up;
			float maxDist = float.MaxValue;
			while (maxDist > 0.01f)
			{
				base.transform.localPosition = Vector3.Lerp(base.transform.localPosition, this.localPosition, Time.deltaTime * 10f);
				this.idealUp = Vector3.Lerp(this.idealUp, Vector3.up, Time.deltaTime * 10f).normalized;
				maxDist = Mathf.Max(Vector3.Distance(this.idealUp, targetUp), Vector3.Distance(base.transform.localPosition, this.localPosition));
				yield return null;
			}
			base.transform.localPosition = this.localPosition;
			this.idealUp = targetUp;
			yield break;
		}

		// Token: 0x040014A5 RID: 5285
		private DebugChannelGroup dbgGroup = new DebugChannelGroup("flag", EVerbosity.Quiet, 0);

		// Token: 0x040014A6 RID: 5286
		private Transform parentTransform;

		// Token: 0x040014A7 RID: 5287
		private Vector3 localPosition = Vector3.zero;

		// Token: 0x040014A8 RID: 5288
		private Vector3 worldPos = Vector3.zero;

		// Token: 0x040014A9 RID: 5289
		private Vector3 tipPos;

		// Token: 0x040014AA RID: 5290
		private Vector3 tipVelocity;

		// Token: 0x040014AB RID: 5291
		private bool held = true;

		// Token: 0x040014AC RID: 5292
		private Vector3 idealUp = Vector3.up;

		// Token: 0x040014AD RID: 5293
		private string heroName;
	}
}
