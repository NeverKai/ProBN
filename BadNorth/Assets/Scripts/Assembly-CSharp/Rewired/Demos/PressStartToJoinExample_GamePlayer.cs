using System;
using UnityEngine;

namespace Rewired.Demos
{
	// Token: 0x0200048B RID: 1163
	[AddComponentMenu("")]
	[RequireComponent(typeof(CharacterController))]
	public class PressStartToJoinExample_GamePlayer : MonoBehaviour
	{
		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06001ABC RID: 6844 RVA: 0x00049B6A File Offset: 0x00047F6A
		private Player player
		{
			get
			{
				return PressStartToJoinExample_Assigner.GetRewiredPlayer(this.gamePlayerId);
			}
		}

		// Token: 0x06001ABD RID: 6845 RVA: 0x00049B77 File Offset: 0x00047F77
		private void OnEnable()
		{
			this.cc = base.GetComponent<CharacterController>();
		}

		// Token: 0x06001ABE RID: 6846 RVA: 0x00049B85 File Offset: 0x00047F85
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

		// Token: 0x06001ABF RID: 6847 RVA: 0x00049BAC File Offset: 0x00047FAC
		private void GetInput()
		{
			this.moveVector.x = this.player.GetAxis("Move Horizontal");
			this.moveVector.y = this.player.GetAxis("Move Vertical");
			this.fire = this.player.GetButtonDown("Fire");
		}

		// Token: 0x06001AC0 RID: 6848 RVA: 0x00049C08 File Offset: 0x00048008
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

		// Token: 0x040010BB RID: 4283
		public int gamePlayerId;

		// Token: 0x040010BC RID: 4284
		public float moveSpeed = 3f;

		// Token: 0x040010BD RID: 4285
		public float bulletSpeed = 15f;

		// Token: 0x040010BE RID: 4286
		public GameObject bulletPrefab;

		// Token: 0x040010BF RID: 4287
		private CharacterController cc;

		// Token: 0x040010C0 RID: 4288
		private Vector3 moveVector;

		// Token: 0x040010C1 RID: 4289
		private bool fire;
	}
}
