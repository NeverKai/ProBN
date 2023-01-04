using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Discord
{
	// Token: 0x02000118 RID: 280
	public class ImageManager
	{
		// Token: 0x06000773 RID: 1907 RVA: 0x0001D69C File Offset: 0x0001BA9C
		internal ImageManager(IntPtr ptr, IntPtr eventsPtr, ref ImageManager.FFIEvents events)
		{
			if (eventsPtr == IntPtr.Zero)
			{
				throw new ResultException(Result.InternalError);
			}
			this.InitEvents(eventsPtr, ref events);
			this.MethodsPtr = ptr;
			if (this.MethodsPtr == IntPtr.Zero)
			{
				throw new ResultException(Result.InternalError);
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000774 RID: 1908 RVA: 0x0001D6F1 File Offset: 0x0001BAF1
		private ImageManager.FFIMethods Methods
		{
			get
			{
				if (this.MethodsStructure == null)
				{
					this.MethodsStructure = Marshal.PtrToStructure(this.MethodsPtr, typeof(ImageManager.FFIMethods));
				}
				return (ImageManager.FFIMethods)this.MethodsStructure;
			}
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x0001D724 File Offset: 0x0001BB24
		private void InitEvents(IntPtr eventsPtr, ref ImageManager.FFIEvents events)
		{
			Marshal.StructureToPtr(events, eventsPtr, false);
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x0001D738 File Offset: 0x0001BB38
		public void Fetch(ImageHandle handle, bool refresh, ImageManager.FetchHandler callback)
		{
			ImageManager.FFIMethods.FetchCallback fetchCallback = delegate(IntPtr ptr, Result result, ImageHandle handleResult)
			{
				Utility.Release(ptr);
				callback(result, handleResult);
			};
			this.Methods.Fetch(this.MethodsPtr, handle, refresh, Utility.Retain<ImageManager.FFIMethods.FetchCallback>(fetchCallback), fetchCallback);
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x0001D784 File Offset: 0x0001BB84
		public ImageDimensions GetDimensions(ImageHandle handle)
		{
			ImageDimensions result = default(ImageDimensions);
			Result result2 = this.Methods.GetDimensions(this.MethodsPtr, handle, ref result);
			if (result2 != Result.Ok)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x0001D7C4 File Offset: 0x0001BBC4
		public void GetData(ImageHandle handle, byte[] data)
		{
			Result result = this.Methods.GetData(this.MethodsPtr, handle, data, data.Length);
			if (result != Result.Ok)
			{
				throw new ResultException(result);
			}
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x0001D7FD File Offset: 0x0001BBFD
		public void Fetch(ImageHandle handle, ImageManager.FetchHandler callback)
		{
			this.Fetch(handle, false, callback);
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x0001D808 File Offset: 0x0001BC08
		public byte[] GetData(ImageHandle handle)
		{
			ImageDimensions dimensions = this.GetDimensions(handle);
			byte[] array = new byte[dimensions.Width * dimensions.Height * 4U];
			this.GetData(handle, array);
			return array;
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x0001D840 File Offset: 0x0001BC40
		public Texture2D GetTexture(ImageHandle handle)
		{
			ImageDimensions dimensions = this.GetDimensions(handle);
			Texture2D texture2D = new Texture2D((int)dimensions.Width, (int)dimensions.Height, TextureFormat.RGBA32, false, true);
			texture2D.LoadRawTextureData(this.GetData(handle));
			texture2D.Apply();
			return texture2D;
		}

		// Token: 0x04000452 RID: 1106
		private IntPtr MethodsPtr;

		// Token: 0x04000453 RID: 1107
		private object MethodsStructure;

		// Token: 0x02000119 RID: 281
		internal struct FFIEvents
		{
		}

		// Token: 0x0200011A RID: 282
		internal struct FFIMethods
		{
			// Token: 0x04000454 RID: 1108
			internal ImageManager.FFIMethods.FetchMethod Fetch;

			// Token: 0x04000455 RID: 1109
			internal ImageManager.FFIMethods.GetDimensionsMethod GetDimensions;

			// Token: 0x04000456 RID: 1110
			internal ImageManager.FFIMethods.GetDataMethod GetData;

			// Token: 0x0200011B RID: 283
			// (Invoke) Token: 0x0600077D RID: 1917
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void FetchCallback(IntPtr ptr, Result result, ImageHandle handleResult);

			// Token: 0x0200011C RID: 284
			// (Invoke) Token: 0x06000781 RID: 1921
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void FetchMethod(IntPtr methodsPtr, ImageHandle handle, bool refresh, IntPtr callbackData, ImageManager.FFIMethods.FetchCallback callback);

			// Token: 0x0200011D RID: 285
			// (Invoke) Token: 0x06000785 RID: 1925
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result GetDimensionsMethod(IntPtr methodsPtr, ImageHandle handle, ref ImageDimensions dimensions);

			// Token: 0x0200011E RID: 286
			// (Invoke) Token: 0x06000789 RID: 1929
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result GetDataMethod(IntPtr methodsPtr, ImageHandle handle, byte[] data, int dataLen);
		}

		// Token: 0x0200011F RID: 287
		// (Invoke) Token: 0x0600078D RID: 1933
		public delegate void FetchHandler(Result result, ImageHandle handleResult);
	}
}
