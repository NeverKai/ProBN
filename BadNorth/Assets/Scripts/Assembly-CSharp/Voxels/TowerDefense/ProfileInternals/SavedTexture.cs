using System;
using System.Runtime.Serialization;
using UnityEngine;

namespace Voxels.TowerDefense.ProfileInternals
{
	// Token: 0x02000587 RID: 1415
	[Serializable]
	public class SavedTexture
	{
		// Token: 0x060024AE RID: 9390 RVA: 0x0007388F File Offset: 0x00071C8F
		public SavedTexture(int width, int height)
		{
			this._tex = new Texture2D(width, height, TextureFormat.ARGB32, false);
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x060024AF RID: 9391 RVA: 0x000738A6 File Offset: 0x00071CA6
		public Texture2D tex
		{
			get
			{
				return this._tex;
			}
		}

		// Token: 0x060024B0 RID: 9392 RVA: 0x000738AE File Offset: 0x00071CAE
		[OnSerializing]
		private void PreSave(StreamingContext context)
		{
			this.bytes = this._tex.EncodeToPNG();
		}

		// Token: 0x060024B1 RID: 9393 RVA: 0x000738C1 File Offset: 0x00071CC1
		[OnDeserialized]
		private void PostLoad(StreamingContext context)
		{
			this._tex = new Texture2D(this.width, this.height, TextureFormat.ARGB32, false);
			this._tex.LoadImage(this.bytes);
			this._tex.Apply();
		}

		// Token: 0x060024B2 RID: 9394 RVA: 0x000738F9 File Offset: 0x00071CF9
		public void Unload()
		{
			UnityEngine.Object.Destroy(this._tex);
		}

		// Token: 0x04001739 RID: 5945
		[SerializeField]
		private int width;

		// Token: 0x0400173A RID: 5946
		[SerializeField]
		private int height;

		// Token: 0x0400173B RID: 5947
		[SerializeField]
		private byte[] bytes;

		// Token: 0x0400173C RID: 5948
		[NonSerialized]
		private Texture2D _tex;
	}
}
