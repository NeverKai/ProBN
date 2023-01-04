using System;
using Rewired;
using UnityEngine;

namespace Voxels.TowerDefense.Tutorial
{
	// Token: 0x02000542 RID: 1346
	[Serializable]
	public class StartLevelTutorialDef : ScriptableObject
	{
		// Token: 0x04001585 RID: 5509
		public ControllerType controller;

		// Token: 0x04001586 RID: 5510
		[Header("Camera")]
		public Notification cameraMove = "TUTORIAL/MOUSE/CAMERA_MOVE";

		// Token: 0x04001587 RID: 5511
		public Notification cameraZoom = "TUTORIAL/MOUSE/CAMERA_ZOOM";

		// Token: 0x04001588 RID: 5512
		public const float cameraRotationTarget = 90f;

		// Token: 0x04001589 RID: 5513
		public const float cameraZoomTarget = 0.3f;

		// Token: 0x0400158A RID: 5514
		[Header("Selection")]
		public bool mustSelectBoth;

		// Token: 0x0400158B RID: 5515
		public Notification selectSquad = "TUTORIAL/MOUSE/SELECT_SQUAD";

		// Token: 0x0400158C RID: 5516
		public Notification moveCursor = string.Empty;

		// Token: 0x0400158D RID: 5517
		public Notification moveSquad = "TUTORIAL/MOUSE/MOVE_SQUAD";

		// Token: 0x0400158E RID: 5518
		[Header("Positioning")]
		public Notification positionArchers = "TUTORIAL/GENERIC/POSITION_ARCHERS";

		// Token: 0x0400158F RID: 5519
		public Notification positionInfantry = "TUTORIAL/GENERIC/POSITION_INFANTRY";
	}
}
