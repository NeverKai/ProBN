using System;
using CS.Platform.Utils.Data;
using UnityEngine;

namespace CS.Platform.Utils
{
	// Token: 0x0200007A RID: 122
	public static class AgeRating
	{
		// Token: 0x06000561 RID: 1377 RVA: 0x00015F09 File Offset: 0x00014309
		public static void AgeRatingSetup(string databaseLocation = null)
		{
			if (string.IsNullOrEmpty(databaseLocation))
			{
				databaseLocation = "Platform/AgeRatingData";
			}
			AgeRating._ratingInfomation = (Resources.Load(databaseLocation) as PlatformAgeRatingDatabase);
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x00015F2D File Offset: 0x0001432D
		public static string AgeNone(string key)
		{
			if (AgeRating._ratingInfomation != null)
			{
				return AgeRating._ratingInfomation.AgeNone(key);
			}
			return null;
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x00015F4C File Offset: 0x0001434C
		public static string AgeSteam(string key)
		{
			if (AgeRating._ratingInfomation != null)
			{
				return AgeRating._ratingInfomation.AgeSteam(key);
			}
			return null;
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x00015F6B File Offset: 0x0001436B
		public static string AgeOculus(string key)
		{
			if (AgeRating._ratingInfomation != null)
			{
				return AgeRating._ratingInfomation.AgeOculus(key);
			}
			return null;
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x00015F8A File Offset: 0x0001438A
		public static string AgePS4(string key)
		{
			if (AgeRating._ratingInfomation != null)
			{
				return AgeRating._ratingInfomation.AgePS4(key);
			}
			return null;
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x00015FA9 File Offset: 0x000143A9
		public static bool AgePS4Active(string key)
		{
			return AgeRating._ratingInfomation != null && AgeRating._ratingInfomation.AgePS4Active(key);
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x00015FC8 File Offset: 0x000143C8
		public static string[] AgeKeys()
		{
			if (AgeRating._ratingInfomation != null)
			{
				return AgeRating._ratingInfomation.Keys;
			}
			return null;
		}

		// Token: 0x0400022F RID: 559
		private static PlatformAgeRatingDatabase _ratingInfomation;
	}
}
