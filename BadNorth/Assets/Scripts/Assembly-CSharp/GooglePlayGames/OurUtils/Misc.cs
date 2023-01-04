using System;

namespace GooglePlayGames.OurUtils
{
	// Token: 0x020003BD RID: 957
	public static class Misc
	{
		// Token: 0x06001572 RID: 5490 RVA: 0x0002C210 File Offset: 0x0002A610
		public static bool BuffersAreIdentical(byte[] a, byte[] b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (a.Length != b.Length)
			{
				return false;
			}
			for (int i = 0; i < a.Length; i++)
			{
				if (a[i] != b[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001573 RID: 5491 RVA: 0x0002C264 File Offset: 0x0002A664
		public static byte[] GetSubsetBytes(byte[] array, int offset, int length)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (offset < 0 || offset >= array.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (length < 0 || array.Length - offset < length)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			if (offset == 0 && length == array.Length)
			{
				return array;
			}
			byte[] array2 = new byte[length];
			Array.Copy(array, offset, array2, 0, length);
			return array2;
		}

		// Token: 0x06001574 RID: 5492 RVA: 0x0002C2DD File Offset: 0x0002A6DD
		public static T CheckNotNull<T>(T value)
		{
			if (value == null)
			{
				throw new ArgumentNullException();
			}
			return value;
		}

		// Token: 0x06001575 RID: 5493 RVA: 0x0002C2F1 File Offset: 0x0002A6F1
		public static T CheckNotNull<T>(T value, string paramName)
		{
			if (value == null)
			{
				throw new ArgumentNullException(paramName);
			}
			return value;
		}
	}
}
