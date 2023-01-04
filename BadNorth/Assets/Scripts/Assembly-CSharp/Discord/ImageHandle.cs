using System;

namespace Discord
{
	// Token: 0x020000C5 RID: 197
	public struct ImageHandle
	{
		// Token: 0x06000679 RID: 1657 RVA: 0x0001C447 File Offset: 0x0001A847
		public static ImageHandle User(long id)
		{
			return ImageHandle.User(id, 128U);
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x0001C454 File Offset: 0x0001A854
		public static ImageHandle User(long id, uint size)
		{
			return new ImageHandle
			{
				Type = ImageType.User,
				Id = id,
				Size = size
			};
		}

		// Token: 0x040003A3 RID: 931
		public ImageType Type;

		// Token: 0x040003A4 RID: 932
		public long Id;

		// Token: 0x040003A5 RID: 933
		public uint Size;
	}
}
