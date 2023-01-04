using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x0200002C RID: 44
	public class Billboard : MonoBehaviour
	{
		// Token: 0x060000E8 RID: 232 RVA: 0x00006258 File Offset: 0x00004658
		public static void UpdateRotations()
		{
			Billboard.flatLook = Singleton<LevelCamera>.instance.transform.forward.GetZeroY();
			Billboard.rotation = Quaternion.LookRotation(Billboard.flatLook);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00006284 File Offset: 0x00004684
		private void Flip()
		{
			Vector3 localScale = base.transform.localScale;
			localScale.x = -localScale.x;
			base.transform.localScale = localScale;
			this.flipped = !this.flipped;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000062C8 File Offset: 0x000046C8
		public void Scale()
		{
			if (!this.scale)
			{
				return;
			}
			Vector3 localScale = base.transform.localScale;
			localScale.y = Mathf.Abs(localScale.x * Constants.upScale);
			base.transform.localScale = localScale;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00006312 File Offset: 0x00004712
		private void OnValidate()
		{
			this.Scale();
		}

		// Token: 0x060000EC RID: 236 RVA: 0x0000631A File Offset: 0x0000471A
		private void Awake()
		{
			this.Scale();
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00006324 File Offset: 0x00004724
		private void LateUpdate()
		{
			if (base.transform.parent.up.y > 0.9f)
			{
				base.transform.rotation = Billboard.rotation;
			}
			else
			{
				base.transform.rotation = Quaternion.LookRotation(Billboard.flatLook, base.transform.parent.up);
			}
			if (this.backward)
			{
				base.transform.rotation *= Quaternion.Euler(0f, 180f, 0f);
			}
			if (this.flippable)
			{
				float num = Vector3.Dot(base.transform.parent.forward, Singleton<LevelCamera>.instance.cameraRef.transform.right);
				if (!this.flipped)
				{
					num = -num;
				}
				if (num < -0.1f)
				{
					this.Flip();
				}
			}
		}

		// Token: 0x04000071 RID: 113
		public bool flippable;

		// Token: 0x04000072 RID: 114
		public bool backward;

		// Token: 0x04000073 RID: 115
		public bool scale = true;

		// Token: 0x04000074 RID: 116
		public bool flipped = true;

		// Token: 0x04000075 RID: 117
		private static Quaternion rotation;

		// Token: 0x04000076 RID: 118
		private static Vector3 flatLook;
	}
}
