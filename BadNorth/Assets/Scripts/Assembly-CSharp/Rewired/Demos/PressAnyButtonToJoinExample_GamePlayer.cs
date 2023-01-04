using System;
using UnityEngine;

namespace Rewired.Demos
{
	// Token: 0x02000488 RID: 1160
	[AddComponentMenu("")]
	[RequireComponent(typeof(CharacterController))]
	public class PressAnyButtonToJoinExample_GamePlayer : MonoBehaviour
	{
		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06001AAF RID: 6831 RVA: 0x000497F5 File Offset: 0x00047BF5
		private Player player
		{
			get
			{
				return (!ReInput.isReady) ? null : ReInput.players.GetPlayer(this.playerId);
			}
		}

		// Token: 0x06001AB0 RID: 6832 RVA: 0x00049817 File Offset: 0x00047C17
		private void OnEnable()
		{
			this.cc = base.GetComponent<CharacterController>();
		}

		// Token: 0x06001AB1 RID: 6833 RVA: 0x00049825 File Offset: 0x00047C25
		private void Update()
		{
			if (!ReInput.isReady)
			{
				return;
			}
			if (this.player == null)
			{
				return;
			}
			this.GetInput();
			this.ProcessInput();
		}

		// Token: 0x06001AB2 RID: 6834 RVA: 0x0004984C File Offset: 0x00047C4C
		private void GetInput()
		{
			this.moveVector.x = this.player.GetAxis("Move Horizontal");
			this.moveVector.y = this.player.GetAxis("Move Vertical");
			this.fire = this.player.GetButtonDown("Fire");
		}

		// Token: 0x06001AB3 RID: 6835 RVA: 0x000498A8 File Offset: 0x00047CA8
		private void ProcessInput()
		{
			if (this.moveVector.x != 0f || this.moveVector.y != 0f)
			{
				this.cc.Move(this.moveVector * this.moveSpeed * Time.deltaTime);
			}
			if (this.fire)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.bulletPrefab, base.transform.position + base.transform.right, base.transform.rotation);
				gameObject.GetComponent<Rigidbody>().AddForce(base.transform.right * this.bulletSpeed, ForceMode.VelocityChange);
			}
		}

		// Token: 0x040010AE RID: 4270
		public int playerId;

		// Token: 0x040010AF RID: 4271
		public float moveSpeed = 3f;

		// Token: 0x040010B0 RID: 4272
		public float bulletSpeed = 15f;

		// Token: 0x040010B1 RID: 4273
		public GameObject bulletPrefab;

		// Token: 0x040010B2 RID: 4274
		private CharacterController cc;

		// Token: 0x040010B3 RID: 4275
		private Vector3 moveVector;

		// Token: 0x040010B4 RID: 4276
		private bool fire;
	}
}
