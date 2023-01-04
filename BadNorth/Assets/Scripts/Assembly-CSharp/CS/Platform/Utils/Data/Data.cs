using System;

namespace CS.Platform.Utils.Data
{
	// Token: 0x0200004E RID: 78
	public static class Data
	{
		// Token: 0x060003A7 RID: 935 RVA: 0x0001165C File Offset: 0x0000FA5C
		public static void EndianFlip(byte[] data, uint elementSize, uint offset, uint totalElements)
		{
			if (0U < elementSize && (ulong)(offset + totalElements * elementSize) <= (ulong)((long)data.Length))
			{
				for (uint num = 0U; num < totalElements; num += 1U)
				{
					for (uint num2 = 0U; num2 < elementSize / 2U; num2 += 1U)
					{
						byte b = data[(int)((UIntPtr)(offset + num2))];
						data[(int)((UIntPtr)(offset + num2))] = data[(int)((UIntPtr)(offset + elementSize - 1U - num2))];
						data[(int)((UIntPtr)(offset + elementSize - 1U - num2))] = b;
					}
					offset += elementSize;
				}
			}
		}
	}
}
