using System;

namespace GooglePlayGames.BasicApi.Events
{
	// Token: 0x020003A4 RID: 932
	public interface IEvent
	{
		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600150C RID: 5388
		string Id { get; }

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600150D RID: 5389
		string Name { get; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600150E RID: 5390
		string Description { get; }

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600150F RID: 5391
		string ImageUrl { get; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06001510 RID: 5392
		ulong CurrentCount { get; }

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06001511 RID: 5393
		EventVisibility Visibility { get; }
	}
}
