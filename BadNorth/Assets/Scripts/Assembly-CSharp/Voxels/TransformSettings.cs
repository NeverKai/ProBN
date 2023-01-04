using System;
using UnityEngine;

namespace Voxels
{
	// Token: 0x02000883 RID: 2179
	[Serializable]
	public struct TransformSettings
	{
		// Token: 0x060038FA RID: 14586 RVA: 0x000F7E6A File Offset: 0x000F626A
		public TransformSettings(Vector3 position, Vector3 scale, Vector3 rotation)
		{
			this.position = position;
			this.scale = scale;
			this.rotation = rotation;
		}

		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x060038FB RID: 14587 RVA: 0x000F7E81 File Offset: 0x000F6281
		public Quaternion quaternion
		{
			get
			{
				return Quaternion.Euler(this.rotation);
			}
		}

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x060038FC RID: 14588 RVA: 0x000F7E8E File Offset: 0x000F628E
		public Matrix4x4 matrix
		{
			get
			{
				return Matrix4x4.TRS(this.position, this.quaternion, this.scale);
			}
		}

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x060038FD RID: 14589 RVA: 0x000F7EA7 File Offset: 0x000F62A7
		public static TransformSettings identity
		{
			get
			{
				return new TransformSettings(Vector3.zero, Vector3.one, Vector3.zero);
			}
		}

		// Token: 0x060038FE RID: 14590 RVA: 0x000F7EC0 File Offset: 0x000F62C0
		public bool GetFlipped()
		{
			bool flag = false;
			if (this.scale.x < 0f)
			{
				flag = !flag;
			}
			if (this.scale.y < 0f)
			{
				flag = !flag;
			}
			if (this.scale.z < 0f)
			{
				flag = !flag;
			}
			return flag;
		}

		// Token: 0x060038FF RID: 14591 RVA: 0x000F7F1E File Offset: 0x000F631E
		public void ApplyLocal(Transform transform)
		{
			transform.localPosition = this.position;
			transform.localRotation = this.quaternion;
			transform.localScale = this.scale;
		}

		// Token: 0x06003900 RID: 14592 RVA: 0x000F7F44 File Offset: 0x000F6344
		public string GetString()
		{
			return string.Format("t {0}, r {1}, s {2}", this.position.ToString("F0"), this.rotation.ToString("F0"), this.scale.ToString("F0"));
		}

		// Token: 0x040026FF RID: 9983
		public Vector3 position;

		// Token: 0x04002700 RID: 9984
		public Vector3 scale;

		// Token: 0x04002701 RID: 9985
		public Vector3 rotation;
	}
}
