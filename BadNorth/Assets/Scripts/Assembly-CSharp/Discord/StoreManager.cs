using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Discord
{
	// Token: 0x020001C8 RID: 456
	public class StoreManager
	{
		// Token: 0x06000A63 RID: 2659 RVA: 0x0001F6D4 File Offset: 0x0001DAD4
		internal StoreManager(IntPtr ptr, IntPtr eventsPtr, ref StoreManager.FFIEvents events)
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

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000A64 RID: 2660 RVA: 0x0001F729 File Offset: 0x0001DB29
		private StoreManager.FFIMethods Methods
		{
			get
			{
				if (this.MethodsStructure == null)
				{
					this.MethodsStructure = Marshal.PtrToStructure(this.MethodsPtr, typeof(StoreManager.FFIMethods));
				}
				return (StoreManager.FFIMethods)this.MethodsStructure;
			}
		}

		// Token: 0x1400004B RID: 75
		// (add) Token: 0x06000A65 RID: 2661 RVA: 0x0001F75C File Offset: 0x0001DB5C
		// (remove) Token: 0x06000A66 RID: 2662 RVA: 0x0001F794 File Offset: 0x0001DB94
		public event StoreManager.EntitlementCreateHandler OnEntitlementCreate;

		// Token: 0x1400004C RID: 76
		// (add) Token: 0x06000A67 RID: 2663 RVA: 0x0001F7CC File Offset: 0x0001DBCC
		// (remove) Token: 0x06000A68 RID: 2664 RVA: 0x0001F804 File Offset: 0x0001DC04
		public event StoreManager.EntitlementDeleteHandler OnEntitlementDelete;

		// Token: 0x06000A69 RID: 2665 RVA: 0x0001F83A File Offset: 0x0001DC3A
		private void InitEvents(IntPtr eventsPtr, ref StoreManager.FFIEvents events)
		{
			events.OnEntitlementCreate = delegate(IntPtr ptr, ref Entitlement entitlement)
			{
				if (this.OnEntitlementCreate != null)
				{
					this.OnEntitlementCreate(ref entitlement);
				}
			};
			events.OnEntitlementDelete = delegate(IntPtr ptr, ref Entitlement entitlement)
			{
				if (this.OnEntitlementDelete != null)
				{
					this.OnEntitlementDelete(ref entitlement);
				}
			};
			Marshal.StructureToPtr(events, eventsPtr, false);
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x0001F874 File Offset: 0x0001DC74
		public void FetchSkus(StoreManager.FetchSkusHandler callback)
		{
			StoreManager.FFIMethods.FetchSkusCallback fetchSkusCallback = delegate(IntPtr ptr, Result result)
			{
				Utility.Release(ptr);
				callback(result);
			};
			this.Methods.FetchSkus(this.MethodsPtr, Utility.Retain<StoreManager.FFIMethods.FetchSkusCallback>(fetchSkusCallback), fetchSkusCallback);
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x0001F8BC File Offset: 0x0001DCBC
		public int CountSkus()
		{
			int result = 0;
			this.Methods.CountSkus(this.MethodsPtr, ref result);
			return result;
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x0001F8E8 File Offset: 0x0001DCE8
		public Sku GetSku(long skuId)
		{
			Sku result = default(Sku);
			Result result2 = this.Methods.GetSku(this.MethodsPtr, skuId, ref result);
			if (result2 != Result.Ok)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x0001F928 File Offset: 0x0001DD28
		public Sku GetSkuAt(int index)
		{
			Sku result = default(Sku);
			Result result2 = this.Methods.GetSkuAt(this.MethodsPtr, index, ref result);
			if (result2 != Result.Ok)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x0001F968 File Offset: 0x0001DD68
		public void FetchEntitlements(StoreManager.FetchEntitlementsHandler callback)
		{
			StoreManager.FFIMethods.FetchEntitlementsCallback fetchEntitlementsCallback = delegate(IntPtr ptr, Result result)
			{
				Utility.Release(ptr);
				callback(result);
			};
			this.Methods.FetchEntitlements(this.MethodsPtr, Utility.Retain<StoreManager.FFIMethods.FetchEntitlementsCallback>(fetchEntitlementsCallback), fetchEntitlementsCallback);
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x0001F9B0 File Offset: 0x0001DDB0
		public int CountEntitlements()
		{
			int result = 0;
			this.Methods.CountEntitlements(this.MethodsPtr, ref result);
			return result;
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x0001F9DC File Offset: 0x0001DDDC
		public Entitlement GetEntitlement(long entitlementId)
		{
			Entitlement result = default(Entitlement);
			Result result2 = this.Methods.GetEntitlement(this.MethodsPtr, entitlementId, ref result);
			if (result2 != Result.Ok)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x0001FA1C File Offset: 0x0001DE1C
		public Entitlement GetEntitlementAt(int index)
		{
			Entitlement result = default(Entitlement);
			Result result2 = this.Methods.GetEntitlementAt(this.MethodsPtr, index, ref result);
			if (result2 != Result.Ok)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x0001FA5C File Offset: 0x0001DE5C
		public bool HasSkuEntitlement(long skuId)
		{
			bool result = false;
			Result result2 = this.Methods.HasSkuEntitlement(this.MethodsPtr, skuId, ref result);
			if (result2 != Result.Ok)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x0001FA98 File Offset: 0x0001DE98
		public void StartPurchase(long skuId, StoreManager.StartPurchaseHandler callback)
		{
			StoreManager.FFIMethods.StartPurchaseCallback startPurchaseCallback = delegate(IntPtr ptr, Result result)
			{
				Utility.Release(ptr);
				callback(result);
			};
			this.Methods.StartPurchase(this.MethodsPtr, skuId, Utility.Retain<StoreManager.FFIMethods.StartPurchaseCallback>(startPurchaseCallback), startPurchaseCallback);
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x0001FAE0 File Offset: 0x0001DEE0
		public IEnumerable<Entitlement> GetEntitlements()
		{
			int num = this.CountEntitlements();
			List<Entitlement> list = new List<Entitlement>();
			for (int i = 0; i < num; i++)
			{
				list.Add(this.GetEntitlementAt(i));
			}
			return list;
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x0001FB1C File Offset: 0x0001DF1C
		public IEnumerable<Sku> GetSkus()
		{
			int num = this.CountSkus();
			List<Sku> list = new List<Sku>();
			for (int i = 0; i < num; i++)
			{
				list.Add(this.GetSkuAt(i));
			}
			return list;
		}

		// Token: 0x040004C4 RID: 1220
		private IntPtr MethodsPtr;

		// Token: 0x040004C5 RID: 1221
		private object MethodsStructure;

		// Token: 0x020001C9 RID: 457
		internal struct FFIEvents
		{
			// Token: 0x040004C8 RID: 1224
			internal StoreManager.FFIEvents.EntitlementCreateHandler OnEntitlementCreate;

			// Token: 0x040004C9 RID: 1225
			internal StoreManager.FFIEvents.EntitlementDeleteHandler OnEntitlementDelete;

			// Token: 0x020001CA RID: 458
			// (Invoke) Token: 0x06000A79 RID: 2681
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void EntitlementCreateHandler(IntPtr ptr, ref Entitlement entitlement);

			// Token: 0x020001CB RID: 459
			// (Invoke) Token: 0x06000A7D RID: 2685
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void EntitlementDeleteHandler(IntPtr ptr, ref Entitlement entitlement);
		}

		// Token: 0x020001CC RID: 460
		internal struct FFIMethods
		{
			// Token: 0x040004CA RID: 1226
			internal StoreManager.FFIMethods.FetchSkusMethod FetchSkus;

			// Token: 0x040004CB RID: 1227
			internal StoreManager.FFIMethods.CountSkusMethod CountSkus;

			// Token: 0x040004CC RID: 1228
			internal StoreManager.FFIMethods.GetSkuMethod GetSku;

			// Token: 0x040004CD RID: 1229
			internal StoreManager.FFIMethods.GetSkuAtMethod GetSkuAt;

			// Token: 0x040004CE RID: 1230
			internal StoreManager.FFIMethods.FetchEntitlementsMethod FetchEntitlements;

			// Token: 0x040004CF RID: 1231
			internal StoreManager.FFIMethods.CountEntitlementsMethod CountEntitlements;

			// Token: 0x040004D0 RID: 1232
			internal StoreManager.FFIMethods.GetEntitlementMethod GetEntitlement;

			// Token: 0x040004D1 RID: 1233
			internal StoreManager.FFIMethods.GetEntitlementAtMethod GetEntitlementAt;

			// Token: 0x040004D2 RID: 1234
			internal StoreManager.FFIMethods.HasSkuEntitlementMethod HasSkuEntitlement;

			// Token: 0x040004D3 RID: 1235
			internal StoreManager.FFIMethods.StartPurchaseMethod StartPurchase;

			// Token: 0x020001CD RID: 461
			// (Invoke) Token: 0x06000A81 RID: 2689
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void FetchSkusCallback(IntPtr ptr, Result result);

			// Token: 0x020001CE RID: 462
			// (Invoke) Token: 0x06000A85 RID: 2693
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void FetchSkusMethod(IntPtr methodsPtr, IntPtr callbackData, StoreManager.FFIMethods.FetchSkusCallback callback);

			// Token: 0x020001CF RID: 463
			// (Invoke) Token: 0x06000A89 RID: 2697
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void CountSkusMethod(IntPtr methodsPtr, ref int count);

			// Token: 0x020001D0 RID: 464
			// (Invoke) Token: 0x06000A8D RID: 2701
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result GetSkuMethod(IntPtr methodsPtr, long skuId, ref Sku sku);

			// Token: 0x020001D1 RID: 465
			// (Invoke) Token: 0x06000A91 RID: 2705
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result GetSkuAtMethod(IntPtr methodsPtr, int index, ref Sku sku);

			// Token: 0x020001D2 RID: 466
			// (Invoke) Token: 0x06000A95 RID: 2709
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void FetchEntitlementsCallback(IntPtr ptr, Result result);

			// Token: 0x020001D3 RID: 467
			// (Invoke) Token: 0x06000A99 RID: 2713
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void FetchEntitlementsMethod(IntPtr methodsPtr, IntPtr callbackData, StoreManager.FFIMethods.FetchEntitlementsCallback callback);

			// Token: 0x020001D4 RID: 468
			// (Invoke) Token: 0x06000A9D RID: 2717
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void CountEntitlementsMethod(IntPtr methodsPtr, ref int count);

			// Token: 0x020001D5 RID: 469
			// (Invoke) Token: 0x06000AA1 RID: 2721
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result GetEntitlementMethod(IntPtr methodsPtr, long entitlementId, ref Entitlement entitlement);

			// Token: 0x020001D6 RID: 470
			// (Invoke) Token: 0x06000AA5 RID: 2725
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result GetEntitlementAtMethod(IntPtr methodsPtr, int index, ref Entitlement entitlement);

			// Token: 0x020001D7 RID: 471
			// (Invoke) Token: 0x06000AA9 RID: 2729
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result HasSkuEntitlementMethod(IntPtr methodsPtr, long skuId, ref bool hasEntitlement);

			// Token: 0x020001D8 RID: 472
			// (Invoke) Token: 0x06000AAD RID: 2733
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void StartPurchaseCallback(IntPtr ptr, Result result);

			// Token: 0x020001D9 RID: 473
			// (Invoke) Token: 0x06000AB1 RID: 2737
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void StartPurchaseMethod(IntPtr methodsPtr, long skuId, IntPtr callbackData, StoreManager.FFIMethods.StartPurchaseCallback callback);
		}

		// Token: 0x020001DA RID: 474
		// (Invoke) Token: 0x06000AB5 RID: 2741
		public delegate void FetchSkusHandler(Result result);

		// Token: 0x020001DB RID: 475
		// (Invoke) Token: 0x06000AB9 RID: 2745
		public delegate void FetchEntitlementsHandler(Result result);

		// Token: 0x020001DC RID: 476
		// (Invoke) Token: 0x06000ABD RID: 2749
		public delegate void StartPurchaseHandler(Result result);

		// Token: 0x020001DD RID: 477
		// (Invoke) Token: 0x06000AC1 RID: 2753
		public delegate void EntitlementCreateHandler(ref Entitlement entitlement);

		// Token: 0x020001DE RID: 478
		// (Invoke) Token: 0x06000AC5 RID: 2757
		public delegate void EntitlementDeleteHandler(ref Entitlement entitlement);
	}
}
