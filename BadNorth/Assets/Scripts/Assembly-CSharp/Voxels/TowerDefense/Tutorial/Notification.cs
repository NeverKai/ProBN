using System;
using I2.Loc;

namespace Voxels.TowerDefense.Tutorial
{
	// Token: 0x02000541 RID: 1345
	[Serializable]
	public class Notification
	{
		// Token: 0x06002310 RID: 8976 RVA: 0x00069B69 File Offset: 0x00067F69
		public Notification(string messageTerm)
		{
			this.messageTerm = messageTerm;
		}

		// Token: 0x06002311 RID: 8977 RVA: 0x00069B78 File Offset: 0x00067F78
		public static implicit operator Notification(string messageTerm)
		{
			return new Notification(messageTerm);
		}

		// Token: 0x06002312 RID: 8978 RVA: 0x00069B80 File Offset: 0x00067F80
		public static implicit operator string(Notification notification)
		{
			return notification.messageTerm;
		}

		// Token: 0x04001582 RID: 5506
		[TermsPopup("")]
		public string messageTerm;

		// Token: 0x04001583 RID: 5507
		public TutorialIcons.ID icon;

		// Token: 0x04001584 RID: 5508
		public TutorialIcons.ID secondaryIcon;
	}
}
