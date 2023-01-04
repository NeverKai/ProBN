using System;
using System.Collections.Generic;

namespace ControlledRandomness
{
	// Token: 0x020004F8 RID: 1272
	public interface IGenOption
	{
		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06002078 RID: 8312
		IEnumerable<Range> ranges { get; }

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06002079 RID: 8313
		IEnumerable<Tag> tags { get; }

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x0600207A RID: 8314
		float probability { get; }
	}
}
