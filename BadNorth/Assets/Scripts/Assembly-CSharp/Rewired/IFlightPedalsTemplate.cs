using System;

namespace Rewired
{
	// Token: 0x0200049F RID: 1183
	public interface IFlightPedalsTemplate : IControllerTemplate
	{
		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06001C55 RID: 7253
		IControllerTemplateAxis leftPedal { get; }

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06001C56 RID: 7254
		IControllerTemplateAxis rightPedal { get; }

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06001C57 RID: 7255
		IControllerTemplateAxis slide { get; }
	}
}
