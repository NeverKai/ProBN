using System;
using UnityEngine;

namespace Rewired.Demos
{
	// Token: 0x02000476 RID: 1142
	[AddComponentMenu("")]
	public class CustomControllersTiltDemo : MonoBehaviour
	{
		// Token: 0x06001A2A RID: 6698 RVA: 0x000469A0 File Offset: 0x00044DA0
		private void Awake()
		{
			Screen.orientation = ScreenOrientation.LandscapeLeft;
			this.player = ReInput.players.GetPlayer(0);
			ReInput.InputSourceUpdateEvent += this.OnInputUpdate;
			this.controller = (CustomController)this.player.controllers.GetControllerWithTag(ControllerType.Custom, "TiltController");
		}

		// Token: 0x06001A2B RID: 6699 RVA: 0x000469F8 File Offset: 0x00044DF8
		private void Update()
		{
			if (this.target == null)
			{
				return;
			}
			Vector3 a = Vector3.zero;
			a.y = this.player.GetAxis("Tilt Vertical");
			a.x = this.player.GetAxis("Tilt Horizontal");
			if (a.sqrMagnitude > 1f)
			{
				a.Normalize();
			}
			a *= Time.deltaTime;
			this.target.Translate(a * this.speed);
		}

		// Token: 0x06001A2C RID: 6700 RVA: 0x00046A88 File Offset: 0x00044E88
		private void OnInputUpdate()
		{
			Vector3 acceleration = Input.acceleration;
			this.controller.SetAxisValue(0, acceleration.x);
			this.controller.SetAxisValue(1, acceleration.y);
			this.controller.SetAxisValue(2, acceleration.z);
		}

		// Token: 0x04001028 RID: 4136
		public Transform target;

		// Token: 0x04001029 RID: 4137
		public float speed = 10f;

		// Token: 0x0400102A RID: 4138
		private CustomController controller;

		// Token: 0x0400102B RID: 4139
		private Player player;
	}
}
