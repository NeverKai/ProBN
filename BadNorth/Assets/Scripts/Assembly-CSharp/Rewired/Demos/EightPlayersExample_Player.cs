using System;
using UnityEngine;

namespace Rewired.Demos
{
	// Token: 0x0200047D RID: 1149
	[AddComponentMenu("")]
	[RequireComponent(typeof(CharacterController))]
	public class EightPlayersExample_Player : MonoBehaviour
	{
		// Token: 0x06001A5E RID: 6750 RVA: 0x00047A21 File Offset: 0x00045E21
		private void Awake()
		{
			this.cc = base.GetComponent<CharacterController>();
		}

		// Token: 0x06001A5F RID: 6751 RVA: 0x00047A2F File Offset: 0x00045E2F
		private void Initialize()
		{
			this.player = ReInput.players.GetPlayer(this.playerId);
			this.initialized = true;
		}

		// Token: 0x06001A60 RID: 6752 RVA: 0x00047A4E File Offset: 0x00045E4E
		private void Update()
		{
			if (!ReInput.isReady)
			{
				return;
			}
			if (!this.initialized)
			{
				this.Initialize();
			}
			this.GetInput();
			this.ProcessInput();
		}

		// Token: 0x06001A61 RID: 6753 RVA: 0x00047A78 File Offset: 0x00045E78
		private void GetInput()
		{
			this.moveVector.x = this.player.GetAxis("Move Horizontal");
			this.moveVector.y = this.player.GetAxis("Move Vertical");
			this.fire = this.player.GetButtonDown("Fire");
		}

		// Token: 0x06001A62 RID: 6754 RVA: 0x00047AD4 File Offset: 0x00045ED4
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

		// Token: 0x04001053 RID: 4179
		public int playerId;

		// Token: 0x04001054 RID: 4180
		public float moveSpeed = 3f;

		// Token: 0x04001055 RID: 4181
		public float bulletSpeed = 15f;

		// Token: 0x04001056 RID: 4182
		public GameObject bulletPrefab;

		// Token: 0x04001057 RID: 4183
		private Player player;

		// Token: 0x04001058 RID: 4184
		private CharacterController cc;

		// Token: 0x04001059 RID: 4185
		private Vector3 moveVector;

		// Token: 0x0400105A RID: 4186
		private bool fire;

		// Token: 0x0400105B RID: 4187
		[NonSerialized]
		private bool initialized;
	}
}
