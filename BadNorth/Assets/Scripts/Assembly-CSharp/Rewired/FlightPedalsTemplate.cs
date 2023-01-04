using System;

namespace Rewired
{
	// Token: 0x020004A5 RID: 1189
	public sealed class FlightPedalsTemplate : ControllerTemplate, IFlightPedalsTemplate, IControllerTemplate
	{
		// Token: 0x06001D59 RID: 7513 RVA: 0x0004E658 File Offset: 0x0004CA58
		public FlightPedalsTemplate(object payload) : base(payload)
		{
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06001D5A RID: 7514 RVA: 0x0004E661 File Offset: 0x0004CA61
		IControllerTemplateAxis IFlightPedalsTemplate.leftPedal
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(0);
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06001D5B RID: 7515 RVA: 0x0004E66A File Offset: 0x0004CA6A
		IControllerTemplateAxis IFlightPedalsTemplate.rightPedal
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(1);
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06001D5C RID: 7516 RVA: 0x0004E673 File Offset: 0x0004CA73
		IControllerTemplateAxis IFlightPedalsTemplate.slide
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(2);
			}
		}

		// Token: 0x04001278 RID: 4728
		public static readonly Guid typeGuid = new Guid("f6fe76f8-be2a-4db2-b853-9e3652075913");

		// Token: 0x04001279 RID: 4729
		public const int elementId_leftPedal = 0;

		// Token: 0x0400127A RID: 4730
		public const int elementId_rightPedal = 1;

		// Token: 0x0400127B RID: 4731
		public const int elementId_slide = 2;
	}
}
