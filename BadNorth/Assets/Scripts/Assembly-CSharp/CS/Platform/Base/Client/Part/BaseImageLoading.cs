using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace CS.Platform.Base.Client.Part
{
	// Token: 0x0200002F RID: 47
	public abstract class BaseImageLoading : MonoBehaviour
	{
		// Token: 0x06000193 RID: 403 RVA: 0x00007328 File Offset: 0x00005728
		public bool TryLoadingFromStored(BaseUserInfo id, ref Texture2D textureStore)
		{
			if (this._StoredPictures.ContainsKey(id))
			{
				textureStore = this._StoredPictures[id];
				return true;
			}
			if (this._WaitingLoadedPictures.ContainsKey(id))
			{
				textureStore = this._WaitingLoadedPictures[id];
				return true;
			}
			return false;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00007378 File Offset: 0x00005778
		public void UnloadPlayingWithImage(BaseUserInfo id)
		{
			if (this._StoredPictures.ContainsKey(id))
			{
				this._StoredPictures.Remove(id);
			}
			if (this._WaitingLoadedPictures.ContainsKey(id))
			{
				this._WaitingLoadedPictures.Remove(id);
			}
		}

		// Token: 0x06000195 RID: 405 RVA: 0x000073B8 File Offset: 0x000057B8
		public void StartLoadingImage(ref Texture2D textureStore, string imageURL)
		{
			if (imageURL != null)
			{
				UnityWebRequest texture = UnityWebRequestTexture.GetTexture(imageURL);
				if (textureStore == null)
				{
					textureStore = new Texture2D(64, 64);
				}
				base.StartCoroutine(this.WaitForFullyLoaded(texture, textureStore));
			}
		}

		// Token: 0x06000196 RID: 406 RVA: 0x000073FC File Offset: 0x000057FC
		protected IEnumerator WaitForFullyLoaded(UnityWebRequest imageLoading, Texture2D storepoint)
		{
			yield return null;
			yield return imageLoading.SendWebRequest();
			DownloadHandlerTexture handler = (DownloadHandlerTexture)imageLoading.downloadHandler;
			storepoint.Resize(handler.texture.width, handler.texture.height, handler.texture.format, handler.texture.mipmapCount != 0);
			storepoint.SetPixels(handler.texture.GetPixels());
			storepoint.Apply();
			if (this._LoadingImages.ContainsKey(storepoint))
			{
				PlatformEvents.UserPictureLoaded(this._LoadingImages[storepoint]);
				this._LoadingImages.Remove(storepoint);
			}
			yield break;
		}

		// Token: 0x0400008C RID: 140
		protected Dictionary<BaseUserInfo, Texture2D> _StoredPictures = new Dictionary<BaseUserInfo, Texture2D>();

		// Token: 0x0400008D RID: 141
		protected Dictionary<BaseUserInfo, Texture2D> _WaitingLoadedPictures = new Dictionary<BaseUserInfo, Texture2D>();

		// Token: 0x0400008E RID: 142
		protected Dictionary<Texture2D, BaseUserInfo> _LoadingImages = new Dictionary<Texture2D, BaseUserInfo>();
	}
}
